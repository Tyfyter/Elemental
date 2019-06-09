using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace elemental.Projectiles
{

    public class IceBallProj : ModProjectile
    {
        public override bool CloneNewInstances => true;
        public override string Texture => "elemental/Items/IceBallItem";
        public override void SetDefaults()
        {
            //projectile.name = "Water Shot";  //projectile name
            projectile.width = 5;       //projectile width
            projectile.height = 5;  //projectile height
            projectile.friendly = true;      //make that the projectile will not damage you
            projectile.magic = true;         // 
            projectile.tileCollide = true;   //make that the projectile will be destroed if it hits the terrain
            projectile.penetrate = 1;      //how many npc will penetrate
            projectile.timeLeft = 900;   //how many time this projectile has before disepire
            projectile.extraUpdates = 3;
            projectile.ignoreWater = false;
            projectile.aiStyle = 1;
            projectile.usesLocalNPCImmunity = true;
        }
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Crystal Shard"); Original name
			DisplayName.SetDefault("Frigid Sphere");
		}
        public override void AI()           //this make that the projectile will face the corect way
        {                                                           // |
            if(projectile.wet)projectile.Kill();
            if(Math.Abs(projectile.velocity.X)>1)projectile.velocity = projectile.velocity.RotatedBy(projectile.velocity.X>0?-0.01f:0.01f);
        }
        /*public override bool PreKill(int timeLeft){
            if(!explode){
                projectile.localNPCImmunity = new int[]{};
                projectile.damage *= 3;
                explode = true;
                return false;
            }
            return true;
        }*/
        public override void Kill(int timeLeft){
            Projectile.NewProjectile(projectile.Center, new Vector2(), mod.ProjectileType<IceBallExpl>(), projectile.damage*3, 0, projectile.owner);
        }
		public override bool PreDraw (SpriteBatch spriteBatch, Color lightColor)
		{
			for(int i = 0; i < 3; i++){
			Dust.NewDust(projectile.position, 0, 0, 92, 0, 0, 0, Color.LightCyan, 0.5f);
			Dust.NewDust(projectile.position, 0, 0, 92, projectile.velocity.X, projectile.velocity.Y, 0, Color.LightCyan, 0.5f);
			}
			return false;
		}
        public override void ModifyDamageHitbox(ref Rectangle hitbox){
            //if(explode)hitbox.Inflate(475,475);
        }
    }
}