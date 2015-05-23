# .NET Portability Analyzer Website

This repository contains the source code for the [.NET Portability website](http://dotnetstatus.azurewebsites.net/).

Today, the repository contains the following components:

* **DotNetStatus**. An ASP.NET 5 implementation of .NET Portability site.

## Using this Repository

1. Install the following:
    1. [Visual Studio 2015](http://www.visualstudio.com/en-us/downloads/visual-studio-2015-downloads-vs.aspx)
    1. [.NET Version Manager (DNVM)](https://github.com/aspnet/home#getting-started-with-aspnet-5-and-dnx)
    1. [.NET Execution Environment (DNX)](https://github.com/aspnet/home#running-an-application)

### Windows

#### Building/Running from Commandline
1. Go to `src\DotNetStatus`
1. Run command: `dnu restore`
1. Run command: `dnx . web`
1. The website should be started on `http://localhost:5000`

#### Building/Running from Visual Studio
1. Open `DotNetStatus.sln`
1. Check that all the Dependencies have loaded (like NPM and Bower) and packages were restored
   * Check the `Output window -> Package Manager Log` to make sure that everything was successful
1. There are two targets you can run/debug with:
   * IIS Express
   * web - To run on DNX

   These can be found under the `Project Properties -> Debug` or by clicking the drop-down menu on the Debug button in your main toolbar.
1. You can also change the Target DNX Version (ex. so that it runs on .NET Core) by:
   * Go to `Project Properties -> Application` 
   * Check off "Use specific DNX version"
   * Change the Platform from `.NET Framework` to `.NET Core`

#### Troubleshooting
1. `dnu restore` outputs: Error: ENOENT, stat 'C:\Users\CurrentUser\AppData\Roaming\npm'
   * Execute: `mkdir %APPDATA%\npm` (Command Prompt) or `mkdir $env:AppData\npm` (Powershell)
   * Execute: `dnu restore`
1. `dnu restore` outputs: 'grunt' is not recognized as an internal or external command
   * Execute: `npm install -g grunt-cli`
   * Execute: `dnu restore`

### Linux

#### Building/Running on Ubuntu
1. Go to `src/DotNetStatus`
1. Install npm
   * `sudo apt-get update` 
   * `sudo apt-get install nodejs`
   * `sudo apt-get install npm`
1. Install Grunt globally
   * `sudo npm install -g grunt-cli`
1. `dnu restore`
1. `cd src/DotNetStatus`
1. `grunt`
1. Run the site: `dnx . kestrel`

#### Troubleshooting
1. `dnu restore` cannot find Microsoft.Fx.Portability
   * Open `~/.config/NuGet/NuGet.config` in a text editor
   * Add `<add key="dotnet-apiport" value="https://www.myget.org/F/dotnet-apiport" />` under `<packageSources>`
   * Try again

## How to Engage, Contribute and Provide Feedback

Some of the best ways to contribute are to try things out, file bugs, and join in design conversations. 

Want to get more familiar with what's going on in the code?

* [Pull requests](https://github.com/Microsoft/dotnet-apiweb/pulls): [Open](https://github.com/Microsoft/dotnet-apiweb/pulls?q=is%3Aopen+is%3Apr)/[Closed](https://github.com/Microsoft/dotnet-apiweb/pulls?q=is%3Apr+is%3Aclosed)

Looking for something to work on? The list of [up-for-grabs issues](https://github.com/Microsoft/dotnet-apiweb/issues?q=is%3Aopen+is%3Aissue) is a great place to start.

We're re-using the same contributing approach as .NET Core. You can check out the .NET Core [contributing guide][Contributing Guide] at the corefx repo wiki for more details.

* [How to Contribute][Contributing Guide]
    * [Contributing Guide][Contributing Guide]
    * [Developer Guide]

You are also encouraged to start a discussion on the .NET Foundation forums!

[Contributing Guide]: https://github.com/dotnet/corefx/wiki/Contributing
[Developer Guide]: https://github.com/dotnet/corefx/wiki/Developer-Guide

## Related Projects

For an overview of all the .NET related projects, have a look at the
[.NET home repository](https://github.com/Microsoft/dotnet).

## License

This project is licensed under the [MIT license](LICENSE).
