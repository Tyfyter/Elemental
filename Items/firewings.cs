using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using elemental.Projectiles;
using elemental;
using System;
using System.Collections.Generic;
using Terraria.ModLoader.IO;

namespace elemental.Items
{
	public class firewings : ElementalItem
	{
        public override bool CloneNewInstances => true;
        public override int Elements => 1;
        int charge = 0;
        internal bool charged = false;
		public override void SetDefaults()
		{
			//item.name = "Fire";
			item.damage = 65;
            item.magic = true;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.channel = true;
            item.mana = 15;
            item.width = 40;
			item.height = 40;
			item.useTime = 1;
			item.useAnimation = 15;
            item.useStyle = 5;
            item.knockBack = 10;        
			item.value = 10000;
			item.rare = 2;
            item.UseSound = SoundID.DD2_BetsyFireballShot;
            item.shootSpeed = 17.5f;
            item.shoot = ProjectileID.RocketFireworksBoxRed;
            item.autoReuse = false;
        }
		
		public override void SetStaticDefaults()
		{
		  DisplayName.SetDefault("Pyrus");
		  Tooltip.SetDefault(@"Burns, burns, burns.");
          //the ̵r̵i̵n̵g wing of fire
		}

        public override void HoldItem(Player player)
        {
            ElementalPlayer modPlayer = player.GetModPlayer<ElementalPlayer>(mod);
            int dust3 = Dust.NewDust(player.Center-new Vector2(((2-player.direction)*2)+(player.direction==1?0:0.85f), 13), 0, 0, 182, 0f, 0f, 25, Color.Orange, (modPlayer.FireWings||charge>=30)?0.65f:0.55f);
            Main.dust[dust3].noGravity = true;
            Main.dust[dust3].velocity*=(modPlayer.FireWings||charge>=30)?0.23f:0.07f;
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
                item.useTime = 1;
                item.useAnimation = 15;
				item.shoot = mod.ProjectileType<FireFeather>();
            }
			else{
                item.useTime = modPlayer.FireWings?7:17;
                item.useAnimation = modPlayer.FireWings?7:17;
                item.shoot = modPlayer.FireWings?ProjectileID.RocketFireworkYellow:ProjectileID.RocketFireworksBoxRed;
			}
            item.mana = modPlayer.FireWings?3:15;
            return base.CanUseItem(player);
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
			base.ModifyTooltips(tooltips);
            Player player = Main.player[item.owner];
            if(NPC.downedGolemBoss&&!NPC.downedAncientCultist&&!charged)tooltips.Add(new TooltipLine(mod, "GolemNotif", "It feels a little warmer now."));
            if(NPC.downedAncientCultist&&!charged){
                int b = 0;
                foreach (var item in player.inventory){
                    if(item.type==ItemID.FragmentSolar)b+=item.stack;
                }
                tooltips.Add(new TooltipLine(mod, "CultistNotif", "It's reacting strangely to the celestial pillars."+(b>=10?" (Shift click solar fragments x10 to charge)":"")));
            }
            if(charged)tooltips.Add(new TooltipLine(mod, "ChargeNotif", "The power of the sun in the palm of your hand."));
        }
		public override TagCompound Save()
		{
			return new TagCompound {
				{"charged",charged}
			};
		}
		
		public override void Load(TagCompound tag)
		{
			charged = tag.GetBool("charged");
		}
        public override void GetWeaponDamage(Player player, ref int damage){
            if(NPC.downedGolemBoss||charged)damage = (int)(damage*1.3f);
            if(charged)damage = (int)(damage*1.3f);
            base.GetWeaponDamage(player, ref damage);
        }

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.SoulofFlight, 15);
			recipe.AddIngredient(ItemID.FireFeather, 4);
			recipe.AddIngredient(ItemID.LivingFireBlock, 10);
			recipe.AddIngredient(ItemID.ManaCrystal, 1);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            ElementalPlayer modPlayer = player.GetModPlayer<ElementalPlayer>(mod);
            if(modPlayer.FireWings)damage = (int)(damage*1.5f);
            if (player.altFunctionUse == 2){
                if(player.controlUseItem||player.controlUseTile){
                    player.itemAnimation = item.useAnimation-1;
                    if(charge<30)charge++;
                    return false;
                }
                if(charge>=30&&player.CheckMana(30, true)){
                    //Activate wings
                    if(modPlayer.FireWings){
                        player.velocity+=new Vector2(speedX,speedY*1.75f);
                        charge = 0;
                        player.AddBuff(BuffID.SolarShield1, 120);
                    }else{
                        int projb = Projectile.NewProjectile(player.Bottom, new Vector2(0,24), ProjectileID.RocketFireworkYellow, (int)(player.oldVelocity.Y*4), player.oldVelocity.Y/5, player.whoAmI);
                        Main.projectile[projb].timeLeft = 0;
                        Main.projectile[projb].magic = true;
                        Main.projectile[projb].ranged = false;
                        player.velocity.Y = player.velocity.Y>0?-24:player.velocity.Y-24;
                        player.position+=player.velocity;
                        modPlayer.FireWings = true;
                        charge = 0;
                        return false;
                    }
                }
                if(player.itemAnimation%2==0&&charge<20)return false;
                if(charge>=30){
                    charge = 0;
                    return false;
                }
                if(player.itemAnimation==1)charge = 0;
                player.itemAnimation = Math.Min(player.itemAnimation,5);
                Vector2 vec = new Vector2(speedX,speedY).RotatedBy(Math.PI/2).Normalized()*6.5f;
                position = position+(vec*(player.itemAnimation-3)*-player.direction);
                damage = (int)(damage*(modPlayer.FireWings?0.175f:0.48125f));//was 0.1 and 0.275, before that it was 0.15 and 0.35, before that it was 0.35 and 0.75.
                int proja = Projectile.NewProjectile(position, new Vector2(speedX,speedY), type, damage, knockBack, item.owner);
                Main.projectile[proja].usesLocalNPCImmunity = true;
                return false;
            }
            int proj = Projectile.NewProjectile(position, new Vector2(speedX,speedY), type, damage, knockBack, item.owner);
            Main.projectile[proj].friendly = true;
            Main.projectile[proj].hostile = false;
            Main.projectile[proj].magic = true;
            Main.projectile[proj].ranged = false;
            Main.projectile[proj].alpha+=200;
            Main.projectile[proj].extraUpdates = 1;
            return false;
        }
    }
}
