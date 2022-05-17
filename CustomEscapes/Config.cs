using Exiled.API.Interfaces;
using System.Collections.Generic;
using System.ComponentModel;

namespace CustomEscapes
{
	public class Config : IConfig
	{
		public bool IsEnabled { get; set; } = true;

		[Description("Determines what a player will turn into based on the class of the cuffer. Format - Cuffer:Target:NewRole")]
		public List<string> CuffedEscapeRoles { get; set; } = new List<string>()
		{
			"ClassD:NtfPrivate:ChaosConscript"
		};

		[Description("Determines the interval to check for escapes.")]
		public float RefreshRate { get; set; } = 1f;
	}
}
