using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace elemental.Projectiles
{

    public class WaterBlast : ModProjectile
    {
        public override void SetDefaults()
        {
            //projectile.name = "Water Blast";  //projectile name
            projectile.width = 20;       //projectile width
            projectile.height = 28;  //projectile height
            projectile.friendly = true;      //make that the projectile will not damage you
            projectile.magic = true;         // 
            projectile.tileCollide = true;   //make that the projectile will be destroed if it hits the terrain
            projectile.penetrate = -1;      //how many npc will penetrate
            projectile.timeLeft = 35;   //how many time this projectile has before disepire
            projectile.light = 0f;    // projectile light
            projectile.extraUpdates = 1;
            projectile.ignoreWater = true;   
			projectile.aiStyle = 1;
        }
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Water Blast");
		}
        public override void AI()           //this make that the projectile will face the corect way
        {                                                           // |
            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f; 
			if(projectile.timeLeft == 5){
              int numberProjectiles = 7; //This defines how many projectiles to shot. 4 + Main.rand.Next(2)= 4 or 5 shots
              for (int i = 0; i < numberProjectiles; i++)
              {
                  Vector2 perturbedSpeed = new Vector2(projectile.velocity.X*(1+Main.rand.Next(1)), projectile.velocity.Y).RotatedByRandom(MathHelper.ToRadians(30)); // This defines the projectiles random spread . 30 degree spread.
                  Projectile.NewProjectile(projectile.position.X, projectile.position.Y, perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("WaterShot2"), projectile.damage/7, projectile.knockBack, projectile.owner);
				  //int iproj = Projectile.NewProjectile(projectile.position.X, projectile.position.Y, perturbedSpeed.X, perturbedSpeed.Y, 27, projectile.damage/7, projectile.knockBack, projectile.owner);
				  //Main.projectile[iproj].Gravity = true;
              }
			}
			if((int)projectile.ai[0] == 1){
				//projectile.timeLeft = 35;
				//projectile.aiStyle = 0;
			}
			
        }
        public override bool OnTileCollide(Vector2 oldVelocity){
            Vector2 diff = oldVelocity - projectile.velocity;
            projectile.velocity -= diff*0.8f;
			projectile.timeLeft = 6;
            return false;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.immune[projectile.owner] = 0;
			projectile.timeLeft = 6;
			target.AddBuff(mod.BuffType("WaterDebuff"), 600);
        }
    }
}