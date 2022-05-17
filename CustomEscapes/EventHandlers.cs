using Exiled.API.Features;
using Exiled.Events.EventArgs;
using MEC;
using System.Collections.Generic;
using System.Linq;

namespace CustomEscapes
{
	class EventHandlers
	{
		private CoroutineHandle coroutine;

		internal void OnRoundStart()
		{
			if (coroutine.IsRunning) Timing.KillCoroutines(coroutine);
			coroutine = Timing.RunCoroutine(EscapeCoroutine());
		}

		internal void OnRoundRestart()
		{
			if (coroutine.IsRunning) Timing.KillCoroutines(coroutine);
		}

		// class-d & scientists
		internal void OnEscape(EscapingEventArgs ev) => Escape(ev.Player, ev);

		// all other roles
		private void Escape(Player player, EscapingEventArgs ev = null)
		{
			if (player.IsCuffed && player.Cuffer != null)
			{
				foreach (var entry in Plugin.singleton.escapeList)
				{
					if (player.Cuffer.Role.Type == entry[0] && player.Role.Type == entry[1])
					{
						if (ev != null) ev.NewRole = entry[2];
						else player.SetRole(entry[2]);
					}
				}
			}
		}

		private IEnumerator<float> EscapeCoroutine()
		{
			while (Round.IsStarted)
			{
				yield return Timing.WaitForSeconds(1f);
				foreach (Player p in Player.List.Where(p => p.IsAlive && (p.Position - new UnityEngine.Vector3(173.4f, 985.2f, 31.2f)).magnitude <= 8).ToList()) Escape(p);
			}
		}
	}
}
