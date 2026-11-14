# Data Structures Advanced — F#

Implementación de la especificación [07_Data_Structures_Advanced](https://yorche3.github.io/programming_languages/core/algorithms/07_Data_Structures_Advanced/) en **F#** (.NET 10), con registros inmutables, valor embebido y pruebas unitarias con **xUnit**.

Implementation of the [07_Data_Structures_Advanced](https://yorche3.github.io/programming_languages/core/algorithms/07_Data_Structures_Advanced/) specification in **F#** (.NET 10), using immutable records, embedded value pattern and **xUnit** unit tests.

**ES:** Este módulo construye dos estructuras que requieren múltiples referencias por nodo: un árbol binario de búsqueda (`BinaryTree`) sobre celdas `TreeNode` y un grafo dirigido (`Graph`) con registro enlazado canónico de vértices (`GraphNode`). La base conceptual `Node` se implementa mediante **valor embebido** en cada estructura. Todas las operaciones devuelven la estructura resultante de forma inmutable, sin colecciones de biblioteca para el registro ni dependencias externas.

**EN:** This module builds two structures that require multiple links per node: a binary search tree (`BinaryTree`) over `TreeNode` cells and a directed graph (`Graph`) with a canonical linked vertex registry (`GraphNode`). The conceptual `Node` base is implemented via **embedded value** in each structure. All operations return the resulting structure immutably, without library collections for the registry or external dependencies.

---

## 📂 Archivos y estructura / Files & Structure

| Archivo / File | Propósito / Purpose |
|---|---|
| [`src/DataStructuresAdvanced.fs/DataStructuresAdvanced.fs`](src/DataStructuresAdvanced.fs/DataStructuresAdvanced.fs) | Definición de tipos (`TreeNode`, `BinaryTree`, `GraphNode`, `Graph`) y módulos con las operaciones del contrato / Type definitions and modules containing contract operations |
| [`src/DataStructuresAdvanced.fs/DataStructuresAdvanced.fsproj`](src/DataStructuresAdvanced.fs/DataStructuresAdvanced.fsproj) | Proyecto de biblioteca (`net10.0`) con generación de documentación XML / Library project (`net10.0`) with XML documentation generation |
| [`test/DataStructuresAdvanced.Tests/DataStructuresAdvancedTests.fs`](test/DataStructuresAdvanced.Tests/DataStructuresAdvancedTests.fs) | Suite xUnit con los 24 casos de la especificación (6 TreeNode, 7 BinaryTree, 11 Graph) / xUnit suite with all 24 specification cases (6 TreeNode, 7 BinaryTree, 11 Graph) |
| [`test/DataStructuresAdvanced.Tests/DataStructuresAdvanced.Tests.fsproj`](test/DataStructuresAdvanced.Tests/DataStructuresAdvanced.Tests.fsproj) | Proyecto de pruebas unitarias (`net10.0`, xUnit, Test SDK, coverlet) / Unit test project (`net10.0`, xUnit, Test SDK, coverlet) |
| [`DataStructuresAdvanced.slnx`](DataStructuresAdvanced.slnx) | Solución .NET que vincula biblioteca y tests / .NET solution linking library and tests |
| [`.gitignore`](.gitignore) | Archivos generados y directorios de compilación excluidos (`bin/`, `obj/`) / Ignored generated files and build directories |

**Estructura de directorios / Directory structure:**

```text
data_structures_advanced/
├── DataStructuresAdvanced.slnx                    # Solución .NET / .NET solution
├── src/DataStructuresAdvanced.fs/
│   ├── DataStructuresAdvanced.fs                  # Contrato e implementación / Contract and implementation
│   └── DataStructuresAdvanced.fsproj              # Proyecto de biblioteca / Library project
├── test/DataStructuresAdvanced.Tests/
│   ├── DataStructuresAdvancedTests.fs             # Suite xUnit (24 tests) / xUnit suite (24 tests)
│   └── DataStructuresAdvanced.Tests.fsproj        # Proyecto de pruebas / Test project
├── .gitignore
└── README.md
```

**Desviación respecto a la ubicación esperada / Deviation from expected location:**

**ES:** La especificación propone `src/data_structures_advanced.ext`, `test/data_structures_advanced_test.ext` y `test/run_tests.ext`. Siguiendo la convención idiomática de .NET y las bases del repositorio, se utiliza una solución `.slnx` con subproyectos en `src/DataStructuresAdvanced.fs/` y `test/DataStructuresAdvanced.Tests/`. No se incluye un ejecutable `run_tests.ext` ya que el ejecutor estándar es el CLI nativo `dotnet test`, el cual descubre y ejecuta automáticamente las pruebas decoradas con `[<Fact>]`.

**EN:** The specification suggests `src/data_structures_advanced.ext`, `test/data_structures_advanced_test.ext` and `test/run_tests.ext`. Following standard .NET idiom and the repository references, a `.slnx` solution is used with subprojects under `src/DataStructuresAdvanced.fs/` and `test/DataStructuresAdvanced.Tests/`. An executable `run_tests.ext` is not included because the standard runner is the native CLI `dotnet test`, which discovers and executes `[<Fact>]` tests automatically.

---

## 🛠️ Enfoque y construcción / Approach & Build

**ES:** El proyecto se estructura como una solución .NET 10 con una biblioteca de clases pura y un proyecto de pruebas unitarias con xUnit. La implementación sigue un paradigma funcional inmutable: los tipos son registros (`record`) cuyos campos no mutan. Cada modificación devuelve una nueva instancia compartiendo estructura inmutable.

El grafo implementa un registro enlazado canónico manual de vértices mediante `GraphNode.NextVertex`, cumpliendo estrictamente con la prohibición de diccionarios, tablas hash o conjuntos de biblioteca. Las listas de adyacencia se mantienen ordenadas ascendentemente mediante inserción ordenada manual (`insertSorted`).

**EN:** The project is structured as a .NET 10 solution with a pure class library and an xUnit test project. The implementation follows an immutable functional paradigm: types are records whose fields do not mutate. Every modification returns a new instance via structural sharing.

The graph implements a manual canonical linked vertex registry via `GraphNode.NextVertex`, strictly complying with the ban on library dictionaries, hash tables, or sets. Adjacency lists are kept sorted in ascending order through manual sorted insertion (`insertSorted`).

---

## 📄 Configuración clave / Key Configuration

| Archivo / File | Contenido / Content |
|---|---|
| `DataStructuresAdvanced.fsproj` | `TargetFramework net10.0`, `GenerateDocumentationFile true`, `<Compile Include="DataStructuresAdvanced.fs" />` |
| `DataStructuresAdvanced.Tests.fsproj` | `TargetFramework net10.0`, `IsPackable false`, referencia de proyecto a `DataStructuresAdvanced.fsproj`, paquetes NuGet `xunit (2.9.3)`, `Microsoft.NET.Test.Sdk (17.14.1)`, `xunit.runner.visualstudio (3.1.4)` y `coverlet.collector (6.0.4)` |
| `DataStructuresAdvanced.slnx` | Formato XML de solución .NET (`<Solution>`) estructurando carpetas `/src/` y `/test/` / XML .NET solution format structuring `/src/` and `/test/` folders |

---

## 🚀 Compilación y ejecución / Build & Run

```bash
dotnet build     # Compila la solución con warnings como errores / Builds the solution
dotnet test      # Compila y ejecuta la suite de pruebas unitarias / Builds and runs the unit test suite
```

**Salida real / Actual output** (dotnet `10.0.115`, 2026-11-13):

```text
$ dotnet build
  Determining projects to restore...
  All projects are up-to-date for restore.
  DataStructuresAdvanced -> ~/programming_languages/fsharp/core/algorithms/data_structures_advanced/src/DataStructuresAdvanced.fs/bin/Debug/net10.0/DataStructuresAdvanced.dll
  DataStructuresAdvanced.Tests -> ~/programming_languages/fsharp/core/algorithms/data_structures_advanced/test/DataStructuresAdvanced.Tests/bin/Debug/net10.0/DataStructuresAdvanced.Tests.dll

Build succeeded.
    0 Warning(s)
    0 Error(s)

Time Elapsed 00:00:03.62

$ dotnet test
Test run for ~/programming_languages/fsharp/core/algorithms/data_structures_advanced/test/DataStructuresAdvanced.Tests/bin/Debug/net10.0/DataStructuresAdvanced.Tests.dll (.NETCoreApp,Version=v10.0)
VSTest version 18.0.2 (x64)

Starting test execution, please wait...
A total of 1 test files matched the specified pattern.

Passed!  - Failed:     0, Passed:    24, Skipped:     0, Total:    24, Duration: 71 ms - DataStructuresAdvanced.Tests.dll (net10.0)
```

**ES:** Salida capturada de la última ejecución real sin modificaciones manuales, abreviando las rutas locales con `~`.

**EN:** Output captured from the latest real execution without manual changes, abbreviating local paths with `~`.

---

## 🧠 Algoritmos y operaciones / Algorithms & Operations

| Operación / Operation | Entrada → salida / Input → output | Complejidad / Complexity | Notas / Notes |
|---|---|---|---|
| `TreeNode.create` | `int -> TreeNode` | `O(1)` | `init`: crea celda con valor y enlaces `Left = None`, `Right = None` / creates cell with value and absent links |
| `TreeNode.withLeft` | `TreeNode option -> TreeNode -> TreeNode` | `O(1)` | `set_left`: copia inmutable del nodo con nuevo enlace izquierdo / immutable copy with new left link |
| `TreeNode.withRight` | `TreeNode option -> TreeNode -> TreeNode` | `O(1)` | `set_right`: copia inmutable del nodo con nuevo enlace derecho / immutable copy with new right link |
| `BinaryTree.empty` | `BinaryTree` | `O(1)` | `init`: valor constante con `Root = None` y `Count = 0` / constant empty tree |
| `BinaryTree.insert` | `int -> BinaryTree -> BinaryTree` | `O(h)` | Inserción en ABB; duplicados son idempotentes y no incrementan tamaño (`h` = altura) / BST insert; duplicates are idempotent |
| `BinaryTree.contains` | `int -> BinaryTree -> bool` | `O(h)` | Búsqueda guiada por la invariante del ABB (`h` = altura) / BST invariant guided lookup |
| `BinaryTree.isEmpty` | `BinaryTree -> bool` | `O(1)` | Comprueba si `Count = 0` / checks whether count is zero |
| `BinaryTree.size` | `BinaryTree -> int` | `O(1)` | Retorna el contador almacenado `Count` / returns stored counter |
| `Graph.empty` | `Graph` | `O(1)` | `init`: grafo vacío con `Vertices = None`, `VertexCount = 0`, `EdgeCount = 0` / constant empty graph |
| `Graph.addVertex` | `int -> Graph -> Graph` | `O(V)` | Registra vértice en el registro enlazado; duplicado es idempotente y preserva adyacencia / adds vertex to linked registry |
| `Graph.addEdge` | `int -> int -> Graph -> (bool * Graph)` | `O(V + d(u))` | Requiere extremos existentes; inserta ordenado en vecinos; duplicados y bucles son idempotentes / directed edge addition |
| `Graph.hasEdge` | `int -> int -> Graph -> bool` | `O(V + d(u))` | Búsqueda de vértice origen y exploración lineal de lista de adyacencia / checks presence of directed edge |
| `Graph.getNeighbors` | `int -> Graph -> int list option` | `O(V)` | Retorna `Some neighbors` si existe (`Some []` si aislado) o `None` si no está registrado / returns neighbors or None |
| `Graph.isEmpty` | `Graph -> bool` | `O(1)` | Comprueba si `VertexCount = 0` / checks whether vertex count is zero |
| `Graph.vertexCount` | `Graph -> int` | `O(1)` | Retorna el número de vértices `VertexCount` / returns vertex count |
| `Graph.edgeCount` | `Graph -> int` | `O(1)` | Retorna el número de aristas `EdgeCount` / returns edge count |

---

## 🧩 Decisiones de diseño / Design decisions

| Decisión / Decision | Alternativa considerada / Alternative | Razón / Reason |
|---|---|---|
| Valor embebido en registros (`TreeNode.Value`, `GraphNode.Label`) / Embedded value pattern | Jerarquía de clases con herencia orientada a objetos / Class hierarchy with OOP inheritance | En F#, los tipos de datos algebraicos y registros inmutables son más idiomáticos, seguros y ligeros que una jerarquía de clases mutables; la especificación permite explícitamente el valor embebido / in F#, immutable records are more idiomatic and concise than OOP class inheritance. |
| Registros inmutables con actualización funcional (`{ record with ... }`) / Immutable records with functional updates | Tipos mutables con miembros de modificación in-place / Mutable types with in-place setters | Garantiza pureza funcional, elimina efectos secundarios entre pruebas y garantiza persistencia estructural sin copias profundas defensivas / ensures functional purity, prevents side effects across tests and enables structural sharing. |
| Módulos con `[<RequireQualifiedAccess>]` / Modules with `[<RequireQualifiedAccess>]` | Miembros de método en las clases o funciones globales / Object methods or global functions | Separa datos de funciones de manera funcional y previene colisiones entre operaciones con nombres homónimos (`isEmpty`, `create`, `empty`) / separates data from functions cleanly and avoids name collisions. |
| `addEdge` devuelve tupla `(bool * Graph)` / `addEdge` returns `(bool * Graph)` tuple | Lanzar excepciones en extremos faltantes o devolver solo `Graph` / Throwing exceptions or returning only `Graph` | Explicita el resultado de la operación sin interrumpir el flujo con excepciones; si falla, retorna `(false, graph)` intacto / makes success or failure explicit without runtime exceptions, preserving graph state on failure. |
| Representación de ausencia con `option` (`None`) / Absence represented with `option` (`None`) | Uso de referencias nulas (`null`) o valores centinela numéricos / Null references or sentinel values | `null` no es idiomático en tipos nativos de F# y causaría advertencias del compilador; `option` ofrece seguridad estática completa / `null` is non-idiomatic in native F# types; `option` provides total type safety. |

---

## 🔀 Adaptaciones idiomáticas / Idiomatic adaptations

| Especificación / Specification | Adaptación / Adaptation | Justificación / Justification |
|---|---|---|
| Base `Node(value)` con herencia de clases / `Node(value)` base via OOP inheritance | Valor embebido en registros inmutables (`Value: int` en `TreeNode`, `Label: int` en `GraphNode`) / Embedded value in immutable records | F# favorece composición y registros funcionales; la especificación autoriza expresamente el patrón de valor embebido / F# favours composition and functional records; the specification explicitly authorizes embedded values. |
| `init()` que instancia un objeto / `init()` instantiating an object | Valores constantes `empty` (`BinaryTree.empty`, `Graph.empty`) y función `TreeNode.create` / Constant values `empty` and function `TreeNode.create` | Las estructuras vacías no contienen estado dinámico; una constante inmutable compartida es óptima y segura / empty structures have no dynamic state; a shared immutable constant is optimal and safe. |
| Modificación in-place (`set_left`, `set_right`, `add_edge`) / In-place mutation | Funciones puras que retornan copias actualizadas (`withLeft`, `withRight`, `addEdge`) / Pure functions returning updated copies | F# es funcional por defecto; la inmutabilidad preserva la invariante y el histórico de estados / F# is functional by default; immutability preserves invariants and history. |
| Modificación de copia devuelta en «Aislamiento del resultado de vecinos» / Mutating returned neighbor copy | Prueba adaptada: se omite la mutación in-place de la lista devuelta / Adapted test: in-place list mutation is omitted | En F#, `int list` es una lista inmutable persistente y no soporta mutación destructiva; se prueba que consultas posteriores no alteran listas previas / in F#, `int list` is strictly immutable. |
| Ubicación en raíz `src/data_structures_advanced.ext` / Expected layout in module root | Proyecto de biblioteca y proyecto de test dentro de solución .NET (`.slnx`) / Solution layout with library and test projects | Disposición canónica del ecosistema .NET y F# / canonical structure for the .NET and F# ecosystem. |

---

## 🚨 Indicadores de fallo / Failure indicators

| Operación / Operation | Situación de fallo / Failure situation | Indicador / Indicator | Ejemplo / Example |
|---|---|---|---|
| `BinaryTree.contains` | Valor no encontrado en el árbol / Value not present in tree | `false` | `BinaryTree.contains 99 tree` → `false` |
| `Graph.addEdge` | Extremo origen o destino no registrado / Missing source or target vertex | `(false, graph)` | `Graph.addEdge 10 99 graph` → `(false, graph)` |
| `Graph.hasEdge` | Arista inexistente o vértices ausentes / Edge absent or missing vertices | `false` | `Graph.hasEdge 10 30 graph` → `false` |
| `Graph.getNeighbors` | Vértice consultado no existe en el registro / Vertex not registered in graph | `None` | `Graph.getNeighbors 99 graph` → `None` |
| `TreeNode.Left` / `Right` | Enlace ausente (subárbol vacío) / Absent link (empty subtree) | `None` | `(TreeNode.create 10).Left` → `None` |
| Caso nulo o entrada inválida / Null or invalid input | Parámetros nulos no representables en el sistema de tipos / Not representable in type system | **No aplica / Not applicable** | En F#, tipos como `int` o estructuras no aceptan `null` / in F#, primitives and non-nullable types cannot be null |

---

## ✅ Cobertura de pruebas / Test coverage

| Caso de la especificación / Specification case | Cubierto / Covered | Prueba / Test | Notas / Notes |
|---|---|:--:|---|
| TreeNode: Crear un nodo de árbol / Create a tree node | Sí / Yes | `DataStructuresAdvancedTests.fs:36` (`TreeNode create a tree node`) | Verifica `Value = 10` / verifies `Value = 10` |
| TreeNode: Enlaces iniciales ausentes / Absent initial links | Sí / Yes | `DataStructuresAdvancedTests.fs:41` (`TreeNode absent initial links`) | Verifica `Left = None` y `Right = None` / verifies absent initial links |
| TreeNode: Enlazar dos hijos / Link two children | Sí / Yes | `DataStructuresAdvancedTests.fs:48` (`TreeNode link two children`) | Verifica `withLeft` (20) y `withRight` (30) / verifies linking both children |
| TreeNode: Recorrer cada enlace / Traverse each link | Sí / Yes | `DataStructuresAdvancedTests.fs:59` (`TreeNode traverse each link`) | Verifica navegación a hijos y que sus enlaces son `None` / verifies navigation and child link absence |
| TreeNode: Desenlazar hijos / Unlink children | Sí / Yes | `DataStructuresAdvancedTests.fs:74` (`TreeNode unlink children`) | Verifica desenlazado secuencial con `None` / verifies sequential unlinking |
| TreeNode: Adaptación inmutable / Immutable adaptation | Sí / Yes | `DataStructuresAdvancedTests.fs:92` (`TreeNode immutable adaptation`) | Verifica que el nodo original permanece intacto / verifies original node is not mutated |
| BinaryTree: Crear vacío / Create empty | Sí / Yes | `DataStructuresAdvancedTests.fs:106` (`BinaryTree create empty`) | Verifica `isEmpty = true`, `size = 0`, `contains 50 = false` / verifies empty tree state |
| BinaryTree: Insertar la raíz / Insert the root | Sí / Yes | `DataStructuresAdvancedTests.fs:114` (`BinaryTree insert the root`) | Inserta 50; verifica `size = 1`, `isEmpty = false`, `contains = true` / verifies root insertion |
| BinaryTree: Insertar menor y mayor / Insert lower and higher | Sí / Yes | `DataStructuresAdvancedTests.fs:122` (`BinaryTree insert lower and higher`) | Inserta 50, 30, 70; verifica `size = 3` y pertenencia / verifies BST insertion of lower and higher |
| BinaryTree: Ramas de varios niveles / Multilevel branches | Sí / Yes | `DataStructuresAdvancedTests.fs:131` (`BinaryTree multilevel branches`) | Inserta 50, 30, 70, 20, 40; verifica `size = 5` y pertenencia / verifies multi-level branches |
| BinaryTree: Valor ausente en árbol no vacío / Missing value in nonempty tree | Sí / Yes | `DataStructuresAdvancedTests.fs:140` (`BinaryTree missing value in nonempty tree`) | Verifica que 99 no está presente en árbol con 50 / verifies absent element lookup |
| BinaryTree: Duplicado idempotente / Idempotent duplicate | Sí / Yes | `DataStructuresAdvancedTests.fs:148` (`BinaryTree idempotent duplicate`) | Inserta 50, 30, 70, 30; verifica `size = 3` / verifies duplicate insertion does not grow size |
| BinaryTree: Inserciones ordenadas / Ordered insertions | Sí / Yes | `DataStructuresAdvancedTests.fs:157` (`BinaryTree ordered insertions`) | Inserta 20, 30, 40, 50, 70; verifica `size = 5` y pertenencia / verifies ordered insertion sequence |
| Graph: Crear vacío / Create empty | Sí / Yes | `DataStructuresAdvancedTests.fs:172` (`Graph create empty`) | Verifica `isEmpty = true`, conteos en 0, `hasEdge` y `getNeighbors` fallan / verifies empty graph |
| Graph: Vértice aislado / Isolated vertex | Sí / Yes | `DataStructuresAdvancedTests.fs:182` (`Graph isolated vertex`) | Registra 10; verifica `vertexCount = 1`, `edgeCount = 0`, vecinos = `Some []` / verifies isolated vertex |
| Graph: Etiquetas no consecutivas y arista dirigida / Nonconsecutive labels and directed edge | Sí / Yes | `DataStructuresAdvancedTests.fs:191` (`Graph nonconsecutive labels and directed edge`) | Vértices 10 y 70, arista 10->70 dirigida unidireccional / verifies directed edge between 10 and 70 |
| Graph: Vértice duplicado conserva adyacencia / Duplicate vertex preserves adjacency | Sí / Yes | `DataStructuresAdvancedTests.fs:202` (`Graph duplicate vertex preserves adjacency`) | Re-registrar vértice 10 conserva su adyacencia `[30]` / verifies idempotency of addVertex |
| Graph: Arista duplicada / Duplicate edge | Sí / Yes | `DataStructuresAdvancedTests.fs:213` (`Graph duplicate edge`) | Añadir 10->30 dos veces mantiene `edgeCount = 1` y vecinos `[30]` / verifies duplicate edge idempotency |
| Graph: Extremo inexistente sin mutación / Missing endpoint without mutation | Sí / Yes | `DataStructuresAdvancedTests.fs:225` (`Graph missing endpoint without mutation`) | Verifica fallos con vértices inexistentes y ausencia de mutación / verifies failed additions preserve graph |
| Graph: Vecinos ordenados y contadores / Sorted neighbours and counts | Sí / Yes | `DataStructuresAdvancedTests.fs:250` (`Graph sorted neighbours and counts`) | Inserta en orden mixto; verifica adyacencia `[30; 50; 70]` y conteos / verifies ascending sorted order |
| Graph: Consulta de arista ausente / Missing-edge query | Sí / Yes | `DataStructuresAdvancedTests.fs:272` (`Graph missing-edge query`) | Verifica `hasEdge 10 30 = false` / verifies non-existent edge query |
| Graph: Vecinos de vértice inexistente / Neighbours of missing vertex | Sí / Yes | `DataStructuresAdvancedTests.fs:281` (`Graph neighbours of missing vertex`) | Verifica `getNeighbors 99 = None` frente a `Some []` de aislado / verifies missing vertex returns None |
| Graph: Bucle y duplicado / Self-loop and duplicate | Sí / Yes | `DataStructuresAdvancedTests.fs:290` (`Graph self-loop and duplicate`) | Verifica bucle 10->10 y su idempotencia / verifies self-loop edge and idempotency |
| Graph: Aislamiento del resultado de vecinos / Neighbour-result isolation | Sí / Yes | `DataStructuresAdvancedTests.fs:303` (`Graph neighbour-result isolation`) | Verifica que adiciones posteriores no alteran listas previamente obtenidas / verifies immutability isolation |

**ES:** Mapeo de casos: `TreeNode` (6 casos) + `BinaryTree` (7 casos) + `Graph` (11 casos) = **24 tests unitarios** en `DataStructuresAdvancedTests.fs`, todos descubiertos y ejecutados en verde por `dotnet test`.

**EN:** Case mapping: `TreeNode` (6 cases) + `BinaryTree` (7 cases) + `Graph` (11 cases) = **24 unit tests** in `DataStructuresAdvancedTests.fs`, all discovered and passing under `dotnet test`.

---

## ⚠️ Limitaciones conocidas / Known limitations

| Limitación / Limitation | Impacto / Impact | Alternativa o plan / Workaround or plan |
|---|---|---|
| Árbol binario no balanceado / Unbalanced binary tree | En el peor caso (inserciones ordenadas) la altura es `O(n)`, degradando búsqueda a `O(n)` / Worst-case insertion sequence results in `O(n)` depth | Comportamiento esperado por la especificación para un ABB puro didáctico; el balanceo corresponde a fases posteriores / intentional pure BST design |
| Registro enlazado lineal de vértices `O(V)` / Linear linked vertex registry `O(V)` | `findVertex` realiza recorrido secuencial en `O(V)` en lugar de acceso `O(1)` / sequential vertex traversal costs `O(V)` | Requisito explícito de la especificación para implementar el registro de forma manual sin tablas hash / strict specification requirement |
| Inmutabilidad de listas de adyacencia / Immutability of adjacency lists | Modificar la lista devuelta por `getNeighbors` in-place no es representable / mutating returned list is not representable | `int list` es intrínsecamente inmutable en F#; el aislamiento de resultados queda garantizado de forma nativa / native structural immutability guarantees isolation |

---

## 📝 Notas de implementación / Implementation Notes

### 🧱 Valor embebido e inmutabilidad / Embedded value and immutability

**ES:** F# no requiere jerarquías complejas de clases para modelar datos puros. La base conceptual `Node` se implementa como valor embebido en los registros `TreeNode` (`Value: int`) y `GraphNode` (`Label: int`). La inmutabilidad de los registros garantiza que ningún estado intermedio pueda ser alterado de forma imprevista, y las actualizaciones (`{ node with Left = left }`) comparten estructura en memoria de manera transparente.

**EN:** F# does not require OOP class inheritance hierarchies to model pure data. The conceptual `Node` base is embedded in records `TreeNode` (`Value: int`) and `GraphNode` (`Label: int`). Record immutability guarantees no intermediate state can be mutated unexpectedly, with functional updates (`{ node with Left = left }`) leveraging transparent structural sharing.

### 🔍 Registro canónico de vértices y adyacencia ordenada / Canonical vertex registry and sorted adjacency

**ES:** El grafo no delega en diccionarios o tablas hash del framework. La colección de vértices se enlaza a mano mediante el campo `NextVertex: GraphNode option`. Para mantener los vecinos ordenados crecientemente sin recurrir a ordenaciones posteriores completas, se implementa una función local recursiva `insertSorted` en `O(d(u))`, que inserta el vecino en su posición adecuada y descarta duplicados.

**EN:** The graph does not delegate to framework dictionaries or hash sets. Vertices are linked manually via `NextVertex: GraphNode option`. Neighbors are maintained in ascending order via an `O(d(u))` recursive `insertSorted` helper that inserts items in their correct position while ignoring duplicates.

### 🚫 Manejo del caso nulo / Null handling

**ES:** En F#, los tipos primitivos (`int`) y las definiciones algebraicas no admiten valores nulos (`null`). La ausencia se modela estrictamente a través del tipo `option` (`None` / `Some`). Por tanto, los casos de prueba asociados a referencias nulas no aplican y no son representables en el sistema de tipos.

**EN:** In F#, primitive types (`int`) and algebraic definitions do not admit `null`. Absence is represented strictly via `option` (`None` / `Some`). Consequently, test cases involving null references are not applicable and unrepresentable in the type system.

---

### 🌐 Otras implementaciones / Other implementations

Este proyecto también está implementado en otros lenguajes. Explora el [repositorio principal](https://github.com/yorche3/programming_languages) para consultar las demás versiones.

This project is also implemented in other languages. Explore the [main repository](https://github.com/yorche3/programming_languages) to see all the versions.

---

## 🔍 Checklist de validación / Validation checklist

- [x] La suite nativa se ejecutó y su salida real está copiada en este README / Native suite was executed and its real output is copied into this README.
- [x] Cada caso de la especificación tiene su fila en _Cobertura de pruebas_ (o `Omitido` con razón) / Each specification case has its row in _Test coverage_ (or `Omitted` with reason).
- [x] Cada desviación del pseudocódigo o de la ubicación esperada está en _Adaptaciones idiomáticas_ / Each deviation from pseudocode or expected location is in _Idiomatic adaptations_.
- [x] Cada operación con fallo posible está en _Indicadores de fallo_ / Each operation with potential failure is in _Failure indicators_.
- [x] No hay rutas absolutas del autor, credenciales ni salidas inventadas / No author absolute paths, credentials, or fabricated outputs.
- [x] Los enlaces relativos resuelven dentro del repositorio y el documento es bilingüe / Relative links resolve within repository and document is bilingual.
- [x] Ninguna sección repite lo que ya dice la especificación / No section repeats what the specification already states.

---

## 📚 Referencias / References

| Tipo / Kind | Referencia / Reference |
|---|---|
| Especificación / Specification | [`07_Data_Structures_Advanced.md`](https://yorche3.github.io/programming_languages/core/algorithms/07_Data_Structures_Advanced/) |
| Primera base del lenguaje / First language reference | `data_structures_basics` — [`../data_structures_basics/README.md`](../data_structures_basics/README.md) |
| Segunda base del lenguaje / Second language reference | `numbers` — [`../../foundations/numbers/README.md`](../../foundations/numbers/README.md) |
| Guía de inicialización / Initialisation guide | [`core/00_Project_Initialization_Guide.md`](https://yorche3.github.io/programming_languages/core/00_Project_Initialization_Guide/) |
| Adaptaciones idiomáticas / Idiomatic adaptations | [`AGENT_Template.md`](https://yorche3.github.io/programming_languages/AGENT_Template/) |
| Validación de la documentación / Documentation validation | [`WORKFLOW.md`](https://yorche3.github.io/programming_languages/WORKFLOW/) |
| Documentación oficial del lenguaje / Language official docs | [F# Documentation](https://learn.microsoft.com/dotnet/fsharp/) · [xUnit.net](https://xunit.net/) |

---

*[← Volver a Algorithms Pure](../README.md) | [↑ Volver a F# Core](../../README.md)*

*🌐 [github.com/yorche3/programming_languages](https://github.com/yorche3/programming_languages) · [GitHub Pages](https://yorche3.github.io/programming_languages/)*
