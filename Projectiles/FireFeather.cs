using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace elemental.Projectiles
{

    public class FireFeather : ModProjectile {
        public override bool CloneNewInstances => true;
        public override string Texture => "elemental/Projectiles/FireBall";
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
            projectile.ignoreWater = false;   
        }
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Crystal Shard"); Original name
			DisplayName.SetDefault("Burning Feather");
		}
        public override void AI()           //this make that the projectile will face the corect way
        {                                                           // |
            Lighting.AddLight(projectile.Center,0.5f,0.2f,0);
            projectile.extraUpdates = projectile.wet?0:2;
        }
		public override bool PreDraw (SpriteBatch spriteBatch, Color lightColor)
		{
			for(int i = 0; i < 3; i++){
			    int a = Dust.NewDust(projectile.Center, 1, 1, 182, -projectile.velocity.X, -projectile.velocity.Y, 0, Color.Orange, 0.65f);
                Main.dust[a].noGravity = true;
                Main.dust[a].velocity = new Vector2(-projectile.velocity.X, -projectile.velocity.Y).RotatedBy((Math.PI/9)*(1.5f-i))/2;
            }
			return false;
		}
		public override void ModifyHitNPC (NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection){
			damage += (int)(target.defense*0.375f);
			if(crit){
				damage += (int)(target.defense*0.375f);
			}
		}
		public override void ModifyHitPvp (Player target, ref int damage, ref bool crit){
			damage += (int)(target.statDefense*0.55f);
		}
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
			target.AddBuff(mod.BuffType("ChaosDebuff"), 60);
        }
    }
}