# .NET Status Website

This repository contains the source code for the [.NET Portability website](http://dotnetstatus.azurewebsites.net/).

Today, the repository contains the following components:

* **DotNetStatus**. An ASP.NET vNext implementation of .NET Portability site.

## Using this Repository

1. Install the following:
    1. [Visual Studio 2015](http://www.visualstudio.com/en-us/downloads/visual-studio-2015-downloads-vs.aspx)
    2. [K Version Manager (KVM)](https://github.com/aspnet/home#install-the-k-version-manager-kvm)
    3. [K Runtime (KRE)](https://github.com/aspnet/home#install-the-k-runtime-environment-kre)
2. Add this feed to your NuGet package sources: https://www.myget.org/F/aspnetvnext/api/v2
    * Wiki: [Configuring feeds used by KPM to Restore Packages](https://github.com/aspnet/Home/wiki/Configuring-the-feed-used-by-kpm-to-restore-packages)

### Building/Running from Commandline

1. Go to `src\DotNetStatus`
2. Run command `kpm restore`
3. Run command `k web`
4. The website should be started on `http://localhost:5000`

### Building/Running from Visual Studio

1. Open `DotNetStatus.sln`
2. Check that all the Dependencies have loaded (like NPM and Bower) and packages were restored
   * Check the `Output window -> Package Manager Log` to make sure that everything was successful
3. There are two targets you can run/debug with:
   * IIS Express
   * web - To run on KRE

   These can be found under the `Project Properties -> Debug` or by clicking the drop-down menu on the Debug button in your main toolbar.
4. You can also change the Target KRE Version (ex. so that it runs on CoreCLR) by going to `Project Properties -> Application -> Target KRE Version`

## How to Engage, Contribute and Provide Feedback

Some of the best ways to contribute are to try things out, file bugs, and join in design conversations. 

Want to get more familiar with what's going on in the code?

* [Pull requests](https://github.com/Microsoft/dotnet-apiweb/pulls): [Open](https://github.com/Microsoft/dotnet-apiweb/pulls?q=is%3Aopen+is%3Apr)/[Closed](https://github.com/Microsoft/dotnet-apiweb/pulls?q=is%3Apr+is%3Aclosed)

Looking for something to work on? The list of [up-for-grabs issues](https://github.com/Microsoft/dotnet-apiweb/issues?q=is%3Aopen+is%3Aissue) is a great place to start.

We're re-using the same contributing approach as .NET Core. You can check out the .NET Core [contributing guide][Contributing Guide] at the corefx repo wiki for more details.

* [How to Contribute][Contributing Guide]
    * [Contributing Guide][Contributing Guide]
    * [Developer Guide]

You are also encouraged to start a discussion on the .NET Foundation forums or by filing an issue.

[Contributing Guide]: https://github.com/dotnet/corefx/wiki/Contributing
[Developer Guide]: https://github.com/dotnet/corefx/wiki/Developer-Guide

## Related Projects

For an overview of all the .NET related projects, have a look at the
[.NET home repository](https://github.com/Microsoft/dotnet).

## License

This project is licensed under the [MIT license](LICENSE).