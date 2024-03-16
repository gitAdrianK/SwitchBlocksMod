﻿using EntityComponent;
using JumpKing;
using JumpKing.Level;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SwitchBlocksMod.Data;
using System;
using System.Collections.Generic;

namespace SwitchBlocksMod.Entities
{
    public class EntitySandPlatforms : Entity, IDisposable
    {
        private static EntitySandPlatforms instance;
        public static EntitySandPlatforms Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new EntitySandPlatforms();
                }
                return instance;
            }
        }

        public void Dispose()
        {
            PlatformDictionary = null;
            currentPlatformList = null;
            instance = null;
        }

        private EntitySandPlatforms()
        {
            PlatformDictionary = Platform.GetPlatformsDictonary("sand");
        }

        int currentScreen = -1;
        int nextScreen;

        float offset;

        public Dictionary<int, List<Platform>> PlatformDictionary { get; private set; }
        List<Platform> currentPlatformList;

        protected override void Update(float deltaTime)
        {
            if (PlatformDictionary == null)
            {
                return;
            }

            nextScreen = LevelManager.CurrentScreen.GetIndex0();
            if (currentScreen != nextScreen)
            {
                if (PlatformDictionary.ContainsKey(nextScreen))
                {
                    currentPlatformList = PlatformDictionary[nextScreen];
                }
                else
                {
                    currentPlatformList = null;
                }
                currentScreen = nextScreen;
            }

            offset += deltaTime;
        }

        public override void Draw()
        {
            if (currentPlatformList == null)
            {
                return;
            }

            SpriteBatch spriteBatch = Game1.spriteBatch;
            foreach (Platform platform in currentPlatformList)
            {
                DrawPlatform(platform, spriteBatch);
            }
        }

        private void DrawPlatform(Platform platform, SpriteBatch spriteBatch)
        {
            int roundedOffset = (int)offset % (platform.texture.Height / 2);
            if (platform.startState == DataSand.State)
            {
                spriteBatch.Draw(
                texture: platform.texture,
                position: platform.position,
                sourceRectangle: new Rectangle(
                    0,
                    platform.texture.Height / 2 - roundedOffset,
                    platform.texture.Width,
                    platform.texture.Height / 2),
                color: Color.White);
            }
            else
            {
                spriteBatch.Draw(
                texture: platform.texture,
                position: platform.position,
                sourceRectangle: new Rectangle(
                    0,
                    roundedOffset,
                    platform.texture.Width,
                    platform.texture.Height / 2),
                color: Color.White);

            }
        }
    }
}
