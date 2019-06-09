using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace elemental.Projectiles
{
	//ported from my tAPI mod because I don't want to make artwork
	public class FireClaw : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			Main.projFrames[projectile.type] = 28;
        }
		public override void SetDefaults()
		{
            projectile.CloneDefaults(ProjectileID.Arkhalis);
            projectile.timeLeft = 28;
            projectile.frame = (Main.rand.Next(0, 3)*7);
			//projectile.width = 22;
			//projectile.height = 22;
			//projectile.aiStyle = 75;
			//projectile.friendly = true;
			//projectile.penetrate = -1;
			//projectile.tileCollide = false;
			//projectile.hide = true;
			//projectile.ownerHitCheck = true; //so you can't hit enemies through walls
			//projectile.melee = true;
		}

		public override void AI()
		{
			int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 6, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 100, Color.Firebrick, 1.9f);
			Main.dust[dust].noGravity = true;
            if(Main.player[projectile.owner].direction == -1){
                projectile.rotation += 90f;
            }else{
                projectile.rotation -= 90f;
            }
			/*if (Main.player[projectile.owner].direction != -1)
			{
                projectile.rotation += 180;
			}*/
            if (++projectile.frame >= 14)
            {
                projectile.Kill();
                //projectile.frame = 0;
            }
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			target.AddBuff(BuffID.Daybreak, 180, false);
		}

		public override void OnHitPvp(Player target, int damage, bool crit)
		{
			target.AddBuff(BuffID.Daybreak, 180, false);
		}

		/*public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			SpriteEffects spriteEffects = SpriteEffects.None;
			if (Main.player[projectile.owner].direction == -1)
			{
				spriteEffects = SpriteEffects.FlipHorizontally;
			}
			Texture2D texture = Main.projectileTexture[projectile.type];
			int frameHeight = Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type];
			int startY = frameHeight * projectile.frame;
			Rectangle sourceRectangle = new Rectangle(0, startY, texture.Width, frameHeight);
			Vector2 origin = sourceRectangle.Size() / 2f;
			origin.X = (float)((projectile.spriteDirection == 1) ? (sourceRectangle.Width - 20) : 20);

			Color drawColor = Color.White;//projectile.GetAlpha(lightColor);
			Main.spriteBatch.Draw(texture, 
				projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), 
				sourceRectangle, drawColor, projectile.rotation, origin, projectile.scale, spriteEffects, 0f);

			return false;
        }*/
	}
}