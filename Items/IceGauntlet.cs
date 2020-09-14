using elemental.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace elemental.Items
{
	public class IceGauntlet : ElementalItem
	{
        public override int Elements => 4;
        public override void SetDefaults()
		{
			//item.name = "Ice Gauntlet";
			item.damage = 100;
            //item.magic = true;
            item.melee = true;
            item.noMelee = false;
            item.noUseGraphic = false;
            item.channel = true;
            item.width = 26;
			item.height = 26;
			item.useTime = 1;
			item.useAnimation = 1;
            item.useStyle = 5;
            item.knockBack = 10;        
			item.value = 10000;
			item.rare = 2;
            item.useStyle = 5;
            item.shoot = 1;
            item.shootSpeed = 1f;
            item.autoReuse = false;
            item.glowMask = 207;
        }
		
		public override void SetStaticDefaults()
		{
		  DisplayName.SetDefault("Ice Gauntlet");
		  Tooltip.SetDefault("");
		}

        public override void HoldStyle(Player player)
        {
            ElementalPlayer modPlayer = player.GetModPlayer<ElementalPlayer>();
            if ((!(player.velocity.Y > 0)) || player.sliding)
            {
                int dust3 = Dust.NewDust((player.direction==1?player.Left:player.Right)-new Vector2(4,-4), 0, 0, 92, 0f, 0f, 25, Color.Goldenrod, 1.5f);
                Main.dust[dust3].noGravity = true;
                Main.dust[dust3].velocity = new Vector2(0, 0);
            }
            else
            {
                int dust3 = Dust.NewDust((player.direction==1?player.TopLeft:player.TopRight)-new Vector2(4,-4), 0, 0, 92, 0f, 0f, 25, Color.Goldenrod, 1.5f);
                Main.dust[dust3].noGravity = true;
                Main.dust[dust3].velocity = new Vector2(0, 0);
            }
            //item.toolTip = modPlayer.stone + "";
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
 
        public override bool CanUseItem(Player player)
        {
            //ElementalPlayer modPlayer = player.GetModPlayer<ElementalPlayer>();
            if (player.altFunctionUse == 2)     //2 is right click
            {
 
                item.useTime = 1;
                item.useAnimation = 5;
				item.damage = 100;
                //item.melee = true;
                //item.magic = false;
                //item.noMelee = false;
                //item.noUseGraphic = true;
                item.channel = true;
                item.noUseGraphic = true;
                item.shootSpeed = 2.5f;    //projectile speed when shoot
                item.shoot = ProjectileType<IceShield>();
                item.autoReuse = true;
                item.reuseDelay = 5;
            }
			else
            {

                item.useTime = 2;
                item.useAnimation = 3;
				item.damage = 10;
                //item.noMelee = true;
                //item.melee = false;
                //item.magic = true;
                //item.noUseGraphic = false;
                item.channel = true;
                item.noUseGraphic = false;
                item.shootSpeed = 2.5f;    //projectile speed when shoot
                item.shoot = 128;
                item.autoReuse = true;
                item.reuseDelay = 0;
            }
            return base.CanUseItem(player);
        }

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.IceBlock, 50);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this);
			recipe.AddRecipe();
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (player.altFunctionUse != 2)
            {

                int numberProjectiles = 3 + Main.rand.Next(2); //This defines how many projectiles to shot. 4 + Main.rand.Next(2)= 4 or 5 shots
                    for (int i = 0; i < numberProjectiles; i++)
                    {
                        Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(30)); // This defines the projectiles random spread . 30 degree spread.
                        int a = Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
                    Main.projectile[a].magic = true;
                    Main.projectile[a].friendly = true;
                    Main.projectile[a].hostile = false;
                    Main.projectile[a].timeLeft = 60;
                    Main.projectile[a].penetrate = -1;
                }
                return false;
            }else{

                ElementalPlayer modPlayer = player.GetModPlayer<ElementalPlayer>();
                if (!modPlayer.IceShield && modPlayer.channelice == 0)
                {
                    Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, damage, knockBack, player.whoAmI);
                }
                modPlayer.channelice = 3;
                return false;
            }
        }
    }
}
