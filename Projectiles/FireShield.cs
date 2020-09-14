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

    public class FireShield : ModProjectile
    {
        private const int maxCharge = 3;
        public override void SetDefaults()
        {
            //projectile.name = "Fire Shield";  //projectile name
            projectile.width = 20;       //projectile width
            projectile.height = 20;  //projectile height
            projectile.friendly = true;      //make that the projectile will not damage you
            projectile.magic = true;         // 
            projectile.tileCollide = true;   //make that the projectile will be destroed if it hits the terrain
            projectile.penetrate = 1;      //how many npc will penetrate
            projectile.timeLeft = 3600;   //how many time this projectile has before disepire
            projectile.light = 0.75f;    // projectile light
            projectile.extraUpdates = 1;
            projectile.ignoreWater = true;   
			projectile.aiStyle = 0;
        }
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Fire Shield");
		}
        public override void AI()           //this make that the projectile will face the corect way
        {                                                           // |
            Player player = Main.player[projectile.owner];
            ElementalPlayer modPlayer = player.GetModPlayer<ElementalPlayer>();
            modPlayer.FlameShield = true;
            Vector2 mousePos = Main.MouseWorld;
            Vector2 unit = (player.Center - mousePos);
            unit.Normalize();
            unit *= -2;
            if (projectile.penetrate < maxCharge && player.channel && projectile.timeLeft == 15)
            {
                projectile.penetrate++;
                projectile.timeLeft = 60;
            }
            projectile.light = projectile.penetrate*(1/3);
            if (player.channel && projectile.timeLeft >= 14)
            {
                projectile.position = player.Center + unit;
                projectile.rotation = (float)Math.Atan2((player.Center - mousePos).Y, (player.Center - mousePos).X) + 101.57f;
            }
            if (!player.channel && projectile.timeLeft == 12)
            {
                projectile.timeLeft = Math.Min(projectile.timeLeft, 15);
                projectile.velocity = unit * 3;
                projectile.damage *= 3;
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.immune[projectile.owner] = 0;
            if ((float)Main.rand.Next(100) <= 33.3) {
                target.AddBuff(24, 600);
            }
        }
    }
}