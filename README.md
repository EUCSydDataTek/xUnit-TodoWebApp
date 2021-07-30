# 2.RazorPages_xUnitTest

Projektet er af typen **xUnit**

Opret unit test project, se [Getting Started with xUnit.net](https://xunit.net/docs/getting-started/netcore/visual-studio)

Opret reference fra Test projetet til TodoWebApp.

Tilføj evt. NuGet pakken: ```Microsoft.EntityFrameworkCore.InMemory``` til projektet med Contexten.

Husk at fjerne evt. seeding af data.

### TodoServiceUnitTest

TodoServiceUnitTest klassen demonstrer Unit Test af hovedsageligt **ServiceLayer**, eftersom databasen er *InMemory* og derfor
ikke fungerer helt som en "rigtig" SQL server.

### IndexPageTests

