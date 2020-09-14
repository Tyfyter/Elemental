using System;
using elemental.Buffs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace elemental.Projectiles
{

    public class CrystalShot : ModProjectile
    {
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
            projectile.extraUpdates = 3;
            projectile.ignoreWater = true;   
			aiType = 477;
        }
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Crystal Shard"); Original name
			DisplayName.SetDefault("Chaos Beam");
		}
        public override void AI()           //this make that the projectile will face the corect way
        {                                                           // |
            //projectile.rotation = projectile.timeLeft*10;  
        }
		public override bool PreDraw (SpriteBatch spriteBatch, Color lightColor)
		{
			for(int i = 0; i < 3; i++){
			Dust.NewDust(projectile.position, 1, 1, 164, 0, 0, 0, Color.White, 0.5f);
			Dust.NewDust(projectile.position, 1, 1, 164, projectile.velocity.X, projectile.velocity.Y, 0, Color.White, 0.5f);
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
			target.AddBuff(BuffType<ChaosDebuff>(), 180);
        }
		
		
    }
}