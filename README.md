# Todo projektet

## Beskrivelse

TodoWebApp er en ASP.NET Core Razor Pages web applikation, hvor man kan oprette et TodoItem med følgende properties:

- TaskDescription
- Priority [Low, Normal, High]

Systemet opretter selv følgende properties samtidigt:

- Id
- CreatedTime
- IsCompleted [false]

"Home" (Index-page) viser en liste af alle TodoItems. Klikkes på et emne, åbnes emnet i Edit-mode og man kan ændre de forskellige properties, samt slette emnet.
"Create Modal" (IndexModal-page) giver en ekstra mulighed for at markere et emne som udført ved at klikke Option-ikonet fra forsiden.
Desuden kan man oprette et nyt emne i en modal boks.

Der er validation alle steder. Desuden kan man klikke

Index-pagen viser alle items, både de udførte og de ikke-udførte. IndexModel-pagen viser kun dem, der mangler at blive udført.

Der er mulighed for at benytte InMemory eller Sqlite databasen.

> ### Oversigt over Branches
> 
> #### 1.TodoWebApp
> 
> Razor Pages, CheckBox, InMemory DB og Sqlite DB
> 
> #### 2.TodoWebApp__xUnitTest
> 
> Unit test af TodoService med SQLite databasen
> 
> #### 3.WebApp_SeleniumTest
> 
> UI test af WebApp.


&nbsp;

# 1.TodoWebApp

Består af en Razor Pages applikation med tilhørende service-klasse. Data gemmes enten **InMemory** eller i en **Sqlite** database.

`Index` pagen demonstrerer automatisk check af udført opgave vha. **link med indhold i form af en HTML Entity** for hhv. checked og uncheced option kontrol.

`IndexModal` pagen demonstrerer **Bootstraps Modal komponent** og automatisk updatering og **submit med JavaScript** når et Todo item markeres som udført.
