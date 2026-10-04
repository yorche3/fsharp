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
// Indicadores de fallo: `None` en las extracciones (`headValue`, `pop`, `peek`,
// `dequeue` y `delete`), `false` en las banderas y `0` en los contadores.

/// Comprueba una aserción del caso solo si el sujeto está entre sus dueños.
let private assertFor (subject: string) (owners: string list) (case: string) (expected: 'a) (actual: 'a) =
    if List.contains subject owners then
        Assert.True((actual = expected), $"{subject} should return {expected} in {case} but got {actual}")

/// Recorre la lista con las operaciones del contrato (`headValue` + `delete`),
/// que devuelve una lista nueva: el escenario queda intacto.
let private traverse (list: LinkedList) =
    let rec walk acc current =
        match LinkedList.headValue current with
        | None -> List.rev acc
        | Some value ->
            match LinkedList.delete value current with
            | Some rest -> walk (value :: acc) rest
            | None -> List.rev acc

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
    assertFor subject [ "linked_list_init"; "linked_list_get_head" ] case1 None (LinkedList.headValue emptyList)

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
    let deleted = LinkedList.delete 10 inserted
    assertFor subject [ "linked_list_delete" ] case3 true (Option.isSome deleted)
    let afterDelete = Option.defaultValue inserted deleted
    assertFor subject [ "linked_list_delete"; "linked_list_size" ] case3 3 (LinkedList.size afterDelete)
    assertFor subject [ "linked_list_delete"; "linked_list_get_head" ] case3 [ 5; 20; 10 ] (traverse afterDelete)

    let case4 = "absent value"
    let absent = LinkedList.delete 99 afterDelete
    assertFor subject [ "linked_list_delete" ] case4 false (Option.isSome absent)
    let afterAbsent = Option.defaultValue afterDelete absent
    assertFor subject [ "linked_list_delete"; "linked_list_size" ] case4 3 (LinkedList.size afterAbsent)
    assertFor subject [ "linked_list_delete"; "linked_list_get_head" ] case4 [ 5; 20; 10 ] (traverse afterAbsent)

    let case5 = "empty the list"
    let deletedHead = LinkedList.delete 5 afterAbsent
    assertFor subject [ "linked_list_delete" ] case5 true (Option.isSome deletedHead)
    let afterHeadDelete = Option.defaultValue afterAbsent deletedHead
    let deletedSecond = LinkedList.delete 20 afterHeadDelete
    assertFor subject [ "linked_list_delete" ] case5 true (Option.isSome deletedSecond)
    let afterSecondDelete = Option.defaultValue afterHeadDelete deletedSecond
    let deletedLast = LinkedList.delete 10 afterSecondDelete
    assertFor subject [ "linked_list_delete" ] case5 true (Option.isSome deletedLast)
    let emptied = Option.defaultValue afterSecondDelete deletedLast
    assertFor subject [ "linked_list_delete"; "linked_list_is_empty" ] case5 true (LinkedList.isEmpty emptied)
    assertFor subject [ "linked_list_delete"; "linked_list_size" ] case5 0 (LinkedList.size emptied)
    assertFor subject [ "linked_list_delete"; "linked_list_get_head" ] case5 None (LinkedList.headValue emptied)

// ============================================================
// Escenario de Stack — 4 casos
// ============================================================

let private stackCases (subject: string) =
    let case1 = "empty state and failed removal"
    let emptyStack = Stack.empty
    assertFor subject [ "stack_init"; "stack_is_empty" ] case1 true (Stack.isEmpty emptyStack)
    assertFor subject [ "stack_init"; "stack_size" ] case1 0 (Stack.size emptyStack)
    assertFor subject [ "stack_init"; "stack_peek" ] case1 None (Stack.peek emptyStack)
    assertFor subject [ "stack_init"; "stack_pop" ] case1 None (Stack.pop emptyStack)

    let case2 = "LIFO and non-mutating peek"
    let pushed = emptyStack |> Stack.push 10 |> Stack.push 20 |> Stack.push 30
    assertFor subject [ "stack_push"; "stack_peek" ] case2 (Some 30) (Stack.peek pushed)
    assertFor subject [ "stack_push"; "stack_size" ] case2 3 (Stack.size pushed)

    let case3 = "removal and reuse"
    let poppedThird = Stack.pop pushed
    assertFor subject [ "stack_pop" ] case3 (Some 30) (poppedThird |> Option.map fst)
    let afterThirdPop = poppedThird |> Option.map snd |> Option.defaultValue pushed
    let reused = Stack.push 40 afterThirdPop
    let poppedReused = Stack.pop reused
    assertFor subject [ "stack_pop"; "stack_push" ] case3 (Some 40) (poppedReused |> Option.map fst)
    let afterReusedPop = poppedReused |> Option.map snd |> Option.defaultValue reused
    let poppedSecond = Stack.pop afterReusedPop
    assertFor subject [ "stack_pop" ] case3 (Some 20) (poppedSecond |> Option.map fst)
    let afterSecondPop = poppedSecond |> Option.map snd |> Option.defaultValue afterReusedPop
    let poppedFirst = Stack.pop afterSecondPop
    assertFor subject [ "stack_pop" ] case3 (Some 10) (poppedFirst |> Option.map fst)
    let afterFirstPop = poppedFirst |> Option.map snd |> Option.defaultValue afterSecondPop
    assertFor subject [ "stack_pop"; "stack_is_empty" ] case3 true (Stack.isEmpty afterFirstPop)
    assertFor subject [ "stack_pop"; "stack_size" ] case3 0 (Stack.size afterFirstPop)

    let case4 = "empty after removal"
    assertFor subject [ "stack_pop" ] case4 None (Stack.pop afterFirstPop)
    assertFor subject [ "stack_pop"; "stack_is_empty" ] case4 true (Stack.isEmpty afterFirstPop)

// ============================================================
// Escenario de Queue — 4 casos
// ============================================================

let private queueCases (subject: string) =
    let case1 = "empty state and failed removal"
    let emptyQueue = Queue.empty
    assertFor subject [ "queue_init"; "queue_is_empty" ] case1 true (Queue.isEmpty emptyQueue)
    assertFor subject [ "queue_init"; "queue_size" ] case1 0 (Queue.size emptyQueue)
    assertFor subject [ "queue_init"; "queue_peek" ] case1 None (Queue.peek emptyQueue)
    assertFor subject [ "queue_init"; "queue_dequeue" ] case1 None (Queue.dequeue emptyQueue)

    let case2 = "FIFO and non-mutating peek"
    let enqueued = emptyQueue |> Queue.enqueue 10 |> Queue.enqueue 20 |> Queue.enqueue 30
    assertFor subject [ "queue_enqueue"; "queue_peek" ] case2 (Some 10) (Queue.peek enqueued)
    assertFor subject [ "queue_enqueue"; "queue_size" ] case2 3 (Queue.size enqueued)

    let case3 = "removal and reuse"
    let dequeuedFirst = Queue.dequeue enqueued
    assertFor subject [ "queue_dequeue" ] case3 (Some 10) (dequeuedFirst |> Option.map fst)
    let afterFirstDequeue = dequeuedFirst |> Option.map snd |> Option.defaultValue enqueued
    let reused = Queue.enqueue 40 afterFirstDequeue
    let dequeuedSecond = Queue.dequeue reused
    assertFor subject [ "queue_dequeue" ] case3 (Some 20) (dequeuedSecond |> Option.map fst)
    let afterSecondDequeue = dequeuedSecond |> Option.map snd |> Option.defaultValue reused
    let dequeuedThird = Queue.dequeue afterSecondDequeue
    assertFor subject [ "queue_dequeue" ] case3 (Some 30) (dequeuedThird |> Option.map fst)
    let afterThirdDequeue = dequeuedThird |> Option.map snd |> Option.defaultValue afterSecondDequeue
    let dequeuedReused = Queue.dequeue afterThirdDequeue
    assertFor subject [ "queue_dequeue"; "queue_enqueue" ] case3 (Some 40) (dequeuedReused |> Option.map fst)
    let afterReusedDequeue = dequeuedReused |> Option.map snd |> Option.defaultValue afterThirdDequeue
    assertFor subject [ "queue_dequeue"; "queue_is_empty" ] case3 true (Queue.isEmpty afterReusedDequeue)
    assertFor subject [ "queue_dequeue"; "queue_size" ] case3 0 (Queue.size afterReusedDequeue)

    let case4 = "empty after removal"
    assertFor subject [ "queue_dequeue" ] case4 None (Queue.dequeue afterReusedDequeue)
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
