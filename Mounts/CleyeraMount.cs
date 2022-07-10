using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace SinsMod.Mounts
{
    public class CleyeraMount : ModMountData
    {
        public override void SetDefaults()
        {
            mountData.buff = mod.BuffType("CleyeraMount");
            mountData.spawnDust = 110;
            mountData.spawnDustNoGravity = true;
            mountData.emitsLight = true;
            mountData.lightColor = new Vector3(0.8f, 0.8f, 0.8f);
            mountData.heightBoost = 12;
            mountData.flightTimeMax = int.MaxValue;
            mountData.fatigueMax = int.MaxValue;
            mountData.fallDamage = 0f;
            mountData.usesHover = true;
            mountData.runSpeed = 24;
            mountData.dashSpeed = 24;
            mountData.acceleration = 2f;
            mountData.swimSpeed = 24;
            mountData.jumpHeight = 24;
            mountData.jumpSpeed = 24;
            mountData.blockExtraJumps = true;
            mountData.constantJump = false;
            mountData.totalFrames = 4;
            int[] array = new int[mountData.totalFrames];
            for (int l = 0; l < array.Length; l++)
            {
                array[l] = 12;
            }
            mountData.playerYOffsets = array;
            mountData.xOffset = 16;
            mountData.bodyFrame = 3;
            mountData.yOffset = 4;
            mountData.playerHeadOffset = 18;
            mountData.standingFrameCount = 4;
            mountData.standingFrameDelay = 4;
            mountData.standingFrameStart = 0;
            mountData.runningFrameCount = 4;
            mountData.runningFrameDelay = 4;
            mountData.runningFrameStart = 0;
            mountData.flyingFrameCount = 4;
            mountData.flyingFrameDelay = 4;
            mountData.flyingFrameStart = 0;
            mountData.inAirFrameCount = 4;
            mountData.inAirFrameDelay = 4;
            mountData.inAirFrameStart = 0;
            mountData.idleFrameCount = 4;
            mountData.idleFrameDelay = 4;
            mountData.idleFrameStart = 0;
            mountData.idleFrameLoop = true;
            mountData.swimFrameCount = 4;
            mountData.swimFrameDelay = 4;
            mountData.swimFrameStart = 0;
            if (Main.netMode != 2)
            {
                mountData.textureWidth = mountData.backTexture.Width;
                mountData.textureHeight = mountData.backTexture.Height;
            }
        }
        public override void UpdateEffects(Player player)
        {
            SinsPlayer modPlayer = player.GetModPlayer<SinsPlayer>();
            if (player.controlJump && modPlayer.mountDelay == 0)
            {
                player.velocity.Y = -18;
            }
            if (player.controlDown && player.velocity.Y != 0 && modPlayer.mountDelay == 0)
            {
                player.velocity.Y = 18;
            }
            if (!player.controlRight && !player.controlLeft)
            {
                player.velocity.X *= 0;
            }
            if (!player.controlJump && !player.controlUp && !player.controlDown)
            {
                player.velocity.Y *= 0;
            }
        }
    }
}