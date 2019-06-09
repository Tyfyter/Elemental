using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace elemental.Items
{
	public class fire_claw : ElementalItem
	{
        public override int Elements => 17;
		public override void SetDefaults()
		{
			//item.name = "Fire";
            item.CloneDefaults(ItemID.Arkhalis);
			item.damage = 100;
            item.melee = true;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.width = 32;
			item.height = 32;
			item.useTime = 5;
			item.useAnimation = 5;
            //item.useStyle = 5;
            item.knockBack = 10;        
			item.value = 10000;
			item.rare = ItemRarityID.Quest;
            item.shoot = mod.ProjectileType("FireClaw");
            item.autoReuse = true;
        }
		
		public override void SetStaticDefaults()
		{
		  DisplayName.SetDefault("Blazing Talons");
          Tooltip.SetDefault("Claws are hard to draw, ok?");
		  //Tooltip.SetDefault("\"unguibus ignis atque flammarum\"\nAKA hot knives.");
		}

        public override void HoldStyle(Player player)

        {
            if ((!(player.velocity.Y > 0)) || player.sliding)
            {
                int dust = Dust.NewDust((player.Center - new Vector2((player.width*player.direction/2)-(float)(1.5*(player.direction-2)),-5)) + new Vector2 (3, 0), 0, 0, 6, 0f, 0f, 25, Color.Firebrick, 0.5f);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].velocity /= 2;
                dust = Dust.NewDust((player.Center - new Vector2((player.width*player.direction/2)-(float)(1.5*(player.direction-2)),-5)), 0, 0, 6, 0f, 0f, 25, Color.Firebrick, 0.5f);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].velocity /= 2;
                dust = Dust.NewDust((player.Center - new Vector2((player.width*player.direction/2)-(float)(1.5*(player.direction-2)),-5)) - new Vector2 (3, 0), 0, 0, 6, 0f, 0f, 25, Color.Firebrick, 0.5f);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].velocity /= 2;
            }
            else
            {
                int dust3 = Dust.NewDust(player.Top + new Vector2(player.direction * -10, 0), 0, 0, 6, 0f, 0f, 25, Color.Firebrick, 2.5f);
                Main.dust[dust3].noGravity = true;
                Main.dust[dust3].velocity /= 2;
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
                player.MinionNPCTargetAim();
                item.useTime = 28;
                item.useAnimation = 28;
                item.damage = 400;
                item.mana = 0;
                if(Main.npc.IndexInRange(player.MinionAttackTargetNPC)){
                    item.mana = 45;
                    Main.PlaySound(2, player.Center, 20);
                    player.Teleport(Main.npc[player.MinionAttackTargetNPC].position+new Vector2(Main.npc[player.MinionAttackTargetNPC].direction*(Main.npc[player.MinionAttackTargetNPC].width), 0), 1);
                }
                //item.summon = true;
				//item.shoot = 327;


            }
			else{
			item.useTime = 7;
			item.useAnimation = 7;
            item.damage = 100;
            item.mana = 0;
            //item.summon = false;
            //item.shoot = mod.ProjectileType("FireWhip");
			}
            return base.CanUseItem(player);
        }

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.FragmentSolar, 18);
            recipe.AddIngredient(null, "ChaosMaterial", 20);   //you need 20 Wind
			recipe.AddTile(TileID.LivingFire);
			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.SetResult(this);
			recipe.AddRecipe();
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 speed = new Vector2(speedX, speedY);
            if (player.altFunctionUse == 2 && Main.npc.IndexInRange(player.MinionAttackTargetNPC))
            {
                speedY = 0;
                if(Main.npc[player.MinionAttackTargetNPC].direction == 1){
                    speedX = -speed.Length();
                }else if(Main.npc[player.MinionAttackTargetNPC].direction == -1){
                    speedX = speed.Length();
                }
                /*if (player.itemAnimation <= 30)
                {
                    int a = Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, damage, knockBack, player.whoAmI);
                    Main.projectile[a].magic = true;
                    Main.projectile[a].friendly = true;
                    Main.projectile[a].hostile = false;
                    Main.projectile[a].penetrate = -1;
                    Vector2 mousePos = Main.MouseWorld;
                    Vector2 unit = (player.Center - mousePos);
                    unit.Normalize();
                    unit *= -1;
                    player.velocity += unit;
                }
                return false;*/
            }
            return true;
        }
    }
}
