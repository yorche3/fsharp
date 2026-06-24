# Calculator — F#

Implementación de la especificación [03_Unit_Test_Calculator](https://yorche3.github.io/programming_languages/core/foundations/03_Unit_Test_Calculator/) en **F# (.NET 10)**, usando **xUnit** como framework de pruebas unitarias.

Calculadora aritmética implementada con sumas y restas repetitivas (sin usar `*`, `/` ni `%`).

---

## 📂 Archivos y estructura / Files & Structure

| Archivo | Propósito |
|---------|-----------|
| [`calculator.slnx`](calculator.slnx) | Archivo de solución .NET — referencia los proyectos `src/` y `test/`. |

### Código fuente / Source (`src/`)

| Archivo | Propósito |
|---------|-----------|
| [`src/Library.fs`](src/Library.fs) | Módulo `Calculator` — implementa las 5 operaciones aritméticas. |
| [`src/Calculator.fsproj`](src/Calculator.fsproj) | Proyecto de biblioteca de clases — target `net10.0`. |

### Pruebas / Tests (`test/`)

| Archivo | Propósito |
|---------|-----------|
| [`test/Tests.fs`](test/Tests.fs) | Pruebas unitarias — 5 tests con atributos `[<Fact>]` de xUnit. |
| [`test/Calculator.Tests.fsproj`](test/Calculator.Tests.fsproj) | Proyecto de tests — referencia `src/Calculator.fsproj` y paquetes NuGet (xUnit, coverlet). |

**Estructura de directorios esperada:**

```text
calculator/
├── calculator.slnx            # Solución .NET
├── src/
│   ├── Library.fs             # Módulo con 5 operaciones aritméticas
│   └── Calculator.fsproj      # Proyecto de biblioteca
├── test/
│   ├── Tests.fs               # Tests unitarios (5 tests)
│   └── Calculator.Tests.fsproj # Proyecto de tests
├── .gitignore                 # Ignora bin/, obj/
└── README.md                  # Este archivo
```

---

## 🛠️ Enfoque y construcción / Approach & Build

**ES:** Este proyecto usa **xUnit**, el framework de pruebas unitarias moderno para .NET, adaptado a F#:

1. Los tests se marcan con el atributo `[<Fact>]` (entre `[< >]` en F#).
2. Las aserciones usan `Assert.Equal(expected, actual)`.
3. El descubrimiento de tests es automático: `dotnet test` encuentra todos los `[<Fact>]`.
4. Se usa una solución `.slnx` para agrupar los proyectos de biblioteca y tests.
5. Se usa **F#** funcional: módulos en lugar de clases, funciones en lugar de métodos, inmutabilidad por defecto.

**EN:** This project uses **xUnit**, the modern unit testing framework for .NET, adapted for F#:

1. Tests are marked with the `[<Fact>]` attribute (using `[< >]` in F#).
2. Assertions use `Assert.Equal(expected, actual)`.
3. Test discovery is automatic: `dotnet test` finds all `[<Fact>]` methods.
4. A `.slnx` solution groups the library and test projects.
5. Uses **functional F#**: modules instead of classes, functions instead of methods, immutability by default.

### Estructura de la solución / Solution structure

```text
calculator.slnx
├── src/Calculator.fsproj          # Biblioteca de clases
└── test/Calculator.Tests.fsproj   # Proyecto de tests (referencia a src/)
```

---

## 📄 Archivos de configuración clave / Key Configuration Files

### `src/Library.fs` — Módulo principal

**ES:** Define el módulo `Calculator` con las 5 operaciones aritméticas. `multiplication` y `division` usan sumas/restas repetitivas mediante funciones recursivas para cumplir la especificación (no usar `*`, `/` ni `%`).

**EN:** Defines the `Calculator` module with the 5 arithmetic operations. `multiplication` and `division` use repeated addition/subtraction via recursive functions to comply with the specification (no `*`, `/` or `%`).

```fsharp
module Calculator

let addition a b = a + b
    
let subtraction a b = a - b
    
let multiplication a b =
    let rec loop acc i =
        if i = 0 then acc
        else loop (addition acc a) (subtraction i 1)
    loop 0 b

let division a b =
    let rec loop quotient remaining =
        if remaining < b then quotient
        else loop (addition quotient 1) (subtraction remaining b)
    loop 0 a

let modulus a b =
    let quotient = division a b
    subtraction a (multiplication quotient b)
```

| Elemento | Propósito |
|----------|-----------|
| `module Calculator` | Declara el módulo que agrupa las funciones |
| `let addition a b = a + b` | Función de suma — directa con `+` |
| `let subtraction a b = a - b` | Función de resta — directa con `-` |
| `let rec loop acc i =` | Función recursiva interna (local al `let`) |
| `if i = 0 then acc else ...` | Caso base: cuando `i` llega a 0, retorna el acumulador |
| `loop 0 b` | Invocación inicial de la función recursiva |
| `let quotient = division a b` | Composición: usa `division` para calcular el módulo |

> **ES:** F# usa `let rec` para funciones **recursivas**. La función `loop` es una función auxiliar local con un acumulador (`acc`) que evita mutabilidad.
> 
> **EN:** F# uses `let rec` for **recursive** functions. The `loop` function is a local helper with an accumulator (`acc`) that avoids mutability.

### `test/Tests.fs` — Pruebas unitarias (xUnit)

**ES:** Cada `[<Fact>]` prueba una operación. Usa `Assert.Equal` con el orden correcto (expected, actual). Las funciones de F# se llaman con espacios: `Calculator.addition 2 3`.

**EN:** Each `[<Fact>]` tests one operation. Uses `Assert.Equal` with the correct order (expected, actual). F# functions are called with spaces: `Calculator.addition 2 3`.

```fsharp
module Tests

open System
open Xunit

[<Fact>]
let ``Addition Test`` () =
    Assert.Equal(5, Calculator.addition 2 3)

[<Fact>]
let ``Subtraction Test`` () =
    Assert.Equal(2, Calculator.subtraction 5 3)

[<Fact>]
let ``Multiplication Test`` () =
    Assert.Equal(12, Calculator.multiplication 3 4)

[<Fact>]
let ``Division Test`` () =
    Assert.Equal(3, Calculator.division 10 3)

[<Fact>]
let ``Modulus Test`` () =
    Assert.Equal(1, Calculator.modulus 10 3)
```

| Elemento | Propósito |
|----------|-----------|
| `[<Fact>]` | Atributo de xUnit que marca un método como prueba |
| ``let ``Addition Test`` () =`` | Test con nombre entre backticks (permite espacios en F#) |
| `Assert.Equal(5, Calculator.addition 2 3)` | Assert: esperado `5`, actual `Calculator.addition 2 3` |
| `Calculator.addition 2 3` | Llamada a función F# con espacios (no paréntesis ni comas) |

> **ES:** En F# la sintaxis de llamada a funciones es `f arg1 arg2` (con espacios), no `f(arg1, arg2)`. Los paréntesis con comas crean **tuplas**, no múltiples argumentos.
> 
> **EN:** In F#, the function call syntax is `f arg1 arg2` (with spaces), not `f(arg1, arg2)`. Parentheses with commas create **tuples**, not multiple arguments.

### `test/Calculator.Tests.fsproj`

**ES:** Proyecto de tests que referencia la biblioteca `src/Calculator.fsproj` y agrega paquetes NuGet:

| Paquete | Versión | Propósito |
|---------|---------|-----------|
| `xunit` | 2.9.3 | Framework de pruebas |
| `xunit.runner.visualstudio` | 3.1.4 | Integración con VS / Rider |
| `Microsoft.NET.Test.Sdk` | 17.14.1 | SDK de pruebas de .NET |
| `coverlet.collector` | 6.0.4 | Cobertura de código |

**EN:** Test project that references the `src/Calculator.fsproj` library and adds NuGet packages:

| Package | Version | Purpose |
|---------|---------|---------|
| `xunit` | 2.9.3 | Test framework |
| `xunit.runner.visualstudio` | 3.1.4 | VS / Rider integration |
| `Microsoft.NET.Test.Sdk` | 17.14.1 | .NET test SDK |
| `coverlet.collector` | 6.0.4 | Code coverage |

---

## 🚀 Compilación y ejecución / Build & Run

### Requisito: Tener el .NET SDK instalado

```bash
dotnet --version   # Debería mostrar: 10.0.x
```

### Ejecutar pruebas unitarias

```bash
# Desde la raíz del proyecto (usa la solución)
dotnet test calculator.slnx

# O directamente desde el proyecto de tests
dotnet test test/Calculator.Tests.fsproj
```

**Salida esperada / Expected output:**

```text
Restored .../calculator.slnx
Restored .../src/Calculator.fsproj (0.1s)
Restored .../test/Calculator.Tests.fsproj (0.1s)
  Calculator succeeded (0.3s) → src/bin/Debug/net10.0/Calculator.dll
  Calculator.Tests succeeded (0.5s) → test/bin/Debug/net10.0/Calculator.Tests.dll
[xUnit] Running 5 test(s) from Calculator.Tests
[xUnit]   Addition Test     ✓ (0.1s)
[xUnit]   Subtraction Test  ✓ (0.1s)
[xUnit]   Multiplication Test ✓ (0.1s)
[xUnit]   Division Test     ✓ (0.1s)
[xUnit]   Modulus Test      ✓ (0.1s)
  Calculator.Tests test succeeded (1.2s)

Test summary: 5 passed, 0 failed — 100% coverage
```

> **ES:** Todas las pruebas deben pasar (5 passed, 0 failed) con código de salida 0.
> **EN:** All tests must pass (5 passed, 0 failed) with exit code 0.

---

## 🧠 Algoritmos / operaciones (según el módulo)

| Función | Implementación | Cumple |
|---------|---------------|--------|
| `addition a b` | `a + b` (suma directa) | ✅ |
| `subtraction a b` | `a - b` (resta directa) | ✅ |
| `multiplication a b` | Suma repetitiva recursiva de `a`, `b` veces con acumulador | ✅ No usa `*` |
| `division a b` | Resta repetitiva recursiva con contador de cociente | ✅ No usa `/` |
| `modulus a b` | `a - multiplication (division a b) b` | ✅ No usa `%` |

### Detalle de algoritmos recursivos

**`multiplication`** — suma `a` consigo mismo `b` veces usando recursión con acumulador:

```fsharp
multiplication 3 4
→ loop 0 4
→ loop (0 + 3) 3  → loop 3 3
→ loop (3 + 3) 2  → loop 6 2
→ loop (6 + 3) 1  → loop 9 1
→ loop (9 + 3) 0  → loop 12 0
→ 12
```

**`division`** — resta `b` de `a` repetidamente, contando cuántas veces cabe:

```fsharp
division 10 3
→ loop 0 10
→ loop 1 (10 - 3) → loop 1 7
→ loop 2 (7 - 3)  → loop 2 4
→ loop 3 (4 - 3)  → loop 3 1
→ 1 < 3 → 3       → cociente = 3
```

---

## 📝 Notas de implementación / Implementation Notes

- **ES:** En F# los tests usan backticks dobles ` ``nombre con espacios`` ` para nombres de prueba legibles.
- **EN:** In F#, tests use double backticks ` ``name with spaces`` ` for readable test names.
- **ES:** Las funciones recursivas requieren `let rec` en F#. Sin `rec`, el compilador no permite la autorreferencia.
- **EN:** Recursive functions require `let rec` in F#. Without `rec`, the compiler does not allow self-reference.
- **ES:** Las funciones `multiplication` y `division` están implementadas con sumas/restas repetitivas para cumplir la especificación educativa (no usar operadores `*` ni `/` directos).
- **EN:** The `multiplication` and `division` functions are implemented with repeated addition/subtraction to comply with the educational specification (no direct `*` or `/` operators).
- **ES:** Los directorios `bin/` y `obj/` son generados por el compilador y no deben versionarse.
- **EN:** The `bin/` and `obj/` directories are compiler-generated and should not be versioned.

### Diferencias con C#

| Aspecto | C# | F# |
|---------|----|----|
| Atributo xUnit | `[Fact]` | `[<Fact>]` |
| Llamada a función | `Calc.Add(2, 3)` | `Calculator.addition 2 3` |
| Estructura | Clases y métodos | Módulos y funciones |
| Mutabilidad | Por defecto mutable | Por defecto inmutable |
| Recursión | `while`/`for` | `let rec` con acumulador |

---

### 🌐 Otras implementaciones / Other implementations

Este proyecto también está implementado en otros lenguajes. Explora el [repositorio principal](https://github.com/yorche3/programming_languages) para ver todas las versiones.

---

*🌐 [github.com/yorche3/programming_languages](https://github.com/yorche3/programming_languages) · [GitHub Pages](https://yorche3.github.io/programming_languages/)*
