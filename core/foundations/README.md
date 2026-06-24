# 🚀 Foundations — F#

Implementaciones de la [Fase 0 — Fundamentos](https://yorche3.github.io/programming_languages/ROADMAP/#fase-0--fundamentos--foundations--completada) en **F# (.NET 10)**: `helloworld`, `hellouser`, `unit_test/calculator` y `numbers`.

---

## 📖 Módulos / Modules

| Módulo | Especificación | Enfoque | Tests | Estado |
|--------|---------------|---------|:-----:|:------:|
| [`helloworld/`](helloworld/) | [01_Hello_World](https://yorche3.github.io/programming_languages/core/foundations/01_Hello_World/) | `dotnet fsi` (script `.fsx`) | — | ✅ |
| [`hellouser/`](hellouser/) | [02_Hello_User](https://yorche3.github.io/programming_languages/core/foundations/02_Hello_User/) | `dotnet fsi` (script `.fsx` con entrada de usuario) | — | ✅ |
| [`unit_test/calculator/`](unit_test/calculator/) | [03_Unit_Test_Calculator](https://yorche3.github.io/programming_languages/core/foundations/03_Unit_Test_Calculator/) | `dotnet test` + **xUnit** (solución `.slnx`) | 5 | ✅ |
| [`numbers/`](numbers/) | [04_Numbers](https://yorche3.github.io/programming_languages/core/foundations/04_Numbers/) | `dotnet test` + **xUnit** (solución `.slnx`) | 15 | ✅ |

---

## 📁 Estructura / Structure

```text
foundations/
├── helloworld/                   # 01_Hello_World
│   ├── hello-world.fsx           # Script .fsx con printfn
│   └── README.md
│
├── hellouser/                    # 02_Hello_User
│   ├── hello-user.fsx            # Script .fsx con Console.ReadLine()
│   └── README.md
│
├── unit_test/
│   └── calculator/               # 03_Unit_Test_Calculator
│       ├── calculator.slnx       # Solución .NET
│       ├── src/
│       │   ├── Library.fs        # Módulo Calculator con 5 operaciones aritméticas
│       │   └── Calculator.fsproj
│       ├── test/
│       │   ├── Tests.fs          # 5 tests con xUnit
│       │   └── Calculator.Tests.fsproj
│       └── README.md
│
└── numbers/                      # 04_Numbers
    ├── numbers.slnx              # Solución .NET
    ├── src/
    │   ├── Numbers.fs            # Módulo Numbers: 15 funciones (3 enfoques × 5 algoritmos)
    │   └── Numbers.fsproj
    ├── test/
    │   ├── NumbersRecTests.fs    # 5 tests recursivos directos
    │   ├── NumbersAccTests.fs    # 5 tests con acumulador
    │   ├── NumbersIteTests.fs    # 5 tests iterativos
    │   └── Numbers.Tests.fsproj
    └── README.md
```

---

## 🛠️ Patrón común / Common Pattern

| Característica | Descripción |
|---------------|-------------|
| **SDK** | .NET 10.0 (`net10.0`) con `dotnet` CLI |
| **Scripts simples** | `dotnet fsi` para `helloworld` y `hellouser` (archivos `.fsx`, sin compilación) |
| **Proyectos con tests** | Solución `.slnx` con proyectos `src/` (biblioteca) y `test/` (xUnit) |
| **Framework de tests** | [xUnit](https://xunit.net/) v2.9.3 con `[<Fact>]` y `Assert.Equal` |
| **Detección de tests** | Automática — `dotnet test` descubre todos los `[<Fact>]` |
| **Paquetes NuGet** | `xunit`, `xunit.runner.visualstudio`, `Microsoft.NET.Test.Sdk`, `coverlet.collector` |
| **Paradigma** | **Funcional** — módulos en vez de clases, funciones en vez de métodos, inmutabilidad por defecto |
| **Llamada a funciones** | `f arg1 arg2` con espacios (no paréntesis ni comas) |
| **Recursión** | `let rec` para funciones recursivas; **TCO garantizado** por el compilador de F# |
| **Pattern matching** | `match ... with` en lugar de `if`/`else` o `switch` |
| **Tipado** | Inferencia de tipos estática con tipos fuertes |

---

## 🚀 Compilación rápida / Quick Build

```bash
# Hello, World!
cd helloworld
dotnet fsi hello-world.fsx

# Hello, User!
cd hellouser
dotnet fsi hello-user.fsx

# Calculator Tests
cd unit_test/calculator
dotnet test calculator.slnx

# Numbers Tests
cd numbers
dotnet test numbers.slnx
```

### Salidas esperadas / Expected outputs

```text
# Hello, World!
Hello, World! from F#

# Hello, User!
Enter your name: Ada
Hello, Ada!

# Calculator Tests (5 passed)
Test summary: 5 passed, 0 failed

# Numbers Tests (15 passed)
Test summary: 15 passed, 0 failed
```

---

## 🧠 Conceptos clave aprendidos / Key Concepts Learned

| Módulo | Conceptos nuevos en F# |
|--------|----------------------|
| `helloworld` | `printfn`, scripts `.fsx`, `dotnet fsi`, inferencia de tipos |
| `hellouser` | `printf` (sin salto de línea), `System.Console.ReadLine()`, `let`, función `main` explícita, especificadores de formato `%s` |
| `calculator` | `module`, `let rec` (recursión), funciones con acumulador, `[<Fact>]` en xUnit, TCO |
| `numbers` | Pattern matching (`match`), 3 enfoques algorítmicos, tail recursion garantizada, helpers con acumulador públicos |

---

## 🌐 Otras implementaciones / Other implementations

Este proyecto también está implementado en otros lenguajes. Explora el [repositorio principal](https://github.com/yorche3/programming_languages) para ver todas las versiones.

---

## ▶️ Siguiente / Next

👉 Después de fundamentos, continúa con [Fase 1 — Algoritmos Puros](https://yorche3.github.io/programming_languages/ROADMAP/#fase-1--algoritmos-puros--algorithms-pure-).  
👉 After foundations, continue with [Phase 1 — Algorithms Pure](https://yorche3.github.io/programming_languages/ROADMAP/#fase-1--algoritmos-puros--algorithms-pure-).

---

*[← Volver a F#](../../README.md)*

*🌐 [github.com/yorche3/programming_languages](https://github.com/yorche3/programming_languages) · [GitHub Pages](https://yorche3.github.io/programming_languages/)*
