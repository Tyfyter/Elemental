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

    public class LightningBlast : ModProjectile
    {
        private const int maxCharge = 150;
        private int _charge;                //The charge level of the weapon
        public override void SetDefaults()
        {
            //projectile.name = "Lightning Blast";  //projectile name
            projectile.width = 20;       //projectile width
            projectile.height = 20;  //projectile height
            projectile.friendly = true;      //make that the projectile will not damage you
            projectile.magic = true;         // 
            projectile.tileCollide = true;   //make that the projectile will be destroed if it hits the terrain
            projectile.penetrate = -1;      //how many npc will penetrate
            projectile.timeLeft = 3600;   //how many time this projectile has before disepire
            projectile.light = 0.75f;    // projectile light
            projectile.extraUpdates = 1;
            projectile.ignoreWater = true;   
			projectile.aiStyle = 0;
        }
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Lightning Blast");
		}
        public override void AI()           //this make that the projectile will face the corect way
        {                                                           // |
            Player player = Main.player[projectile.owner];
            Vector2 mousePos = Main.MouseWorld;
            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
            Vector2 unit = (player.Center - mousePos);
            unit.Normalize();
            unit *= -1;
            if (_charge < maxCharge && player.channel)
            {
                _charge++;
                projectile.damage += (int)(_charge/2);
            }
            if (player.channel)
            {
                projectile.position = player.Center + unit;
            }

        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.immune[projectile.owner] = 0;
            if ((float)Main.rand.Next(100) <= (float)(_charge/2)) {
                target.AddBuff(mod.BuffType("LightningDebuff"), 600);
            }
        }
    }
}