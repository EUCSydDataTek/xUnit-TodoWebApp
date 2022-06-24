# Todo projektet

## Beskrivelse

TodoWebApp er en ASP.NET Core Razor Pages web applikation, som giver mulighed for at vise en liste af 
Todo items, samt at markere et Todo item som udført ved at klikke på det. Man kan også oprette et nyt Todo item,
enten via en ny Razor Page form eller via en Modal pop-up. Der er validation alle steder. Desuden kan man klikke
på Todo navnet og redigere det enkelte item. Det er også muligt at slette det helt.

Default pagen viser alle items, både de udførte og de ikke-udførte. DefaultModal viser kun dem, der mangler at blive udført.

Der er mulighed for at benytte InMemory eller Sqlite databasen.

> ### Oversigt over Branches
> 
> #### 1.TodoWebApp
> 
> Razor Pages, CheckBox, InMemory DB og Sqlite DB
> 
> #### 2.RazorPages_DAL_xUnitTest
> 
> Unit test af TodoService med InMemory database samt af PageModel i RazorPages
> 
> #### 3.WebApp_SeleniumTest
> 
> UI test af WebApp.


&nbsp;

# 1.TodoWebApp

Består af en Razor Pages applikation med tilhørende service-klasse. Data gemmes enten **InMemory** eller i en **Sqlite** database.

`Index` pagen demonstrerer automatisk check af udført opgave vha. **link med indhold i form af en HTML Entity** for hhv. checked og uncheced option kontrol.

`IndexModal` pagen demonstrerer **Bootstraps Modal komponent** og automatisk updatering og **submit med JavaScript** når et Todo item markeres som udført.
