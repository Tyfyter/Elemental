using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using elemental.Projectiles;

namespace elemental.Items
{
	public class fire : ElementalItem {
        public override int Elements => 17;
		public override void SetDefaults()
		{
			//item.name = "Fire";
			item.damage = 35;
            item.magic = true;
            item.noMelee = true;
            item.noUseGraphic = false;
            item.channel = true;
            item.mana = 1;
            item.width = 40;
			item.height = 40;
			item.useTime = 1;
			item.useAnimation = 5;
            item.useStyle = 5;
            item.knockBack = 10;        
			item.value = 10000;
			item.rare = 2;
            item.UseSound = SoundID.DD2_BetsyFireballImpact;
            item.shootSpeed = 12.5f;
            item.shoot = mod.ProjectileType("FireWhip");
            item.autoReuse = false;
        }
		
		public override void SetStaticDefaults()
		{
		  DisplayName.SetDefault("Pandemonium Flame");
		  Tooltip.SetDefault("");
		}

        public override void HoldStyle(Player player)
        {
            if (((!(player.velocity.Y > 0))&&!(player.wingTime!=player.wingTimeMax)) || player.sliding)
            {
                int dust3 = Dust.NewDust(player.Center+new Vector2(12*player.direction, 0), 0, 0, 6, 0f, 0f, 25, Color.Magenta, 2.5f);
                Main.dust[dust3].noGravity = true;
                Main.dust[dust3].velocity *= 2;
            }
            else
            {
                int dust3 = Dust.NewDust(player.Top + new Vector2(player.direction * -12, 0), 0, 0, 6, 0f, 0f, 25, Color.Magenta, 2.5f);
                Main.dust[dust3].noGravity = true;
                Main.dust[dust3].velocity *= 2;
            }

        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
 
        public override bool CanUseItem(Player player)
        {
            ElementalPlayer modPlayer = player.GetModPlayer<ElementalPlayer>(mod);
            if (player.altFunctionUse == 2)     //2 is right click
            {
 
                item.useTime = 2;
                item.useAnimation = 14;
				item.shoot = mod.ProjectileType("FireBall");


            }
			else{
                item.useTime = 1;
                item.useAnimation = 5;
                item.shoot = mod.ProjectileType("FireWhip");
			}
            return base.CanUseItem(player) && !modPlayer.FlameShield;
        }

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.LivingFireBlock, 10);
			recipe.AddIngredient(ItemID.SoulofLight, 6);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (player.altFunctionUse == 2){
                Vector2 mousePos = Main.MouseWorld;
                Vector2 unit = (player.Center - mousePos);
                Vector2 speed = new Vector2(speedX,speedY);
                double angle = 
                (
                    (
                        (
                            player.itemAnimation-7f
                        )
                    /14)
                    *(
                        1/(
                            unit.Length()/400
                        )
                    )
                );
                speed = speed.RotatedBy(angle);
                int a = Projectile.NewProjectile(position.X, position.Y, speed.X, speed.Y, type, damage, knockBack, player.whoAmI, (float)angle, 30);
                Main.projectile[a].magic = true;
                Main.projectile[a].friendly = true;
                Main.projectile[a].hostile = false;
                Main.projectile[a].penetrate = -1;
                Main.projectile[a].usesLocalNPCImmunity = true;
                unit.Normalize();
                //unit *= 0.66f;
                player.velocity = player.velocity.Length()>=12?new Vector2(player.velocity.Length(), 0).RotatedBy(MathHelper.Lerp(player.velocity.ToRotation(), unit.ToRotation(), 0.75f)):player.velocity+unit;
                return false;
            }else{
                ElementalPlayer modPlayer = player.GetModPlayer<ElementalPlayer>();
                damage=(int)(damage*2.5f);
				if(player.controlUseItem){
					player.itemAnimation = item.useAnimation-1;
                    modPlayer.channelsword = 5;
                }
                return !modPlayer.FireWhip;
            }
        }
    }
}
