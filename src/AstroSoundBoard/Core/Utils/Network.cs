// ****************************** Module Header ****************************** //
//
//
// Last Modified: 17:04:2017 / 17:19
// Creation: 17:04:2017
// Project: AstroSoundBoard
//
//
// <copyright file="Network.cs" company="Patrick Hollweck" GitHub="https://github.com/FetzenRndy">//</copyright>
// *************************************************************************** //

namespace AstroSoundBoard.Core.Utils
{
	using System.Net.NetworkInformation;

	public class Network
	{
		/// <summary>
		/// Checks if the User is Connected to the Internet
		/// </summary>
		/// <returns>Bool -> Is Connected - True -> Is Not Connected -> False</returns>
		public static bool CheckInternetConnection()
		{
			Ping ping = new Ping();
			PingReply reply = ping.Send("google.com", 1000, new byte[32]);

			return reply?.Status == IPStatus.Success;
		}
	}
}