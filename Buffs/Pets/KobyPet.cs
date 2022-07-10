using Terraria;
using Terraria.ModLoader;

namespace SinsMod.Buffs.Pets
{
    public class KobyPet : ModBuff
	{
		public override void SetDefaults()
		{
            DisplayName.SetDefault("GABA");
            Description.SetDefault("There are mistakes..." +
                "\nNo,it's GABA.");
            Main.buffNoTimeDisplay[Type] = true;
			Main.vanityPet[Type] = true;
		}
		public override void Update(Player player, ref int buffIndex)
		{
			player.buffTime[buffIndex] = 18000;
            player.GetModPlayer<SinsPlayer>().KobyPet = true;
			bool petProjectileNotSpawned = player.ownedProjectileCounts[mod.ProjectileType("KobyPet")] <= 0;
			if (petProjectileNotSpawned && player.whoAmI == Main.myPlayer)
			{
				Projectile.NewProjectile(player.position.X + player.width / 2, player.position.Y + player.height / 2, 0f, 0f, mod.ProjectileType("KobyPet"), 0, 0f, player.whoAmI, 0f, 0f);
			}
		}
	}
}