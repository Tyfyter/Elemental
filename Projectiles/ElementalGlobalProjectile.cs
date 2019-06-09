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
        public override void OnHitNPC(Projectile projectile, NPC target, int damage, float knockback, bool crit){
            if(TrackHitEnemies)HitEnemies.Add(target.whoAmI);
        }
    }
}