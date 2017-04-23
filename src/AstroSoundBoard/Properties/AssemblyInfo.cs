// ****************************** Module Header ****************************** //
//
//
// Last Modified: 23:04:2017 / 14:14
// Creation: 23:04:2017
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
[assembly: AssemblyVersion("0.9.0")]
[assembly: AssemblyFileVersion("0.9.0")]

// LOG4NET
[assembly: log4net.Config.XmlConfigurator(Watch = true)]
[assembly: NeutralResourcesLanguage("en")]