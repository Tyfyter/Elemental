using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace elemental.Buffs
{
    public class IncinerateDebuff : ModBuff
    {
        public override void SetDefaults()
        {
			DisplayName.SetDefault("Incineration");
			Description.SetDefault("Burn with me.");
            //Main.buffName[Type] = "Wet"; //the name displayed when hovering over the buff ingame.
            //Main.buffTip[Type] = "Why is this water so heavy?"; //The description of the buff shown when hovering over ingame.          
            Main.debuff[Type] = true;   //Tells the game if this is a buff or not.
			canBeCleared = false;
        }
    }
}