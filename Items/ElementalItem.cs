using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using elemental.Projectiles;
using System.Collections.Generic;
using elemental.Classes;

namespace elemental.Items
{
	public abstract class ElementalItem : ModItem {
        //
        //
        ///<param name="fire">1</param>
        ///<param name="wind">2</param>
        ///<param name="ice">4</param>
        ///<param name="water">8</param>
        ///<param name="chaos">16</param>
        ///<param name="lightning">32</param>
        ///<param name="darkness">64</param>
        public abstract int Elements { get; }
        public virtual bool isElemental => true;
        public override bool Autoload(ref string name){
            return !name.ToLower().Equals("elementalitem");
        }
#pragma warning disable 672
        public override void GetWeaponDamage(Player player, ref int damage){
            if(player.HasBuff(BuffID.ChaosState)&&(Elements&ElEnum.chaos)!=0){
                damage=(int)(damage*1.3f);
            }
        }
#pragma warning restore 672
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Player player = Main.player[item.owner];
            string text = "";
            string comma = "";
            for(int i = 0; i < 7; i++){
                if((Elements&(1<<i))!=0){
                    string col = "";
                    switch(i){
                        case 0:
                        col = "FF6400";
                        break;
                        case 1:
                        col = "FFFF66";
                        break;
                        case 2:
                        col = "32FFFF";
                        break;
                        case 3:
                        col = "2D2DD7";
                        break;
                        case 4:
                        col = "FF22AE";
                        break;
                        case 5:
                        col = "0AEB7D";
                        break;
                        case 6:
                        col = "5A197E";
                        break;
                    }
                    text+=$"{comma}[c/{col}:{ElEnum.ElNames[i]}]";
                    comma = ", ";
                }
            }
            tooltips.Insert(1, new TooltipLine(mod, "Elements", text));
            for (int i = 0; i < tooltips.Count; i++){
                if(tooltips[i].Name.Equals("ItemName")){
                    //string s = "";
                    tooltips[i].overrideColor = GetColor(Elements);
                    //tooltips[i].text = s+"\n"+Elements.toBoolArray().toString();
                }
            }
        }
        static Color GetColor(int elements){
            string n = "";
            return GetColor(elements, ref n, false);
        }
        static Color GetColor(int elements, ref string outs, bool debug = true){
            int amount = 0;
            int r = 0;
            int g = 0;
            int b = 0;
            if((elements&ElEnum.fire)!=0){
                r+=255;
                g+=100;
                b+=0;
                amount++;
                if(debug)outs+=(outs.Length>0?" ":"")+"fire "+r+","+g+","+b+","+amount;
            }
            if((elements&ElEnum.wind)!=0){
                r+=255;
                g+=255;
                b+=102;
                amount++;
                if(debug)outs+=(outs.Length>0?" ":"")+"wind "+r+","+g+","+b+","+amount;
            }
            if((elements&ElEnum.ice)!=0){
                r+=50;
                g+=255;
                b+=255;
                amount++;
                if(debug)outs+=(outs.Length>0?" ":"")+"ice "+r+","+g+","+b+","+amount;
            }
            if((elements&ElEnum.water)!=0){
                r+=45;
                g+=45;
                b+=215;
                amount++;
                if(debug)outs+=(outs.Length>0?" ":"")+"water "+r+","+g+","+b+","+amount;
            }
            if((elements&ElEnum.chaos)!=0){
                r+=255;
                g+=32;
                b+=174;
                amount++;
                if(debug)outs+=(outs.Length>0?" ":"")+"chaos "+r+","+g+","+b+","+amount;
            }
            if((elements&ElEnum.lightning)!=0){
                r+=10;
                g+=235;
                b+=125;
                amount++;
                if(debug)outs+=(outs.Length>0?" ":"")+"lightning "+r+","+g+","+b+","+amount;
            }
            if((elements&ElEnum.darkness)!=0){
                r+=90;
                g+=25;
                b+=126;
                amount++;
                if(debug)outs+=(outs.Length>0?" ":"")+"darkness "+r+","+g+","+b+","+amount;
            }
            Color output = new Color(r/amount, g/amount, b/amount);
            return output;
        }
    }
    public class NonElementalItem : ElementalItem
    {
        public override int Elements => 0;
        public override bool isElemental => false;
        public override bool Autoload(ref string name){
            return false;
        }
    }
}