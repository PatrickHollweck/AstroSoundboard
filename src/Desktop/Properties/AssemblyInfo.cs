// ****************************** Module Header ****************************** //
//
//
// Last Modified: 26:07:2017 / 13:36
// Creation: 20:06:2017
// Project: AstroSoundBoard
//
//
// <copyright file="AssemblyInfo.cs" company="Patrick Hollweck" GitHub="https://github.com/FetzenRndy">//</copyright>
// *************************************************************************** //

using System.Reflection;
using System.Resources;
using System.Runtime.InteropServices;
using System.Windows;
using log4net.Config;

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
[assembly: AssemblyVersion("1.2.0.0")]
[assembly: AssemblyFileVersion("1.2.0.0")]

// LOG4NET
[assembly: XmlConfigurator(Watch = true)]
[assembly: NeutralResourcesLanguage("en")]