using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace elemental.Projectiles
{

    public class FireBall : ModProjectile
    {
        public override bool CloneNewInstances => true;
        int ticks = -10;
        public override void SetDefaults()
        {
            //projectile.name = "Water Shot";  //projectile name
            projectile.width = 5;       //projectile width
            projectile.height = 5;  //projectile height
            projectile.friendly = true;      //make that the projectile will not damage you
            projectile.magic = true;         // 
            projectile.tileCollide = true;   //make that the projectile will be destroed if it hits the terrain
            projectile.penetrate = 2;      //how many npc will penetrate
            projectile.timeLeft = 900;   //how many time this projectile has before disepire
            projectile.light = 0.0f;    // projectile light
            projectile.ignoreWater = true;   
        }
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Crystal Shard"); Original name
			DisplayName.SetDefault("Pandemonium Blaze");
		}
        public override void AI()           //this make that the projectile will face the corect way
        {                                                           // |
            projectile.rotation = MathHelper.ToRadians(projectile.timeLeft*6);
            if(ticks==-10){
                ticks = (int)(projectile.ai[1]*1.5);
            }else if(ticks>0){
                projectile.velocity = projectile.velocity.RotatedBy(-projectile.ai[0]/projectile.ai[1]);
                ticks--;
            }
        }
		public override bool PreDraw (SpriteBatch spriteBatch, Color lightColor)
		{
			for(int i = 0; i < 3; i++){
			Dust.NewDust(projectile.Center, 1, 1, 164, 0, 0, 0, Color.Orange, 0.5f);
			Dust.NewDust(projectile.Center, 1, 1, 164, projectile.velocity.X, projectile.velocity.Y, 0, Color.OrangeRed, 0.5f);
			}
			return false;
		}
		public override void ModifyHitNPC (NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection){
			damage += (int)(target.defense*projectile.ai[0]);
			if(crit && projectile.ai[0] != 0.25f){
				damage += (int)(target.defense*projectile.ai[0]);
			}
		}
		public override void ModifyHitPvp (Player target, ref int damage, ref bool crit){
			damage += (int)(target.statDefense*projectile.ai[0]);
		}
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
			target.AddBuff(mod.BuffType("ChaosDebuff"), 180);
        }
		
		
    }
}