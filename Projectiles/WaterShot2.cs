using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace elemental.Projectiles
{

    public class WaterShot2 : ModProjectile
    {
        public override void SetDefaults()
        {
            //projectile.name = "Water";  //projectile name
            projectile.width = 20;       //projectile width
            projectile.height = 20;  //projectile height
            projectile.friendly = true;      //make that the projectile will not damage you
            projectile.magic = true;         // 
            projectile.tileCollide = true;   //make that the projectile will be destroed if it hits the terrain
            projectile.penetrate = 2;      //how many npc will penetrate
            projectile.timeLeft = 900;   //how many time this projectile has before disepire
            projectile.light = 0f;    // projectile light
            projectile.extraUpdates = 1;
            projectile.ignoreWater = true;   
            projectile.aiStyle = 1;
			//projectile.aiStyle = 14;
        }
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Water, lol");
		}
        public override void AI()           //this make that the projectile will face the corect way
        {                                                           // |
            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
            if(projectile.wet){
                Vector2 move = Vector2.Zero;
                float distance = 400f;
                bool target = false;
                for (int k = 0; k < 200; k++)
                {
                    if (Main.npc[k].active && !Main.npc[k].dontTakeDamage && !Main.npc[k].friendly && Main.npc[k].lifeMax > 5)
                    {
                        Vector2 newMove = Main.npc[k].Center - projectile.Center;
                        NPC npc = Main.npc[k];
                        float distanceTo = (float)Math.Sqrt(newMove.X * newMove.X + newMove.Y * newMove.Y);
                        if (distanceTo < distance && npc.type != NPCID.TargetDummy && npc.wet)
                        {
                            move = newMove;
                            distance = distanceTo;
                            target = true;
                        }
                    }
                }
                if (target)
                {
                    AdjustMagnitude(ref move);
                    projectile.velocity = (10 * projectile.velocity + move) / 11f;
                    //AdjustMagnitude(ref projectile.velocity);
                }
            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity){
            Vector2 diff = oldVelocity - projectile.velocity;
            projectile.velocity -= diff*0.8f;
            return false;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.immune[projectile.owner] = 5;
			target.AddBuff(mod.BuffType("WaterDebuff"), 600);
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor){
            for(int i = 0; i < 5; i++){
                Vector2 offset = new Vector2(0, Main.rand.NextFloat(0, projectile.height));
                offset = offset.RotatedByRandom(180);
                int a = Dust.NewDust(projectile.position + offset, 0, 0, Main.rand.Next(new int[]{29,33,41,45}));
                Main.dust[a].velocity /= 2;
                Main.dust[a].scale /= 2;
                Main.dust[a].noGravity = true;
            }
            return false;
        }

		private void AdjustMagnitude(ref Vector2 vector)
		{
			float magnitude = (float)Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
			if (magnitude > 6f)
			{
				vector *= 6f / magnitude;
			}
		}
    }
}