# markdownsharp
This is a fork of [jrummell's GitHub clone][1] of the [Google Code MarkdownSharp project][2]

I have converted the project to a Universal Windows Platform (UWP) targeted portable class library (with ASP.Net 5 as a secondary target).  By targeting just UWP, I was able to preserve the `RegexOptions.Compile` directive which other PCL packages can't use.

I converted some of the basic unit tests into a UWP UnitTest project, but haven't had time to move the embedded resource tests or benchmarks.

[1]: https://github.com/jrummell/markdownsharp
[2]: http://code.google.com/p/markdownsharp 
