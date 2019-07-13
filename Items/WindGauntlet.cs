using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace elemental.Items
{
	public class WindGauntlet : ElementalItem
	{
        public override int Elements => 2;
        public static short customGlowMask = 207;
		public override void SetDefaults()
		{
			//item.name = "Wind Gauntlet";
			item.damage = 100;
            item.melee = true;
            item.noMelee = false;
            item.noUseGraphic = true;
            item.width = 80;
			item.height = 80;
			item.useTime = 2;
			item.useAnimation = 30;
            item.useStyle = 60;
            item.knockBack = 10;        
			item.value = 10000;
			item.rare = 2;
            item.useStyle = 5;
            item.shoot = 1;
            item.glowMask = customGlowMask;
            item.autoReuse = false;
        }
		public override void SetStaticDefaults()
		{
		  DisplayName.SetDefault("Wind Gauntlet");
		  Tooltip.SetDefault("Strike with the power of the wind!");
          customGlowMask = elementalmod.SetStaticDefaultsGlowMask(this);
		}

        public override void HoldStyle(Player player)

        {
            if ((!(player.velocity.Y > 0))||player.sliding)
            {
                int dust3 = Dust.NewDust((player.velocity.X==0?(player.direction==1?player.Left:player.Right)-new Vector2(4,-4):player.Center), 0, 0, 87, 0f, 0f, 25, Color.Goldenrod, 1.5f);
                Main.dust[dust3].noGravity = true;
                Main.dust[dust3].velocity = new Vector2(0, 0);
            }else
            {
                int dust3 = Dust.NewDust((player.direction==1?player.TopLeft:player.TopRight)-new Vector2(4,-4), 0, 0, 87, 0f, 0f, 25, Color.Goldenrod, 1.5f);
                Main.dust[dust3].noGravity = true;
                Main.dust[dust3].velocity = new Vector2(0, 0);
            }

        }

        public override void HoldItem(Player player)
        {
            player.portalPhysicsFlag = true;
            base.HoldItem(player);
        }
        public override void GetWeaponDamage(Player player, ref int damage){
            damage += (int)(damage*(Main.player[item.owner].velocity.Length()/8));
            base.GetWeaponDamage(player, ref damage);
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
 
        public override bool CanUseItem(Player player)
        {
            ElementalPlayer modPlayer = player.GetModPlayer<ElementalPlayer>(mod);
            if (player.altFunctionUse == 2)     //2 is right click
            {
 
                item.useTime = 1;
                item.useAnimation = 15;
                item.noMelee = true;
                item.noUseGraphic = true;
                item.shootSpeed = 25f;    //projectile speed when shoot
                return base.CanUseItem(player) && modPlayer.WindGauntletCD <= 0;


            }
			else
            {

                item.useTime = 2;
                item.useAnimation = 30;
                item.noMelee = false;
                item.noUseGraphic = true;
                item.shootSpeed = 17.5f;    //projectile speed when shoot
                return base.CanUseItem(player);
            }
        }

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.TitanGlove, 1);
			recipe.AddIngredient(null, "WindMaterial", 10);
			recipe.AddIngredient(ItemID.Topaz, 6);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            ElementalPlayer modPlayer = player.GetModPlayer<ElementalPlayer>(mod);
            if (player.altFunctionUse == 2)
            {
                modPlayer.WindGauntletCD = 60;
            }else
            {
                player.immuneTime += 1;
            }
            if (player.itemAnimation == 1)
            {
                player.immuneTime += 45;
            }
            player.velocity.X += speedX/10;
            player.velocity.Y += speedY/10;
            player.immuneTime += 1;
            player.immune = true;
            if(player.altFunctionUse != 2){
                int dust3 = Dust.NewDust((player.itemLocation-player.position).RotatedBy(player.itemRotation)+player.position, 0, 0, 87, 0f, 0f, 25, Color.Goldenrod, 1.5f);
                Main.dust[dust3].noGravity = true;
                Main.dust[dust3].velocity = new Vector2(0, 0);
            }
            return false;
        }
        
        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            damage = 100 * ((int)Main.player[item.owner].velocity.Length());
            player.immuneTime += 45;
            target.velocity += player.velocity*(item.knockBack/10);
            player.velocity /= 3;
            player.itemAnimation = 10;
        }

        public override void UseItemHitbox(Player player, ref Rectangle hitbox, ref bool noHitbox)
        {
            hitbox.Location = new Point((int)player.position.X-8, (int)player.position.Y-8);
            hitbox.Width = player.width+16;
            hitbox.Height = player.height+16;
            base.UseItemHitbox(player, ref hitbox, ref noHitbox);
        }
    }
}
