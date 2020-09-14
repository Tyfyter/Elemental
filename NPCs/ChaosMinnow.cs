using System;
using elemental.Items;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace elemental.NPCs{
    public class ChaosMinnow : ModNPC {
        public override string Texture => "elemental/Items/Chaos_Minnow";
        public override bool CloneNewInstances => true;
        int time = 4;
        public override void SetDefaults(){
            npc.width = 8;
            npc.height = 8;
            npc.noTileCollide = true;
            npc.noGravity = true;
            npc.lifeMax = 25;
            npc.damage = 25;
        }
        public override void AI(){
			Lighting.AddLight(npc.Center, Color.DarkCyan.R/75, Color.DarkCyan.G/150, Color.DarkCyan.B/75);
            baseai();
        }
        public override void NPCLoot(){
            if(Main.rand.Next(14)==0){
                Item.NewItem(npc.Center, new Vector2(), ItemType<Chaos_Minnow>());
            }
        }
        public override void HitEffect(int hitDirection, double damage){
            if(hitDirection==0&&damage==10){
                npc.life = npc.lifeMax-=5;
            }
        }
        void baseai(){
            npc.noTileCollide = true;
            int num986 = 90;
            if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead)
            {
                npc.TargetClosest(false);
                npc.direction = 1;
                npc.netUpdate = true;
            }
            if (npc.ai[0] == 0f)
            {
                npc.ai[1] += 1f;
                int num987 = npc.type;
                npc.noGravity = true;
                npc.dontTakeDamage = true;
                npc.velocity.Y = npc.ai[3];
                if (npc.type == 373)
                {
                    float num988 = 0.104719758f;
                    float num989 = npc.ai[2];
                    float num990 = (float)(Math.Cos((double)(num988 * npc.localAI[1])) - 0.5) * num989;
                    npc.position.X = npc.position.X - num990 * -(float)npc.direction;
                    npc.localAI[1] += 1f;
                    num990 = (float)(Math.Cos((double)(num988 * npc.localAI[1])) - 0.5) * num989;
                    npc.position.X = npc.position.X + num990 * -(float)npc.direction;
                    if (Math.Abs(Math.Cos((double)(num988 * npc.localAI[1])) - 0.5) > 0.25)
                    {
                        npc.spriteDirection = ((Math.Cos((double)(num988 * npc.localAI[1])) - 0.5 >= 0.0) ? -1 : 1);
                    }
                    npc.rotation = npc.velocity.Y * (float)npc.spriteDirection * 0.1f;
                    if ((double)npc.rotation < -0.2)
                    {
                        npc.rotation = -0.2f;
                    }
                    if ((double)npc.rotation > 0.2)
                    {
                        npc.rotation = 0.2f;
                    }
                    npc.alpha -= 6;
                    if (npc.alpha < 0)
                    {
                        npc.alpha = 0;
                    }
                }
                if (npc.ai[1] >= (float)num986)
                {
                    npc.ai[0] = 1f;
                    npc.ai[1] = 0f;
                    if (!Collision.SolidCollision(npc.position, npc.width, npc.height))
                    {
                        npc.ai[1] = 1f;
                    }
                    Main.PlaySound(4, (int)npc.Center.X, (int)npc.Center.Y, 19, 1f, 0f);
                    npc.TargetClosest(true);
                    npc.spriteDirection = npc.direction;
                    Vector2 vector127 = Main.player[npc.target].Center - npc.Center;
                    vector127.Normalize();
                    npc.velocity = vector127 * 16f;
                    npc.rotation = npc.velocity.ToRotation();
                    if (npc.direction == -1)
                    {
                        npc.rotation += 3.14159274f;
                    }
                    npc.netUpdate = true;
                    return;
                }
            }
            else if (npc.ai[0] == 1f)
            {
                npc.noGravity = true;
                if (!Collision.SolidCollision(npc.position, npc.width, npc.height))
                {
                    if (npc.ai[1] < 1f)
                    {
                        npc.ai[1] = 1f;
                    }
                }
                else
                {
                    npc.alpha -= 15;
                    if (npc.alpha < 150)
                    {
                        npc.alpha = 150;
                    }
                }
                if (npc.ai[1] >= 1f)
                {
                    npc.alpha -= 60;
                    if (npc.alpha < 0)
                    {
                        npc.alpha = 0;
                    }
                    npc.dontTakeDamage = false;
                    npc.ai[1] += 1f;
                    if (Collision.SolidCollision(npc.position, npc.width, npc.height))
                    {
                        if (npc.DeathSound != null)
                        {
                            Main.PlaySound(npc.DeathSound, npc.position);
                        }
                        npc.life = npc.lifeMax-=5;
                        npc.HitEffect(0, 1.0);
                        return;
                    }
                }
                if (npc.ai[1] >= 60f)
                {
                    npc.noGravity = false;
                }
                npc.rotation = npc.velocity.ToRotation();
                if (npc.direction == -1)
                {
                    npc.rotation += 3.14159274f;
                    return;
                }
            }
        }
    }
}