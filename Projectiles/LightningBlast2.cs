using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria.Enums;
using static elemental.Extentions;

namespace elemental.Projectiles
{

    public class LightningBlast2 : ModProjectile
    {
        private const int maxCharge = 150;
        private int _charge;                //The charge level of the weapon
        public float mult = 1;
        public override bool CloneNewInstances => true;
        public override void SetDefaults()
        {
            //projectile.name = "Lightning Blast";  //projectile name
            projectile.width = 2;       //projectile width
            projectile.height = 2;  //projectile height
            projectile.friendly = true;      //make that the projectile will not damage you
            projectile.magic = true;         // 
            projectile.tileCollide = false;   //make that the projectile will be destroed if it hits the terrain
            projectile.penetrate = -1;      //how many npc will penetrate
            projectile.timeLeft = 3600;   //how many time this projectile has before disepire
            projectile.light = 0.75f;    // projectile light
            projectile.extraUpdates = 1;
            projectile.ignoreWater = true;   
            projectile.hide = true;
			projectile.aiStyle = 0;
            projectile.restrikeDelay = 0;
        }
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Lightning Blast");
		}
        public override void AI()           //this make that the projectile will face the corect way
        {                                                           // |
            Player player = Main.player[projectile.owner];
            ElementalPlayer modPlayer = player.GetModPlayer<ElementalPlayer>(mod);
            Vector2 mousePos = Main.MouseWorld;
            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
            Vector2 unit = (mousePos - player.Center);
            unit.Normalize();
            if (_charge < maxCharge && modPlayer.channellightning >= 1 && modPlayer.channellightningid == projectile.whoAmI)
            {
                _charge++;
                projectile.damage++;
            }
            if(!(modPlayer.channellightning >= 1 && modPlayer.channellightningid == projectile.whoAmI)){
                if(projectile.hide)Main.PlaySound(42, projectile.position, 186);
                projectile.hide = false;
                projectile.tileCollide = true;
            }

        }

        public override bool PreKill(int timeLeft){
            Player player = Main.player[projectile.owner];
            ElementalPlayer modPlayer = player.GetModPlayer<ElementalPlayer>(mod);
            return !(modPlayer.channellightning >= 1 && modPlayer.channellightningid == projectile.whoAmI);
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.immune[projectile.owner] = 0;
            if ((float)Main.rand.Next(100) <= (float)(_charge/2)) {
                //target.AddBuff(mod.BuffType("LightningDebuff"), 600);
            }
        }

        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection){
            Player player = Main.player[projectile.owner];
            ElementalPlayer modPlayer = player.GetModPlayer<ElementalPlayer>(mod);
            damage = (int)(damage*mult);
            if(modPlayer.channellightning >= 1 && modPlayer.channellightningid == projectile.whoAmI){
                target.position = projectile.position + projectile.velocity;
                target.velocity = lerp(projectile.velocity/2, target.velocity, 1-constrain(target.knockBackResist*1.25f, 0.1f, 1f));
                damage = (int)(damage/10);
                knockback = 0;
                if(mult<10)mult+=0.165f;
            }
        }
    }
}