using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static elemental.Extentions;

namespace elemental.Projectiles
{

    public class EclipseBlade : ModProjectile
    {
        int hitimmune = 5;
        int npchitlast = -1;
        int timehitlast = 0;
        int armorbreak = 2;
        public override bool CloneNewInstances => true;

        public override void SetDefaults()
        {
            //projectile.name = "Water Shot";  //projectile name
            projectile.width = 24;       //projectile width
            projectile.height = 24;  //projectile height
            projectile.friendly = true;      //make that the projectile will not damage you
            projectile.magic = true;         // 
            projectile.tileCollide = true;   //make that the projectile will be destroed if it hits the terrain
            projectile.penetrate = -1;      //how many npc will penetrate
            projectile.timeLeft = 900;   //how many time this projectile has before disepire
            projectile.light = 0f;    // projectile light
            projectile.extraUpdates = 4;
            projectile.ignoreWater = true;   
			projectile.aiStyle = 0;
            projectile.alpha = 25;
            projectile.usesLocalNPCImmunity = true;
        }
        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough){
            width = 20;
            height = 20;
            return base.TileCollideStyle(ref width, ref height, ref fallThrough);
        }
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Eclipse Blade");
		}
        public override void AI()           //this make that the projectile will face the corect way
        {        
            if(projectile.velocity.Length() != 0){
                projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + (float)(0.25*Math.PI); 
            }
            timehitlast++;
            if(hitimmune > 0){
                hitimmune--;
                if(hitimmune == 0) projectile.extraUpdates += 1;
            }
            if(npchitlast != -1 && timehitlast < 20 && timehitlast%3==0){
                NPC target = Main.npc[npchitlast];
			    target.velocity = lerp((projectile.velocity/2+projectile.Center)-target.Center, target.velocity, 1-constrain(target.knockBackResist*1.25f, 0.1f, 1f));
                return;
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if(target.type == NPCID.TargetDummy)return;
            if(Main.rand.Next(0,8)==0)armorbreak++;
            target.defense-=armorbreak;
            npchitlast = target.whoAmI;
            timehitlast = 0;
            /*if(npchitlast != -1 && timehitlast < 15){
			    target.velocity = new Vector2(MathHelper.Lerp(target.velocity.X, projectile.position.X - target.position.X, 0.75f), MathHelper.Lerp(target.velocity.Y, projectile.position.Y - target.position.Y, 0.75f));
                return;
            }*/
			//target.velocity = lerp(target.Center-((projectile.velocity*Math.Max(target.knockBackResist,1))+projectile.Center), target.velocity, 1-constrain(target.knockBackResist*1.25f, 0.1f, 1f));
            if(npchitlast == target.whoAmI){
                float dmg = damage*2;
                dmg*=(target.oldVelocity-target.velocity).Length()+1;
                target.StrikeNPC((int)damage, 0, 0);
            }
            projectile.Center = target.Center;
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection){
            if(npchitlast == target.whoAmI)damage/=4;
        }

        public override bool OnTileCollide(Vector2 oldVelocity){
            if(hitimmune > 0){
                projectile.velocity = oldVelocity;
                hitimmune = 5;
                return false;
            }
            if(npchitlast != -1 && timehitlast < 20){
                projectile.velocity = new Vector2();
                return false;
            }
            return base.OnTileCollide(oldVelocity);
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor){
            Lighting.AddLight(projectile.Center, Color.BlueViolet.ToVector3());
            return true;
        }
    }
}