## ASP.Net Core 3.1 - Basics
- Cross Platform
- Deployed in IIS, Apache, Nginx, Docker, Self-host in your process
- One unified programming model for MVC and Web API. MVC Controller class and ASP.Net Web API Controller class inherit
from the same Controller Base Class and returns IActionResult.
- IActionResult is a implementation to obtain the result of a Method in an controller we can have ViewResult (MVC) or JsonResult (Web API).
Pero esto puede variar ya que ambos se basan en el mismo Base class.
- Depedency Injection. 
- Testability (Unit Testing)
- Open source
- Modular
  - ASP.Net Core provides modularity with Middleware components
  - Both the request and response pipelines are composed using the middleware components
  - Richt set of built-in middlware components are provided out of the box. 
  - Custom middleware components can alse created.

###  ASP.NET Core Templates
 - Web API
 - Web Application (Razor Pages)
 - Web Application VMC
 - Razor Class LIbrary

### Project File
- No need to unload project as prior versions
- Files and folders are no longer included in Project file as prior versions.
- Project files and folder: Si queremos agregar archivos una manera rapida es:  / Anticlick Web App Project / Open in File Explorer

.csproj: Para editarlo / Anticlick Web App Project / Edit csproj file.
 - XML con detalles asociados a
  - Target Framework: Se utiliza la nomenclatura Target Framework Moniker (TFM) Eg. net451, netcoreapp2.2, etc. 
  - AspNetCoreHostingModel: How shoud be hosted:
    - InProcess: 
      - Hosts the app inside of the ISS worker process (w3wp.exe or iisexpress.exe).
      - Hosting works wiht 1 server - IIS Express
      - Better for performance
    - OutOfProcess: Hosting model forward web request to a backend ASP.Net Core app running the Kestrel Server (dotnet.exe).
      - OutOfProcess Hosting have 2 server
        -  Internal: Kestrel
        -  External: IIS, Nginx o Apache.
      - We can add security and load balancing layer
      - Penalty of proxing requests between internal and external server.
      - Para implementar OUtOfProcess podemos indicarlo en el nodo AspNetCoreHostingModel o eliminarlo,
ya que por defecto si no existe este nodo ASP utiliza el OutOfProcess model
  - Package Reference: Related to NuGet Packages and metapackage (Microsoft.AspNetCore.App)
    - Metapackage has no content of its own. It just contains a list of dependencies (other packages). 
      See the Package in Dependencies/NuGet folder
    - Each package has its own version assigned at the moment to install it.

### Kestrel
- Cross-Platform Web Server for ASP.Net Core
- The process used to host the app is dotnet.exe
- Way to use Kestrel
  - Kestrel can be used as the internet facing web Server
  - Kestrel can be used in combination with a reverse proxy (Exposed to Internet) (IIS, Nginx, Apache).
    Add security and load balancing.

### Program.cs and Startup.cs
Main method is in file Program.cs, this is an entry point that creates a Web Host Builder (Build and runs) that uses Startup.cs file to mount 
(Setting up the web server, Loading configuration, middlewares, logging, etc) the Web App

### launchSettings.json
- Properties/launchSettings.json
- Only required in local machine for run our code. Define Port and SSL
- Startup use this file to identify if we are in Development (Inside profiles in IIS, node environmentVariables - ASPNETCORE_ENVIRONMENT: "Development")
  - We can change this or create another json nodes to define other profiles based on the env we are (staging, prod, etc)
- Profiles
 - IIS Express: Commonly uses in VS Studio o run using iisSettingsNode (http://localhost:15410)
 - [ProjectName]: Profiles that use DONTNET Core CLI, so Kestrel (http://localhost:5000)
 - Command name: Puede tener el valor de IISExpress, Project or IIS
   - Project: AspNetCoreHostingModel: ignored | Only one server - Kestrel
   - IISExpress or IIS: AspNetCoreHostingModel: InProcess | Only one server - IIS Express or ISS
   - IISExpress or IIS: AspNetCoreHostingModel: OutOfProcess | Internal Server- KEstre and External Server - IIS Express or IIS

### appSettings.json
- El orden en el que IConfiguration Service lee valores registrados en el appSettings.json es el siguiente.
  1. Files (appsettings.{env}.json y si no lo encuentra pues va al appsettings.json or )
  2. User secrets
  3. Environment variables (lauchSettings.json)
  4. Command-line arguments
- Si existe el valor en mas de 1 archivo, pues lee el valor del ultimo archivo basado en el orden previo:
- Si queremos modificar el order previo podemos modificar la definicion de WebHost.cs
https://github.com/aspnet/MetaPackages/blob/release/2.2/src/Microsoft.AspNetCore/WebHost.cs (This repository has been archived by the owner on Nov 21, 2018. It is now read-only.)

### Middleware
- La idea de los middlewares tienen acceso el request y response (Pipeline) en la aplicacio ASP.Net Core
- Los middleware components se corren en el orden que se les indique en el Startup.cs
- May process and/or pass the request to the next middleware
- May handle the request and short-circuit the pipeline.
- May process the outgoing response.


Request Processing Pipeline
- Los middleware se configuran dentro del Startup.cs y comunmente son methodos del IApplicationBuilder, 
el cual vemos que que se define multiples veces en el metodo Configure:
 - app.UserDeveloperExceptionPage();
 - app.Run (==anonymous method implementing lambda expression==  

```
public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
	WebHost.CrateDefultBuilder(args)
		.UseStartup<Startup>(); 
```

- Examples: Logging, StaticFiles, MVC, 

### Middleware App.Run() definition
Si nos vamos a la implementacion de run vemos que utiliza:
- RequestDelegate: Es un delegate que toma el http context como parametro.




### Resources
- https://www.youtube.com/watch?v=nt6anXAwfYI&list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU&index=11


## Por aprender
- [ ] Extension methods
- [ ] Anonymous methods
- [ ] Habilitar Nullable en .csproj
- [ ] Logger (Local and using other AWS, Azure, Application Insights)
