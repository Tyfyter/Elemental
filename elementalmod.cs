using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.GameInput;
using Terraria.ModLoader;
using Terraria.UI;
using Terraria.DataStructures;
using Terraria.GameContent.UI;

namespace elemental
{
	class elementalmod : Mod{
        internal static Mod mod;

        private HotKey Ability = new HotKey("Use Ability", Keys.Z);
        private HotKey ReloadGun = new HotKey("Reload Gun", Keys.R);
        //private HotKey FixBug = new HotKey("Fix Freezing Bug", Keys.N);
		public elementalmod()
        {
            Properties = new ModProperties()
            {
                Autoload = true,
                AutoloadGores = true,
                AutoloadSounds = true
            };

        }
        public override void Load()
        {
            mod = this;
            Properties = new ModProperties()
            {
                Autoload = true,
                AutoloadGores = true,
                AutoloadSounds = true
            };

            RegisterHotKey(Ability.Name, Ability.DefaultKey.ToString());
            RegisterHotKey(ReloadGun.Name, ReloadGun.DefaultKey.ToString());
        }

        public override void HotKeyPressed(string name) {
            if(PlayerInput.Triggers.JustPressed.KeyStatus[GetTriggerName(name)]) {
                if(name.Equals(Ability.Name)) {
                    Stone();
                }
            }
			
            if(PlayerInput.Triggers.JustPressed.KeyStatus[GetTriggerName(name)]) {
                if(name.Equals(ReloadGun.Name)) {
                    ReloadGunF();
                }
            }
			
            /*if(PlayerInput.Triggers.JustPressed.KeyStatus[GetTriggerName(name)]) {
                if(name.Equals(FixBug.Name)) {
                    FixFreeze();
                }
            }*/
        }

        public static Vector2 UsableNormalize(Vector2 input){
            Vector2 output = input;
            output.Normalize();
            return output;
        }
        public static Vector2 NormalizeMin(Vector2 input){
            Vector2 output = input;
            if(output.Length()>1)output.Normalize();
            return output;
        }

        public void Stone()
        {
            Player player = Main.player[Main.myPlayer];
            ElementalPlayer modPlayer = player.GetModPlayer<ElementalPlayer>(this);
            modPlayer.stone = !modPlayer.stone;
            if(!modPlayer.stone && player.HasBuff(mod.BuffType("StoneDamageDebuff"))){
                PlayerDeathReason YUdie = new PlayerDeathReason();
                YUdie.SourceCustomReason = player.name+" got too stoned.";
                player.Hurt(YUdie, player.buffTime[player.FindBuffIndex(mod.BuffType("StoneDamageDebuff"))], 0);
                player.DelBuff(player.FindBuffIndex(mod.BuffType("StoneDamageDebuff")));
            }
            player.chatOverhead.NewMessage(modPlayer.stone+"", 30);
        }
		
        public void ReloadGunF()
        {
            Player player = Main.player[Main.myPlayer];
            ElementalPlayer modPlayer = player.GetModPlayer<ElementalPlayer>(this);
			modPlayer.reloadgun = true;
			//player.HeldItem.ReloadGun();
        }
		
        /*public void FixFreeze()
        {
            Player player = Main.player[Main.myPlayer];
			player.animationTime = 0;
        }*/

        public string GetTriggerName(string name)
        {
            return Name + ": " + name;
        }
        public static short SetStaticDefaultsGlowMask(ModItem modItem)
        {
            if (!Main.dedServ)
            {
                Texture2D[] glowMasks = new Texture2D[Main.glowMaskTexture.Length + 1];
                for (int i = 0; i < Main.glowMaskTexture.Length; i++)
                {
                    glowMasks[i] = Main.glowMaskTexture[i];
                }
                glowMasks[glowMasks.Length - 1] = mod.GetTexture("Glow/" + modItem.GetType().Name + "_Glow");
                Main.glowMaskTexture = glowMasks;
                return (short)(glowMasks.Length - 1);
            }
            else return 0;
        }
    }
    public static class Extentions{
		public static Vector2 Normalized(this Vector2 input, bool errorcheck){
			input.Normalize();
			return input;
		}
		public static Vector2 Normalized(this Vector2 input){
			bool n = false;
			return input.Normalized(n);
		}
        public static Vector2 lerp(Vector2 a, Vector2 b, float c){
            //Main.NewText(a+" and "+b+" lerped by "+c+" equal "+((a*(1-c))+(b*c)));
            return (a*(1-c))+(b*c);
        }
        public static float constrain(float i, float high, float low){
            //Main.NewText(high+">"+i+">"+low);
            return i<low?low:(i>high?high:i);
        }
    }
}
