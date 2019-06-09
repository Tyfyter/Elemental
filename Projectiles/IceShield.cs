using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria.Enums;
using static elemental.Projectiles.VectorFunctions;

namespace elemental.Projectiles
{

    public class IceShield : ModProjectile
    {
        public override bool CloneNewInstances => true;
        private const int maxCharge = 3;
        Ray ray;
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
			DisplayName.SetDefault("Ice Shield");
		}
        public override void AI()           //this make that the projectile will face the corect way
        {                                                           // |
            Player player = Main.player[projectile.owner];
            ElementalPlayer modPlayer = player.GetModPlayer<ElementalPlayer>(mod);
            modPlayer.IceShield = true;
            Vector2 mousePos = Main.MouseWorld;
            Vector2 unit = (player.Center - mousePos);
            unit.Normalize();
            unit *= -10;
            projectile.Center = player.Center /*- (new Vector2(35,0))*/ + new Vector2(20).RotatedBy((float)Math.Atan2((player.Center - mousePos).Y, (player.Center - mousePos).X) + 2.557f);
            projectile.rotation = (float)Math.Atan2((player.Center - mousePos).Y, (player.Center - mousePos).X) + 3.1157f;
            for (int i = 0; i<Main.projectile.Length; i++)
            {
                if (projectile.Hitbox.Intersects(Main.projectile[i].Hitbox)&&Main.projectile[i].type != mod.ProjectileType("IceShield")&&Main.projectile[i].damage > 0)
                {
                    projectile.ai[0] += Main.projectile[i].damage*0.75f;
                    Main.projectile[i].Kill();
                }
            }
            if (modPlayer.channelice > 0)
            {
                projectile.timeLeft = 3600;
            }
            if (modPlayer.channelice == 0)
            {
                Vector2 tempzerovector = new Vector2(0, 0);
                Projectile.NewProjectile(projectile.position, tempzerovector, 624, (int)((projectile.damage+projectile.ai[0])/3), projectile.knockBack*3, projectile.owner);
                int numberProjectiles = 3 + Main.rand.Next(2); //This defines how many projectiles to shot. 4 + Main.rand.Next(2)= 4 or 5 shots
                for (int i = 0; i < numberProjectiles; i++)
                {
                    Vector2 perturbedSpeed = new Vector2((player.Center - mousePos).X, (player.Center - mousePos).Y);
                    perturbedSpeed.Normalize();
                    perturbedSpeed *= -3;
                    perturbedSpeed = perturbedSpeed.RotatedByRandom(MathHelper.ToRadians(30)); // This defines the projectiles random spread . 30 degree spread.
                    int a = Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, 177, projectile.damage + (int)projectile.ai[0], projectile.knockBack + (int)projectile.ai[0], player.whoAmI);
                    Main.projectile[a].magic = true;
                    Main.projectile[a].friendly = true;
                    Main.projectile[a].hostile = false;
                    Main.projectile[a].timeLeft = 30+(int)(projectile.ai[0]/3);
                    Main.projectile[a].penetrate = -1;
                }
                //projectile.timeLeft = Math.Min(3,projectile.timeLeft);
                projectile.Kill();
            }
        }
		public override void OnHitNPC (NPC target, int damage, float knockback, bool crit)
		{
            projectile.ai[0] += target.damage;
		}
        public override void ModifyDamageHitbox(ref Rectangle hitbox){
            ray = new Ray(new Vector2().to3(), new Vector2(hitbox.Width,hitbox.Height).to3());
            ray.Position = ray.Position.to2().RotatedBy(projectile.rotation,(ray.Position+(ray.Direction.to2()/2).to3()).to2()).to3();
            ray.Position = (ray.Position+new Vector3(hitbox.X,hitbox.Y,0));
            ray.Direction = ((ray.Direction).to2().RotatedBy(projectile.rotation,ray.Direction.to2()/2).to3());
            hitbox = dothething(ray.Position.to2(),(ray.Position+ray.Direction).to2());
            Main.player[projectile.owner].chatOverhead.NewMessage(ray.ToString(),14);
        }
        Rectangle dothething(Vector2 a, Vector2 b){
            int x = (int)Math.Min(a.X,b.X);
            int y = (int)Math.Min(a.Y,b.Y);
            return new Rectangle(x,y,(int)Math.Max(a.X,b.X)-x,(int)Math.Max(a.Y,b.Y)-y);
        }
    }
    public static class VectorFunctions{
        public static Vector3 to3(this Vector2 input){
            return new Vector3(input.X,input.Y,0);
        }
        public static Vector2 to2(this Vector3 input){
            return new Vector2(input.X,input.Y);
        }
    }
}