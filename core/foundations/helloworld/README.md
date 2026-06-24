# Hello, World! — F#

Implementación de la especificación [01_Hello_World](https://yorche3.github.io/programming_languages/core/foundations/01_Hello_World/) en **F#**, ejecutado con **dotnet fsi** (F# Interactive).

---

## 📂 Archivos y estructura / Files & Structure

| Archivo | Propósito |
|---------|-----------|
| [`hello-world.fsx`](hello-world.fsx) | Código fuente: imprime `"Hello, World! from F#"` en la consola. |

**Estructura de directorios esperada:**

```text
helloworld/
├── hello-world.fsx   # Código fuente (script)
└── README.md        # Este archivo
```

---

## 🛠️ Enfoque y construcción / Approach & Build

**ES:** Este proyecto usa **F#** con **F# Interactive (fsi)** y sigue un enfoque minimalista: un único archivo script `.fsx`, sin dependencias externas, ejecutado directamente con `dotnet fsi`.

Características:
- **Sin dependencias externas** — solo usa `printfn` de la biblioteca estándar de F#.
- **Ejecución directa** — `dotnet fsi` ejecuta scripts `.fsx` sin necesidad de compilación previa.
- **Sin archivo de proyecto** — no necesita `.fsproj` para scripts simples.
- **Sin compilación** — los scripts `.fsx` se interpretan directamente, ideales para prototipos y aprendizaje.

**EN:** This project uses **F#** with **F# Interactive (fsi)** and follows a minimalist approach: a single `.fsx` script file, no external dependencies, executed directly with `dotnet fsi`.

Features:
- **No external dependencies** — only uses `printfn` from F#'s standard library.
- **Direct execution** — `dotnet fsi` runs `.fsx` scripts without prior compilation.
- **No project file** — no `.fsproj` needed for simple scripts.
- **No compilation** — `.fsx` scripts are interpreted directly, ideal for prototyping and learning.

---

## 📄 Archivos de configuración clave / Key Configuration Files

### `hello-world.fsx`

**ES:** Script F# que imprime un saludo en la consola usando `printfn`.

**EN:** F# script that prints a greeting to the console using `printfn`.

```fsharp
printfn "Hello, World! from F#"
```

| Elemento | Propósito |
|----------|-----------|
| `printfn` | Función de F# que imprime una cadena con formato seguida de un salto de línea |
| `"Hello, World! from F#"` | Argumento: la cadena a imprimir |

> **ES:** `printfn` es la función de salida formateada de F#, similar a `printf` en C pero con tipado seguro. La `n` al final indica que añade automáticamente un salto de línea (`\n`). También existe `printf` (sin salto de línea) y `sprintf` (que devuelve una cadena).
> 
> **EN:** `printfn` is F#'s formatted output function, similar to C's `printf` but type-safe. The trailing `n` means it automatically adds a newline (`\n`). There's also `printf` (no newline) and `sprintf` (returns a string).

---

## 🚀 Compilación y ejecución / Build & Run

### Requisito: Tener .NET SDK instalado (incluye F#)

```bash
# Verificar instalación
dotnet --version

# Linux (Debian/Ubuntu)
sudo apt install dotnet-sdk-8.0

# macOS
brew install dotnet-sdk

# Windows
winget install Microsoft.DotNet.SDK.8
```

> **ES:** F# viene incluido con el SDK de .NET. No es necesario instalar nada adicional.
> **EN:** F# comes bundled with the .NET SDK. No additional installation is needed.

### Ejecutar como script (recomendado)

```bash
cd core/foundations/helloworld
dotnet fsi hello-world.fsx
```

**Salida esperada / Expected output:**

```text
Hello, World! from F#
```

### Compilar y ejecutar como proyecto (alternativa)

**ES:** Aunque este ejemplo usa un script `.fsx`, también se puede compilar F# como un proyecto tradicional con un archivo `.fsproj`.

**EN:** Although this example uses a `.fsx` script, F# can also be compiled as a traditional project with a `.fsproj` file.

```bash
# Crear un proyecto F# desde cero
dotnet new console -lang F# -n hello-world
cd HelloWorld

# Reemplazar Program.fs con el contenido del script
# Luego compilar y ejecutar
dotnet run
```

---

## 📝 Notas de implementación / Implementation Notes

- **ES:** Los scripts `.fsx` son una característica distintiva de F#. Permiten escribir y ejecutar código de forma interactiva sin necesidad de compilación.
- **EN:** `.fsx` scripts are a distinctive feature of F#. They allow writing and running code interactively without compilation.
- **ES:** F# Interactive (`dotnet fsi`) también puede usarse como REPL para probar código de forma interactiva.
- **EN:** F# Interactive (`dotnet fsi`) can also be used as a REPL to test code interactively.
- **ES:** F# soporta múltiples paradigmas: funcional, imperativo y orientado a objetos. Este ejemplo usa el estilo más simple (imperativo).
- **EN:** F# supports multiple paradigms: functional, imperative, and object-oriented. This example uses the simplest (imperative) style.
- **ES:** Para proyectos más grandes se recomienda usar `.fsproj` y compilar con `dotnet build`.
- **EN:** For larger projects, it's recommended to use `.fsproj` and compile with `dotnet build`.

---

### 🌐 Otras implementaciones / Other implementations

Este proyecto también está implementado en otros lenguajes. Explora el [repositorio principal](https://github.com/yorche3/programming_languages) para ver todas las versiones.

---

*🌐 [github.com/yorche3/programming_languages](https://github.com/yorche3/programming_languages) · [GitHub Pages](https://yorche3.github.io/programming_languages/)*
