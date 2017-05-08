// ****************************** Module Header ****************************** //
//
//
// Last Modified: 08:05:2017 / 17:11
// Creation: 08:05:2017
// Project: AstroSoundBoard
//
//
// <copyright file="AssemblyInfo.cs" company="Patrick Hollweck" GitHub="https://github.com/FetzenRndy">//</copyright>
// *************************************************************************** //

using System.Reflection;
using System.Resources;
using System.Runtime.InteropServices;
using System.Windows;

// .NET
[assembly: AssemblyTitle("AstroSoundBoard")]
[assembly: AssemblyDescription("A Soundboard for AstroKitty")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("BugLine")]
[assembly: AssemblyProduct("AstroSoundBoard")]
[assembly: AssemblyCopyright("Copyright ©  2017")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// COM
[assembly: ComVisible(false)]

// WPF
[assembly: ThemeInfo(ResourceDictionaryLocation.None, ResourceDictionaryLocation.SourceAssembly)]

// VERSION
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]

// LOG4NET
[assembly: log4net.Config.XmlConfigurator(Watch = true)]
[assembly: NeutralResourcesLanguage("en")]