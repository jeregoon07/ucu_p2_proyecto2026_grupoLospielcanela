<!-- markdownlint-disable MD033 -->
<!-- markdownlint-disable-next-line MD041 -->
<img alt="UCU" src="https://www.ucu.edu.uy/plantillas/images/logo_ucu.svg"
width="150"/>

# Universidad Católica del Uruguay

## Programación II

# Plantilla de Proyecto

## Descripción

Esta plantilla proporciona una estructura base para los proyectos de
Programación II. Incluye la configuración necesaria para desarrollar
aplicaciones en C# con buenas prácticas de programación, pruebas automatizadas y
documentación.

¿Qué hay configurado en esta plantilla?

1. Un proyecto de biblioteca (creado con [`dotnet new classlib --name
   Library`](https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet-new?tabs=netcore22))
   en la carpeta `src\Library`.

2. Un proyecto de aplicación de consola, creado con [`dotnet new console --name
   Program`](https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet-new?tabs=netcore22),
   en la carpeta `src\Program`.

3. Un proyecto de prueba en [NUnit](https://nunit.org/), creado con [`dotnet new
   nunit --name
   LibraryTests`](https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet-new?tabs=netcore22),
   en la carpeta `test\LibraryTests`.

4. Un proyecto de [Doxygen](https://www.doxygen.nl/index.html) para generación
   de sitio web de documentación en la carpeta `docs`.

5. Análisis estático con [Roslyn
   analyzers](https://docs.microsoft.com/en-us/dotnet/fundamentals/code-analysis/overview)
   en los proyectos de biblioteca y de aplicación.

6. Análisis de estilo con
   [StyleCop](https://github.com/DotNetAnalyzers/StyleCopAnalyzers/blob/master/README.md)
   en los proyectos de biblioteca y de aplicación.

7. Una solución `Project.sln` que referencia todos los proyectos de C# y
   facilita la compilación con [`dotnet
   build`](https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet-build).

8. Tareas preconfiguradas para ejecutar las pruebas con cobertura y generar
   documentación desde VSCode en la carpeta `.vscode`.

9. Análisis de cobertura de los casos de prueba mediante los indicadores que
   aparecen en los márgenes con el complemento de Visual Studio Code [Coverage
   Gutters](https://marketplace.visualstudio.com/items?itemName=ryanluker.vscode-coverage-gutters).

10. Ejecución automática de compilación y prueba mediante [GitHub
    Actions](https://docs.github.com/en/actions) configuradas en el repositorio
    al hacer [push](https://github.com/git-guides/git-push) o [pull
    request](https://docs.github.com/en/github/collaborating-with-pull-requests).

Vean este 🎥
[video](https://correoucuedu-my.sharepoint.com/:v:/r/personal/fmachado_ucu_edu_uy/Documents/Stream%20Migrated%20Videos/Demo%20Project%20Template-20211025_014904.mp4)
que explica el funcionamiento de la plantilla.

## Convenciones de código

[Convenciones de código en
C#](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/inside-a-program/coding-conventions)

[Convenciones de nombres en
C#](https://docs.microsoft.com/en-us/dotnet/standard/design-guidelines/naming-guidelines)

[C# Compiler Errors
(CS*)](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/compiler-messages/)

[Roslyn Analyzer Warnings
(CA*)](https://docs.microsoft.com/en-us/dotnet/fundamentals/code-analysis/categories)

[StyleCop Analyzer Warnings
(SA*)](https://github.com/DotNetAnalyzers/StyleCopAnalyzers/blob/master/DOCUMENTATION.md)

Las violaciones a estas convenciones son reportadas como *warnings* al compilar.
Aunque recomendamos corregir las violaciones, es posible omitir esta
configuración de la siguiente forma:

Comentar las siguientes líneas en los archivos de proyecto (`*.csproj`)

```xml
    <EnableNETAnalyzers>true</EnableNETAnalyzers>
    <AnalysisMode>All</AnalysisMode>
    <EnforceCodeStyleInBuild>true</EnforceCodeStyleInBuild>
```

Comentar la línea `<PackageReference Include="StyleCop.Analyzers"
Version="1.1.118"/>` en los archivos de proyecto (`*.csproj`)

## Uso de ![GitHub Copilot](https://img.shields.io/badge/GitHub%20Copilot-000?logo=githubcopilot&logoColor=fff)

Es posible usar GitHub Copilot en este repositorio. Consulta [cómo usar Copilot
para aprender](./COPILOT.md).
