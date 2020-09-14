using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria.Enums;

namespace elemental.Projectiles
{

    public class FireWhip : ModProjectile
    {
        public override string Texture => "elemental/Projectiles/FireBall";
        public override bool CloneNewInstances => true;
        Vector2 oldPos2;
        Vector2 oldPos;
        float oldRot;
        float oldRot2;
        float oldRot3;
        int prev = -1;
        int next = -1;
        int count = 0;
        public override void SetDefaults()
        {
            //projectile.name = "Ice Shield";  //projectile name
            projectile.width = 3;       //projectile width
            projectile.height = 3;  //projectile height
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.magic = true;         // 
            projectile.tileCollide = false;   //make that the projectile will be destroed if it hits the terrain
            projectile.penetrate = -1;      //how many npc will penetrate
            projectile.timeLeft = 600;
            projectile.extraUpdates = 1;
            projectile.ignoreWater = true;   
			projectile.aiStyle = 0;
            oldPos = projectile.position;
            oldPos2 = projectile.position;
        }
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Pandemonium Flame");
		}
        public override void AI(){
            Player player = Main.player[projectile.owner];
            ElementalPlayer modPlayer = player.GetModPlayer<ElementalPlayer>();
            modPlayer.FireWhip = true;
            Vector2 mousePos = Main.MouseWorld;
            oldPos2 = oldPos;
            oldPos = projectile.Center;
            oldRot3 = oldRot2;
            oldRot2 = oldRot;
            oldRot = projectile.rotation;
            try{ 
                if(prev == -1){
                    projectile.rotation = (float)Math.Atan2((player.Center - mousePos).Y, (player.Center - mousePos).X)+2.35619f;
                    projectile.Center = player.Center + new Vector2(0, 16).RotatedBy(projectile.rotation - 0.79919);
                }else{
                    if(!Main.projectile[prev].active){
                        projectile.Kill();
                        return;
                    }
                    projectile.rotation = ((FireWhip)Main.projectile[prev].modProjectile).oldRot3;
                    projectile.Center = ((FireWhip)Main.projectile[prev].modProjectile).oldPos + new Vector2(0, 16).RotatedBy(projectile.rotation - 0.79919);
                }
                projectile.velocity = new Vector2();
                if(next == -1 && count < 7){
                    next = Projectile.NewProjectile(projectile.Center, new Vector2(), projectile.type, projectile.damage, projectile.knockBack, projectile.owner);
                    ((FireWhip)Main.projectile[next].modProjectile).prev = projectile.whoAmI;
                    ((FireWhip)Main.projectile[next].modProjectile).count = count+1;
                }
            }catch (Exception){projectile.Kill();return;}
            /*for (int i = 0; i<Main.projectile.Length; i++){
                if (projectile.Hitbox.Intersects(Main.projectile[i].Hitbox)&&Main.projectile[i].type != ProjectileType<HeresySword>()){
                    //projectile.damage += (int)(Main.projectile[i].damage*0.1f);
                    //Main.projectile[i].velocity.;
                }
            }*/
            if (modPlayer.channelsword > 0 && projectile.timeLeft < 575){
                projectile.timeLeft = 575;
            }
            if (modPlayer.channelsword == 0){
                projectile.Kill();
            }
            //ray display
            //Vector2 pos2d = (new Vector2(0,50).RotatedBy(projectile.rotation-MathHelper.ToRadians(45)));
            /*for (int i = 0; i < 10; i++){
                Dust a = Dust.NewDustPerfect(((pos2d)*(2.5f*i/10))+player.MountedCenter, 90, newColor:Color.Goldenrod);
                a.noGravity = true;
                a.velocity*=0;
            }*/
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor){
			if(projectile.timeLeft<595-(count*3))for(int i = 0; i < 3; i++){
			Dust.NewDust(projectile.Center, 0, 0, 164, 0, 0, 0, Color.Orange, 0.5f);
			Dust.NewDustPerfect(projectile.Center, 164, new Vector2(0, 8).RotatedBy(projectile.rotation - 0.79919), 0, Color.OrangeRed, 0.5f);
			}
            return false;
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection){
            Player player = Main.player[projectile.owner];
            if(projectile.timeLeft>=599){
                oldPos = projectile.position;
                crit = true;
                knockback=0;
            }
            if(projectile.timeLeft>=598)oldPos2 = oldPos;
            damage=(int)(damage*(projectile.position-oldPos2).Length()/(projectile.timeLeft>=599?6:16));
            knockback=0;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit){
            if(projectile.timeLeft>=599)oldPos = projectile.position;
            if(projectile.timeLeft>=598)oldPos2 = oldPos;
            if(projectile.timeLeft>=599)oldRot = projectile.rotation;
            if(projectile.timeLeft>=598)oldRot2 = oldRot;
            if(projectile.timeLeft>=597)oldRot3 = oldRot2;
            target.velocity += new Vector2(0, Math.Abs(projectile.rotation-oldRot3)*12).RotatedBy(-oldRot3 - 0.79919);
        }

    }
    public class FireWhipAlt : ModProjectile
    {
        public override string Texture => "elemental/Projectiles/FireBall";
        public override bool CloneNewInstances => true;
        Vector2 oldPos2;
        Vector2 oldPos;
        public override void SetDefaults()
        {
            //projectile.name = "Ice Shield";  //projectile name
            projectile.width = 3;       //projectile width
            projectile.height = 3;  //projectile height
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.magic = true;         // 
            projectile.tileCollide = false;   //make that the projectile will be destroed if it hits the terrain
            projectile.penetrate = -1;      //how many npc will penetrate
            projectile.timeLeft = 600;
            projectile.extraUpdates = 1;
            projectile.ignoreWater = true;   
			projectile.aiStyle = 0;
            oldPos = projectile.position;
            oldPos2 = projectile.position;
        }
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Pandemonium Flame");
		}
        public override void AI(){
            Player player = Main.player[projectile.owner];
            ElementalPlayer modPlayer = player.GetModPlayer<ElementalPlayer>();
            modPlayer.FireWhip = true;
            Vector2 mousePos = Main.MouseWorld;
            oldPos2 = oldPos;
            oldPos = projectile.Center;
            projectile.rotation = (float)Math.Atan2((player.Center - mousePos).Y, (player.Center - mousePos).X)+2.35619f;
            projectile.Center = player.Center + new Vector2(0, 42).RotatedBy(projectile.rotation - 0.79919);
            projectile.velocity = new Vector2();
            /*for (int i = 0; i<Main.projectile.Length; i++){
                if (projectile.Hitbox.Intersects(Main.projectile[i].Hitbox)&&Main.projectile[i].type != ProjectileType<HeresySword>()){
                    //projectile.damage += (int)(Main.projectile[i].damage*0.1f);
                    //Main.projectile[i].velocity.;
                }
            }*/
            if (modPlayer.channelsword > 0 && projectile.timeLeft < 590){
                projectile.timeLeft = 575;
            }
            if (modPlayer.channelsword == 0){
                projectile.Kill();
            }
            //ray display
            //Vector2 pos2d = (new Vector2(0,50).RotatedBy(projectile.rotation-MathHelper.ToRadians(45)));
            /*for (int i = 0; i < 10; i++){
                Dust a = Dust.NewDustPerfect(((pos2d)*(2.5f*i/10))+player.MountedCenter, 90, newColor:Color.Goldenrod);
                a.noGravity = true;
                a.velocity*=0;
            }*/
        }
    }
}