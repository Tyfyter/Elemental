using System;
using elemental.Buffs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace elemental.Projectiles
{

    public class WindShot : ModProjectile
    {
        public override void SetDefaults()
        {
            //projectile.name = "Wind Shot";  //projectile name
            projectile.width = 20;       //projectile width
            projectile.height = 20;  //projectile height
            projectile.friendly = true;      //make that the projectile will not damage you
            projectile.magic = true;         // 
            projectile.tileCollide = true;   //make that the projectile will be destroed if it hits the terrain
            projectile.penetrate = 20;      //how many npc will penetrate
            projectile.timeLeft = 200;   //how many time this projectile has before disepire
            projectile.light = 0.75f;    // projectile light
            projectile.extraUpdates = 1;
            projectile.ignoreWater = true;   
        }
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Wind Shot");
		}
        public override void AI()           //this make that the projectile will face the corect way
        {                                                           // |
            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;  
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.immune[projectile.owner] -= 9;
            float f = target.boss?0.75f:1.5f;
            target.windDebuff(target.boss?60:600, f, f, f);
			//target.AddBuff(BuffType<WindDebuff>(), target.boss?60:600);
        }
    }
}