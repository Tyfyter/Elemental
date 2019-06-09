using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace elemental.NPCs
{
    public class NpcDrops : GlobalNPC
    {
        public override void NPCLoot(NPC npc){
            if (npc.type == NPCID.Harpy)   //this is where you choose the npc you want
            {
                if (Main.rand.Next(9) == 0) //this is the item rarity, so 9 is 1 in 10 chance that the npc will drop the item.
                {
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("WindMaterial"), 1); //this is where you set what item to drop, mod.ItemType("CustomSword") is an example of how to add your custom item. and 1 is the amount
                    }
                }
            }
            if (npc.type == NPCID.WyvernHead)   //this is where you choose the npc you want
            {
                if (Main.rand.Next(4) == 0) //this is the item rarity, so 4 is 1 in 5 chance that the npc will drop the item.
                {
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("WindMaterial"), Main.rand.Next(3)+1); //this is where you set what item to drop, mod.ItemType("CustomSword") is an example of how to add your custom item. and 1 is the amount
                    }
                }

                if (Main.rand.Next(4) == 0) //this is the item rarity, so 4 is 1 in 5 chance that the npc will drop the item.
                {
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("LightningMaterial"), 1); //this is where you set what item to drop, mod.ItemType("CustomSword") is an example of how to add your custom item. and 1 is the amount
                    }
                }
            }
            if(npc.type == NPCID.ChaosElemental || (npc.GivenOrTypeName.ToLower().Contains("chaos")&&!npc.GivenOrTypeName.ToLower().Contains("minnow")) || npc.GivenOrTypeName.ToLower().Contains("chaotic") || npc.GivenOrTypeName.ToLower().Contains("discord") || npc.GivenOrTypeName.ToLower().Contains("lux")){
                if (Main.rand.Next(9) == 0) //this is the item rarity, so 4 is 1 in 5 chance that the npc will drop the item.
                {
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("ChaosMaterialConverter"), Main.rand.Next(3)+1); //this is where you set what item to drop, mod.ItemType("CustomSword") is an example of how to add your custom item. and 1 is the amount
                    }
                }
            }
            if(npc.GivenOrTypeName.ToLower().Contains("umbra")){
                if (Main.rand.Next(4) == 0) //this is the item rarity, so 4 is 1 in 5 chance that the npc will drop the item.
                {
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("ChaosMaterialConverter"), Main.rand.Next(3)+1); //this is where you set what item to drop, mod.ItemType("CustomSword") is an example of how to add your custom item. and 1 is the amount
                    }
                }
            }
        }
    }
}