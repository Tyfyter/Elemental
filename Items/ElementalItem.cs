using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using elemental.Projectiles;

namespace elemental.Items
{
	public class ElementalItem : ModItem {
        //32        16    8     4   2    1
        //lightning chaos water ice wind fire
        public virtual int Elements => 0; 
        public override bool Autoload(ref string name){
            return !name.ToLower().Equals("elementalitem");
        }
        public override void GetWeaponDamage(Player player, ref int damage){
            if(player.HasBuff(BuffID.ChaosState)&&(Elements&16)!=0){
                damage=(int)(damage*1.3f);
            }
        }
    }
}