// ****************************** Module Header ****************************** //
//
//
// Last Modified: 19:11:2017 / 19:09
// Creation: 19:11:2017
// Project: AstroSoundBoard
//
//
// <copyright file="ResourceService.cs" company="Patrick Hollweck" GitHub="https://github.com/FetzenRndy">//</copyright>
// *************************************************************************** //

namespace AstroSoundBoard.Services.Resource
{
    using System.Globalization;
    using System.Resources;

    using AstroSoundBoard.Properties;

    public class ResourceService
    {
        public static ResourceSet GetResourceSet()
        {
            return Resources.ResourceManager.GetResourceSet(CultureInfo.CurrentUICulture, true, true);
        }
    }
}