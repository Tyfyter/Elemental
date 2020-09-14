/*using Terraria;
using Terraria.ModLoader;

namespace elemental.Commands
{
	public class LightningCommand : ModCommand
	{
		public override CommandType Type
		{
			get { return CommandType.Chat; }
		}

		public override string Command
		{
			get { return "l"; }
		}

		public override string Usage
		{
			get { return "/l number"; }
		}

		public override string Description 
		{
			get { return "change lightning angle"; }
		}

		public override void Action(CommandCaller player, string input, string[] args)
		{
            player.GetModPlayer<ElementalPlayer>().test = (int)args[0];
		}
	}
}*/