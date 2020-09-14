using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria.Enums;
using static Terraria.ModLoader.ModContent;

namespace elemental.Projectiles
{

    public class MaybeSomeKindofSword : ModProjectile
    {
        private const int maxCharge = 3;
        public override void SetDefaults()
        {
            //projectile.name = "Ice Shield";  //projectile name
            projectile.width = 30;       //projectile width
            projectile.height = 60;  //projectile height
            projectile.friendly = true;      //make that the projectile will not damage you
            projectile.magic = true;         // 
            projectile.tileCollide = false;   //make that the projectile will be destroed if it hits the terrain
            projectile.penetrate = -1;      //how many npc will penetrate
            projectile.timeLeft = 3600;   //how many time this projectile has before disepire
            projectile.light = 0.75f;    // projectile light
            projectile.extraUpdates = 1;
            projectile.ignoreWater = true;   
			projectile.aiStyle = 0;
        }
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Maybe Some Kind of Sword, idk.");
		}
        public override void AI()           //this make that the projectile will face the corect way
        {                                                           // |
            Player player = Main.player[projectile.owner];
            ElementalPlayer modPlayer = player.GetModPlayer<ElementalPlayer>();
            modPlayer.IceShield = true;
            Vector2 mousePos = Main.MouseWorld;
            Vector2 unit = (player.Center - mousePos);
            unit.Normalize();
            unit *= -10;
            projectile.position = player.position /*- (new Vector2(35,0))*/ + new Vector2(50).RotatedBy((float)Math.Atan2((player.Center - mousePos).Y, (player.Center - mousePos).X) + 1.557f);
            projectile.rotation = (float)Math.Atan2((player.Center - mousePos).Y, (player.Center - mousePos).X) + 1.8157f;
            for (int i = 0; i<Main.projectile.Length; i++)
            {
                if (projectile.Hitbox.Intersects(Main.projectile[i].Hitbox)&&Main.projectile[i].type != ProjectileType<IceShield>())
                {
                    projectile.damage += (int)(Main.projectile[i].damage*0.1f);
                    //Main.projectile[i].velocity.;
                }
            }
            if (modPlayer.channelsword > 0)
            {
                projectile.timeLeft = 3600;
            }
            if (modPlayer.channelsword == 0)
            {
                //projectile.timeLeft = Math.Min(3,projectile.timeLeft);
                projectile.Kill();
            }
        }
    }
}