namespace DataStructuresBasics

// Contrato del módulo `data_structures_basics` — especificación 06_Data_Structures_Basics.
//
// Esqueleto del paso 4b: declara los tipos nuevos y las firmas del contrato, y
// deja el cuerpo de cada operación en su indicador natural, sin resolver ningún
// caso. El algoritmo es del paso 5 y la suite, del 4c: aquí no se calcula nada.
//
// Indicadores naturales:
//   * `Node` es el único tipo que usa `None`: los campos `Value`/`Next`, su enlace ausente;
//   * las operaciones que extraen un entero devuelven `int` y su fallo es
//     `Contract.FAILURE_VALUE` (`-1`);
//   * las banderas devuelven `false` y los contadores, `0`;
//   * `Stack.pop`, `Queue.dequeue` y `LinkedList.delete` devuelven una tupla con
//     el valor (o el éxito) y la estructura resultante: en el fallo, el
//     indicador y la **misma** estructura sin tocar, nunca una excepción.
//
// El registro es inmutable y F# no usa `null` para tipos propios, así que la
// ausencia de enlace es `None`, que solo aparece en `Node`. Los campos `Value`
// y `Next` de `Node` son la vía del contrato para `get_value` y `get_next`; el
// resto de las operaciones se expone como función, no como campo.

/// Celda enlazada compartida por las tres estructuras / shared linked cell.
type Node =
    { Value: int
      Next: Node option }

/// Lista enlazada simple / singly linked list.
type LinkedList =
    { Head: Node option
      Tail: Node option
      Count: int }

/// Pila LIFO / LIFO stack.
type Stack =
    { Top: Node option
      Count: int }

/// Cola FIFO / FIFO queue.
type Queue =
    { Front: Node option
      Rear: Node option
      Count: int }

/// Indicador de fallo del contrato: lo devuelven las operaciones que extraen un
/// entero cuando la estructura está vacía o el valor no está.
[<RequireQualifiedAccess>]
module Contract =

    let FAILURE_VALUE = -1

[<RequireQualifiedAccess>]
module Node =

    /// Crea la celda con su valor y el enlace ausente (`init`).
    let create (value: int) : Node =
        { Value = value; Next = None }

    /// Devuelve una celda nueva enlazada con la siguiente (`set_next`).
    let withNext (next: Node) (node: Node) : Node =
        { node with Next = Some next }

[<RequireQualifiedAccess>]
module LinkedList =

    /// Lista vacía: sin cabeza, sin cola y contador a cero (`init`).
    let empty : LinkedList =
        { Head = None; Tail = None; Count = 0 }

    let isEmpty (_list: LinkedList) : bool = _list.Count = 0

    let size (_list: LinkedList) : int = _list.Count

    /// Valor de la cabeza, o el indicador de fallo si la lista está vacía (`get_head`).
    let headValue (_list: LinkedList) : int =
        match _list.Head with
        | Some node -> node.Value
        | None -> Contract.FAILURE_VALUE

    let insertHead (_value: int) (list: LinkedList) : LinkedList =
        let newHead = Node.create _value

        match list.Head with
        | None -> { Head = Some newHead; Tail = Some newHead; Count = 1 }
        | Some headNode -> { list with Head = Some (Node.withNext headNode newHead); Count = list.Count + 1 }

    // Al insertar al final hay que reconstruir la cadena: la estructura es
    // inmutable y la celda de cola no se puede enlazar en el sitio (O(n); ver la
    // adaptación de complejidad en el README del módulo).
    let insertTail (_value: int) (list: LinkedList) : LinkedList =
        let newTail = Node.create _value
        match list.Tail with
        | None -> { Head = Some newTail; Tail = Some newTail; Count = 1 }
        | Some tailNode ->
            let rec rebuildChain current =
                match current with
                | Some node when node = tailNode -> Some (Node.withNext newTail node)
                | Some node -> Some { node with Next = rebuildChain node.Next }
                | None -> None
            let newHead = rebuildChain list.Head
            { Head = newHead; Tail = Some newTail; Count = list.Count + 1 }

    /// Elimina la primera aparición: `(true, lista resultante)` si estaba,
    /// `(false, la misma lista)` si el valor no está (`delete`).
    let delete (_value: int) (_list: LinkedList) : (bool * LinkedList) =
        let rec deleteNode current =
            match current with
            | Some node when node.Value = _value -> (true, node.Next)
            | Some node ->
                let (found, newNext) = deleteNode node.Next
                (found, Some { node with Next = newNext })
            | None -> (false, None)

        let (found, newHead) = deleteNode _list.Head

        // El valor ausente no toca la lista: se devuelve la misma instancia.
        if not found then
            (false, _list)
        else
            let rec findTail node =
                match node.Next with
                | Some nextNode -> findTail nextNode
                | None -> node

            (true, { Head = newHead; Tail = newHead |> Option.map findTail; Count = _list.Count - 1 })

[<RequireQualifiedAccess>]
module Stack =

    /// Pila vacía: sin tope y contador a cero (`init`).
    let empty : Stack =
        { Top = None; Count = 0 }

    let isEmpty (_stack: Stack) : bool = _stack.Count = 0

    let size (_stack: Stack) : int = _stack.Count

    let push (_value: int) (stack: Stack) : Stack =
        let newTop = Node.create _value

        match stack.Top with
        | Some topNode -> { Top = Some (Node.withNext topNode newTop); Count = stack.Count + 1 }
        | None -> { Top = Some newTop; Count = 1 }

    /// Extrae el tope: `(valor, pila)`; si la pila está vacía, el indicador de
    /// fallo y la misma pila (`pop`).
    let pop (_stack: Stack) : (int * Stack) =
        match _stack.Top with
        | Some node ->
            let newTop = node.Next
            let newStack = { Top = newTop; Count = _stack.Count - 1 }
            (node.Value, newStack)
        | None -> (Contract.FAILURE_VALUE, _stack)

    /// Observa el tope sin extraerlo: el valor, o el indicador de fallo si la
    /// pila está vacía (`peek`).
    let peek (_stack: Stack) : int =
        match _stack.Top with
        | Some node -> node.Value
        | None -> Contract.FAILURE_VALUE

[<RequireQualifiedAccess>]
module Queue =

    /// Cola vacía: sin frente, sin cola y contador a cero (`init`).
    let empty : Queue =
        { Front = None; Rear = None; Count = 0 }

    let isEmpty (_queue: Queue) : bool = _queue.Count = 0

    let size (_queue: Queue) : int = _queue.Count

    // Misma adaptación que `LinkedList.insertTail`: encolar reconstruye la
    // cadena (O(n) en vez del O(1) que promete la especificación).
    let enqueue (_value: int) (queue: Queue) : Queue =
        let newNode = Node.create _value

        match queue.Rear with
        | None -> { Front = Some newNode; Rear = Some newNode; Count = 1 }
        | Some rearNode ->
            let rec rebuildChain current =
                match current with
                | Some node when node = rearNode -> Some (Node.withNext newNode node)
                | Some node -> Some { node with Next = rebuildChain node.Next }
                | None -> None

            let newFront = rebuildChain queue.Front
            { Front = newFront; Rear = Some newNode; Count = queue.Count + 1 }

    /// Extrae el frente: `(valor, cola)`; si la cola está vacía, el indicador de
    /// fallo y la misma cola (`dequeue`).
    let dequeue (_queue: Queue) : (int * Queue) =
        match _queue.Front with
        | Some node ->
            let newFront = node.Next
            let newRear = if newFront.IsNone then None else _queue.Rear
            let newQueue = { Front = newFront; Rear = newRear; Count = _queue.Count - 1 }
            (node.Value, newQueue)
        | None -> (Contract.FAILURE_VALUE, _queue)

    /// Observa el frente sin extraerlo: el valor, o el indicador de fallo si la
    /// cola está vacía (`peek`).
    let peek (_queue: Queue) : int =
        match _queue.Front with
        | Some node -> node.Value
        | None -> Contract.FAILURE_VALUE
