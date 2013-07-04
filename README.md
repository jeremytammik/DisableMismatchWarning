DisableMismatchWarning
======================

Disable Processor Architecture Mismatch Warning MSB3270 in Revit Projects

A standard Revit 2014 add-in project will generate a warning MSB3270 saying:

There was a mismatch between the processor architecture of the project being built "MSIL" and the processor architecture of the reference "RevitAPI, Version=2014.0.0.0, Culture=neutral, processorArchitecture=x86", "AMD64". This mismatch may cause runtime failures. Please consider changing the targeted processor architecture of your project through the Configuration Manager so as to align the processor architectures between your project and references, or take a dependency on references with a processor architecture that matches the targeted processor architecture of your project.

This can be supressed by adding a new property group to the project file.

This C# .NET command line console application achieves this recursively for all C# and VB Visual Studio project files in and under the current working directory.

For more detailes, please refer to The Building Coder Revit API blog:

http://thebuildingcoder.typepad.com

http://thebuildingcoder.typepad.com/blog/2013/06/processor-architecture-mismatch-warning.html
