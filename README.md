# F# (F Sharp)

Proyectos en **F# (.NET 10)**, ejecutados con el SDK de .NET (`dotnet fsi`, `dotnet test`).

Usa `.fsx` scripts para programas simples y proyectos `.fsproj` con soluciones `.slnx` para bibliotecas con pruebas unitarias mediante **xUnit**.

---

## 📂 Módulos / Modules

| Módulo | Descripción |
|--------|-------------|
| [`core/foundations/`](core/foundations/) | **Fase 0 — Fundamentos**: `helloworld`, `hellouser`, `calculator`, `numbers` |

---

### ▶️ Comenzar / Getting Started

```bash
# Hello, World!
cd core/foundations/helloworld
dotnet fsi hello-world.fsx

# Hello, User!
cd core/foundations/hellouser
dotnet fsi hello-user.fsx

# Calculator Tests
cd core/foundations/unit_test/calculator
dotnet test calculator.slnx

# Numbers Tests
cd core/foundations/numbers
dotnet test numbers.slnx
```

---

## 📦 Requisitos / Requirements

| Herramienta | Instalación |
|-------------|-------------|
| [.NET SDK](https://dotnet.microsoft.com/download) (incluye F#) | `brew install dotnet-sdk` (macOS) / `sudo apt install dotnet-sdk-10.0` (Linux) / [descargar](https://dotnet.microsoft.com/download) (Windows) |

```bash
# Verificar instalación
dotnet --version
```

> **ES:** F# viene incluido con el SDK de .NET. No es necesario instalar nada adicional.
> **EN:** F# comes bundled with the .NET SDK. No additional installation is needed.

---

## 🏗️ Tipos de proyecto / Project Types

### 1. Script simple (archivo `.fsx`)

**ES:** Un único archivo fuente `.fsx`, sin dependencias externas, ejecutado directamente con `dotnet fsi` (F# Interactive). Ideal para `helloworld` y `hellouser`.

**EN:** A single `.fsx` source file, no external dependencies, executed directly with `dotnet fsi` (F# Interactive). Ideal for `helloworld` and `hellouser`.

```bash
dotnet fsi <archivo>.fsx
```

### 2. Proyecto biblioteca + tests (`.fsproj` + `.slnx` + xUnit)

**ES:** Para proyectos que requieren pruebas unitarias, se utilizan proyectos `.fsproj` agrupados en una solución `.slnx` con **xUnit** como framework de tests.

**EN:** For projects requiring unit tests, `.fsproj` projects are grouped in a `.slnx` solution with **xUnit** as the testing framework.

```bash
dotnet test <solucion>.slnx
```

---

## 🧠 Características del lenguaje F# usadas / F# Language Features Used

| Característica | Descripción |
|---------------|-------------|
| **Scripts `.fsx`** | Archivos ejecutables sin compilación previa, ideales para prototipos |
| **`printfn` / `printf`** | Salida formateada con tipado seguro (hereda de C) |
| **Pattern matching** | `match ... with` para control de flujo expresivo |
| **`let` / `let rec`** | Declaración de valores y funciones recursivas |
| **Inmutabilidad** | Variables inmutables por defecto con `let`; `let mutable` para mutabilidad |
| **Tail Call Optimization** | TCO garantizado por el compilador de F# |
| **Inferencia de tipos** | Tipado estático sin necesidad de anotaciones explícitas |
| **Módulos** | `module` para agrupar funciones, en lugar de clases |
| **Llamada con espacios** | `f arg1 arg2` en lugar de `f(arg1, arg2)` |

---

## 🌐 Otras implementaciones / Other implementations

Este proyecto también está implementado en otros lenguajes. Explora el [repositorio principal](https://github.com/yorche3/programming_languages) para ver todas las versiones.

---

*🌐 [github.com/yorche3/programming_languages](https://github.com/yorche3/programming_languages) · [GitHub Pages](https://yorche3.github.io/programming_languages/)*