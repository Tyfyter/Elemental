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
            Main.debuff[Type] = true;
			canBeCleared = false;
        }
    }
}