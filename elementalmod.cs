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
using elemental.Items;
using elemental.NPCs;
using System;
using static Terraria.ModLoader.ModContent;

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
            ElementalPlayer modPlayer = player.GetModPlayer<ElementalPlayer>();
            modPlayer.stone = !modPlayer.stone;
            if(!modPlayer.stone && player.HasBuff(BuffType("StoneDamageDebuff"))){
                PlayerDeathReason YUdie = new PlayerDeathReason();
                YUdie.SourceCustomReason = player.name+" got too stoned.";
                player.Hurt(YUdie, player.buffTime[player.FindBuffIndex(BuffType("StoneDamageDebuff"))], 0);
                player.DelBuff(player.FindBuffIndex(BuffType("StoneDamageDebuff")));
            }
            player.chatOverhead.NewMessage(modPlayer.stone+"", 30);
        }
		
        public void ReloadGunF()
        {
            Player player = Main.player[Main.myPlayer];
            ElementalPlayer modPlayer = player.GetModPlayer<ElementalPlayer>();
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
        public static float constrain(float i, float low, float high){
            //Main.NewText(high+">"+i+">"+low);
            return i<low?low:(i>high?high:i);
        }
        public static Vector2 constrain(Vector2 i, Vector2 low, Vector2 high){
            //Main.NewText(high+">"+i+">"+low);
            float x = i.X<low.X?low.X:(i.X>high.X?high.X:i.X);
            float y = i.Y<low.Y?low.Y:(i.Y>high.Y?high.Y:i.Y);
            return new Vector2(x, y);
        }
        public static bool[] toBoolArray(this int inint){
            List<bool> outb = new List<bool>(){};
            for (int i = 0; (1<<i) <= inint; i++){
                outb.Add((inint&(1<<i))!=0);
            }
            outb.Reverse();
            return outb.ToArray();
        }
        public static string toString<T>(this T[] inarr, string seperator = ", ", string caps = "[]"){
            string outb = "";
            foreach (var item in inarr){
                outb+=item.ToString()+seperator;
            }
            return caps.Substring(0,1)+outb.Substring(0, outb.Length-2)+caps.Substring(1,1);
        }
        public static ElementalItem toElementalItem(this ModItem mi){
            return (mi as ElementalItem)??new NonElementalItem();
        }
        public static ElementalItem toElementalItem(this Item i){
            return i.modItem!=null?((i.modItem as ElementalItem)??new NonElementalItem()):new NonElementalItem();
        }
        ///<param name=sub>the "0 point"(theoretical/enforced max, if knockback is higher than this it will be reduced)</param>
        ///<param name=div>the rate of increase(higher div = slower increase)</param>
        ///<param name=max>the maximum knockback multiplier this will set npc.knockBackResist to, if knockback is higher than this it will not be changed</param>
		public static void windDebuff(this NPC npc, int time, float div = 4f, float sub = 4f,float max = 4f){
            ElementalGlobalNPC gnpc = npc.GetGlobalNPC<ElementalGlobalNPC>();
            if(gnpc.windKBtime<=0){
                gnpc.baseKB = npc.knockBackResist;
            }
            if(gnpc.currKB<=max)gnpc.currKB = Math.Min(gnpc.currKB+(sub-gnpc.currKB)/div,max);
            if(gnpc.windKBtime<time)gnpc.windKBtime=time;
		}
        public static bool HasBuff<T>(this Player player) where T : ModBuff {
            return player.HasBuff(BuffType<T>());
        }
        public static bool HasBuff<T>(this NPC npc) where T : ModBuff {
            return npc.HasBuff(BuffType<T>());
        }
        ///<param name=type>the type of item that will be consumed</param>
        ///<param name=count>the amount of items that will be consumed</param>
        ///<param name=safe>whether or not any items will be consumed if the player has less than ``count`` items of the specified type</param>
        public static bool ConsumeItems(this Player player, int type, int count, bool safe = false){
            if(safe){
                int c = 0;
                for (int i = 0; i < 58; i++){
                    if (player.inventory[i].type == type)c+=player.inventory[i].stack;
                }
                if(c<count)return false;
            }
            for (int i = 0; i < 58; i++){
                if (player.inventory[i].stack > 0 && player.inventory[i].type == type){
                    if (ItemLoader.ConsumeItem(player.inventory[i], player)){
                        player.inventory[i].stack-=count;
                        if (player.inventory[i].stack <= 0){
                            count = -player.inventory[i].stack;
                            player.inventory[i].SetDefaults(0, false);
                        }else return true;
                    }
                    return true;
                }
            }
            return false;
        }
    }
}
