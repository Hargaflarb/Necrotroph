﻿using Necrotroph_Eksamensprojekt.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Necrotroph_Eksamensprojekt.Menu
{
    /// <summary>
    /// Malthe
    /// </summary>
    public class MainMenu : GameState
    {
        private static List<UIObject> uIObjects;
        private static bool hasBeenLoaded = false;

        private static MainMenu instance;
        public static MainMenu Instance 
        {
            get
            {
                if (instance == null)
                {
                    instance = new MainMenu();
                }
                return instance;
            }
        }
        private MainMenu()
        {
            uIObjects = new List<UIObject>();
            SoundManager.Instance.StopMusic();
        }


        public override void Initialize()
        {
            base.Initialize();
        }

        public override void LoadContent()
        {
            UIManager.AddUIObject(new UIButton(new Vector2(GameWorld.ScreenSize.X / 2, GameWorld.ScreenSize.Y / 2), new Vector2(50, 50), "Start Game", () => { GameWorld.GameStateToChangeTo = InGame.Instance; }));
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            foreach (UIObject UIObject in UIManager.ActiveUIObjects)
            {
                UIObject.Update(gameTime);
            }

            UIManager.AddAndRemoveUIObjects();
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin(SpriteSortMode.FrontToBack);

            foreach (UIObject uiObject in UIManager.ActiveUIObjects)
            {
                if (uiObject.Active)
                {
                    uiObject.Draw(_spriteBatch);
                }
            }

            _spriteBatch.End();

        }

        public override void Enter()
        {
            UIManager.UIObjectsToAdd.AddRange(uIObjects);
            uIObjects.Clear();
            base.Enter();
        }

        public override void Exit()
        {
            uIObjects.AddRange(UIManager.ClearUIObjects());
            base.Exit();
        }

    }
}
