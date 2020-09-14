using elemental.Items;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace elemental.NPCs {
    public class NpcDrops : GlobalNPC {
        public override void NPCLoot(NPC npc){
            if(npc.type == NPCID.Harpy){
                if(Main.rand.Next(9) == 0){
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemType<WindMaterial>(), 1);
                }
            }
            if(npc.type == NPCID.WyvernHead){
                if(Main.rand.Next(4) == 0){
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemType<WindMaterial>(), Main.rand.Next(3)+1);
                }

                if(Main.rand.Next(4) == 0){
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemType<LightningMaterial>(), 1);
                }
            }
            if(npc.type == NPCID.ChaosElemental || (npc.GivenOrTypeName.ToLower().Contains("chaos")&&!npc.GivenOrTypeName.ToLower().Contains("minnow")) || npc.GivenOrTypeName.ToLower().Contains("chaotic") || npc.GivenOrTypeName.ToLower().Contains("discord") || npc.GivenOrTypeName.ToLower().Contains("lux")){
                if(Main.rand.Next(9) == 0){
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemType<ChaosMaterialConverter>(), Main.rand.Next(3)+1);
                }
            }
            if(npc.GivenOrTypeName.ToLower().Contains("umbra")){
                if(Main.rand.Next(4) == 0){
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemType<ChaosMaterialConverter>(), Main.rand.Next(3)+1);
                }
            }
        }
    }
}