using Necrotroph_Eksamensprojekt.GameObjects;
using Necrotroph_Eksamensprojekt.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Necrotroph_Eksamensprojekt.GameStates
{
    public class EndScreen : GameState
    {
        private static List<UIObject> uIObjects;


        private static EndScreen instance;
        public static EndScreen Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new EndScreen();
                }
                return instance;
            }
        }

        private EndScreen()
        {
            uIObjects = new List<UIObject>();
        }


        public override void LoadContent()
        {
            //UIManager.AddUIObject(new UIButton(new Vector2(GameWorld.ScreenSize.X / 2, GameWorld.ScreenSize.Y / 2), new Vector2(50, 50), "Restart", () => { GameWorld.GameStateToChangeTo = InGame.Instance; }));
            UIManager.AddUIObject(new UIButton(new Vector2(GameWorld.ScreenSize.X / 2, GameWorld.ScreenSize.Y / 2), new Vector2(50, 50), "Main Menu", () => { GameWorld.GameStateToChangeTo = MainMenu.Instance; }));
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
