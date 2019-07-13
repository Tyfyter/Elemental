using System;
using elemental.Buffs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace elemental.Projectiles
{

    public class IceWaveProj : ModProjectile
    {
        public override bool CloneNewInstances => true;
        public override string Texture => "elemental/Items/IceBallItem";
        public override void SetDefaults()
        {
            //projectile.name = "Water Shot";  //projectile name
            projectile.width = 1;       //projectile width
            projectile.height = 1;  //projectile height
            projectile.friendly = true;      //make that the projectile will not damage you
            projectile.magic = true;         // 
            projectile.tileCollide = true;   //make that the projectile will be destroed if it hits the terrain
            projectile.penetrate = 1;      //how many npc will penetrate
            projectile.timeLeft = 3;   //how many time this projectile has before disepire
            projectile.extraUpdates = 0;
            projectile.ignoreWater = true;
            projectile.aiStyle = 1;
            projectile.usesLocalNPCImmunity = true;
        }
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Crystal Shard"); Original name
			DisplayName.SetDefault("Frostbite");
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
		public override bool PreDraw (SpriteBatch spriteBatch, Color lightColor)
		{
			for(int i = 0; i < 3; i++){
			    Dust.NewDustDirect(projectile.Center, 0, 0, 92, 0, 0, 0, Color.LightCyan, 0.5f).velocity*=0;
			}
			return false;
		}
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit){
			target.AddBuff(BuffID.Frozen, target.boss?30:600);
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection){
            damage+=(int)(target.defense*(target.wet||target.HasBuff<WaterDebuff>()?2:0.5f));
            damage*=target.wet||target.HasBuff<WaterDebuff>()?2:1;
        }
    }
}