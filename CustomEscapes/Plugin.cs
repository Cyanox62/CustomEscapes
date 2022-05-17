using Exiled.API.Features;
using System;
using System.Collections.Generic;

namespace CustomEscapes
{
    public class Plugin : Plugin<Config>
    {
		internal static Plugin singleton;
		internal List<List<RoleType>> escapeList = new List<List<RoleType>>();

		private EventHandlers ev;

		public override void OnEnabled()
		{
			base.OnEnabled();

			singleton = this;

			ev = new EventHandlers();
			Exiled.Events.Handlers.Server.RestartingRound += ev.OnRoundRestart;
			Exiled.Events.Handlers.Server.RoundStarted += ev.OnRoundStart;
			Exiled.Events.Handlers.Player.Escaping += ev.OnEscape;

			LoadConfigs();
		}

		public override void OnDisabled()
		{
			base.OnDisabled();

			Exiled.Events.Handlers.Server.RestartingRound -= ev.OnRoundRestart;
			Exiled.Events.Handlers.Server.RoundStarted -= ev.OnRoundStart;
			Exiled.Events.Handlers.Player.Escaping -= ev.OnEscape;
			ev = null;
		}

		public override string Author => "Cyanox";

		private void LoadConfigs()
		{
			foreach (string str in singleton.Config.CuffedEscapeRoles)
			{
				string[] split = str.Split(':');
				if (split.Length == 3)
				{
					if (Enum.TryParse(split[0], out RoleType r1))
					{
						if (Enum.TryParse(split[1], out RoleType r2))
						{
							if (Enum.TryParse(split[2], out RoleType r3))
							{
								escapeList.Add(new List<RoleType>() { r1, r2, r3 });
							} 
							else Log.Error($"Error in config line '{str}' - Third argument is invalid, skipping...");
						} 
						else Log.Error($"Error in config line '{str}' - Second argument is invalid, skipping...");
					}
					else Log.Error($"Error in config line '{str}' - First argument is invalid, skipping...");
				}
				else Log.Error($"Error in config line '{str}' - Expected three arguments, skipping...");
			}
		}
	}
}
