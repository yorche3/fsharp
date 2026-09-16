# Naive Sort — F#

Implementación de la especificación [05_Naive_Sort](https://yorche3.github.io/programming_languages/core/algorithms/05_Naive_Sort/) en **F# (.NET 10)**, usando **xUnit** como framework de pruebas unitarias.

Implementa los tres algoritmos elementales de ordenamiento ($O(n^2)$) — **Selection Sort**, **Bubble Sort** e **Insertion Sort** — con recursión y pattern matching, sin invocar `List.sort` ni ninguna rutina de ordenamiento del sistema.

---

## 📂 Archivos y estructura / Files & Structure

### Raíz del proyecto / Project root

| Archivo | Propósito |
|---------|-----------|
| [`NaiveSort.slnx`](NaiveSort.slnx) | Archivo de solución .NET — referencia los proyectos `src/` y `test/`. |

### Código fuente / Source (`src/`)

| Archivo | Propósito |
|---------|-----------|
| [`src/NaiveSort.fs`](src/NaiveSort.fs) | Módulo `NaiveSort` — las 3 funciones del contrato, cada una con su helper local. |
| [`src/NaiveSort.fsproj`](src/NaiveSort.fsproj) | Proyecto de biblioteca de clases — target `net10.0`. |

### Pruebas / Tests (`test/`)

| Archivo | Propósito |
|---------|-----------|
| [`test/NaiveSortTests.fs`](test/NaiveSortTests.fs) | 3 tests (7 casos cada uno = 21 aserciones). |
| [`test/NaiveSort.Tests.fsproj`](test/NaiveSort.Tests.fsproj) | Proyecto de tests — referencia `src/NaiveSort.fsproj` + paquetes NuGet (xUnit, coverlet). |

**Estructura de directorios esperada:**

```text
naive_sort/
├── NaiveSort.slnx              # Solución .NET
├── src/
│   ├── NaiveSort.fs            # Módulo con las 3 funciones del contrato
│   └── NaiveSort.fsproj        # Proyecto de biblioteca
├── test/
│   ├── NaiveSortTests.fs       # 3 tests (7 casos × 3 algoritmos)
│   └── NaiveSort.Tests.fsproj  # Proyecto de tests
├── .gitignore                  # (en la raíz del submódulo) ignora bin/, obj/
└── README.md                   # Este archivo
```

---

## 🛠️ Enfoque y construcción / Approach & Build

**ES:** Sigue el mismo patrón que [`numbers`](../foundations/numbers/): una solución `.slnx` que agrupa un proyecto de biblioteca (`src/`) y uno de tests (`test/`) con xUnit. El proyecto se creó con las plantillas del SDK:

```bash
dotnet new classlib -lang F# -n NaiveSort -o src
dotnet new xunit    -lang F# -n NaiveSort.Tests -o test
dotnet new sln -n NaiveSort --format slnx
dotnet sln NaiveSort.slnx add src/NaiveSort.fsproj test/NaiveSort.Tests.fsproj
```

**EN:** Follows the same pattern as [`numbers`](../foundations/numbers/): a `.slnx` solution grouping a library project (`src/`) and a test project (`test/`) with xUnit. The project was created with the SDK templates:

A diferencia de `numbers/`, donde los helpers con acumulador son funciones públicas, aquí cada algoritmo encapsula su helper como **función local** (`pickMin`, `bubblePass`, `insert`), de modo que la única API son las tres funciones del contrato.

**EN:** Unlike `numbers/`, where accumulator helpers are public functions, here each algorithm encapsulates its helper as a **local function** (`pickMin`, `bubblePass`, `insert`), so the only API is the three contract functions.

---

## 📄 Archivos de configuración clave / Key Configuration Files

### `src/NaiveSort.fsproj` — Proyecto de biblioteca

**ES:** Proyecto de class library de F# con documentación XML habilitada y un único archivo de compilación.

**EN:** F# class library project with XML documentation enabled and a single compilation file.

```xml
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <TargetFramework>net10.0</TargetFramework>
    <GenerateDocumentationFile>true</GenerateDocumentationFile>
  </PropertyGroup>

  <ItemGroup>
    <Compile Include="NaiveSort.fs" />
  </ItemGroup>

</Project>
```

### `src/NaiveSort.fs` — Implementación

**ES:** F# usa `let rec` para funciones recursivas y `match` para el pattern matching. Las listas de F# son inmutables, así que cada función devuelve una lista nueva en lugar de ordenar *in-place*.

**EN:** F# uses `let rec` for recursive functions and `match` for pattern matching. F# lists are immutable, so each function returns a new list instead of sorting in place.

```fsharp
module NaiveSort

let rec selectionSort arr =
    let rec pickMin minValue acc rest = ...
    match arr with
    | [] -> []
    | [x] -> [x]
    | first::rest ->
        let minValue, restWithoutMin = pickMin first [] rest
        minValue :: selectionSort restWithoutMin

let rec bubbleSort arr =
    let rec bubblePass acc swapped rest = ...
    let rec sort arr =
        let result, swapped = bubblePass [] false arr
        if swapped then sort result else result
    sort arr

let rec insertionSort arr =
    let rec insert x sorted = ...
    match arr with
    | [] -> []
    | first::rest -> insert first (insertionSort rest)
```

### `test/NaiveSortTests.fs` — Pruebas

**ES:** Los siete casos se declaran como valores con nombre (`standardInput`, `reverseOutput`, …) y un único helper `assertSortsAllCases` genera una aserción por caso para cualquier algoritmo. El mensaje se construye con interpolación de cadenas.

**EN:** The seven cases are declared as named values (`standardInput`, `reverseOutput`, …) and a single `assertSortsAllCases` helper generates one assertion per case for any algorithm. The message is built with string interpolation.

```fsharp
let assertSortsAllCases (sortFunction: int list -> int list) (algorithm: string) =
    for (description, input, expected) in cases do
        Assert.True((sortFunction input = expected), $"{algorithm} should sort {description}")

[<Fact>]
let ``Selection sort`` () =
    assertSortsAllCases NaiveSort.selectionSort "selection_sort"
```

---

## 🚀 Compilación y ejecución / Build & Run

### Requisito: .NET SDK (incluye F#)

```bash
dotnet --version
```

### Compilar / Build

```bash
cd core/algorithms/naive_sort
dotnet build NaiveSort.slnx
```

**Salida real / Actual output:**

```text
  NaiveSort -> /home/yorche3/programming_languages/fsharp/core/algorithms/naive_sort/src/bin/Debug/net10.0/NaiveSort.dll
  NaiveSort.Tests -> /home/yorche3/programming_languages/fsharp/core/algorithms/naive_sort/test/bin/Debug/net10.0/NaiveSort.Tests.dll

Build succeeded.
    0 Warning(s)
    0 Error(s)

Time Elapsed 00:00:00.94
```

### Ejecutar pruebas / Run tests

```bash
dotnet test NaiveSort.slnx
```

**Salida real / Actual output:**

```text
  Determining projects to restore...
  All projects are up-to-date for restore.
  NaiveSort -> /home/yorche3/programming_languages/fsharp/core/algorithms/naive_sort/src/bin/Debug/net10.0/NaiveSort.dll
  NaiveSort.Tests -> /home/yorche3/programming_languages/fsharp/core/algorithms/naive_sort/test/bin/Debug/net10.0/NaiveSort.Tests.dll
Test run for /home/yorche3/programming_languages/fsharp/core/algorithms/naive_sort/test/bin/Debug/net10.0/NaiveSort.Tests.dll (.NETCoreApp,Version=v10.0)
VSTest version 18.0.2 (x64)

Starting test execution, please wait...
A total of 1 test files matched the specified pattern.

Passed!  - Failed:     0, Passed:     3, Skipped:     0, Total:     3, Duration: 21 ms - NaiveSort.Tests.dll (net10.0)
```

---

## 🧠 Algoritmos / operaciones

| Función | Estrategia | Complejidad | In-place | Tests |
|---------|-----------|-------------|:--------:|:-----:|
| `selectionSort` | El helper local `pickMin` devuelve el mínimo y el resto; el mínimo se antepone al resultado de ordenar el resto | $O(n^2)$ siempre | ❌ (lista nueva) | 7 |
| `bubbleSort` | `bubblePass` hace una pasada y devuelve `(lista, swapped)`; `sort` repite mientras haya intercambios | $O(n^2)$ peor/promedio, $O(n)$ mejor | ❌ (lista nueva) | 7 |
| `insertionSort` | Ordena la cola y el helper local `insert` coloca la cabeza en su sitio | $O(n^2)$ peor/promedio, $O(n)$ mejor | ❌ (lista nueva) | 7 |

| Helper local | Papel |
|--------------|-------|
| `pickMin` | Recorre la lista acumulando el mínimo y el resto de elementos. |
| `bubblePass` | Una pasada de burbuja; devuelve la lista y la bandera `swapped`. |
| `insert` | Inserta un entero en una lista ya ordenada. |

**Casos cubiertos:** lista desordenada, ya ordenada, en orden inverso, elementos idénticos, con negativos, un solo elemento y lista vacía.

---

## 📝 Notas de implementación / Implementation Notes

### 🔁 Divergencias idiomáticas respecto al pseudocódigo / Idiomatic divergences from the pseudocode

| Pseudocódigo | F# | Motivo / Reason |
|--------------|-----|-----------------|
| `swap(arr, i, min_idx)` *in-place* | Se construye una lista nueva | Las listas de F# son inmutables / F# lists are immutable |
| Bucles `for` / `while` | Recursión con `match` | F# favorece la recursión y el pattern matching / F# favours recursion and pattern matching |
| `swapped = false` mutado durante la pasada | `bubblePass` devuelve `(int list * bool)` | Equivalente funcional de la bandera; conserva la salida temprana / Functional equivalent of the flag |
| `if n <= 1` | Cláusulas `[]` y `[x]` | Pattern matching idiomático / Idiomatic pattern matching |
| `selection_sort(arr)` | `selectionSort` | F# usa `camelCase` para funciones / F# uses `camelCase` for functions |
| Helpers en el ámbito del módulo | Helpers como funciones **locales** | Encapsulación: la única API son las 3 funciones del contrato / Only the 3 contract functions are API |

**ES:** En `insertionSort`, `insert` usa `x <= y` para insertar antes de un elemento igual, de modo que el algoritmo es **estable**; el pseudocódigo usa `arr[j] > key`, equivalente.

**EN:** In `insertionSort`, `insert` uses `x <= y` to insert before an equal element, making the algorithm **stable**; the pseudocode uses `arr[j] > key`, which is equivalent.

### 🚫 Caso nulo / Null case

**ES:** El caso nulo de la especificación **se omite** porque F# no permite el literal `null` para `int list` y no existe representación de lista inválida: toda lista es ordenable. F# tampoco lanza excepciones aquí, así que el criterio «sin lanzar excepciones» se cumple de forma trivial. La justificación está documentada en `src/NaiveSort.fs` y en `test/NaiveSortTests.fs`.

**EN:** The specification's null case **is omitted** because F# does not allow the `null` literal for `int list` and there is no invalid-list representation: every list is sortable. F# throws no exceptions here either, so the «without throwing exceptions» criterion holds trivially. The rationale is documented in `src/NaiveSort.fs` and `test/NaiveSortTests.fs`.

> **ES:** El hermano de C# sí modela el indicador de fallo (`int[]?` con `null -> null`) porque tiene los *nullable reference types* habilitados; F# es *null-safe* por diseño, así que la representación equivalente es la ausencia de caso inválido.
> **EN:** The C# sibling does model the failure indicator (`int[]?` with `null -> null`) because it has nullable reference types enabled; F# is null-safe by design, so the equivalent representation is the absence of an invalid case.

### 📁 Desviación de ubicación y nombres / Location and naming deviation

**ES:** La especificación espera `src/naive_sort.ext` y `test/naive_sort_test.ext`. En F# el archivo del módulo toma el nombre del módulo (`NaiveSort.fs`, `PascalCase`) y el archivo de tests sigue la convención de `numbers/` (`NaiveSortTests.fs`). No hay `run_tests.ext`: `dotnet test` descubre y ejecuta los tests de la solución. La separación `src/` ↔ `test/` sí se respeta, y ambos proyectos van en la misma solución `.slnx`.

**EN:** The specification expects `src/naive_sort.ext` and `test/naive_sort_test.ext`. In F# the module file takes the module name (`NaiveSort.fs`, `PascalCase`) and the test file follows the `numbers/` convention (`NaiveSortTests.fs`). There is no `run_tests.ext`: `dotnet test` discovers and runs the solution tests. The `src/` ↔ `test/` separation is preserved, and both projects live in the same `.slnx` solution.

---

### 🌐 Otras implementaciones / Other implementations

Este proyecto también está implementado en otros lenguajes. Explora el [repositorio principal](https://github.com/yorche3/programming_languages) para ver todas las versiones.

---

*[← Volver a Algorithms Pure](README.md) | [↑ Volver a F# Core](../../README.md)*

*🌐 [github.com/yorche3/programming_languages](https://github.com/yorche3/programming_languages) · [GitHub Pages](https://yorche3.github.io/programming_languages/)*
