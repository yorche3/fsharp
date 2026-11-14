module DataStructuresAdvancedTests

open Xunit
open DataStructuresAdvanced

// Suite del módulo `data_structures_advanced` — especificación
// 07_Data_Structures_Advanced. 24 casos: TreeNode 6, BinaryTree 7, Graph 11.
//
// Cada fila de la especificación es un test con instancia recién creada y sus
// pasos en orden; los registros son inmutables, así que cada paso devuelve la
// estructura siguiente y no hay estado compartido entre casos.
//
// Adaptaciones: `insert` y `addVertex` devuelven la estructura (su «éxito» se
// observa con `size`/`contains`/`vertexCount`); `addEdge` devuelve `(bool, grafo)`;
// la ausencia es `None`. «Modificar la copia devuelta» de `getNeighbors` no es
// representable: `int list` es inmutable; se conservan el resto de pasos.

let private expect (case: string) (what: string) (expected: 'a) (actual: 'a) =
    Assert.True((actual = expected), $"{case}: {what} should be {expected} but got {actual}")

let private valueOf (node: TreeNode option) = node |> Option.map (fun n -> n.Value)

let private insertAll (values: int list) (tree: BinaryTree) =
    values |> List.fold (fun t v -> BinaryTree.insert v t) tree

let private withVertices (labels: int list) (graph: Graph) =
    labels |> List.fold (fun g l -> Graph.addVertex l g) graph

let private edge (u: int) (v: int) (graph: Graph) = Graph.addEdge u v graph

// ============================================================
// TreeNode — 6 casos
// ============================================================

[<Fact>]
let ``TreeNode create a tree node`` () =
    let case = "create a tree node"
    expect case "get_value" 10 (TreeNode.create 10).Value

[<Fact>]
let ``TreeNode absent initial links`` () =
    let case = "absent initial links"
    let node = TreeNode.create 10
    expect case "get_left" None node.Left
    expect case "get_right" None node.Right

[<Fact>]
let ``TreeNode link two children`` () =
    let case = "link two children"
    let parent =
        TreeNode.create 10
        |> TreeNode.withLeft (Some(TreeNode.create 20))
        |> TreeNode.withRight (Some(TreeNode.create 30))

    expect case "value of left child" (Some 20) (valueOf parent.Left)
    expect case "value of right child" (Some 30) (valueOf parent.Right)

[<Fact>]
let ``TreeNode traverse each link`` () =
    let case = "traverse each link"
    let parent =
        TreeNode.create 10
        |> TreeNode.withLeft (Some(TreeNode.create 20))
        |> TreeNode.withRight (Some(TreeNode.create 30))

    expect case "left child value" (Some 20) (valueOf parent.Left)
    expect case "right child value" (Some 30) (valueOf parent.Right)
    expect case "left child's left link" None (parent.Left |> Option.bind (fun n -> n.Left))
    expect case "left child's right link" None (parent.Left |> Option.bind (fun n -> n.Right))
    expect case "right child's left link" None (parent.Right |> Option.bind (fun n -> n.Left))
    expect case "right child's right link" None (parent.Right |> Option.bind (fun n -> n.Right))

[<Fact>]
let ``TreeNode unlink children`` () =
    let case = "unlink children"
    let linked =
        TreeNode.create 10
        |> TreeNode.withLeft (Some(TreeNode.create 20))
        |> TreeNode.withRight (Some(TreeNode.create 30))

    let leftCleared = TreeNode.withLeft None linked
    expect case "left after clearing left" None leftCleared.Left
    expect case "value after clearing left" 10 leftCleared.Value
    expect case "right after clearing left" (Some 30) (valueOf leftCleared.Right)

    let bothCleared = TreeNode.withRight None leftCleared
    expect case "left after clearing right" None bothCleared.Left
    expect case "right after clearing right" None bothCleared.Right
    expect case "value after clearing right" 10 bothCleared.Value

[<Fact>]
let ``TreeNode immutable adaptation`` () =
    let case = "immutable adaptation"
    let original = TreeNode.create 10
    let updated = TreeNode.withLeft (Some(TreeNode.create 20)) original

    expect case "new node left value" (Some 20) (valueOf updated.Left)
    expect case "original left link" None original.Left
    expect case "original value" 10 original.Value

// ============================================================
// BinaryTree — 7 casos
// ============================================================

[<Fact>]
let ``BinaryTree create empty`` () =
    let case = "create empty"
    let tree = BinaryTree.empty
    expect case "is_empty" true (BinaryTree.isEmpty tree)
    expect case "size" 0 (BinaryTree.size tree)
    expect case "contains 50" false (BinaryTree.contains 50 tree)

[<Fact>]
let ``BinaryTree insert the root`` () =
    let case = "insert the root"
    let tree = BinaryTree.empty |> BinaryTree.insert 50
    expect case "size" 1 (BinaryTree.size tree)
    expect case "is_empty" false (BinaryTree.isEmpty tree)
    expect case "contains 50" true (BinaryTree.contains 50 tree)

[<Fact>]
let ``BinaryTree insert lower and higher`` () =
    let case = "insert lower and higher"
    let tree = BinaryTree.empty |> insertAll [ 50; 30; 70 ]
    expect case "size" 3 (BinaryTree.size tree)

    for v in [ 50; 30; 70 ] do
        expect case $"contains {v}" true (BinaryTree.contains v tree)

[<Fact>]
let ``BinaryTree multilevel branches`` () =
    let case = "multilevel branches"
    let tree = BinaryTree.empty |> insertAll [ 50; 30; 70; 20; 40 ]
    expect case "size" 5 (BinaryTree.size tree)

    for v in [ 50; 30; 70; 20; 40 ] do
        expect case $"contains {v}" true (BinaryTree.contains v tree)

[<Fact>]
let ``BinaryTree missing value in nonempty tree`` () =
    let case = "missing value in nonempty tree"
    let tree = BinaryTree.empty |> BinaryTree.insert 50
    expect case "contains 99" false (BinaryTree.contains 99 tree)
    expect case "size" 1 (BinaryTree.size tree)
    expect case "contains 50" true (BinaryTree.contains 50 tree)

[<Fact>]
let ``BinaryTree idempotent duplicate`` () =
    let case = "idempotent duplicate"
    let tree = BinaryTree.empty |> insertAll [ 50; 30; 70; 30 ]
    expect case "size" 3 (BinaryTree.size tree)

    for v in [ 50; 30; 70 ] do
        expect case $"contains {v}" true (BinaryTree.contains v tree)

[<Fact>]
let ``BinaryTree ordered insertions`` () =
    let case = "ordered insertions"
    let tree = BinaryTree.empty |> insertAll [ 20; 30; 40; 50; 70 ]
    expect case "size" 5 (BinaryTree.size tree)

    for v in [ 20; 30; 40; 50; 70 ] do
        expect case $"contains {v}" true (BinaryTree.contains v tree)

    expect case "contains 99" false (BinaryTree.contains 99 tree)

// ============================================================
// Graph — 11 casos
// ============================================================

[<Fact>]
let ``Graph create empty`` () =
    let case = "create empty"
    let graph = Graph.empty
    expect case "is_empty" true (Graph.isEmpty graph)
    expect case "vertex_count" 0 (Graph.vertexCount graph)
    expect case "edge_count" 0 (Graph.edgeCount graph)
    expect case "has_edge 10 30" false (Graph.hasEdge 10 30 graph)
    expect case "get_neighbors 10" None (Graph.getNeighbors 10 graph)

[<Fact>]
let ``Graph isolated vertex`` () =
    let case = "isolated vertex"
    let graph = Graph.empty |> Graph.addVertex 10
    expect case "is_empty" false (Graph.isEmpty graph)
    expect case "vertex_count" 1 (Graph.vertexCount graph)
    expect case "edge_count" 0 (Graph.edgeCount graph)
    expect case "neighbors of 10" (Some []) (Graph.getNeighbors 10 graph)

[<Fact>]
let ``Graph nonconsecutive labels and directed edge`` () =
    let case = "nonconsecutive labels and directed edge"
    let graph = Graph.empty |> withVertices [ 10; 70 ]
    let (added, graph) = edge 10 70 graph
    expect case "add_edge 10 70" true added
    expect case "has_edge 10 70" true (Graph.hasEdge 10 70 graph)
    expect case "has_edge 70 10" false (Graph.hasEdge 70 10 graph)
    expect case "vertex_count" 2 (Graph.vertexCount graph)
    expect case "edge_count" 1 (Graph.edgeCount graph)

[<Fact>]
let ``Graph duplicate vertex preserves adjacency`` () =
    let case = "duplicate vertex preserves adjacency"
    let graph = Graph.empty |> withVertices [ 10; 30 ]
    let (added, graph) = edge 10 30 graph
    expect case "add_edge 10 30" true added
    let graph = Graph.addVertex 10 graph
    expect case "vertex_count" 2 (Graph.vertexCount graph)
    expect case "edge_count" 1 (Graph.edgeCount graph)
    expect case "neighbors of 10" (Some [ 30 ]) (Graph.getNeighbors 10 graph)

[<Fact>]
let ``Graph duplicate edge`` () =
    let case = "duplicate edge"
    let graph = Graph.empty |> withVertices [ 10; 30 ]
    let (first, graph) = edge 10 30 graph
    let (second, graph) = edge 10 30 graph
    expect case "first add_edge" true first
    expect case "second add_edge" true second
    expect case "vertex_count" 2 (Graph.vertexCount graph)
    expect case "edge_count" 1 (Graph.edgeCount graph)
    expect case "neighbors of 10" (Some [ 30 ]) (Graph.getNeighbors 10 graph)

[<Fact>]
let ``Graph missing endpoint without mutation`` () =
    let case = "missing endpoint without mutation"
    let graph = Graph.empty |> withVertices [ 10; 30 ]
    let (_, graph) = edge 10 30 graph
    let (a, afterA) = edge 10 99 graph
    let (b, afterB) = edge 99 10 afterA
    let (c, afterC) = edge 98 99 afterB
    let (d, afterD) = edge 99 99 afterC
    expect case "add_edge 10 99" false a
    expect case "add_edge 99 10" false b
    expect case "add_edge 98 99" false c
    expect case "add_edge 99 99" false d
    expect case "graph unchanged by failures" graph afterD
    expect case "vertex_count" 2 (Graph.vertexCount afterD)
    expect case "edge_count" 1 (Graph.edgeCount afterD)
    expect case "neighbors of 10" (Some [ 30 ]) (Graph.getNeighbors 10 afterD)
    expect case "neighbors of 30" (Some []) (Graph.getNeighbors 30 afterD)
    expect case "has_edge 10 99" false (Graph.hasEdge 10 99 afterD)
    expect case "has_edge 99 10" false (Graph.hasEdge 99 10 afterD)
    expect case "has_edge 98 99" false (Graph.hasEdge 98 99 afterD)
    expect case "has_edge 99 99" false (Graph.hasEdge 99 99 afterD)
    expect case "neighbors of 98" None (Graph.getNeighbors 98 afterD)
    expect case "neighbors of 99" None (Graph.getNeighbors 99 afterD)

[<Fact>]
let ``Graph sorted neighbours and counts`` () =
    let case = "sorted neighbours and counts"
    let graph = Graph.empty |> withVertices [ 10; 30; 50; 70 ]
    let steps = [ (10, 70); (10, 30); (10, 50); (30, 50) ]

    let graph =
        steps
        |> List.fold
            (fun g (u, v) ->
                let (ok, next) = edge u v g
                expect case $"add_edge {u} {v}" true ok
                next)
            graph

    expect case "neighbors of 10" (Some [ 30; 50; 70 ]) (Graph.getNeighbors 10 graph)
    expect case "neighbors of 30" (Some [ 50 ]) (Graph.getNeighbors 30 graph)
    expect case "neighbors of 50" (Some []) (Graph.getNeighbors 50 graph)
    expect case "neighbors of 70" (Some []) (Graph.getNeighbors 70 graph)
    expect case "vertex_count" 4 (Graph.vertexCount graph)
    expect case "edge_count" 4 (Graph.edgeCount graph)

[<Fact>]
let ``Graph missing-edge query`` () =
    let case = "missing-edge query"
    let graph = Graph.empty |> withVertices [ 10; 30 ]
    expect case "has_edge 10 30" false (Graph.hasEdge 10 30 graph)
    expect case "vertex_count" 2 (Graph.vertexCount graph)
    expect case "edge_count" 0 (Graph.edgeCount graph)
    expect case "is_empty" false (Graph.isEmpty graph)

[<Fact>]
let ``Graph neighbours of missing vertex`` () =
    let case = "neighbours of missing vertex"
    let graph = Graph.empty |> Graph.addVertex 10
    expect case "neighbors of 99" None (Graph.getNeighbors 99 graph)
    expect case "neighbors of 10 differ from failure" (Some []) (Graph.getNeighbors 10 graph)
    expect case "vertex_count" 1 (Graph.vertexCount graph)
    expect case "edge_count" 0 (Graph.edgeCount graph)

[<Fact>]
let ``Graph self-loop and duplicate`` () =
    let case = "self-loop and duplicate"
    let graph = Graph.empty |> Graph.addVertex 10
    let (first, graph) = edge 10 10 graph
    let (second, graph) = edge 10 10 graph
    expect case "first add_edge" true first
    expect case "second add_edge" true second
    expect case "has_edge 10 10" true (Graph.hasEdge 10 10 graph)
    expect case "neighbors of 10" (Some [ 10 ]) (Graph.getNeighbors 10 graph)
    expect case "vertex_count" 1 (Graph.vertexCount graph)
    expect case "edge_count" 1 (Graph.edgeCount graph)

[<Fact>]
let ``Graph neighbour-result isolation`` () =
    let case = "neighbour-result isolation"
    let graph = Graph.empty |> withVertices [ 10; 30; 50 ]
    let (_, graph) = edge 10 30 graph
    let saved = Graph.getNeighbors 10 graph
    let (added, graph) = edge 10 50 graph
    expect case "add_edge 10 50" true added
    expect case "saved neighbors of 10" (Some [ 30 ]) saved
    expect case "fresh neighbors of 10" (Some [ 30; 50 ]) (Graph.getNeighbors 10 graph)
    expect case "vertex_count" 3 (Graph.vertexCount graph)
    expect case "edge_count" 2 (Graph.edgeCount graph)
