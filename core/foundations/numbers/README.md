# Numbers — F#

Implementación de la especificación [04_Numbers](https://yorche3.github.io/programming_languages/core/foundations/04_Numbers/) en **F# (.NET 10)**, usando **xUnit** como framework de pruebas unitarias.

Tres enfoques de implementación para los mismos 5 algoritmos: **recursivo directo**, **recursivo con acumulador** e **iterativo** (con funciones locales recursivas).

---

## 📂 Archivos y estructura / Files & Structure

### Raíz del proyecto / Project root

| Archivo | Propósito |
|---------|-----------|
| [`numbers.slnx`](numbers.slnx) | Archivo de solución .NET — referencia los proyectos `src/` y `test/`. |

### Código fuente / Source (`src/`)

| Archivo | Propósito |
|---------|-----------|
| [`src/Numbers.fs`](src/Numbers.fs) | Módulo `Numbers` — 15 funciones (3 enfoques × 5 algoritmos). |
| [`src/Numbers.fsproj`](src/Numbers.fsproj) | Proyecto de biblioteca de clases — target `net10.0`. |

### Pruebas / Tests (`test/`)

| Archivo | Propósito |
|---------|-----------|
| [`test/NumbersRecTests.fs`](test/NumbersRecTests.fs) | 5 tests para el enfoque recursivo directo |
| [`test/NumbersAccTests.fs`](test/NumbersAccTests.fs) | 5 tests para el enfoque con acumulador |
| [`test/NumbersIteTests.fs`](test/NumbersIteTests.fs) | 5 tests para el enfoque iterativo |
| [`test/Numbers.Tests.fsproj`](test/Numbers.Tests.fsproj) | Proyecto de tests — referencia `src/Numbers.fsproj` + paquetes NuGet (xUnit, coverlet). |

**Estructura de directorios esperada:**

```text
numbers/
├── numbers.slnx              # Solución .NET
├── src/
│   ├── Numbers.fs            # Módulo con 15 funciones (3 enfoques × 5 algoritmos)
│   └── Numbers.fsproj        # Proyecto de biblioteca
├── test/
│   ├── NumbersRecTests.fs    # Tests recursivos (5)
│   ├── NumbersAccTests.fs    # Tests con acumulador (5)
│   ├── NumbersIteTests.fs    # Tests iterativos (5)
│   └── Numbers.Tests.fsproj  # Proyecto de tests
├── .gitignore                # Ignora bin/, obj/
└── README.md                 # Este archivo
```

---

## 🛠️ Enfoque y construcción / Approach & Build

**ES:** Sigue el mismo patrón que [`calculator`](../unit_test/calculator/): una solución `.slnx` que agrupa un proyecto de biblioteca (`src/`) y uno de tests (`test/`) con xUnit.

Las 15 funciones se organizan en 3 grupos por enfoque:

| Enfoque | Prefijo | Ejemplo | Tests |
|---------|---------|---------|:-----:|
| Recursivo directo | `...Rec` | `fibonacciRec n` | ✅ |
| Recursivo con acumulador | `...Acc` | `fibonacciAcc n` | ✅ |
| Iterativo (función local `loop`) | `...Ite` | `fibonacciIte n` | ✅ |

**EN:** Follows the same pattern as [`calculator`](../unit_test/calculator/): a `.slnx` solution grouping a library project (`src/`) and a test project (`test/`) with xUnit.

The 15 functions are organized into 3 groups by approach:

| Approach | Prefix | Example | Tests |
|----------|--------|---------|:-----:|
| Direct recursion | `...Rec` | `fibonacciRec n` | ✅ |
| Accumulator recursion | `...Acc` | `fibonacciAcc n` | ✅ |
| Iterative (local `loop` function) | `...Ite` | `fibonacciIte n` | ✅ |

> **ES:** A diferencia de C#, en F# **los tres enfoques tienen tests directos**. Esto es porque en F# los acumuladores son funciones públicas (no helpers privados), y además F# sí garantiza **Tail Call Optimization (TCO)**, haciendo que la versión con acumulador sea equivalente en eficiencia a la iterativa.
> 
> **EN:** Unlike C#, in F# **all three approaches have direct tests**. This is because in F# accumulators are public functions (not private helpers), and F# does guarantee **Tail Call Optimization (TCO)**, making the accumulator version equivalent in efficiency to the iterative one.

---

## 📄 Archivos de configuración clave / Key Configuration Files

### `src/Numbers.fs` — Implementación

**ES:** Cada algoritmo tiene 3 implementaciones. F# usa `let rec` para funciones recursivas, `match` para pattern matching, y funciones anidadas con `let rec loop` para el estilo iterativo.

**EN:** Each algorithm has 3 implementations. F# uses `let rec` for recursive functions, `match` for pattern matching, and nested `let rec loop` functions for the iterative style.

```fsharp
module Numbers

// ─── Recursivo directo / Direct recursion ─────────────────────────────────

let rec sumFirstNRec n =
    match n with
    | n when n <= 0 -> 0
    | _ -> n + sumFirstNRec (n - 1)

let rec factorialRec n =
    match n with
    | n when n <= 1 -> 1
    | _ -> n * factorialRec (n - 1)

let rec fibonacciRec n =
    match n with
    | n when n <= 0 -> 0
    | 1 -> 1
    | _ -> fibonacciRec (n - 1) + fibonacciRec (n - 2)

let rec greatestCommonDivisorRec a b =
    match b with
    | 0 -> a
    | _ -> greatestCommonDivisorRec b (a % b)

let leastCommonMultipleRec a b =
    let gcd = greatestCommonDivisorRec a b
    (a * b) / gcd

// ─── Recursivo con acumulador / Accumulator recursion ─────────────────────

let rec sumFirstNHelp i acc =
    match i with
    | i when i <= 0 -> acc
    | _ -> sumFirstNHelp (i - 1) (acc + i)

let sumFirstNAcc n = sumFirstNHelp n 0

let rec factorialHelp i acc =
    match i with
    | i when i <= 1 -> acc
    | _ -> factorialHelp (i - 1) (acc * i)

let factorialAcc n = factorialHelp n 1

let rec fibonacciHelp i acc1 acc2 =
    match i with
    | i when i <= 0 -> acc1
    | _ -> fibonacciHelp (i - 1) acc2 (acc1 + acc2)

let fibonacciAcc n = fibonacciHelp n 0 1

// ... (GCD y LCM con acumulador — usan el mismo patrón)

// ─── Iterativo (función local loop) / Iterative (local loop function) ─────

let sumFirstNIte n =
    let rec loop i acc =
        match i with
        | i when i <= 0 -> acc
        | _ -> loop (i - 1) (acc + i)
    loop n 0

let factorialIte n =
    let rec loop i acc =
        match i with
        | i when i <= 1 -> acc
        | _ -> loop (i - 1) (acc * i)
    loop n 1

let fibonacciIte n =
    let rec loop i acc1 acc2 =
        match i with
        | i when i <= 0 -> acc1
        | _ -> loop (i - 1) acc2 (acc1 + acc2)
    loop n 0 1

// ... (GCD y LCM iterativos — usan el mismo patrón con loop local)
```

#### Diferencias clave entre los enfoques / Key differences between approaches

| Aspecto | `Rec` | `Acc` | `Ite` |
|---------|:-----:|:-----:|:-----:|
| Llamadas recursivas | Múltiples (ej: Fibonacci O(2ⁿ)) | Una sola (tail call) | Una sola (tail call) |
| `let rec` | Directo en la función | En el helper interno | En `loop` local |
| TCO aplicable | ❌ No (trabajo pendiente) | ✅ Sí | ✅ Sí |
| Legibilidad | Alta (matemática) | Media | Media |
| Eficiencia | Baja para Fibonacci | O(n) | O(n) |

> **ES:** En F# la **Tail Call Optimization (TCO)** está garantizada por el compilador. Cuando una función termina con una llamada recursiva directa, el compilador la optimiza a un salto, sin crecer la pila de llamadas. Esto hace que los enfoques `Acc` e `Ite` sean equivalentes en rendimiento y seguros para cualquier valor de `n`.
> 
> **EN:** In F#, **Tail Call Optimization (TCO)** is guaranteed by the compiler. When a function ends with a direct recursive call, the compiler optimizes it to a jump, without growing the call stack. This makes the `Acc` and `Ite` approaches equivalent in performance and safe for any value of `n`.

### `test/NumbersRecTests.fs` — Pruebas recursivas

**ES:** Cada `[<Fact>]` prueba una operación del enfoque recursivo directo.

**EN:** Each `[<Fact>]` tests one operation of the direct recursive approach.

```fsharp
module NumbersRecTests

open Xunit

[<Fact>]
let ``Sum of first N numbers using recursion`` () =
    Assert.Equal(0, Numbers.sumFirstNRec 0)
    Assert.Equal(6, Numbers.sumFirstNRec 3)

[<Fact>]
let ``Fibonacci sequence using recursion`` () =
    Assert.Equal(0, Numbers.fibonacciRec 0)
    Assert.Equal(1, Numbers.fibonacciRec 1)
    Assert.Equal(8, Numbers.fibonacciRec 6)

[<Fact>]
let ``Greatest Common Divisor (GCD) using recursion`` () =
    Assert.Equal(4, Numbers.greatestCommonDivisorRec 12 8)
    Assert.Equal(1, Numbers.greatestCommonDivisorRec 7 5)
```

### `test/NumbersAccTests.fs` — Pruebas con acumulador

**ES:** Cada `[<Fact>]` prueba una operación del enfoque con acumulador.

**EN:** Each `[<Fact>]` tests one operation of the accumulator approach.

```fsharp
module NumbersAccTests

open Xunit

[<Fact>]
let ``Sum of first N numbers using accumulator`` () =
    Assert.Equal(0, Numbers.sumFirstNAcc 0)
    Assert.Equal(6, Numbers.sumFirstNAcc 3)

[<Fact>]
let ``Fibonacci sequence using accumulator`` () =
    Assert.Equal(0, Numbers.fibonacciAcc 0)
    Assert.Equal(1, Numbers.fibonacciAcc 1)
    Assert.Equal(8, Numbers.fibonacciAcc 6)
```

### `test/NumbersIteTests.fs` — Pruebas iterativas

**ES:** Cada `[<Fact>]` prueba una operación del enfoque iterativo (función local `loop`).

**EN:** Each `[<Fact>]` tests one operation of the iterative approach (local `loop` function).

```fsharp
module NumbersIteTests

open Xunit

[<Fact>]
let ``Sum of first N numbers using iteration`` () =
    Assert.Equal(0, Numbers.sumFirstNIte 0)
    Assert.Equal(6, Numbers.sumFirstNIte 3)

[<Fact>]
let ``Fibonacci sequence using iteration`` () =
    Assert.Equal(0, Numbers.fibonacciIte 0)
    Assert.Equal(1, Numbers.fibonacciIte 1)
    Assert.Equal(8, Numbers.fibonacciIte 6)
```

---

## 🚀 Compilación y ejecución / Build & Run

### Requisito: Tener el .NET SDK instalado

```bash
dotnet --version   # Debería mostrar: 10.0.x
```

### Ejecutar pruebas unitarias

```bash
# Desde la raíz del proyecto (usa la solución)
dotnet test numbers.slnx

# O directamente desde el proyecto de tests
dotnet test test/Numbers.Tests.fsproj
```

**Salida esperada / Expected output:**

```text
Restored .../numbers.slnx
Restored .../src/Numbers.fsproj (0.1s)
Restored .../test/Numbers.Tests.fsproj (0.1s)
  Numbers succeeded (0.3s) → src/bin/Debug/net10.0/Numbers.dll
  Numbers.Tests succeeded (0.5s) → test/bin/Debug/net10.0/Numbers.Tests.dll
[xUnit] Running 15 test(s) from Numbers.Tests
[xUnit]   Sum of first N numbers using recursion        ✓ (0.1s)
[xUnit]   Factorial of a number using recursion          ✓ (0.1s)
[xUnit]   Fibonacci sequence using recursion             ✓ (0.1s)
[xUnit]   Greatest Common Divisor (GCD) using recursion  ✓ (0.1s)
[xUnit]   Least Common Multiple (LCM) using recursion    ✓ (0.1s)
[xUnit]   Sum of first N numbers using accumulator       ✓ (0.1s)
[xUnit]   Factorial of a number using accumulator         ✓ (0.1s)
[xUnit]   Fibonacci sequence using accumulator            ✓ (0.1s)
[xUnit]   Greatest Common Divisor (GCD) using accumulator ✓ (0.1s)
[xUnit]   Least Common Multiple (LCM) using accumulator   ✓ (0.1s)
[xUnit]   Sum of first N numbers using iteration          ✓ (0.1s)
[xUnit]   Factorial of a number using iteration            ✓ (0.1s)
[xUnit]   Fibonacci sequence using iteration               ✓ (0.1s)
[xUnit]   Greatest Common Divisor (GCD) using iteration    ✓ (0.1s)
[xUnit]   Least Common Multiple (LCM) using iteration      ✓ (0.1s)
  Numbers.Tests test succeeded (1.2s)

Test summary: 15 passed, 0 failed — 100% coverage
```

> **ES:** 15 tests en total (5 recursivos + 5 acumulador + 5 iterativos). Todos deben pasar.
> **EN:** 15 tests total (5 recursive + 5 accumulator + 5 iterative). All must pass.

---

## 🧠 Algoritmos / operaciones (según el módulo)

### 3 enfoques × 5 algoritmos = 15 funciones / 15 tests

| Algoritmo | Casos de prueba | `Rec` | `Acc` | `Ite` |
|-----------|----------------|:-----:|:-----:|:-----:|
| `sumFirstN` | `(0) = 0`, `(3) = 6` | ✅ | ✅ | ✅ |
| `factorial` | `(0) = 1`, `(4) = 24` | ✅ | ✅ | ✅ |
| `fibonacci` | `(0) = 0`, `(1) = 1`, `(6) = 8` | ✅ | ✅ | ✅ |
| `greatestCommonDivisor` | `(12, 8) = 4`, `(7, 5) = 1` | ✅ | ✅ | ✅ |
| `leastCommonMultiple` | `(6, 8) = 24`, `(6, 4) = 12` | ✅ | ✅ | ✅ |

### Detalle por algoritmo

#### `sumFirstN` — Suma de los primeros N números

| `n` | Esperado | `Rec` | `Acc` | `Ite` |
|:---:|:--------:|:-----:|:-----:|:-----:|
| 0 | 0 | `match 0 ≤ 0 → 0` | `help 0 0 → 0` | `loop 0 0 → 0` |
| 3 | 6 | `3+2+1+0 = 6` | `help 3 0 → help 2 3 → help 1 5 → help 0 6 → 6` | `loop 3 0 → loop 2 3 → loop 1 5 → loop 0 6 → 6` |

#### `factorial` — Factorial de un número

| `n` | Esperado | `Rec` | `Acc` | `Ite` |
|:---:|:--------:|:-----:|:-----:|:-----:|
| 0 | 1 | `match 0 ≤ 1 → 1` | `help 0 1 → 1` | `loop 0 1 → 1` |
| 4 | 24 | `4×3×2×1 = 24` | `help 4 1 → help 3 4 → help 2 12 → help 1 24 → 24` | `loop 4 1 → loop 3 4 → loop 2 12 → loop 1 24 → 24` |

#### `fibonacci` — Secuencia de Fibonacci

| `n` | Esperado | `Rec` (llamadas) | `Acc` / `Ite` (iteraciones) |
|:---:|:--------:|:-----------------:|:---------------------------:|
| 0 | 0 | 1 llamada | 0 iteraciones |
| 1 | 1 | 1 llamada | 1 iteración |
| 6 | 8 | 25 llamadas | 6 iteraciones |
| 10 | 55 | 177 llamadas | 10 iteraciones |
| 30 | 832040 | 2.69M llamadas | 30 iteraciones |

> **ES:** La diferencia es dramática. `fibonacciRec 30` hace ~2.7 millones de llamadas recursivas, mientras que `fibonacciAcc 30` y `fibonacciIte 30` solo iteran 30 veces. Además, sin TCO, `fibonacciRec` desborda la pila para valores como 35+.
> 
> **EN:** The difference is dramatic. `fibonacciRec 30` makes ~2.7 million recursive calls, while `fibonacciAcc 30` and `fibonacciIte 30` only iterate 30 times. Also, without TCO, `fibonacciRec` overflows the stack for values like 35+.

#### `greatestCommonDivisor` — Máximo común divisor (GCD)

| `(a, b)` | Esperado | Algoritmo |
|:--------:|:--------:|-----------|
| `(12, 8)` | 4 | `gcd 12 8 → gcd 8 4 → gcd 4 0 → 4` |
| `(7, 5)` | 1 | `gcd 7 5 → gcd 5 2 → gcd 2 1 → gcd 1 0 → 1` |

> **ES:** Usa el **algoritmo de Euclides**: `gcd(a, b) = gcd(b, a % b)`. Todas las versiones son idénticas porque el algoritmo ya es tail-recursive por naturaleza.
> 
> **EN:** Uses the **Euclidean algorithm**: `gcd(a, b) = gcd(b, a % b)`. All versions are identical because the algorithm is already tail-recursive by nature.

#### `leastCommonMultiple` — Mínimo común múltiplo (LCM)

| `(a, b)` | Esperado | Fórmula |
|:--------:|:--------:|---------|
| `(6, 8)` | 24 | `(6 × 8) / gcd(6, 8) = 48 / 2 = 24` |
| `(6, 4)` | 12 | `(6 × 4) / gcd(6, 4) = 24 / 2 = 12` |

> **ES:** `lcm(a, b) = (a × b) / gcd(a, b)`. No tiene versión recursiva/iterativa propia porque delega en `gcd`.
> 
> **EN:** `lcm(a, b) = (a × b) / gcd(a, b)`. It doesn't have its own recursive/iterative version because it delegates to `gcd`.

---

## 📝 Notas de implementación / Implementation Notes

### 🔁 Sobre Tail Call Optimization (TCO) en F#

**ES:**

A diferencia de C#, **F# garantiza TCO** a nivel de compilador (IL). Cuando una función termina con una llamada recursiva directa (tail call), el compilador de F# genera código IL con `tail.` prefix, que el JIT de .NET respeta. Esto significa:

- Las funciones `Acc` e `Ite` son completamente seguras para cualquier valor de `n`.
- No hay riesgo de `StackOverflowException`.
- La versión con acumulador es equivalente en rendimiento a la iterativa.
- Incluso `greatestCommonDivisor` (que es tail-recursive desde el inicio) se beneficia de TCO.

**¿Por qué tener 3 enfoques entonces?**

| Enfoque | Propósito educativo |
|---------|-------------------|
| `Rec` | Más cercano a la definición matemática. Fácil de entender y verificar. |
| `Acc` | Muestra cómo transformar recursión directa a tail recursion con acumulador. |
| `Ite` | Encierra el acumulador en una función local `loop`, equivalente a un `for`/`while`. |

**EN:**

Unlike C#, **F# guarantees TCO** at the compiler level (IL). When a function ends with a direct recursive call (tail call), the F# compiler generates IL code with a `tail.` prefix, which the .NET JIT respects. This means:

- The `Acc` and `Ite` functions are completely safe for any value of `n`.
- No risk of `StackOverflowException`.
- The accumulator version is equivalent in performance to the iterative one.
- Even `greatestCommonDivisor` (which is tail-recursive from the start) benefits from TCO.

**Why have 3 approaches then?**

| Approach | Educational purpose |
|----------|-------------------|
| `Rec` | Closest to the mathematical definition. Easy to understand and verify. |
| `Acc` | Shows how to transform direct recursion to tail recursion with accumulator. |
| `Ite` | Encapsulates the accumulator in a local `loop` function, equivalent to a `for`/`while`. |

---

### ⚠️ Atención con `fibonacciRec`

**ES:** La versión recursiva directa de Fibonacci tiene complejidad **O(2ⁿ)**. Para `n = 35`, hace ~18.5 millones de llamadas y puede tardar varios segundos. Para `n = 40`, hace ~165 millones. Se recomienda **no ejecutar** `fibonacciRec` con `n > 35`.

**EN:** The direct recursive version of Fibonacci has **O(2ⁿ)** complexity. For `n = 35`, it makes ~18.5 million recursive calls and can take several seconds. For `n = 40`, it makes ~165 million. It is recommended **not to run** `fibonacciRec` with `n > 35`.

---

### Diferencias clave con la versión de C#

| Aspecto | C# | F# |
|---------|----|----|
| TCO | No garantizado | ✅ Garantizado |
| Helpers `Acc` | `private static` (ocultos) | Funciones públicas (con tests) |
| Tests para `Acc` | ❌ No (implícitos) | ✅ Sí (directos) |
| Enfoque iterativo | `for`/`while` | Función local `loop` con recursión |
| Pattern matching | `if`/`else` / `switch` | `match ... with` |
| Funciones recursivas | `static int F(int n)` | `let rec f n =` |

---

### 🌐 Otras implementaciones / Other implementations

Este proyecto también está implementado en otros lenguajes. Explora el [repositorio principal](https://github.com/yorche3/programming_languages) para ver todas las versiones.

---

*🌐 [github.com/yorche3/programming_languages](https://github.com/yorche3/programming_languages) · [GitHub Pages](https://yorche3.github.io/programming_languages/)*
