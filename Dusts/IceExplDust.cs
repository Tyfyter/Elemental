using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using elemental.Dusts;

namespace elemental.Dusts
{
	public class IceExplDust : ModDust
	{
        //public static int error = -1;
		public override void OnSpawn(Dust dust) {
			dust.noGravity = true;
            updateType = 92;
		}

		public override bool Update(Dust dust) {
            try{
                dust.color = Color.Lerp(((TwoColorsAndANumber)dust.customData).color, ((TwoColorsAndANumber)dust.customData).secondcolor, (++((TwoColorsAndANumber)dust.customData).number)/10);
                Lighting.AddLight(dust.position, dust.color.ToVector3()/255);
			    if (((TwoColorsAndANumber)dust.customData).number>10)dust.active = false;
                //if(error==-1)error=dust.dustIndex;if(dust.dustIndex==error)Main.NewText(((TwoColorsAndANumber)dust.customData).ToString());
            }
            catch (Exception){}
			return true;
		}
	}
}