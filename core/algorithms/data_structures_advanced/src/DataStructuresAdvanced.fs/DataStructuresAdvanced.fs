namespace DataStructuresAdvanced

// Contrato del módulo `data_structures_advanced` — especificación
// 07_Data_Structures_Advanced.
//
// Contrato completo: declara los tipos nuevos, las firmas y un cuerpo centinela
// por operación. La implementación real llega en el paso siguiente; hasta
// entonces las operaciones devuelven el valor neutro de su tipo y la suite debe
// fallar.
//
// Indicadores naturales:
//   * los enlaces ausentes son `None`, la ausencia nativa de F# (`Node option`
//     en la base homologada); F# no usa `null` para tipos propios;
//   * las banderas devuelven `bool` (`false` no coincide con la ausencia);
//   * los contadores devuelven `int` y la estructura vacía es `0`;
//   * `Graph.getNeighbors` devuelve `int list option`: `None` = vértice no
//     registrado, `Some []` = vértice aislado;
//   * `Graph.addEdge` devuelve una tupla con el éxito y la estructura
//     resultante: en el fallo, `false` y la **misma** estructura sin tocar,
//     nunca una excepción — igual que `LinkedList.delete` en la base.
//
// Los campos `Value`, `Left` y `Right` de `TreeNode` son la vía del contrato
// para `get_value`, `get_left` y `get_right`; las mutaciones se ofrecen como
// función (`withLeft`, `withRight`), porque los registros son inmutables.

/// Nodo del árbol binario de búsqueda: valor y dos enlaces opcionales.
type TreeNode =
    { Value: int
      Left: TreeNode option
      Right: TreeNode option }

/// Árbol binario de búsqueda: raíz opcional y contador de valores distintos.
type BinaryTree =
    { Root: TreeNode option
      Count: int }

/// Vértice del grafo: etiqueta, adyacencia ordenada y enlace al siguiente
/// vértice del registro canónico. Interno a `Graph`.
type GraphNode =
    { Label: int
      Neighbors: int list
      NextVertex: GraphNode option }

/// Grafo dirigido: registro enlazado de vértices y contadores propios.
type Graph =
    { Vertices: GraphNode option
      VertexCount: int
      EdgeCount: int }

[<RequireQualifiedAccess>]
module TreeNode =

    /// Nodo con el valor indicado y ambos enlaces ausentes (`init`).
    let create (_value: int) : TreeNode =
        { Value = 0; Left = None; Right = None }

    /// Copia del nodo con el enlace izquierdo reemplazado (`set_left`).
    let withLeft (_left: TreeNode option) (node: TreeNode) : TreeNode = node

    /// Copia del nodo con el enlace derecho reemplazado (`set_right`).
    let withRight (_right: TreeNode option) (node: TreeNode) : TreeNode = node

[<RequireQualifiedAccess>]
module BinaryTree =

    /// Árbol vacío utilizable: sin raíz y contador a cero (`init`).
    let empty : BinaryTree =
        { Root = None; Count = 0 }

    /// Inserta el valor; un duplicado también es éxito y no crece el tamaño.
    let insert (_value: int) (tree: BinaryTree) : BinaryTree = tree

    /// Pertenencia según la invariante del ABB.
    let contains (_value: int) (_tree: BinaryTree) : bool = false

    let isEmpty (_tree: BinaryTree) : bool = false

    /// Número de valores distintos del árbol.
    let size (_tree: BinaryTree) : int = 0

[<RequireQualifiedAccess>]
module Graph =

    /// Grafo vacío utilizable: sin vértices y contadores a cero (`init`).
    let empty : Graph =
        { Vertices = None
          VertexCount = 0
          EdgeCount = 0 }

    /// Registra el vértice; un duplicado también es éxito y conserva la
    /// adyacencia.
    let addVertex (_label: int) (graph: Graph) : Graph = graph

    /// Añade la arista dirigida `u -> v`: `(true, grafo)` si se añadió o ya
    /// estaba, `(false, el mismo grafo)` si falta alguno de los dos extremos.
    let addEdge (_u: int) (_v: int) (graph: Graph) : (bool * Graph) = (false, graph)

    /// Verdadero si existe la arista dirigida `u -> v`.
    let hasEdge (_u: int) (_v: int) (_graph: Graph) : bool = false

    /// Vecinos ordenados del vértice: `None` si no está registrado, `Some []`
    /// si está aislado.
    let getNeighbors (_label: int) (_graph: Graph) : int list option = None

    let isEmpty (_graph: Graph) : bool = false

    /// Número de vértices registrados.
    let vertexCount (_graph: Graph) : int = 0

    /// Número de aristas registradas.
    let edgeCount (_graph: Graph) : int = 0
