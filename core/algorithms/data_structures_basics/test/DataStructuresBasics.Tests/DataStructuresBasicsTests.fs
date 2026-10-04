module DataStructuresBasicsTests

open Xunit
open DataStructuresBasics

// Suite del módulo `data_structures_basics` — especificación 06_Data_Structures_Basics.
//
// Los 15 casos de la especificación (Node 2, LinkedList 5, Stack 4, Queue 4) son
// pasos sucesivos sobre el mismo estado lógico. Cada escenario se reproduce
// completo una vez por operación y solo se comprueban las aserciones de la
// operación bajo prueba (`assertFor`): así hay un test por operación sin
// duplicar el escenario ni renunciar al estado compartido que pide la
// especificación. Los registros son inmutables, de modo que cada paso devuelve
// la estructura siguiente y ninguna aserción daña el escenario.
//
// Indicadores de fallo: solo `Node` usa `None` (su enlace ausente). Las
// extracciones devuelven un valor: `int` con `Contract.FAILURE_VALUE` (`-1`) en
// `headValue`, `peek` y la parte del valor de `pop`/`dequeue`, y `(false, …)` en
// `delete`; las banderas devuelven `false` y los contadores, `0`.

/// Comprueba una aserción del caso solo si el sujeto está entre sus dueños.
let private assertFor (subject: string) (owners: string list) (case: string) (expected: 'a) (actual: 'a) =
    if List.contains subject owners then
        Assert.True((actual = expected), $"{subject} should return {expected} in {case} but got {actual}")

/// Recorre la lista con las operaciones del contrato (`headValue` + `delete`),
/// que devuelve una lista nueva: el escenario queda intacto. Los valores de los
/// casos son positivos, así que no chocan con el indicador de fallo.
let private traverse (list: LinkedList) =
    let rec walk acc current =
        let value = LinkedList.headValue current

        if value = Contract.FAILURE_VALUE then
            List.rev acc
        else
            let (found, rest) = LinkedList.delete value current

            if found then walk (value :: acc) rest else List.rev acc

    walk [] list

// ============================================================
// Escenario de Node — 2 casos
// ============================================================

let private nodeCases (subject: string) =
    let case1 = "initialize and observe value/link"
    let first = Node.create 10
    assertFor subject [ "node_init"; "node_get_value" ] case1 10 first.Value
    assertFor subject [ "node_init"; "node_get_next" ] case1 None first.Next

    let case2 = "initialize another node, link and traverse"
    let second = Node.create 20
    let linked = Node.withNext second first
    assertFor subject [ "node_set_next"; "node_get_next"; "node_get_value" ] case2 (Some 20) (linked.Next |> Option.map (fun node -> node.Value))
    assertFor subject [ "node_init"; "node_get_next" ] case2 None second.Next

// ============================================================
// Escenario de LinkedList — 5 casos
// ============================================================

let private linkedListCases (subject: string) =
    let case1 = "empty state"
    let emptyList = LinkedList.empty
    assertFor subject [ "linked_list_init"; "linked_list_is_empty" ] case1 true (LinkedList.isEmpty emptyList)
    assertFor subject [ "linked_list_init"; "linked_list_size" ] case1 0 (LinkedList.size emptyList)
    assertFor subject [ "linked_list_init"; "linked_list_get_head" ] case1 Contract.FAILURE_VALUE (LinkedList.headValue emptyList)

    let case2 = "insert at both ends"
    let inserted =
        LinkedList.empty
        |> LinkedList.insertTail 10
        |> LinkedList.insertTail 20
        |> LinkedList.insertHead 5
        |> LinkedList.insertTail 10

    assertFor subject [ "linked_list_insert_head"; "linked_list_insert_tail"; "linked_list_size" ] case2 4 (LinkedList.size inserted)
    assertFor subject [ "linked_list_insert_head"; "linked_list_insert_tail"; "linked_list_get_head" ] case2 [ 5; 10; 20; 10 ] (traverse inserted)

    let case3 = "delete first occurrence"
    let (deletedFirst, afterDelete) = LinkedList.delete 10 inserted
    assertFor subject [ "linked_list_delete" ] case3 true deletedFirst
    assertFor subject [ "linked_list_delete"; "linked_list_size" ] case3 3 (LinkedList.size afterDelete)
    assertFor subject [ "linked_list_delete"; "linked_list_get_head" ] case3 [ 5; 20; 10 ] (traverse afterDelete)

    let case4 = "absent value"
    let (deletedAbsent, afterAbsent) = LinkedList.delete 99 afterDelete
    assertFor subject [ "linked_list_delete" ] case4 false deletedAbsent
    assertFor subject [ "linked_list_delete"; "linked_list_size" ] case4 3 (LinkedList.size afterAbsent)
    assertFor subject [ "linked_list_delete"; "linked_list_get_head" ] case4 [ 5; 20; 10 ] (traverse afterAbsent)

    let case5 = "empty the list"
    let (deletedHead, afterHeadDelete) = LinkedList.delete 5 afterAbsent
    assertFor subject [ "linked_list_delete" ] case5 true deletedHead
    let (deletedSecond, afterSecondDelete) = LinkedList.delete 20 afterHeadDelete
    assertFor subject [ "linked_list_delete" ] case5 true deletedSecond
    let (deletedLast, emptied) = LinkedList.delete 10 afterSecondDelete
    assertFor subject [ "linked_list_delete" ] case5 true deletedLast
    assertFor subject [ "linked_list_delete"; "linked_list_is_empty" ] case5 true (LinkedList.isEmpty emptied)
    assertFor subject [ "linked_list_delete"; "linked_list_size" ] case5 0 (LinkedList.size emptied)
    assertFor subject [ "linked_list_delete"; "linked_list_get_head" ] case5 Contract.FAILURE_VALUE (LinkedList.headValue emptied)

// ============================================================
// Escenario de Stack — 4 casos
// ============================================================

let private stackCases (subject: string) =
    let case1 = "empty state and failed removal"
    let emptyStack = Stack.empty
    assertFor subject [ "stack_init"; "stack_is_empty" ] case1 true (Stack.isEmpty emptyStack)
    assertFor subject [ "stack_init"; "stack_size" ] case1 0 (Stack.size emptyStack)
    assertFor subject [ "stack_init"; "stack_peek" ] case1 Contract.FAILURE_VALUE (Stack.peek emptyStack)
    assertFor subject [ "stack_init"; "stack_pop" ] case1 Contract.FAILURE_VALUE (Stack.pop emptyStack |> fst)
    assertFor subject [ "stack_init"; "stack_pop" ] case1 emptyStack (Stack.pop emptyStack |> snd)

    let case2 = "LIFO and non-mutating peek"
    let pushed = emptyStack |> Stack.push 10 |> Stack.push 20 |> Stack.push 30
    assertFor subject [ "stack_push"; "stack_peek" ] case2 30 (Stack.peek pushed)
    assertFor subject [ "stack_push"; "stack_size" ] case2 3 (Stack.size pushed)

    let case3 = "removal and reuse"
    let (poppedThird, afterThirdPop) = Stack.pop pushed
    assertFor subject [ "stack_pop" ] case3 30 poppedThird
    let reused = Stack.push 40 afterThirdPop
    let (poppedReused, afterReusedPop) = Stack.pop reused
    assertFor subject [ "stack_pop"; "stack_push" ] case3 40 poppedReused
    let (poppedSecond, afterSecondPop) = Stack.pop afterReusedPop
    assertFor subject [ "stack_pop" ] case3 20 poppedSecond
    let (poppedFirst, afterFirstPop) = Stack.pop afterSecondPop
    assertFor subject [ "stack_pop" ] case3 10 poppedFirst
    assertFor subject [ "stack_pop"; "stack_is_empty" ] case3 true (Stack.isEmpty afterFirstPop)
    assertFor subject [ "stack_pop"; "stack_size" ] case3 0 (Stack.size afterFirstPop)

    let case4 = "empty after removal"
    assertFor subject [ "stack_pop" ] case4 Contract.FAILURE_VALUE (Stack.pop afterFirstPop |> fst)
    assertFor subject [ "stack_pop" ] case4 afterFirstPop (Stack.pop afterFirstPop |> snd)
    assertFor subject [ "stack_pop"; "stack_is_empty" ] case4 true (Stack.isEmpty afterFirstPop)

// ============================================================
// Escenario de Queue — 4 casos
// ============================================================

let private queueCases (subject: string) =
    let case1 = "empty state and failed removal"
    let emptyQueue = Queue.empty
    assertFor subject [ "queue_init"; "queue_is_empty" ] case1 true (Queue.isEmpty emptyQueue)
    assertFor subject [ "queue_init"; "queue_size" ] case1 0 (Queue.size emptyQueue)
    assertFor subject [ "queue_init"; "queue_peek" ] case1 Contract.FAILURE_VALUE (Queue.peek emptyQueue)
    assertFor subject [ "queue_init"; "queue_dequeue" ] case1 Contract.FAILURE_VALUE (Queue.dequeue emptyQueue |> fst)
    assertFor subject [ "queue_init"; "queue_dequeue" ] case1 emptyQueue (Queue.dequeue emptyQueue |> snd)

    let case2 = "FIFO and non-mutating peek"
    let enqueued = emptyQueue |> Queue.enqueue 10 |> Queue.enqueue 20 |> Queue.enqueue 30
    assertFor subject [ "queue_enqueue"; "queue_peek" ] case2 10 (Queue.peek enqueued)
    assertFor subject [ "queue_enqueue"; "queue_size" ] case2 3 (Queue.size enqueued)

    let case3 = "removal and reuse"
    let (dequeuedFirst, afterFirstDequeue) = Queue.dequeue enqueued
    assertFor subject [ "queue_dequeue" ] case3 10 dequeuedFirst
    let reused = Queue.enqueue 40 afterFirstDequeue
    let (dequeuedSecond, afterSecondDequeue) = Queue.dequeue reused
    assertFor subject [ "queue_dequeue" ] case3 20 dequeuedSecond
    let (dequeuedThird, afterThirdDequeue) = Queue.dequeue afterSecondDequeue
    assertFor subject [ "queue_dequeue" ] case3 30 dequeuedThird
    let (dequeuedReused, afterReusedDequeue) = Queue.dequeue afterThirdDequeue
    assertFor subject [ "queue_dequeue"; "queue_enqueue" ] case3 40 dequeuedReused
    assertFor subject [ "queue_dequeue"; "queue_is_empty" ] case3 true (Queue.isEmpty afterReusedDequeue)
    assertFor subject [ "queue_dequeue"; "queue_size" ] case3 0 (Queue.size afterReusedDequeue)

    let case4 = "empty after removal"
    assertFor subject [ "queue_dequeue" ] case4 Contract.FAILURE_VALUE (Queue.dequeue afterReusedDequeue |> fst)
    assertFor subject [ "queue_dequeue" ] case4 afterReusedDequeue (Queue.dequeue afterReusedDequeue |> snd)
    assertFor subject [ "queue_dequeue"; "queue_is_empty" ] case4 true (Queue.isEmpty afterReusedDequeue)

// ============================================================
// Un test por operación de la especificación (23)
// ============================================================

[<Fact>]
let ``Node init`` () = nodeCases "node_init"

[<Fact>]
let ``Node get value`` () = nodeCases "node_get_value"

[<Fact>]
let ``Node get next`` () = nodeCases "node_get_next"

[<Fact>]
let ``Node set next`` () = nodeCases "node_set_next"

[<Fact>]
let ``LinkedList init`` () = linkedListCases "linked_list_init"

[<Fact>]
let ``LinkedList get head`` () = linkedListCases "linked_list_get_head"

[<Fact>]
let ``LinkedList insert head`` () = linkedListCases "linked_list_insert_head"

[<Fact>]
let ``LinkedList insert tail`` () = linkedListCases "linked_list_insert_tail"

[<Fact>]
let ``LinkedList delete`` () = linkedListCases "linked_list_delete"

[<Fact>]
let ``LinkedList is empty`` () = linkedListCases "linked_list_is_empty"

[<Fact>]
let ``LinkedList size`` () = linkedListCases "linked_list_size"

[<Fact>]
let ``Stack init`` () = stackCases "stack_init"

[<Fact>]
let ``Stack push`` () = stackCases "stack_push"

[<Fact>]
let ``Stack pop`` () = stackCases "stack_pop"

[<Fact>]
let ``Stack peek`` () = stackCases "stack_peek"

[<Fact>]
let ``Stack is empty`` () = stackCases "stack_is_empty"

[<Fact>]
let ``Stack size`` () = stackCases "stack_size"

[<Fact>]
let ``Queue init`` () = queueCases "queue_init"

[<Fact>]
let ``Queue enqueue`` () = queueCases "queue_enqueue"

[<Fact>]
let ``Queue dequeue`` () = queueCases "queue_dequeue"

[<Fact>]
let ``Queue peek`` () = queueCases "queue_peek"

[<Fact>]
let ``Queue is empty`` () = queueCases "queue_is_empty"

[<Fact>]
let ``Queue size`` () = queueCases "queue_size"
