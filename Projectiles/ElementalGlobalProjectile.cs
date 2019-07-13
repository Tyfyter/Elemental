using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;

namespace elemental.Projectiles
{
	public class ElementalGlobalProjectile : GlobalProjectile
	{
		public override bool InstancePerEntity => true;
		public override bool CloneNewInstances => true;
        public List<int> HitEnemies = new List<int>(){};
        public bool TrackHitEnemies = false;
        public List<float?> aioverflow = new List<float?>{};
        public int extraAI = -1;
        public int OverrideTextureInt = 0;
        public Texture2D OverrideTexture = null;
        public bool OverrideColor = false;
        public Color Color = Color.White;
        public bool render = true;
        public bool nullprecull = false;
        public Func<Projectile,bool> preKill;
        public Func<Projectile,bool> preColl;
        public Action<Projectile, NPC, int, bool> onHitNPC;
        public Action<Projectile, Player, int> onHitPlayer;
        public Func<Projectile, NPC, int, int> modHitNPC;
        public Func<Projectile, Player, int, int> modHitPlayer;
        public override void OnHitNPC(Projectile projectile, NPC target, int damage, float knockback, bool crit){
            if(TrackHitEnemies)HitEnemies.Add(target.whoAmI);
            if(onHitNPC!=null){
                onHitNPC.Invoke(projectile, target, damage, crit);
            }
        }
        public override void OnHitPlayer(Projectile projectile, Player target, int damage, bool crit){
            if(onHitPlayer!=null){
                onHitPlayer.Invoke(projectile, target, damage);
            }
        }
        public override void ModifyHitPlayer(Projectile projectile, Player target, ref int damage, ref bool crit){
            if(modHitPlayer!=null){
                damage = modHitPlayer.Invoke(projectile, target, damage);
            }
        }
        public override void ModifyHitNPC(Projectile projectile, NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection){
            if(modHitPlayer!=null){
                damage = modHitNPC.Invoke(projectile, target, damage);
            }
        }
        public override void AI(Projectile projectile){
            if(extraAI==-1)return;
            int a = projectile.type;
            projectile.type = extraAI;
            projectile.VanillaAI();
            projectile.type = a;
        }
        public override bool PreKill(Projectile projectile, int timeLeft){
            bool o = true;
            if(preKill!=null){
                o = preKill.Invoke(projectile);
            }
            return o&&!nullprecull;
        }
        public override bool OnTileCollide(Projectile projectile, Vector2 oldVelocity){
            bool o = true;
            if(preColl!=null){
                o = preColl.Invoke(projectile);
            }
            return o;
        }
        public override bool PreDraw(Projectile projectile, SpriteBatch spriteBatch, Color lightColor){
            if(OverrideTextureInt!=0){
                Main.instance.LoadProjectile(OverrideTextureInt);
                spriteBatch.Draw(Main.projectileTexture[OverrideTextureInt], projectile.Center - Main.screenPosition, new Rectangle(0,0,Main.projectileTexture[OverrideTextureInt].Width,Main.projectileTexture[OverrideTextureInt].Height), lightColor, projectile.rotation, new Rectangle(0,0,Main.projectileTexture[OverrideTextureInt].Width,Main.projectileTexture[OverrideTextureInt].Height).Center(), 1f, SpriteEffects.None, 0f);
            }
            if(OverrideTexture!=null){
                spriteBatch.Draw(OverrideTexture, projectile.Center - Main.screenPosition, new Rectangle(0,0,OverrideTexture.Width,OverrideTexture.Height), lightColor, projectile.rotation, new Rectangle(0,0,OverrideTexture.Width,OverrideTexture.Height).Center(), 1f, SpriteEffects.None, 0f);
            }
            return render;
        }
    }
}