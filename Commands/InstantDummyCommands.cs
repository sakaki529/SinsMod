using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace SinsMod.Commands
{
    internal class DefenceCommand : ModCommand
    {
        public override CommandType Type => CommandType.World;
        public override string Command => "def";
        public override string Usage => "/def [defence]" +
            "\nEx)/def 50: This Command will sets instant dummy's defence to 50";
        public override string Description => "Changes instant dummy's defence state";
        public override void Action(CommandCaller caller, string input, string[] args)
        {
            if (!int.TryParse(args[0], out int num))
            {
                throw new UsageException(args[0] + " is not an integer. Ex)/dummyDef 50");
            }
            SinsNPC.InstantDummyDefence = num;
            string key = "Mods.SinsMod.DefenceCommand";
            if (Main.netMode != 2)
            {
                Main.NewText(Language.GetTextValue(key, num), SinsColor.MultiTypeColor);
            }
            else
            {
                NetMessage.BroadcastChatMessage(NetworkText.FromKey(key, num), SinsColor.MultiTypeColor);
            }
        }
    }
    internal class DamegeMultiplierCommand : ModCommand
    {
        public override CommandType Type => CommandType.World;
        public override string Command => "dmg";
        public override string Usage => "/dmg [multiplier]" +
            "\nEx)/damage 0.5: This Command will sets instant dummy's damege multiplier to 0.5f(50%)";
        public override string Description => "Changes instant dummy's damege multiplier state";
        public override void Action(CommandCaller caller, string input, string[] args)
        {
            if (!float.TryParse(args[0], out float num))
            {
                throw new UsageException(args[0] + " is not a number. Ex)/dummyDM 0.5");
            }
            SinsNPC.InstantDummyDamegeMultiplier = num;
            string key = "Mods.SinsMod.DamegeMultiplierCommand";
            if (Main.netMode != 2)
            {
                Main.NewText(Language.GetTextValue(key, num, num * 100), SinsColor.MultiTypeColor);
            }
            else
            {
                NetMessage.BroadcastChatMessage(NetworkText.FromKey(key, num, num * 100), SinsColor.MultiTypeColor);
            }
        }
    }
    internal class BuffImmunityCommand : ModCommand
    {
        public override CommandType Type => CommandType.World;
        public override string Command => "buff";
        public override string Usage => "/buff [boolean]" +
            "\nEx)/buff true: This Command will enables instant dummy's buff immunity";
        public override string Description => "Toggle instant dummy's buff immunity.";
        public override void Action(CommandCaller caller, string input, string[] args)
        {
            if (!bool.TryParse(args[0], out bool flag))
            {
                throw new UsageException("Use /buff true or /dummyBI false");
            }
            SinsNPC.InstantDummyBuffImmunity = flag;
            string key = "Mods.SinsMod.BuffImmunityCommand";
            string text = SinsNPC.InstantDummyBuffImmunity ? "Enabled" : "Disabled";
            if (Main.netMode != 2)
            {
                Main.NewText(Language.GetTextValue(key, text), SinsColor.MultiTypeColor);
            }
            else
            {
                NetMessage.BroadcastChatMessage(NetworkText.FromKey(key, text), SinsColor.MultiTypeColor);
            }
        }
    }
    internal class ScaleCommand : ModCommand
    {
        public override CommandType Type => CommandType.World;
        public override string Command => "scale";
        public override string Usage => "/scale [scale]" +
            "\nEx)/scale 0.5: This Command will sets active instant dummy's scale to 0.5f(50%)";
        public override string Description => "Changes active instant dummy's scale state";
        public override void Action(CommandCaller caller, string input, string[] args)
        {
            if (!float.TryParse(args[0], out float num))
            {
                throw new UsageException(args[0] + " is not a number. Ex)/dummySize 0.5");
            }
            if (SinsNPC.InstantDummyScale != num)
            {
                for (int i = 0; i < 200; i++)
                {
                    if (Main.npc[i].active && Main.npc[i].type == mod.NPCType("InstantDummy"))
                    {
                        Main.npc[i].position.X = Main.npc[i].position.X + Main.npc[i].width / 2 * num * (num < SinsNPC.InstantDummyScale ? 1 : -1);
                        Main.npc[i].position.Y = Main.npc[i].position.Y + Main.npc[i].height / 2 * num * (num < SinsNPC.InstantDummyScale ? 1 : -1);
                        Main.npc[i].width = (int)(14 * num);
                        Main.npc[i].height = (int)(48 * num);
                    }
                }
            }
            SinsNPC.InstantDummyScale = num;
            string key = "Mods.SinsMod.ScaleCommand";
            if (Main.netMode != 2)
            {
                Main.NewText(Language.GetTextValue(key, num, num * 100), SinsColor.MultiTypeColor);
            }
            else
            {
                NetMessage.BroadcastChatMessage(NetworkText.FromKey(key, num, num * 100), SinsColor.MultiTypeColor);
            }
        }
    }
    internal class ResetCommand : ModCommand
    {
        public override CommandType Type => CommandType.World;
        public override string Command => "reset";
        public override string Usage => "/reset" +
            "\nEx)/reset: This Command will resets instant dummy's status";
        public override string Description => "Resets instant dummy's status";
        public override void Action(CommandCaller caller, string input, string[] args)
        {
            /*if (!int.TryParse(args[0], out int num) || num != 0)
            {
                throw new UsageException(args[0] + " should be 0. Usage)/dummyReset 0");
            }*/
            SinsNPC.InstantDummyDefence = 0;
            SinsNPC.InstantDummyDamegeMultiplier = 1.0f;
            SinsNPC.InstantDummyBuffImmunity = false;
            if (SinsNPC.InstantDummyScale != 1.0f)
            {
                SinsNPC.InstantDummyScale = 1.0f;
                for (int i = 0; i < 200; i++)
                {
                    if (Main.npc[i].active && Main.npc[i].type == mod.NPCType("InstantDummy"))
                    {
                        Main.npc[i].position.X = Main.npc[i].position.X + Main.npc[i].width / 2;
                        Main.npc[i].position.Y = Main.npc[i].position.Y + Main.npc[i].height / 2;
                        Main.npc[i].width = 14;
                        Main.npc[i].height = 48;
                    }
                }
            }
            string key = "Mods.SinsMod.ResetCommand";
            if (Main.netMode != 2)
            {
                Main.NewText(Language.GetTextValue(key), SinsColor.MultiTypeColor);
            }
            else
            {
                NetMessage.BroadcastChatMessage(NetworkText.FromKey(key), SinsColor.MultiTypeColor);
            }
        }
    }
}