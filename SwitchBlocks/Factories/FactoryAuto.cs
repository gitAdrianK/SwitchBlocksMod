﻿using JumpKing.API;
using JumpKing.Level;
using JumpKing.Level.Sampler;
using JumpKing.Workshop;
using Microsoft.Xna.Framework;
using SwitchBlocks.Blocks;
using System;
using System.Collections.Generic;

namespace SwitchBlocks.Factories
{
    /// <summary>
    /// Factory for auto blocks.
    /// </summary>
    public class FactoryAuto : IBlockFactory
    {
        private static readonly HashSet<Color> supportedBlockCodes = new HashSet<Color> {
            ModBlocks.AUTO_ON,
            ModBlocks.AUTO_OFF,
            ModBlocks.AUTO_RESET,
        };

        public bool CanMakeBlock(Color blockCode, Level level)
        {
            return supportedBlockCodes.Contains(blockCode);
        }

        public bool IsSolidBlock(Color blockCode)
        {
            switch (blockCode)
            {
                case var _ when blockCode == ModBlocks.AUTO_ON:
                    return true;
                case var _ when blockCode == ModBlocks.AUTO_OFF:
                    return true;
            }
            return false;
        }

        public IBlock GetBlock(Color blockCode, Rectangle blockRect, Level level, LevelTexture textureSrc, int currentScreen, int x, int y)
        {
            switch (blockCode)
            {
                case var _ when blockCode == ModBlocks.AUTO_ON:
                    return new BlockAutoOn(blockRect);
                case var _ when blockCode == ModBlocks.AUTO_OFF:
                    return new BlockAutoOff(blockRect);
                case var _ when blockCode == ModBlocks.AUTO_RESET:
                    return new BlockAutoReset(blockRect);
                default:
                    throw new InvalidOperationException($"{typeof(FactoryAuto).Name} is unable to create a block of Color code ({blockCode.R}, {blockCode.G}, {blockCode.B})");
            }
        }
    }
}
