# AstroKitty Soundboard

![HeaderImage](https://github.com//FetzenRndy/AstroSoundboard/blob/master/src/AstroSoundBoard/Resources/Images/SplashScreen.png?raw=true)

---

[![CodeFactor](https://www.codefactor.io/repository/github/fetzenrndy/astrosoundboard/badge)](https://www.codefactor.io/repository/github/fetzenrndy/astrosoundboard) [![Build Status](https://travis-ci.org/FetzenRndy/AstroSoundboard.svg?branch=master)](https://travis-ci.org/FetzenRndy/AstroSoundboard) [![Gitter](https://img.shields.io/gitter/room/nwjs/nw.js.svg)](https://gitter.im/AstroSoundboard/Lobby) [![CodeFactor](https://img.shields.io/badge/YouTrack-Online-green.svg)](https://bugline.myjetbrains.com/youtrack/issues/AstroBoard) [![Github All Releases](https://img.shields.io/github/downloads/FetzenRndy/AstroSoundBoard/total.svg)](https://github.com/FetzenRndy/AstroSoundboard/) [![GitHub release](https://img.shields.io/github/release/FetzenRndy/AstroSoundboard.svg)](https://github.com/FetzenRndy/AstroSoundboard)

## How to get the Soundboard?
Download the AstroKittySoundBoard.exe [here](https://github.com/FetzenRndy/AstroSoundboard/releases) and run it. - Thats all.

## Code

A little tour of the Code.

#### Folder Structure
```
├───public
│   └───versions
└───src
    └───AstroSoundBoard
        ├───Core
        │   ├───Components
        │   ├───Objects
        │   │   └───DataObjects
        │   └───Utils
        │       └───Extensions
        ├───Resources
        │   ├───Assets
        │   ├───Fonts
        │   └───Images
        └───WPF
            ├───Base
            ├───Controls
            ├───Pages
            └───Windows
            
```

### Building

If you want to fork you need tom register a new Sentry app, and add the API Key in the Credentials class.


#### Librarays
| AutoUpdater.NET                                         | NHotkey                                             | Fody.Costura                              | Fody.Property Changed                              | Fody                               | log4net                                     | Xaml Material Design Kit                   | Json.NET                                 | SharpRaven                                          |
|---------------------------------------------------------|-----------------------------------------------------|-------------------------------------------|---------------------------------------------------|------------------------------------|---------------------------------------------|--------------------------------------------|------------------------------------------|-----------------------------------------------------|
| A Updating lib                                          | Easy Global Hotkey Handling                         | Dependency embedding                      | Auto Implements IPropChanged                  | Core Package for Fody Plugins      | Logging lib                                 | Xaml Material Design Kit                    | Json Handling                            | Sentry Error Handling lib                           |
| [Source](https://github.com/ravibpatel/AutoUpdater.NET) | [Source](https://github.com/thomaslevesque/NHotkey) | [Source](https://github.com/Fody/Costura) | [Source](https://github.com/Fody/PropertyChanged) | [Source](https://github.com/Fody/) | [Source](https://github.com/apache/log4net) | [Source](http://materialdesigninxaml.net/) | [Source](http://www.newtonsoft.com/json) | [Source](https://github.com/getsentry/raven-csharp) |
