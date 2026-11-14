namespace DataStructuresAdvanced

// Contrato del módulo `data_structures_advanced` — especificación
// 07_Data_Structures_Advanced.
//
// Enlace ausente: `None`. Preguntas: `bool`. Contadores: `int`. Vecinos:
// `None` = vértice no registrado, `Some []` = aislado. `addEdge` falla con
// `(false, el mismo grafo)`, nunca con una excepción.

/// Nodo del ABB: valor y dos enlaces opcionales.
type TreeNode =
    { Value: int
      Left: TreeNode option
      Right: TreeNode option }

/// ABB: raíz opcional y contador de valores distintos.
type BinaryTree =
    { Root: TreeNode option
      Count: int }

/// Vértice: etiqueta, vecinos ordenados y enlace al siguiente. Interno a
/// `Graph`.
type GraphNode =
    { Label: int
      Neighbors: int list
      NextVertex: GraphNode option }

/// Grafo dirigido: registro enlazado y contadores propios.
type Graph =
    { Vertices: GraphNode option
      VertexCount: int
      EdgeCount: int }

[<RequireQualifiedAccess>]
module TreeNode =

    /// Nodo con el valor y ambos enlaces ausentes (`init`).
    let create (value: int) : TreeNode =
        { Value = value; Left = None; Right = None }

    /// Copia con el enlace izquierdo reemplazado (`set_left`).
    let withLeft (left: TreeNode option) (node: TreeNode) : TreeNode =
        { node with Left = left }

    /// Copia con el enlace derecho reemplazado (`set_right`).
    let withRight (right: TreeNode option) (node: TreeNode) : TreeNode =
        { node with Right = right }

[<RequireQualifiedAccess>]
module BinaryTree =

    /// Árbol vacío (`init`).
    let empty : BinaryTree =
        { Root = None; Count = 0 }

    let rec private insertNode
        (value: int)
        (node: TreeNode option)
        : TreeNode option * bool =
        match node with
        | None -> (Some (TreeNode.create value), true)
        | Some n ->
            if value = n.Value then (Some n, false)
            elif value < n.Value then
                let newLeft, inserted = insertNode value n.Left
                (Some (TreeNode.withLeft newLeft n), inserted)
            else
                let newRight, inserted = insertNode value n.Right
                (Some (TreeNode.withRight newRight n), inserted)

    /// Inserta; un duplicado también es éxito y no crece.
    let insert (value: int) (tree: BinaryTree) : BinaryTree =
        let newRoot, inserted = insertNode value tree.Root
        { Root = newRoot
          Count = if inserted then tree.Count + 1 else tree.Count }

    let rec private containsNode (value: int) (node: TreeNode option) : bool =
        match node with
        | None -> false
        | Some n ->
            if value = n.Value then true
            elif value < n.Value then containsNode value n.Left
            else containsNode value n.Right

    /// Pertenencia según la invariante del ABB.
    let contains (value: int) (tree: BinaryTree) : bool =
        containsNode value tree.Root

    let isEmpty (tree: BinaryTree) : bool = tree.Count = 0

    let size (tree: BinaryTree) : int = tree.Count

[<RequireQualifiedAccess>]
module Graph =

    /// Grafo vacío (`init`).
    let empty : Graph =
        { Vertices = None
          VertexCount = 0
          EdgeCount = 0 }

    let rec private findVertex (label: int) (node: GraphNode option) : GraphNode option =
        match node with
        | None -> None
        | Some n -> if n.Label = label then Some n else findVertex label n.NextVertex

    /// Registra el vértice; el duplicado conserva la adyacencia.
    let addVertex (label: int) (graph: Graph) : Graph =
        match findVertex label graph.Vertices with
        | Some _ -> graph
        | None ->
            let newNode =
                { Label = label; Neighbors = []; NextVertex = graph.Vertices }

            { graph with
                Vertices = Some newNode
                VertexCount = graph.VertexCount + 1 }

    let rec private insertSorted (value: int) (sorted: int list) : int list =
        match sorted with
        | [] -> [value]
        | h :: _ when value < h -> value :: sorted
        | h :: t when value > h -> h :: insertSorted value t
        | _ -> sorted

    let rec private updateNeighbors
        (label: int)
        (newNeighbors: int list)
        (node: GraphNode option)
        : GraphNode option =
        match node with
        | None -> None
        | Some n when n.Label = label -> Some { n with Neighbors = newNeighbors }
        | Some n ->
            Some { n with NextVertex = updateNeighbors label newNeighbors n.NextVertex }

    /// Añade `u -> v`: `(false, el mismo grafo)` si falta un extremo.
    let addEdge (u: int) (v: int) (graph: Graph) : (bool * Graph) =
        match findVertex u graph.Vertices, findVertex v graph.Vertices with
        | Some uNode, Some _ ->
            if List.contains v uNode.Neighbors then
                (true, graph)
            else
                let newNeighbors = insertSorted v uNode.Neighbors
                let newVertices = updateNeighbors u newNeighbors graph.Vertices

                (true,
                 { graph with
                     Vertices = newVertices
                     EdgeCount = graph.EdgeCount + 1 })
        | _ -> (false, graph)

    /// Verdadero si existe la arista dirigida `u -> v`.
    let hasEdge (u: int) (v: int) (graph: Graph) : bool =
        match findVertex u graph.Vertices, findVertex v graph.Vertices with
        | Some uNode, Some _ -> List.contains v uNode.Neighbors
        | _ -> false

    /// Vecinos ordenados: `None` si el vértice no está registrado, `Some []`
    /// si está aislado.
    let getNeighbors (label: int) (graph: Graph) : int list option =
        match findVertex label graph.Vertices with
        | Some n -> Some n.Neighbors
        | None -> None

    let isEmpty (graph: Graph) : bool = graph.VertexCount = 0

    let vertexCount (graph: Graph) : int = graph.VertexCount

    let edgeCount (graph: Graph) : int = graph.EdgeCount
