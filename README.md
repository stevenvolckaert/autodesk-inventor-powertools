# Steven Volckaert's Inventor® Power Tools

**Steven Volckaert's Inventor® Power Tools** is a collection of productivity tools for Autodesk Inventor® 2012,
distributed as an add-in It
automates some common, repetitive tasks, like generating drawings for each part in an assembly.

After generation, each drawing is left open in the editor, giving the engineer the opportunity to further adapt them,
if necessary.

## Forking and compiling from source

Since Autodesk Inventor® 2012 is built on the .NET Framework 3.5, the project targets `.NET Framework 3.5`. This
framework is not installed by default on Windows 8, Windows 8.1 or Windows 10, meaning you need to install it manually
in order to compile from source. [Installing the .NET Framework 3.5 on Windows 8, Windows 8.1 and Windows 10][1]
explains how to do this.

Using this method didn't work for me on Windows 10, but I managed to install .NET Framework 3.5 using the `DISM.exe`
tool. See [Can't install .NET 3.5 on Windows 10](http://superuser.com/q/946988/319367) on StackExchange for more
information.

## License

Steven Volckaert's Enterprise Library is licensed under the [MIT license](LICENSE).

[1]: https://msdn.microsoft.com/en-us/library/hh506443%28v=vs.110%29.aspx
