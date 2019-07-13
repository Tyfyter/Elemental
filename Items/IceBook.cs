using System;
using elemental.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace elemental.Items
{
	public class IceBook : ElementalItem
	{
		int charge = 0;
        int maxcharge = 0;
        public override int Elements => 4;
        public override void SetDefaults()
		{
			//item.name = "Ice Gauntlet";
			item.damage = 20;
            //item.magic = true;
            item.magic = true;
            item.noMelee = true;
            item.noUseGraphic = false;
            item.width = 26;
			item.height = 26;
			item.useTime = 1;
			item.useAnimation = 1;
            item.useStyle = 5;
            item.knockBack = 10;        
			item.value = 10000;
			item.rare = 2;
            item.mana = 5;
            item.useStyle = 5;
            item.shoot = 1;
            item.shootSpeed = 1f;
            item.autoReuse = false;
            item.glowMask = 207;
        }
		
		public override void SetStaticDefaults()
		{
		  DisplayName.SetDefault("Ice Tome");
		  Tooltip.SetDefault("");
		}

        public override void HoldStyle(Player player)
        {
            ElementalPlayer modPlayer = player.GetModPlayer<ElementalPlayer>(mod);
            if ((!(player.velocity.Y > 0)) || player.sliding)
            {
                int dust3 = Dust.NewDust((player.direction==1?player.Left:player.Right)-new Vector2(4,-4), 0, 0, 92, 0f, 0f, 25, Color.Goldenrod, 0.5f);
                Main.dust[dust3].noGravity = true;
                Main.dust[dust3].velocity *= 1.1f;
            }
            else
            {
                int dust3 = Dust.NewDust((player.direction==1?player.TopLeft:player.TopRight)-new Vector2(4,-4), 0, 0, 92, 0f, 0f, 25, Color.Goldenrod, 0.5f);
                Main.dust[dust3].noGravity = true;
                Main.dust[dust3].velocity *= 1.1f;
            }
            //item.toolTip = modPlayer.stone + "";
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
 
        public override bool CanUseItem(Player player)
        {
            //ElementalPlayer modPlayer = player.GetModPlayer<ElementalPlayer>(mod);
            if (player.altFunctionUse != 2)     //2 is right click
            {
                item.useTime = 1;
                item.useAnimation = 7;
                //item.melee = true;
                //item.magic = false;
                //item.noMelee = false;
                //item.noUseGraphic = true;
                item.noUseGraphic = true;
                item.shootSpeed = 6.5f;    //projectile speed when shoot
                item.shoot = ProjectileID.FrostArrow;
            }
			else
            {

                item.useTime = 1;
                item.useAnimation = 7;
                //item.noMelee = true;
                //item.melee = false;
                //item.magic = true;
                //item.noUseGraphic = false;
                item.noUseGraphic = false;
                item.shootSpeed = 2.5f;    //projectile speed when shoot
                item.shoot = 1;
            }
            return base.CanUseItem(player);
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (player.altFunctionUse != 2){
                if(player.controlUseItem&&player.CheckMana(1, true)){
                    player.itemAnimation = item.useAnimation-1;
                    if(charge<90){
                        charge++;
                        if(charge>=90)for(int i = 0; i < 3; i++){
                            int a = Dust.NewDust(position-new Vector2(speedX,speedY), 0, 0, 92);
                            Main.dust[a].noGravity = true;
                        }
                    }else if(Main.time%12<=1){
                        int a = Dust.NewDust(position-new Vector2(speedX,speedY), 0, 0, 92);
                        Main.dust[a].noGravity = true;
                    }
                    if (player.controlUseTile){
                        damage = charge;
                        charge = 0;
                        player.itemAnimation = 0;
                        int ball = Item.NewItem(player.Center, new Vector2(), mod.ItemType<IceBallItem>(), noGrabDelay:true);
                        Main.item[ball].damage = damage;
                    }
                    return false;
                }
                damage = charge;
                speedX*=(charge/120)+1;
                speedY*=(charge/120)+1;
                charge = 0;
                player.itemAnimation = 0;
                type = mod.ProjectileType<IceBallProj>();
                return true;
            }else{
                if(player.controlUseTile&&player.CheckMana(1, true)){
                    player.itemAnimation = item.useAnimation-1;
                    if(charge<15)maxcharge=++charge;
                    Vector2 vel1 = new Vector2(speedX,speedY).RotatedBy(charge/45f)*2;
                    Vector2 vel2 = new Vector2(speedX,speedY).RotatedBy(-charge/45f)*2;
                    for(float i = 0; i < 20+(charge*2f); i+=1.5f){
                        int a = Dust.NewDust(position+(vel1*i), 0, 0, 92);
                        Main.dust[a].noGravity = true;
                        Main.dust[a].velocity*=0;
                        int b = Dust.NewDust(position+(vel2*i), 0, 0, 92);
                        Main.dust[b].noGravity = true;
                        Main.dust[b].velocity*=0;
                    }
                    return false;
                }else if((charge=charge-3)>-1){
                    player.itemAnimation = item.useAnimation-1;
                    Vector2 vel1 = new Vector2(speedX,speedY).RotatedBy(charge/45f)*2;
                    Vector2 vel2 = new Vector2(speedX,speedY).RotatedBy(-charge/45f)*2;
                    for(float i = 0; i < 20+(maxcharge*2f); i+=1.5f){
                        Projectile.NewProjectile(position+(vel1*i), new Vector2(), mod.ProjectileType<IceWaveProj>(), maxcharge*3, 1, player.whoAmI);
                        Projectile.NewProjectile(position+(vel2*i), new Vector2(), mod.ProjectileType<IceWaveProj>(), maxcharge*3, 1, player.whoAmI);
                    }
                    if(charge<=0){
                        maxcharge = 0;
                        charge = 0;
                    }
                    return false;
                }
                return false;
            }
        }
    }
}
