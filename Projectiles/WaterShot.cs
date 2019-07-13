using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace elemental.Projectiles
{

    public class WaterShot : ModProjectile
    {
        public override void SetDefaults()
        {
            //projectile.name = "Water Shot";  //projectile name
            projectile.width = 15;       //projectile width
            projectile.height = 15;  //projectile height
            projectile.friendly = true;      //make that the projectile will not damage you
            projectile.magic = true;         // 
            projectile.tileCollide = true;   //make that the projectile will be destroed if it hits the terrain
            projectile.penetrate = 2;      //how many npc will penetrate
            projectile.timeLeft = 900;   //how many time this projectile has before disepire
            projectile.light = 0f;    // projectile light
            projectile.extraUpdates = 1;
            projectile.ignoreWater = true;   
			projectile.aiStyle = 1;
        }
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Water Shot");
		}
        public override void AI()           //this make that the projectile will face the corect way
        {                                                           // |
            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;  
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
			target.AddBuff(mod.BuffType("WaterDebuff"), target.boss?60:600);
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor){
            for(int i = 0; i < 6; i++){
                Vector2 offset = new Vector2(0, Main.rand.NextFloat(0, projectile.height/2));
                offset = offset.RotatedByRandom(180);
                int a = Dust.NewDust(projectile.Center + offset, 0, 0, Main.rand.Next(new int[]{29,33,Dust.dustWater()}), Scale:0.8f);
                Main.dust[a].velocity = projectile.velocity/2;
            }
            return false;
        }
    }
}