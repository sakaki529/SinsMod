using Terraria;
using Terraria.ModLoader;

namespace SinsMod.Buffs.Pets
{
    public class CleyeraPet : ModBuff
	{
		public override void SetDefaults()
		{
            DisplayName.SetDefault("Wa");
            Description.SetDefault("Wa");
            Main.buffNoTimeDisplay[Type] = true;
			Main.vanityPet[Type] = true;
		}
		public override void Update(Player player, ref int buffIndex)
		{
			player.buffTime[buffIndex] = 18000;
            player.GetModPlayer<SinsPlayer>().CleyeraPet = true;
			bool petProjectileNotSpawned = player.ownedProjectileCounts[mod.ProjectileType("CleyeraPet")] <= 0;
			if (petProjectileNotSpawned && player.whoAmI == Main.myPlayer)
			{
				Projectile.NewProjectile(player.position.X + player.width / 2, player.position.Y + player.height / 2, 0f, 0f, mod.ProjectileType("CleyeraPet"), 0, 0f, player.whoAmI, 0f, 0f);
			}
		}
	}
}