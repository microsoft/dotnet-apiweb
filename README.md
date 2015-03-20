# .NET Portability Analyzer Website

This repository contains the source code for the [.NET Portability website](http://dotnetstatus.azurewebsites.net/).

Today, the repository contains the following components:

* **DotNetStatus**. An ASP.NET vNext implementation of .NET Portability site.

## Using this Repository

1. Install the following:
    1. [Visual Studio 2015 CTP6](https://www.visualstudio.com/downloads/visual-studio-2015-ctp-vs)
    2. [K Version Manager (KVM)](https://github.com/aspnet/home#install-the-k-version-manager-kvm)
    3. [K Runtime (KRE)](https://github.com/aspnet/home#install-the-k-runtime-environment-kre)

### Windows

#### Building/Running from Commandline
1. Go to `src\DotNetStatus`
2. Run command: `kpm restore`
   * There are a couple of problems you may encounter
   * __Problem__: Error: ENOENT, stat 'C:\Users\CurrentUser\AppData\Roaming\npm'
      1. Execute: `mkdir %APPDATA%\npm` (Command Prompt) or `mkdir $env:AppData\npm` (Powershell)
      2. Execute: `kpm restore`
   * __Problem__: 'grunt' is not recognized as an internal or external command
      1. Execute: `npm install -g grunt-cli`
      2. Execute: `kpm restore`
3. Run command: `k web`
4. The website should be started on `http://localhost:5000`

#### Building/Running from Visual Studio
1. Open `DotNetStatus.sln`
2. Check that all the Dependencies have loaded (like NPM and Bower) and packages were restored
   * Check the `Output window -> Package Manager Log` to make sure that everything was successful
3. There are two targets you can run/debug with:
   * IIS Express
   * web - To run on KRE

   These can be found under the `Project Properties -> Debug` or by clicking the drop-down menu on the Debug button in your main toolbar.
4. You can also change the Target KRE Version (ex. so that it runs on CoreCLR) by going to `Project Properties -> Application -> Target KRE Version`

### Linux
#### Building/Running on Ubuntu
1. Go to `src/DotNetStatus`
2. Install npm
   * `sudo apt-get update` 
   * `sudo apt-get install nodejs`
   * `sudo apt-get install npm`
3. Install Grunt globally
   * `sudo npm install -g grunt-cli`
4. Build [libuv](https://github.com/libuv/libuv)
   * `wget http://dist.libuv.org/dist/v1.0.0-rc1/libuv-v1.0.0-rc1.tar.gz`
   * `tar -xvf libuv-v1.0.0-rc1.tar.gz`
   * `cd libuv-v1.0.0-rc1/`
   * `./gyp_uv.py -f make -Duv_library=shared_library`
   * `make -C out`
   * `sudo cp out/Debug/lib.target/libuv.so /usr/lib/libuv.so.1.0.0-rc1`
   * `sudo ln -s libuv.so.1.0.0-rc1 /usr/lib/libuv.so.1`

    The instructions are from Punit Ganshani's [blog](http://www.ganshani.com/blog/2014/12/shell-script-to-setup-net-on-linux/). He created a setup script, too. [Shell Script to Setup .NET on Linux](https://github.com/punitganshani/ganshani/blob/master/Samples/ASPNET5.0_SampleForLinux/SetupDotNetOnLinux.sh).
5. `kpm restore`
6. `cd src/DotNetStatus`
7. `grunt`
8. Run the site: `k kestrel`

#### Troubleshooting
1. `kpm restore` cannot find Microsoft.Fx.Portability
   * Download the NuGet directly from [myget.org](https://www.myget.org/gallery/dotnet-apiport) and unpack it manually
   * Execute: `wget https://www.myget.org/F/dotnet-apiport/api/v2/package/Microsoft.Fx.Portability/1.0.0-alpha-15031101`
   * Execute: `mkdir ~/.k/packages/Microsoft.Fx.Portability && unzip 1.0.0-alpha-15031101 -d ~/.k/packages/Microsoft.Fx.Portability/1.0.0-alpha-15031101`

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
