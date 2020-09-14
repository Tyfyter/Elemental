using System;
using System.Collections.Generic;
using elemental.Classes;
using elemental.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace elemental.Items
{
    public class Perihelion : ElementalItem {
        public override int Elements => ElEnum.fire;

        public override void SetDefaults(){
            item.damage = 290;
            item.magic = true;
            item.mana = 90;
            item.shoot = ProjectileType<HeliosBeam>();
            item.shootSpeed = 12.5f;
            item.useTime = item.useAnimation = 40;
            item.useStyle = 5;
            item.noUseGraphic = true;
            item.width = 12;
            item.height = 10;
            item.value = 10000;
            item.rare = 3;
        }
		public override void SetStaticDefaults(){
			DisplayName.SetDefault("Perihelion");
			Tooltip.SetDefault("\"The power of the sun, in the palm of my hand\"");
		}
        public override void UpdateInventory(Player player){
            item.mana = (player.GetModPlayer<ElementalPlayer>()).FireWings?22:90;
            item.autoReuse = (player.GetModPlayer<ElementalPlayer>()).FireWings;
        }
        public override float UseTimeMultiplier(Player player){
            return (player.GetModPlayer<ElementalPlayer>()).FireWings?4:1;
        }
        // public override void GetWeaponDamage(Player player, ref int damage){
        //     if(player.GetModPlayer<ElementalPlayer>().FireWings)damage = (int)(damage*0.6f);
        // }
        public override void ModifyWeaponDamage(Player player, ref float add, ref float mult, ref float flat){
            if(player.GetModPlayer<ElementalPlayer>().FireWings)mult*=0.6f;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack){
            Main.PlaySound(2, (int)position.X, (int)position.Y, 14, 0.3f);
            Main.PlaySound(2, (int)position.X, (int)position.Y, 20);
            Main.PlaySound(2, (int)position.X, (int)position.Y, 20, pitchOffset:-0.5f);
            (Projectile.NewProjectileDirect(position, new Vector2(speedX,speedY), type, damage, knockBack, item.owner).modProjectile as HeliosBeam).small = player.GetModPlayer<ElementalPlayer>().FireWings;
            return false;
        }
    }
}