using System;
using elemental.Dusts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace elemental.Projectiles
{

    public class IceBallExpl : ModProjectile
    {
        public override bool CloneNewInstances => true;
        public override string Texture => "elemental/Items/IceBallItem";
        public bool explode = false;
        public override void SetDefaults()
        {
            //projectile.name = "Water Shot";  //projectile name
            projectile.width = 1;       //projectile width
            projectile.height = 1;  //projectile height
            projectile.friendly = true;      //make that the projectile will not damage you
            projectile.magic = true;         // 
            projectile.tileCollide = true;   //make that the projectile will be destroed if it hits the terrain
            projectile.penetrate = 2;      //how many npc will penetrate
            projectile.timeLeft = 10;   //how many time this projectile has before disepire
            projectile.extraUpdates = 0;
            projectile.ignoreWater = false;
            projectile.aiStyle = 1;
            projectile.localNPCHitCooldown = 10;
            projectile.usesLocalNPCImmunity = true;
        }
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Frigid Sphere");
		}
		public override bool PreDraw (SpriteBatch spriteBatch, Color lightColor)
		{
			return false;
		}
        public override void ModifyDamageHitbox(ref Rectangle hitbox){
            int a = 479/10*(10-projectile.timeLeft);
            hitbox.Inflate(a,a);
			for(int i = 0; i < 60; i++){
			    Dust.NewDustDirect(projectile.Center+new Vector2(a,0).RotatedBy(MathHelper.ToRadians(i+a/2)*6), 0, 0, mod.DustType<IceExplDust>(), 0, 0, 0, Color.LightCyan, 1f).customData = new TwoColorsAndANumber(0, Color.Cyan, Color.DarkBlue);
			}
        }
    }
}