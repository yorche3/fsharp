# Data Structures Basics — F#

Implementación de la especificación [06_Data_Structures_Basics](https://yorche3.github.io/programming_languages/core/algorithms/06_Data_Structures_Basics/) en **F#** (.NET 10), con registros inmutables y pruebas unitarias con **xunit**.

Implementation of the [06_Data_Structures_Basics](https://yorche3.github.io/programming_languages/core/algorithms/06_Data_Structures_Basics/) specification in **F#** (.NET 10), with immutable records and **xunit** unit tests.

**ES:** La celda enlazada compartida (`Node`) y los tres ADT —lista enlazada, pila y cola— se implementan a mano sobre registros inmutables, sin ninguna colección de la biblioteca estándar. Cada operación devuelve la estructura resultante: no hay mutación ni identidad de instancia, y el enlace ausente solo aparece en `Node`.

**EN:** The shared linked cell (`Node`) and the three ADTs —linked list, stack and queue— are implemented by hand over immutable records, without any standard-library collection. Every operation returns the resulting structure: there is no mutation and no instance identity, and the absent link appears only in `Node`.

---

## 📂 Archivos y estructura / Files & Structure

| Archivo / File | Propósito / Purpose |
|---|---|
| [`src/DataStructuresBasics.fs/DataStructuresBasics.fs`](src/DataStructuresBasics.fs/DataStructuresBasics.fs) | Los cuatro tipos —`Node`, `LinkedList`, `Stack`, `Queue`—, el módulo `Contract` y las 21 funciones del contrato / The four types, the `Contract` module and the contract's 21 functions |
| [`src/DataStructuresBasics.fs/DataStructuresBasics.fsproj`](src/DataStructuresBasics.fs/DataStructuresBasics.fsproj) | Proyecto de biblioteca (`net10.0`) / Library project |
| [`test/DataStructuresBasics.Tests/DataStructuresBasicsTests.fs`](test/DataStructuresBasics.Tests/DataStructuresBasicsTests.fs) | Suite xunit: los 15 casos × las 23 operaciones = 23 tests / xunit suite: the 15 cases × the 23 operations = 23 tests |
| [`test/DataStructuresBasics.Tests/DataStructuresBasics.Tests.fsproj`](test/DataStructuresBasics.Tests/DataStructuresBasics.Tests.fsproj) | Proyecto de pruebas (xunit, Test SDK, coverlet) / Test project |
| [`DataStructuresBasics.slnx`](DataStructuresBasics.slnx) | Solución que enlaza la biblioteca y las pruebas / Solution linking library and tests |

**Estructura de directorios / Directory structure:**

```text
data_structures_basics/
├── DataStructuresBasics.slnx                    # Solución / Solution
├── src/DataStructuresBasics.fs/
│   ├── DataStructuresBasics.fs                  # Contrato e implementación
│   └── DataStructuresBasics.fsproj              # Proyecto de biblioteca
├── test/DataStructuresBasics.Tests/
│   ├── DataStructuresBasicsTests.fs             # 15 casos × 23 operaciones
│   └── DataStructuresBasics.Tests.fsproj        # Proyecto de pruebas
└── README.md
```

**Desviación respecto a la ubicación esperada / Deviation from expected location:**

**ES:** La especificación propone `src/data_structures_basics.ext`, `test/data_structures_basics_test.ext` y `test/run_tests.ext`. Aquí se usa la disposición de .NET: una solución (`.slnx`) con un proyecto de biblioteca en `src/` y otro de pruebas en `test/`, cada uno en su carpeta (`DataStructuresBasics.fs/`), con el nombre del módulo en `PascalCase` —que en F# es el nombre del espacio de nombres y el del archivo—. El `src/` coincide; el **punto de entrada no existe porque no hace falta**: el runner es `dotnet test`, que descubre los `[<Fact>]` de xunit.

**EN:** The specification suggests `src/data_structures_basics.ext`, `test/data_structures_basics_test.ext` and `test/run_tests.ext`. This module uses the .NET layout: a solution (`.slnx`) with a library project under `src/` and a test project under `test/`, each in its own folder (`DataStructuresBasics.fs/`), with the module name in `PascalCase` —which in F# is both the namespace and the file name—. `src/` matches; the **entry point does not exist because it is not needed**: the runner is `dotnet test`, which discovers the xunit `[<Fact>]` tests.

---

## 🛠️ Enfoque y construcción / Approach & Build

**ES:** El proyecto se creó con las plantillas del CLI de .NET (biblioteca de clases y proyecto de pruebas xunit, unidos por una solución), y después se renombraron la solución, los proyectos y los archivos al nombre del módulo. F# no necesita manifiesto propio: el `.fsproj` **es** el manifiesto y el orden de compilación de los ficheros.

El contrato se declara con **registros inmutables** y un módulo `[<RequireQualifiedAccess>]` por estructura, más el módulo `Contract` con el indicador de fallo. `Stack` y `Queue` no envuelven `LinkedList`: cada uno enlaza sus celdas con las funciones de `Node`.

**EN:** The project was created with the .NET CLI templates (class library and xunit test project, joined by a solution), and then the solution, projects and files were renamed to the module name. F# needs no separate manifest: the `.fsproj` **is** the manifest and the file compilation order.

The contract is declared with **immutable records** and one `[<RequireQualifiedAccess>]` module per structure, plus the `Contract` module holding the failure indicator. `Stack` and `Queue` do not wrap `LinkedList`: each one links its cells with the `Node` functions.

---

## 📄 Configuración clave / Key Configuration

| Archivo / File | Contenido / Content |
|---|---|
| `DataStructuresBasics.fsproj` | `TargetFramework net10.0`, `GenerateDocumentationFile true`, un único `<Compile Include="DataStructuresBasics.fs" />` |
| `DataStructuresBasics.Tests.fsproj` | `net10.0`, `IsPackable false`, `<ProjectReference>` al proyecto de biblioteca y paquetes `xunit 2.9.3`, `Microsoft.NET.Test.Sdk 17.14.1`, `xunit.runner.visualstudio 3.1.4` y `coverlet.collector 6.0.4` |
| `DataStructuresBasics.slnx` | Solución con los dos proyectos / Solution with both projects |

**ES:** El único valor declarado del contrato es `Contract.FAILURE_VALUE = -1`; el resto del contrato son los tipos y las firmas del fuente.

**EN:** The contract's only declared value is `Contract.FAILURE_VALUE = -1`; the rest of the contract is the source's types and signatures.

---

## 🚀 Compilación y ejecución / Build & Run

```bash
dotnet build     # Compila la solución / Builds the solution
dotnet test      # Compila y ejecuta la suite / Builds and runs the suite
```

**Salida real / Actual output** (dotnet `10.0.112`, 2026-10-04):

```text
$ dotnet build
Build succeeded.
    0 Warning(s)
    0 Error(s)

$ dotnet test
Passed!  - Failed:     0, Passed:    23, Skipped:     0, Total:    23, Duration: 57 ms - DataStructuresBasics.Tests.dll (net10.0)
```

**ES:** Salida copiada de la última ejecución real; se han omitido las líneas de restauración y las rutas de compilación. El acta de evidencia del sprint se encuentra en [`docs/evidence/algorithms/data_structures_basics/fsharp.md`](https://github.com/yorche3/programming_languages/blob/main/docs/evidence/algorithms/data_structures_basics/fsharp.md).

**EN:** Output copied from the last real run; restore lines and build paths are omitted. The sprint evidence record is at [`docs/evidence/algorithms/data_structures_basics/fsharp.md`](https://github.com/yorche3/programming_languages/blob/main/docs/evidence/algorithms/data_structures_basics/fsharp.md).

---

## 🧠 Algoritmos y operaciones / Algorithms & Operations

| Operación / Operation | Entrada → salida / Input → output | Complejidad / Complexity | Notas / Notes |
|---|---|---|---|
| `Node.create` | `int → Node` | `O(1)` | `init`: valor y enlace ausente (`None`) / value and absent link (`None`) |
| `Node.withNext` | `Node → Node → Node` | `O(1)` | `set_next`: devuelve una celda nueva / returns a new cell |
| `Node.Value` (campo / field) | `Node → int` | `O(1)` | `get_value`; no muta / does not mutate |
| `Node.Next` (campo / field) | `Node → Node option` | `O(1)` | `get_next`; `None` es el enlace ausente / `None` is the absent link |
| `LinkedList.empty` | `LinkedList` | `O(1)` | `init` |
| `LinkedList.headValue` | `LinkedList → int` | `O(1)` | `-1` si la lista está vacía / `-1` when the list is empty |
| `LinkedList.insertHead` | `int → LinkedList → LinkedList` | `O(1)` | La cabeza nueva enlaza con la anterior / the new head links to the previous one |
| `LinkedList.insertTail` | `int → LinkedList → LinkedList` | `O(n)` | Reconstruye la cadena; ver adaptaciones / rebuilds the chain; see adaptations |
| `LinkedList.delete` | `int → LinkedList → (bool * LinkedList)` | `O(n)` | Primera aparición; en el fallo, la misma lista / first occurrence; on failure, the same list |
| `LinkedList.isEmpty` | `LinkedList → bool` | `O(1)` | Contador a cero / counter equals zero |
| `LinkedList.size` | `LinkedList → int` | `O(1)` | Contador guardado / stored counter |
| `Stack.empty` | `Stack` | `O(1)` | `init` |
| `Stack.push` | `int → Stack → Stack` | `O(1)` | El tope nuevo enlaza con el anterior / the new top links to the previous one |
| `Stack.pop` | `Stack → (int * Stack)` | `O(1)` | `-1` y la misma pila si está vacía / `-1` and the same stack when empty |
| `Stack.peek` | `Stack → int` | `O(1)` | `-1` si la pila está vacía / `-1` when the stack is empty |
| `Stack.isEmpty` | `Stack → bool` | `O(1)` | Contador a cero / counter equals zero |
| `Stack.size` | `Stack → int` | `O(1)` | Contador guardado / stored counter |
| `Queue.empty` | `Queue` | `O(1)` | `init` |
| `Queue.enqueue` | `int → Queue → Queue` | `O(n)` | Reconstruye la cadena; ver adaptaciones / rebuilds the chain; see adaptations |
| `Queue.dequeue` | `Queue → (int * Queue)` | `O(1)` | `-1` y la misma cola si está vacía / `-1` and the same queue when empty |
| `Queue.peek` | `Queue → int` | `O(1)` | `-1` si la cola está vacía / `-1` when the queue is empty |
| `Queue.isEmpty` | `Queue → bool` | `O(1)` | Contador a cero / counter equals zero |
| `Queue.size` | `Queue → int` | `O(1)` | Contador guardado / stored counter |

---

## 🧩 Decisiones de diseño / Design decisions

| Decisión / Decision | Alternativa considerada / Alternative | Razón / Reason |
|---|---|---|
| Registros **inmutables** / **Immutable** records | Clases con campos `mutable` y métodos `unit` / classes with `mutable` fields and `unit` methods | F# favorece los datos inmutables: el resultado de cada operación es explícito, la suite no necesita aislar copias y `Stack`/`Queue` no pueden corromper la estructura recibida / F# favours immutable data: each operation's result is explicit, the suite needs no isolated copies and `Stack`/`Queue` cannot corrupt the received structure. |
| Un módulo `[<RequireQualifiedAccess>]` por estructura / one module per structure | Miembros de instancia en cada tipo / instance members on each type | Los cuatro tipos son datos; las operaciones son funciones y el nombre cualificado evita colisiones entre `isEmpty`, `size` y `peek` de las cuatro estructuras / the four types are data; the operations are functions and qualification avoids clashes among the four structures' `isEmpty`, `size` and `peek`. |
| `empty` como **valor** / `empty` as a **value** | Función `init()` que devuelve el valor vacío / an `init()` function | Un registro sin valores que inicializar se expresa mejor con una constante: no hay estado que preparar y no puede fallar / a record with nothing to initialise is better expressed as a constant: there is no state to prepare and it cannot fail. |
| `Head`/`Tail`/`Count` **públicos** y las operaciones como funciones / public fields and operations as functions | Ocultar los campos y exponer accesores / hiding fields behind accessors | El consumidor de una estructura enlazada necesita poder moverse por los nodos (`Node.Next` es `get_next`); el resto del contrato sigue siendo función para que la representación pueda cambiar sin tocar la suite / the consumer of a linked structure must be able to walk nodes (`Node.Next` is `get_next`); the rest of the contract stays functional so the representation can change without touching the suite. |
| Indicador `-1` y tuplas `(valor, estructura)` / `-1` and `(valor, structure)` tuples | `int option` en las extracciones / `int option` on extractions | Regla de la casa: solo `Node` puede devolver o comparar con `None`; el resto devuelve un valor, y así la ausencia de enlace y el fallo de una operación no se confunden / house rule: only `Node` may return or compare with `None`; everything else returns a value, so an absent link and an operation failure cannot be confused. |
| Un test por operación con ejecutor compartido / one test per operation with a shared executor | Un test por estructura con todas sus aserciones / one test per structure with all its assertions | Mantiene el escenario completo por ADT (que pide la especificación) y, a la vez, cada operación tiene su test y su mensaje de fallo / keeps the full scenario per ADT (as the specification asks) while giving every operation its own test and failure message. |

---

## 🔀 Adaptaciones idiomáticas / Idiomatic adaptations

| Especificación / Specification | Adaptación / Adaptation | Justificación / Justification |
|---|---|---|
| `Node`, `LinkedList`, `Stack` y `Queue` son **tipos nuevos** / new types | Cuatro registros inmutables en un `namespace` / four immutable records in a namespace | F# declara tipos con registros; la ausencia de enlace se expresa con `Node option`, sin `null` / F# declares types with records; the absent link is expressed with `Node option`, without `null`. |
| Cada estructura tiene `init()` / each structure has `init()` | Valor `empty` por estructura y `Node.create` para la celda / an `empty` value per structure and `Node.create` for the cell | No hay estado que inicializar: el valor vacío es una constante y la celda se crea con su valor / there is no state to initialise: the empty value is a constant and the cell is created with its value. |
| `get_value()` / `get_next()` | Campos `Value` y `Next` de `Node` / `Node`'s `Value` and `Next` fields | El recorrido de una estructura enlazada necesita leer el enlace; el resto de las consultas son funciones / walking a linked structure needs to read the link; the remaining queries are functions. |
| `set_next(next)` «devuelve un nodo nuevo si el lenguaje es inmutable» / "returns a new node when the language is immutable" | `Node.withNext next node` devuelve la celda copiada / returns the copied cell | F# es inmutable: la celda original no se toca, como la propia especificación contempla / F# is immutable: the original cell is not touched, as the specification itself contemplates. |
| Inserciones al final y `enqueue` en `O(1)` / tail insertions and `enqueue` in `O(1)` | **`O(n)`**: reconstruyen el camino hasta la cola / they rebuild the path to the tail | Con un único `Node` compartido y un solo puntero `Rear`/`Tail`, la celda de cola no se puede enlazar sin copiar el camino; usar dos cadenas daría O(1) amortizado pero cambiaría la representación que fija la especificación / with a single shared `Node` and a single `Rear`/`Tail` pointer, the tail cell cannot be linked without copying the path; two chains would give amortised O(1) but would change the representation the specification fixes. |
| Indicador natural de fallo / natural failure indicator | `-1` (`Contract.FAILURE_VALUE`) y tuplas `(valor, estructura)` / `-1` and `(value, structure)` tuples | Solo `Node` usa `None`; las operaciones que extraen un entero fallan con `-1`, y `pop`/`dequeue`/`delete` devuelven el indicador con la **misma** estructura, sin excepciones / only `Node` uses `None`; integer extractions fail with `-1`, and `pop`/`dequeue`/`delete` return the indicator with the **same** structure, with no exceptions. |
| Caso nulo o entrada inválida / null case or invalid input | **No aplica / Not applicable** | La especificación 06 no define entrada nula: sus casos son pasos sobre el mismo estado y todos los valores son enteros positivos / specification 06 defines no null input: its cases are steps on the same state and every value is a positive integer. |
| `src/data_structures_basics.ext`, `test/…_test.ext`, `run_tests.ext` | `src/DataStructuresBasics.fs/…`, `test/DataStructuresBasics.Tests/…` y `dotnet test` | Disposición de .NET con el nombre del módulo; xunit descubre los tests, así que no hay `run_tests` / .NET layout with the module name; xunit discovers tests, so there is no `run_tests`. |

---

## 🚨 Indicadores de fallo / Failure indicators

| Operación / Operation | Situación de fallo / Failure situation | Indicador / Indicator | Ejemplo / Example |
|---|---|---|---|
| `LinkedList.headValue` | Lista vacía / empty list | `-1` | `LinkedList.headValue LinkedList.empty` → `-1` |
| `LinkedList.delete` | Valor ausente / absent value | `(false, la misma lista)` / `(false, the same list)` | `LinkedList.delete 99 list` → `(false, list)` |
| `Stack.pop` | Pila vacía / empty stack | `(-1, la misma pila)` / `(-1, the same stack)` | `Stack.pop Stack.empty` → `(-1, Stack.empty)` |
| `Stack.peek` | Pila vacía / empty stack | `-1` | `Stack.peek Stack.empty` → `-1` |
| `Queue.dequeue` | Cola vacía / empty queue | `(-1, la misma cola)` / `(-1, the same queue)` | `Queue.dequeue Queue.empty` → `(-1, Queue.empty)` |
| `Queue.peek` | Cola vacía / empty queue | `-1` | `Queue.peek Queue.empty` → `-1` |
| `Node.Next` | Enlace ausente / absent link | `None` | `(Node.create 10).Next` → `None` |
| Caso nulo o inválido / null or invalid input | — | **No aplica / Not applicable** | La especificación no define entrada nula / the specification defines no null input |

**ES:** No hay excepciones en el contrato: `pop`, `dequeue` y `delete` devuelven siempre una tupla, y con `Option`/`Result` prohibidos en esta fase solo `Node` usa `None`.

**EN:** The contract has no exceptions: `pop`, `dequeue` and `delete` always return a tuple, and with `Option`/`Result` forbidden in this phase only `Node` uses `None`.

---

## ✅ Cobertura de pruebas / Test coverage

**ES:** Los 15 casos de la especificación se aplican a las 23 operaciones: cada test reproduce el escenario completo de su ADT y solo comprueba las aserciones de la operación bajo prueba. La columna _Prueba_ nombra los tests que comprueban cada caso.

**EN:** The 15 specification cases apply to the 23 operations: every test replays its ADT's full scenario and asserts only the assertions owned by the operation under test. The _Test_ column names the tests that check each case.

| Caso de la especificación / Specification case | Cubierto / Covered | Prueba / Test | Notas / Notes |
|---|---|:--:|---|
| Node: initialize and observe value/link | Sí / Yes | `Node init`, `Node get value`, `Node get next` | `Value` = 10; `Next` = `None` |
| Node: initialize another node, link and traverse | Sí / Yes | `Node set next`, `Node get next`, `Node get value` | `withNext` enlaza la celda `b`; su valor es 20 |
| LinkedList: empty state | Sí / Yes | `LinkedList init`, `LinkedList is empty`, `LinkedList size`, `LinkedList get head` | `true`, `0`, `-1` |
| LinkedList: insert at both ends | Sí / Yes | `LinkedList insert head`, `LinkedList insert tail`, `LinkedList size`, `LinkedList get head` | `size` = 4 y recorrido `5, 10, 20, 10` |
| LinkedList: delete first occurrence | Sí / Yes | `LinkedList delete`, `LinkedList size`, `LinkedList get head` | `true`; recorrido `5, 20, 10`; `size` = 3 |
| LinkedList: absent value | Sí / Yes | `LinkedList delete`, `LinkedList size`, `LinkedList get head` | `false`; recorrido y tamaño no cambian |
| LinkedList: empty the list | Sí / Yes | `LinkedList delete`, `LinkedList is empty`, `LinkedList size`, `LinkedList get head` | Tres borrados con `true`; `true`, `0`, `-1` |
| Stack: empty state and failed removal | Sí / Yes | `Stack init`, `Stack is empty`, `Stack size`, `Stack peek`, `Stack pop` | `true`, `0`; `peek` y `pop` = `-1` (y la misma pila) |
| Stack: LIFO and non-mutating `peek` | Sí / Yes | `Stack push`, `Stack peek`, `Stack size` | `peek` = 30; `size` = 3 |
| Stack: removal and reuse | Sí / Yes | `Stack pop`, `Stack push`, `Stack is empty`, `Stack size` | Resultados `30, 40, 20, 10`; `true`, `0` |
| Stack: empty after removal | Sí / Yes | `Stack pop`, `Stack is empty` | `-1` con la misma pila; sigue vacía / stays empty |
| Queue: empty state and failed removal | Sí / Yes | `Queue init`, `Queue is empty`, `Queue size`, `Queue peek`, `Queue dequeue` | `true`, `0`; `peek` y `dequeue` = `-1` (y la misma cola) |
| Queue: FIFO and non-mutating `peek` | Sí / Yes | `Queue enqueue`, `Queue peek`, `Queue size` | `peek` = 10; `size` = 3 |
| Queue: removal and reuse | Sí / Yes | `Queue dequeue`, `Queue enqueue`, `Queue is empty`, `Queue size` | Resultados `10, 20, 30, 40`; `true`, `0` |
| Queue: empty after removal | Sí / Yes | `Queue dequeue`, `Queue is empty` | `-1` con la misma cola; sigue vacía / stays empty |

**ES:** Total: **23 tests** (uno por operación), sin casos omitidos; el total aparece en la salida real de `dotnet test`.

**EN:** Total: **23 tests** (one per operation), with no omitted cases; the total appears in the real `dotnet test` output.

---

## ⚠️ Limitaciones conocidas / Known limitations

| Limitación / Limitation | Impacto / Impact | Alternativa o plan / Workaround or plan |
|---|---|---|
| `insertTail` y `enqueue` son `O(n)` / are `O(n)` | Insertar al final cuesta un recorrido de la cadena / appending costs one chain walk | Viene de la representación inmutable de un único `Node`; se documenta en _Adaptaciones idiomáticas_ y se mide en la tabla de operaciones / it comes from the immutable single-`Node` representation; documented under _Idiomatic adaptations_ and measured in the operations table. |
| Cada operación que reconstruye la cadena asigna celdas nuevas / operations rebuilding the chain allocate new cells | Coste de asignación `O(n)` por inserción al final o borrado / `O(n)` allocation cost per tail insertion or deletion | El recolector de basura de .NET recupera las cadenas antiguas; no hay fugas ni punteros colgando / .NET's garbage collector reclaims the old chains; there are no leaks or dangling pointers. |
| Los recorridos reconstruyen la lista (`delete`) / traversals rebuild the list | Recorrer destruyendo consume `O(n)` copias intermedias / draining consumes `O(n)` intermediate copies | Es la única forma de avanzar con las operaciones del contrato; la suite recorre una lista nueva y no daña el escenario / it is the only way to advance with the contract's operations; the suite walks a fresh list and does not damage the scenario. |

---

## 📝 Notas de implementación / Implementation Notes

### 🧱 Registros inmutables y `Node` compartido / Immutable records and shared `Node`

**ES:** Los cuatro tipos son registros. `Node` es la única celda enlazada: `LinkedList`, `Stack` y `Queue` guardan referencias a `Node` y su propio contador, y ninguno envuelve a otro. Las actualizaciones usan `{ registro with … }`, que devuelve una copia superficial con el campo cambiado.

**EN:** The four types are records. `Node` is the only linked cell: `LinkedList`, `Stack` and `Queue` hold `Node` references plus their own counter, and none wraps another. Updates use `{ record with … }`, which returns a shallow copy with the changed field.

### ⚠️ Enlazar la celda nueva es obligatorio / Linking the new cell is mandatory

**ES:** En una estructura inmutable, crear la celda y colocarla en la cabeza **no basta**: hay que enlazarla con la cadena anterior (`Node.withNext`). Si se olvida, la estructura queda con un solo elemento y el recorrido lo delata. Lo mismo al final de la cadena, donde además hay que reconstruir el camino (`insertTail`, `enqueue`).

**EN:** In an immutable structure, creating the cell and placing it at the head is **not enough**: it must be linked to the previous chain (`Node.withNext`). Forgetting it leaves the structure with a single element and the traversal shows it. The same applies at the tail, where the path must also be rebuilt (`insertTail`, `enqueue`).

### 🧷 `None` solo en `Node` / `None` only in `Node`

**ES:** El enlace ausente es `None` y se compara dentro de los módulos (`match list.Head with | None -> …`), pero ninguna operación pública devuelve `option`: los fallos son `-1` o tuplas con la misma estructura. Un `namespace` de F# no admite valores, así que el indicador vive en el módulo `Contract`.

**EN:** The absent link is `None` and is matched inside the modules (`match list.Head with | None -> …`), but no public operation returns `option`: failures are `-1` or tuples carrying the same structure. An F# namespace cannot contain values, so the indicator lives in the `Contract` module.

### 🧪 Cómo funciona la suite / How the suite works

**ES:** La suite tiene 23 tests —uno por operación— sobre los 15 casos. Cada escenario encadena los pasos devolviendo la estructura siguiente (no hace falta aislar copias) y cada aserción declara sus **dueños**: el ejecutor `assertFor` solo la comprueba cuando la operación bajo prueba está entre ellos, así que un fallo imprime `"<operación> should return <esperado> in <caso> but got <obtenido>"`. El recorrido se comprueba con `traverse` (`headValue` + `delete`, con `-1` como centinela).

**EN:** The suite has 23 tests —one per operation— over the 15 cases. Each scenario chains the steps by returning the next structure (no isolated copies needed) and every assertion declares its **owners**: the `assertFor` executor only checks it when the operation under test is among them, so a failure prints `"<operation> should return <expected> in <case> but got <actual>"`. Traversals are checked with `traverse` (`headValue` + `delete`, with `-1` as the sentinel).

### 💾 Memoria y asignación / Memory and allocation

**ES:** Las operaciones que reconstruyen la cadena (`insertTail`, `enqueue`, `delete`) crean copias del camino afectado; el recolector de basura de .NET libera las cadenas que dejan de ser accesibles. No hay memoria manual ni fugas.

**EN:** Operations that rebuild the chain (`insertTail`, `enqueue`, `delete`) copy the affected path; .NET's garbage collector releases chains that become unreachable. There is no manual memory management and no leaks.

### 🚫 Caso nulo / Null case

**ES:** **No aplica.** La especificación 06 no define ninguna entrada nula o inválida; sus casos son pasos sucesivos sobre el mismo estado y todos los valores son enteros positivos que no chocan con el indicador `-1`. La única ausencia del módulo —el enlace de una celda— se representa con `None` dentro de `Node`.

**EN:** **Not applicable.** Specification 06 defines no null or invalid input; its cases are successive steps on the same state and every value is a positive integer that does not collide with the `-1` indicator. The module's only absence —a cell's link— is represented with `None` inside `Node`.

### 📁 Desviación de ubicación y nombres / Location and naming deviation

**ES:** La especificación espera `src/data_structures_basics.ext`, `test/data_structures_basics_test.ext` y `test/run_tests.ext`. Aquí el módulo es un proyecto de biblioteca (`DataStructuresBasics.fsproj`) con su fuente homónimo, las pruebas son otro proyecto xunit y el runner es `dotnet test`: no hay `run_tests` porque xunit descubre los tests. Los nombres siguen la convención de .NET (`PascalCase` para archivos y proyectos, `camelCase` para las funciones).

**EN:** The specification expects `src/data_structures_basics.ext`, `test/data_structures_basics_test.ext` and `test/run_tests.ext`. Here the module is a library project (`DataStructuresBasics.fsproj`) with its like-named source, the tests are a separate xunit project and the runner is `dotnet test`: there is no `run_tests` because xunit discovers the tests. Names follow the .NET convention (`PascalCase` for files and projects, `camelCase` for functions).

---

### 🌐 Otras implementaciones / Other implementations

Este proyecto también está implementado en otros lenguajes. Explora el [repositorio principal](https://github.com/yorche3/programming_languages) para ver todas las versiones.

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
| Especificación / Specification | [`06_Data_Structures_Basics.md`](https://yorche3.github.io/programming_languages/core/algorithms/06_Data_Structures_Basics/) |
| Acta de evidencia / Evidence record | [`docs/evidence/algorithms/data_structures_basics/fsharp.md`](https://github.com/yorche3/programming_languages/blob/main/docs/evidence/algorithms/data_structures_basics/fsharp.md) |
| Módulo homologado del lenguaje / Homologated module | [`../naive_sort/README.md`](../naive_sort/README.md) |
| Guía de inicialización / Initialisation guide | [`core/00_Project_Initialization_Guide.md`](https://yorche3.github.io/programming_languages/core/00_Project_Initialization_Guide/) |
| Adaptaciones idiomáticas / Idiomatic adaptations | [`AGENT_Template.md`](https://yorche3.github.io/programming_languages/AGENT_Template/) |
| Validación de la documentación / Documentation validation | [`WORKFLOW.md`](https://yorche3.github.io/programming_languages/WORKFLOW/) |
| Plantilla del README / README template | [`README_Template.md`](https://yorche3.github.io/programming_languages/README_Template/) |
| Documentación oficial del lenguaje / Language official docs | [F# Documentation](https://learn.microsoft.com/dotnet/fsharp/) · [xunit](https://xunit.net/) |

---

*[← Volver a Algorithms Pure](../README.md) | [↑ Volver a F# Core](../../README.md)*

*🌐 [github.com/yorche3/programming_languages](https://github.com/yorche3/programming_languages) · [GitHub Pages](https://yorche3.github.io/programming_languages/)*
