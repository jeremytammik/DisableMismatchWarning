# DisableMismatchWarning

![Revit API](https://img.shields.io/badge/Revit%20API-2018-blue.svg)
![Platform](https://img.shields.io/badge/platform-Windows-lightgray.svg)
![.NET](https://img.shields.io/badge/.NET-4.5.2-blue.svg)
[![License](http://img.shields.io/:license-mit-blue.svg)](http://opensource.org/licenses/MIT)

Disable Processor Architecture Mismatch Warning MSB3270 in Revit Projects

A standard Revit 2014 add-in project will generate a warning MSB3270 saying:

There was a mismatch between the processor architecture of the project being built "MSIL" and the processor architecture of the reference "RevitAPI, Version=2014.0.0.0, Culture=neutral, processorArchitecture=x86", "AMD64". This mismatch may cause runtime failures. Please consider changing the targeted processor architecture of your project through the Configuration Manager so as to align the processor architectures between your project and references, or take a dependency on references with a processor architecture that matches the targeted processor architecture of your project.

This can be supressed by adding a new property group to the project file.

This C# .NET command line console application achieves this recursively for all C# and VB Visual Studio project files in and under the current working directory.

For more details, please refer to [The Building Coder Revit API blog](http://thebuildingcoder.typepad.com) articles:

- [Processor Architecture Mismatch Warning](http://thebuildingcoder.typepad.com/blog/2013/06/processor-architecture-mismatch-warning.html)
- [Recursively Disable Architecture Mismatch Warning](http://thebuildingcoder.typepad.com/blog/2013/07/recursively-disable-architecture-mismatch-warning.html)
- [DisableMismatchWarning Update](http://thebuildingcoder.typepad.com/blog/2014/09/architecture-mismatch-warning-disabler-update.html#3)


## Author

Jeremy Tammik,
[The Building Coder](http://thebuildingcoder.typepad.com),
[ADN](http://www.autodesk.com/adn)
[Open](http://www.autodesk.com/adnopen),
[Autodesk Inc.](http://www.autodesk.com)


## License

This sample is licensed under the terms of the [MIT License](http://opensource.org/licenses/MIT).
Please see the [LICENSE](LICENSE) file for full details.

