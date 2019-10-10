using System;
using System.Collections.Generic;
using elemental.Classes;
using elemental.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace elemental.Items
{
    public class Aphelion : ElementalItem {
        public override int Elements => ElEnum.ice;

        public override void SetDefaults(){
            item.damage = 290;
            item.magic = true;
            item.mana = 90;
            item.shoot = mod.ProjectileType<AphelionSpike>();
            item.shootSpeed = 12.5f;
            item.useTime = item.useAnimation = 13;
            item.useStyle = 5;
            item.noUseGraphic = true;
            item.width = 12;
            item.height = 10;
            item.value = 10000;
            item.rare = 3;
        }
		public override void SetStaticDefaults(){
			DisplayName.SetDefault("Aphelion");
			Tooltip.SetDefault("\"The power of the sun, in the palm of my hand\"");
		}
        public override bool AltFunctionUse(Player player) => true;
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack){
            if(player.itemAnimation == 3)return false;
            Main.PlaySound(2, (int)position.X, (int)position.Y, 30, 0.8f);
            Main.PlaySound(2, (int)position.X, (int)position.Y, 46, 0.5f);
            Main.PlaySound(2, (int)position.X, (int)position.Y, 120, 0.05f);
            (Projectile.NewProjectileDirect(position, new Vector2(speedX, speedY), type, damage, knockBack, item.owner).modProjectile as AphelionSpike).held = player.altFunctionUse == 2;
            return false;
        }
    }
}