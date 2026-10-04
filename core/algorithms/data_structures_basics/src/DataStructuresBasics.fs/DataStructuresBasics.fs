namespace DataStructuresBasics

// Contrato del módulo `data_structures_basics` — especificación 06_Data_Structures_Basics.
//
// Esqueleto del paso 4b: declara los tipos nuevos y las firmas del contrato, y
// deja el cuerpo de cada operación en su indicador natural, sin resolver ningún
// caso. El algoritmo es del paso 5 y la suite, del 4c: aquí no se calcula nada.
//
// Indicadores naturales:
//   * las operaciones que extraen un entero devuelven `int option` y su fallo es `None`;
//   * las banderas devuelven `false` y los contadores, `0`;
//   * las operaciones que devuelven una estructura devuelven la misma instancia
//     sin tocarla, porque la especificación no les da caso de fallo.
//
// La ausencia de enlace es `None`: el registro es inmutable y F# no usa `null`
// para tipos propios. Los campos `Value` y `Next` de `Node` son la vía del
// contrato para `get_value` y `get_next`; el resto de las operaciones se expone
// como función, no como campo.

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

    let isEmpty (_list: LinkedList) : bool = false

    let size (_list: LinkedList) : int = 0

    /// Valor de la cabeza, o `None` si la lista está vacía (`get_head`).
    let headValue (_list: LinkedList) : int option = None

    let insertHead (_value: int) (list: LinkedList) : LinkedList = list

    // Al insertar al final hay que reconstruir la cadena: la estructura es
    // inmutable y la celda de cola no se puede enlazar en el sitio (O(n); ver la
    // adaptación de complejidad en el README del módulo).
    let insertTail (_value: int) (list: LinkedList) : LinkedList = list

    /// Elimina la primera aparición: `Some` con la lista resultante, `None` si
    /// el valor no está (`delete`).
    let delete (_value: int) (_list: LinkedList) : LinkedList option = None

[<RequireQualifiedAccess>]
module Stack =

    /// Pila vacía: sin tope y contador a cero (`init`).
    let empty : Stack =
        { Top = None; Count = 0 }

    let isEmpty (_stack: Stack) : bool = false

    let size (_stack: Stack) : int = 0

    let push (_value: int) (stack: Stack) : Stack = stack

    /// Extrae el tope: `Some (valor, pila)`, o `None` si está vacía (`pop`).
    let pop (_stack: Stack) : (int * Stack) option = None

    /// Observa el tope sin extraerlo: `Some valor`, o `None` si está vacía (`peek`).
    let peek (_stack: Stack) : int option = None

[<RequireQualifiedAccess>]
module Queue =

    /// Cola vacía: sin frente, sin cola y contador a cero (`init`).
    let empty : Queue =
        { Front = None; Rear = None; Count = 0 }

    let isEmpty (_queue: Queue) : bool = false

    let size (_queue: Queue) : int = 0

    // Misma adaptación que `LinkedList.insertTail`: encolar reconstruye la
    // cadena (O(n) en vez del O(1) que promete la especificación).
    let enqueue (_value: int) (queue: Queue) : Queue = queue

    /// Extrae el frente: `Some (valor, cola)`, o `None` si está vacía (`dequeue`).
    let dequeue (_queue: Queue) : (int * Queue) option = None

    /// Observa el frente sin extraerlo: `Some valor`, o `None` si está vacía (`peek`).
    let peek (_queue: Queue) : int option = None

