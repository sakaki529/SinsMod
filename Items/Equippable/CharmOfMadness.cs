using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Items.Equippable
{
	[AutoloadEquip(EquipType.Neck)]
	public class CharmOfMadness : ModItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Charm of Madness");
            string tooltip = "Grants immunity to most debuffs" +
                "\nIncreases length of invincibility after taking damage" +
                "\nIncreases maximum life by 500" +
                "\nPress the buff immuniny key to enable/disable immune to all debuff(There are some exceptions)" +
                "\n[c/ff0000:-Exceptions List-]" +
                "\n[c/25c059:Vanilla]: Potion Sickness, Chaos State, Mana Sickness, Creative Shock" +
                "\n[c/7989a2:Sins]: Life Elixir Sickness, Nullification Potion Sickness, Nothingness, Revive Cooldown";
            if (ModLoader.GetMod("CalamityMod") != null)
            {
                tooltip += "\n[c/630002:Calamity]: Scarf Cooldown, Concoction Cooldown, Heart Attack, God Slayer Cooldown";
            }
            /*if (ModLoader.GetMod("SacredTools") != null)
            {
                tooltip += "\n[c/630002:Shadows of Abaddon]: ";
            }*/
            /*if (ModLoader.GetLoadedMods().Contains("Bluemagic"))
            {
                tooltip += "\n[c/277ecd:Bluemagic]: ";
            }*/
            Tooltip.SetDefault(tooltip);
        }
		public override void SetDefaults()
		{
			item.width = 24;
			item.height = 24;
			item.value = Item.sellPrice(1, 20, 0, 0);
			item.rare = 11;
			item.accessory = true;
            item.GetGlobalItem<SinsItem>().CustomRarity = 17;
        }
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
            SinsPlayer modPlayer = player.GetModPlayer<SinsPlayer>();
            modPlayer.CoM = true;
            if (modPlayer.AllDebuffImmunity)
            {
                for (int i = 0; i < player.buffImmune.Length; i++)
                {
                    if (Main.debuff[i])
                    {
                        player.buffImmune[i] = true;
                        player.buffImmune[BuffID.PotionSickness] = false;
                        player.buffImmune[BuffID.Werewolf] = false;
                        player.buffImmune[BuffID.Merfolk] = false;
                        player.buffImmune[BuffID.ChaosState] = false;
                        player.buffImmune[BuffID.ManaSickness] = false;
                        player.buffImmune[BuffID.Sunflower] = false;
                        player.buffImmune[BuffID.NoBuilding] = false;
                        player.buffImmune[mod.BuffType("ReviveCooldown")] = false;
                        player.buffImmune[mod.BuffType("LifeElixirSickness")] = false;
                        player.buffImmune[mod.BuffType("NullificationPotionSickness")] = false;
                        player.buffImmune[mod.BuffType("Nothingness")] = false;
                        if (SinsMod.Instance.CalamityLoaded)
                        {
                            player.buffImmune[ModLoader.GetMod("CalamityMod").BuffType("HeartAttack")] = false;
                            player.buffImmune[ModLoader.GetMod("CalamityMod").BuffType("RageMode")] = false;
                            player.buffImmune[ModLoader.GetMod("CalamityMod").BuffType("AdrenalineMode")] = false;
                            player.buffImmune[ModLoader.GetMod("CalamityMod").BuffType("ScarfCooldown")] = false;
                            player.buffImmune[ModLoader.GetMod("CalamityMod").BuffType("ConcoctionCooldown")] = false;
                            player.buffImmune[ModLoader.GetMod("CalamityMod").BuffType("GodSlayerCooldown")] = false;
                        }
                        if (SinsMod.Instance.BluemagicLoaded)
                        {
                            player.buffImmune[ModLoader.GetMod("Bluemagic").BuffType("HeroOne")] = false;
                            player.buffImmune[ModLoader.GetMod("Bluemagic").BuffType("HeroTwo")] = false;
                            player.buffImmune[ModLoader.GetMod("Bluemagic").BuffType("HeroThree")] = false;
                        }
                    }
                }
            }
            player.longInvince = true;
            player.statLifeMax2 += 500;
            player.lavaImmune = true;
            player.buffImmune[BuffID.Poisoned] = true;//Poisoned
            player.buffImmune[BuffID.Darkness] = true;//Darkness
            player.buffImmune[BuffID.Cursed] = true;//Cursed
            player.buffImmune[BuffID.OnFire] = true;//OnFire
            player.buffImmune[BuffID.Bleeding] = true;//Bleeding
            player.buffImmune[BuffID.Confused] = true;//Confused
            player.buffImmune[BuffID.Slow] = true;//Slow
            player.buffImmune[BuffID.Weak] = true;//Weak
            player.buffImmune[BuffID.Silenced] = true;//Silenced
            player.buffImmune[BuffID.BrokenArmor] = true;//BrokenArmor
            player.buffImmune[BuffID.Horrified] = true;//Horrified
            player.buffImmune[BuffID.TheTongue] = true;//TheTongue
            player.buffImmune[BuffID.CursedInferno] = true;//TheTongue
            player.buffImmune[BuffID.Frostburn] = true;//Frostburn
            player.buffImmune[BuffID.Chilled] = true;//Chilled(SlowSpeed)
            player.buffImmune[BuffID.Frozen] = true;//Frozen(CantMove)
            player.buffImmune[BuffID.Burning] = true;//Burning(HellStorn)
            player.buffImmune[BuffID.Suffocation] = true;//Suffocation
            player.buffImmune[BuffID.Ichor] = true;//Ichor
            player.buffImmune[BuffID.Venom] = true;//Venom
            player.buffImmune[BuffID.Blackout] = true;//BlackOut
            player.buffImmune[BuffID.Electrified] = true;//Electrified
            player.buffImmune[BuffID.MoonLeech] = true;//MoonBite
            player.buffImmune[BuffID.Rabies] = true;//FeralBite
            player.buffImmune[BuffID.Webbed] = true;//Webbed
            player.buffImmune[BuffID.ShadowFlame] = true;//Shadowflame
            player.buffImmune[BuffID.Stoned] = true;//Stoned
            player.buffImmune[BuffID.Dazed] = true;//Dazed
            player.buffImmune[BuffID.Obstructed] = true;//Obstructed
            player.buffImmune[BuffID.VortexDebuff] = true;//Distorted
            player.buffImmune[BuffID.Daybreak] = true;//Daybreak
            player.buffImmune[BuffID.WindPushed] = true;//MightyWind
            player.buffImmune[BuffID.WitheredArmor] = true;//WitheredArmor
            player.buffImmune[BuffID.WitheredWeapon] = true;//WitheredWeapon
            player.buffImmune[BuffID.OgreSpit] = true;//Oozed
            player.buffImmune[BuffID.BetsysCurse] = true;//Betsy'sCurse
            player.buffImmune[mod.BuffType("AbyssalFlame")] = true;
            player.buffImmune[mod.BuffType("Agony")] = true;
            player.buffImmune[mod.BuffType("Chroma")] = true;
            player.buffImmune[mod.BuffType("Nightmare")] = true;
            player.buffImmune[mod.BuffType("Sins")] = true;
            player.buffImmune[mod.BuffType("SuperSlow")] = true;
            if (SinsMod.Instance.CalamityLoaded)
            {
                player.buffImmune[ModLoader.GetMod("CalamityMod").BuffType("AbyssalFlames")] = true;
                player.buffImmune[ModLoader.GetMod("CalamityMod").BuffType("ArmorCrunch")] = true;
                player.buffImmune[ModLoader.GetMod("CalamityMod").BuffType("AstralInfectionDebuff")] = true;
                player.buffImmune[ModLoader.GetMod("CalamityMod").BuffType("BrimstoneFlames")] = true;
                player.buffImmune[ModLoader.GetMod("CalamityMod").BuffType("BurningBlood")] = true;
                player.buffImmune[ModLoader.GetMod("CalamityMod").BuffType("CrushDepth")] = true;
                player.buffImmune[ModLoader.GetMod("CalamityMod").BuffType("ExoFreeze")] = true;
                player.buffImmune[ModLoader.GetMod("CalamityMod").BuffType("ExtremeGrav")] = true;
                player.buffImmune[ModLoader.GetMod("CalamityMod").BuffType("ExtremeGravity")] = true;
                player.buffImmune[ModLoader.GetMod("CalamityMod").BuffType("FishAlert")] = true;
                player.buffImmune[ModLoader.GetMod("CalamityMod").BuffType("GlacialState")] = true;
                player.buffImmune[ModLoader.GetMod("CalamityMod").BuffType("GodSlayerInferno")] = true;
                player.buffImmune[ModLoader.GetMod("CalamityMod").BuffType("HolyInferno")] = true;
                player.buffImmune[ModLoader.GetMod("CalamityMod").BuffType("HolyLight")] = true;
                player.buffImmune[ModLoader.GetMod("CalamityMod").BuffType("Horror")] = true;
                player.buffImmune[ModLoader.GetMod("CalamityMod").BuffType("LethalLavaBurn")] = true;
                player.buffImmune[ModLoader.GetMod("CalamityMod").BuffType("MarkedforDeath")] = true;
                player.buffImmune[ModLoader.GetMod("CalamityMod").BuffType("NOU")] = true;
                player.buffImmune[ModLoader.GetMod("CalamityMod").BuffType("Plague")] = true;
                player.buffImmune[ModLoader.GetMod("CalamityMod").BuffType("Shred")] = true;
                player.buffImmune[ModLoader.GetMod("CalamityMod").BuffType("TemporalSadness")] = true;
                player.buffImmune[ModLoader.GetMod("CalamityMod").BuffType("VulnerabilityHex")] = true;
                player.buffImmune[ModLoader.GetMod("CalamityMod").BuffType("WeakPetrification")] = true;
                player.buffImmune[ModLoader.GetMod("CalamityMod").BuffType("WhisperingDeath")] = true;
            }
            if (SinsMod.Instance.FargoSoulsLoaded)
            {
                player.buffImmune[ModLoader.GetMod("FargowiltasSouls").BuffType("Antisocial")] = true;
                player.buffImmune[ModLoader.GetMod("FargowiltasSouls").BuffType("Atrophied")] = true;
                player.buffImmune[ModLoader.GetMod("FargowiltasSouls").BuffType("Berserked")] = true;
                player.buffImmune[ModLoader.GetMod("FargowiltasSouls").BuffType("Bloodthirsty")] = true;
                player.buffImmune[ModLoader.GetMod("FargowiltasSouls").BuffType("ClippedWings")] = true;
                player.buffImmune[ModLoader.GetMod("FargowiltasSouls").BuffType("Crippled")] = true;
                player.buffImmune[ModLoader.GetMod("FargowiltasSouls").BuffType("CurseoftheMoon")] = true;
                player.buffImmune[ModLoader.GetMod("FargowiltasSouls").BuffType("Defenseless")] = true;
                player.buffImmune[ModLoader.GetMod("FargowiltasSouls").BuffType("FlamesoftheUniverse")] = true;
                player.buffImmune[ModLoader.GetMod("FargowiltasSouls").BuffType("Flipped")] = true;
                player.buffImmune[ModLoader.GetMod("FargowiltasSouls").BuffType("FlippedHallow")] = true;
                player.buffImmune[ModLoader.GetMod("FargowiltasSouls").BuffType("Fused")] = true;
                player.buffImmune[ModLoader.GetMod("FargowiltasSouls").BuffType("GodEater")] = true;
                player.buffImmune[ModLoader.GetMod("FargowiltasSouls").BuffType("Hexed")] = true;
                player.buffImmune[ModLoader.GetMod("FargowiltasSouls").BuffType("Infested")] = true;
                player.buffImmune[ModLoader.GetMod("FargowiltasSouls").BuffType("Jammed")] = true;
                player.buffImmune[ModLoader.GetMod("FargowiltasSouls").BuffType("Lethargic")] = true;
                player.buffImmune[ModLoader.GetMod("FargowiltasSouls").BuffType("LightningRod")] = true;
                player.buffImmune[ModLoader.GetMod("FargowiltasSouls").BuffType("LivingWasteland")] = true;
                player.buffImmune[ModLoader.GetMod("FargowiltasSouls").BuffType("MarkedforDeath")] = true;
                player.buffImmune[ModLoader.GetMod("FargowiltasSouls").BuffType("MutantNibble")] = true;
                player.buffImmune[ModLoader.GetMod("FargowiltasSouls").BuffType("NullificationCurse")] = true;
                player.buffImmune[ModLoader.GetMod("FargowiltasSouls").BuffType("OceanicMaul")] = true;
                player.buffImmune[ModLoader.GetMod("FargowiltasSouls").BuffType("Purified")] = true;
                player.buffImmune[ModLoader.GetMod("FargowiltasSouls").BuffType("ReverseManaFlow")] = true;
                player.buffImmune[ModLoader.GetMod("FargowiltasSouls").BuffType("Rotting")] = true;
                player.buffImmune[ModLoader.GetMod("FargowiltasSouls").BuffType("SqueakyToy")] = true;
                player.buffImmune[ModLoader.GetMod("FargowiltasSouls").BuffType("Stunned")] = true;
                player.buffImmune[ModLoader.GetMod("FargowiltasSouls").BuffType("Unstable")] = true;
            }
            if (SinsMod.Instance.TERRARIATALELoaded)
            {
                player.buffImmune[ModLoader.GetMod("TERRARIATALE").BuffType("Karma")] = true;
                player.buffImmune[ModLoader.GetMod("TERRARIATALE").BuffType("SansDebuff")] = true;
                player.buffImmune[ModLoader.GetMod("TERRARIATALE").BuffType("CantFly")] = true;
            }
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "EssenceOfMadness", 12);
            recipe.AddIngredient(null, "NightmareBar", 12);
            recipe.AddIngredient(ItemID.AnkhCharm);
            if (SinsMod.Instance.CalamityLoaded)
            {
                recipe.AddIngredient(ModLoader.GetMod("CalamityMod").ItemType("DarksunFragment"), 80);
            }
            recipe.AddTile(null, "AlterOfConfession");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}