using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Necrotroph_Eksamensprojekt.Commands;
using Necrotroph_Eksamensprojekt.Components;
using Necrotroph_Eksamensprojekt.Enemies;
using Necrotroph_Eksamensprojekt.Factories;
using Necrotroph_Eksamensprojekt.GameObjects;
using Necrotroph_Eksamensprojekt.ObjectPools;
using Necrotroph_Eksamensprojekt.Observer;

namespace Necrotroph_Eksamensprojekt.Menu
{
    /// <summary>
    /// Selve classen var lavet af Malthe, men meget af indholdet var copiret fra GameWorld, hvilket var et fælles arbejde
    /// </summary>
    public class InGame : GameState, IListener
    {
        private List<UIObject> uIObjects;

        private List<GameObject> gameObjectsToAdd;
        private List<GameObject> activeGameObjects;
        private List<GameObject> gameObjectsToRemove;
        private Vector2 previousPlayerPosition;
        private int itemsCollected;
        private float lightSpawnRate = 25;
        private bool gameWon = false;
        private static Texture2D tileSprite;
        private Dictionary<int, GameObject> activeMemorabilia;
        private GameObject mem1;
        private GameObject mem2;
        private GameObject mem3;
        private GameObject mem4;
        private GameObject mem5;

        private static InGame instance;
        public static InGame Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new InGame();
                }
                return instance;
            }
        }
        private InGame()
        {
            uIObjects = new List<UIObject>();


        }


        public int ItemsCollected
        {
            get => itemsCollected;
            set
            {
                if (value <= 0)
                {
                    itemsCollected = 0;
                }
                else if (value >= 5)
                {
                    itemsCollected = 5;
                }
                else
                {
                    itemsCollected = value;
                }
            }
        }
        public Vector2 TileOffset
        {
            get
            {
                return new Vector2(Player.Instance.Transform.WorldPosition.X % TileSprite.Width, Player.Instance.Transform.WorldPosition.Y % TileSprite.Height);
            }
        }
        public static Texture2D TileSprite { get => tileSprite; set => tileSprite = value; }
        public Dictionary<int, GameObject> ActiveMemorabilia { get => activeMemorabilia; set => activeMemorabilia = value; }
        public GameObject Mem1 { get => mem1; set => mem1 = value; }
        public GameObject Mem2 { get => mem2; set => mem2 = value; }
        public GameObject Mem3 { get => mem3; set => mem3 = value; }
        public GameObject Mem4 { get => mem4; set => mem4 = value; }
        public GameObject Mem5 { get => mem5; set => mem5 = value; }



        public override void Initialize()
        {
            gameObjectsToAdd = new List<GameObject>();
            gameObjectsToRemove = new List<GameObject>();
            activeGameObjects = new List<GameObject>();
            activeMemorabilia = new Dictionary<int, GameObject>();

            AddPlayer(Vector2.Zero);
            AddObject(TreePool.Instance.GetObject(new Vector2(200, 0)));
            Map.GenerateMap();

            InputHandler.AddHeldKeyCommand(Keys.D, new WalkCommand(Player.Instance, new Vector2(1, 0)));
            InputHandler.AddHeldKeyCommand(Keys.A, new WalkCommand(Player.Instance, new Vector2(-1, 0)));
            InputHandler.AddHeldKeyCommand(Keys.W, new WalkCommand(Player.Instance, new Vector2(0, -1)));
            InputHandler.AddHeldKeyCommand(Keys.S, new WalkCommand(Player.Instance, new Vector2(0, 1)));
            InputHandler.AddUnclickedCommand(Keys.D, new WalkCommand(Player.Instance, new Vector2(1, 0)));
            InputHandler.AddUnclickedCommand(Keys.A, new WalkCommand(Player.Instance, new Vector2(-1, 0)));
            InputHandler.AddUnclickedCommand(Keys.W, new WalkCommand(Player.Instance, new Vector2(0, -1)));
            InputHandler.AddUnclickedCommand(Keys.S, new WalkCommand(Player.Instance, new Vector2(0, 1)));

            InputHandler.AddPressedKeyCommand(Keys.F, new TurnLightOnOffCommand());
            InputHandler.AddPressedKeyCommand(Keys.Space, new LightBurstCommand());

            //InputHandler.AddPressedKeyCommand(Keys.LeftShift, new SprintCommand());
            //InputHandler.AddUnclickedCommand(Keys.LeftShift, new SprintCommand());

            base.Initialize();
        }

        public override void LoadContent()
        {
            AddObject(EnemyFactory.CreateEnemy(new Vector2(-1000, -1000), EnemyType.Hunter));
            TimeLineManager.AddEvent(GameWorld.Rnd.Next((int)LightEaterEnemy.MinTimeBetweenLighteaters, (int)LightEaterEnemy.MaxTimeBetweenLighteaters) * 1000, SpawnLightEater);
            GameObject mem1 = MemorabeliaFactory.CreateMemorabilia(new Vector2(4000, -500));
            AddObject(mem1);
            activeMemorabilia.Add(1, mem1);
            GameObject mem2 = MemorabeliaFactory.CreateMemorabilia(new Vector2(-4000, 2500));
            AddObject(mem2);
            activeMemorabilia.Add(2, mem2);
            GameObject mem3 = MemorabeliaFactory.CreateMemorabilia(new Vector2(3600, 1000));
            AddObject(mem3);
            activeMemorabilia.Add(4, mem3);
            GameObject mem4 = MemorabeliaFactory.CreateMemorabilia(new Vector2(-1500, 500));
            AddObject(mem4);
            activeMemorabilia.Add(8, mem4);
            GameObject mem5 = MemorabeliaFactory.CreateMemorabilia(new Vector2(-2066, 0));
            AddObject(mem5);
            activeMemorabilia.Add(16, mem5);

            UIManager.AddUIObject(new UIButton(new Vector2(GameWorld.ScreenSize.X / 2, 1000), new Vector2(50, 50), "Menu", () => { GameWorld.GameStateToChangeTo = MainMenu.Instance; }));
            UIManager.AddUIObject(new UIButton(new Vector2(150, 900), new Vector2(50, 50), "Save Game", () => { SaveManager.SaveGame(); }));
            UIManager.AddUIObject(new UIButton(new Vector2(150, 1000), new Vector2(50, 50), "Load Game", () => { SaveManager.LoadGame(); }));
            UIManager.AddUIObject(TextFactory.CreateTextObject(() => { return ItemsCollected + "/5"; }, Color.White, new Vector2(50, 50), 1f));

            ShaderManager.SetSpritesAndShaders();
            TimeLineManager.AddEvent(lightSpawnRate * 1000, SpawnLightRefill);

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            InputHandler.HandleInput();
            SeekerEnemyManager.Update();
            foreach (GameObject gameObject in activeGameObjects)
            {
                if (gameObject.Active)
                {
                    gameObject.Update(gameTime);
                }

            }

            foreach (UIObject uiObject in UIManager.ActiveUIObjects)
            {
                if (uiObject.Active)
                {
                    uiObject.Update(gameTime);
                }
            }
            TimeLineManager.Update(gameTime);
            CheckCollision();


            Map.CheckForObejctsToLoad();
            Map.CheckForObjectsToUnload();
            AddAndRemoveGameObjects();
            UIManager.AddAndRemoveUIObjects();
            CheckForWin();

        }

        public override void Draw(GameTime gameTime)
        {
            ShaderManager.PrepareShadows(_spriteBatch);

            GraphicsDevice.Clear(Color.DarkGreen);

            //Higher layer numbers are closer, lower are further away
            _spriteBatch.Begin(SpriteSortMode.FrontToBack);

            //floorTiles - Malthe
            int xReptition = (int)(GameWorld.ScreenSize.X / TileSprite.Width);
            int yReptition = (int)(GameWorld.ScreenSize.Y / TileSprite.Height);
            for (int x = -1; x <= xReptition + 1; x++)
            {
                for (int y = -1; y <= yReptition + 1; y++)
                {
                    _spriteBatch.Draw(TileSprite, new Vector2(x * TileSprite.Width, y * TileSprite.Height) - TileOffset, Color.White);
                }
            }

            //gameObjects
            foreach (GameObject gameObject in activeGameObjects)
            {
                if (gameObject.Active)
                {
                    gameObject.Draw(_spriteBatch);
                }
            }

            //UI
            foreach (UIObject uiObject in UIManager.ActiveUIObjects)
            {
                if (uiObject.Active)
                {
                    uiObject.Draw(_spriteBatch);
                }
            }

#if !DEBUG
            //Shader - Malthe
            ShaderManager.Draw(_spriteBatch);
#endif

            _spriteBatch.End();

        }

        /// <summary>
        /// Malthe
        /// </summary>
        public override void Enter()
        {
            UIManager.UIObjectsToAdd.AddRange(uIObjects);
            uIObjects.Clear();
            base.Enter();
        }

        /// <summary>
        /// Malthe
        /// </summary>
        public override void Exit()
        {
            uIObjects.AddRange(UIManager.ClearUIObjects());
            base.Exit();
        }

        public void AddAndRemoveGameObjects()
        {
            foreach (GameObject gameObject in gameObjectsToAdd)
            {
                gameObject.Start();
                activeGameObjects.Add(gameObject);
            }
            gameObjectsToAdd.Clear();
            foreach (GameObject gameObject in gameObjectsToRemove)
            {
                if (activeGameObjects.Contains(gameObject))
                {
                    activeGameObjects.Remove(gameObject);
                }
            }
            gameObjectsToRemove.Clear();
        }

        /// <summary>
        /// Malthe i think
        /// </summary>
        public void CheckCollision()
        {
            for (int i = 0; i < activeGameObjects.Count; i++)
            {

                for (int j = i + 1; j < activeGameObjects.Count; j++)
                {
                    if (activeGameObjects[i].CheckCollision(activeGameObjects[j]) && activeGameObjects[j].Active)
                    {
                        activeGameObjects[i].OnCollision(activeGameObjects[j]);
                        activeGameObjects[j].OnCollision(activeGameObjects[i]);
                    }
                }

            }

        }



        /// <summary>
        /// Adds object to the gameworld during next update
        /// </summary>
        /// <param name="gameObject"></param>
        public void AddObject(GameObject gameObject)
        {
            gameObject.Awake();
            gameObjectsToAdd.Add(gameObject);
        }
        /// <summary>
        /// Removes object from the gameworld during next update
        /// </summary>
        /// <param name="gameObject"></param>
        public void RemoveObject(GameObject gameObject)
        {
            gameObjectsToRemove.Add(gameObject);
        }
        /// <summary>
        /// Method to create player
        /// </summary>
        /// <param name="position"></param>
        private void AddPlayer(Vector2 position)
        {
            Player newPlayer = Player.Instance;
            newPlayer.AddComponent<Movable>(300);
            newPlayer.AddComponent<SpriteRenderer>(Content.Load<Texture2D>("PlayerSprites/playerIdleSouthLightOn"), 1f, new Vector2(0.6f, 0.3f), new Vector2(0.5f, 0.85f));
            newPlayer.AddComponent<Animator>();
            newPlayer.AddComponent<LightEmitter>(0.2f, new Vector2(0, -55));
            //remember to add more animations
            newPlayer.GetComponent<Animator>().AddAnimation("IdleLeftLightOff", Content.Load<Texture2D>("PlayerSprites/playerIdleWestLightOff"));
            newPlayer.GetComponent<Animator>().AddAnimation("IdleLeftLightOn", Content.Load<Texture2D>("PlayerSprites/playerIdleWestLightOn"));
            newPlayer.GetComponent<Animator>().AddAnimation("IdleDownLightOff", Content.Load<Texture2D>("PlayerSprites/playerIdleSouthLightOff"));
            newPlayer.GetComponent<Animator>().AddAnimation("IdleDownLightOn", Content.Load<Texture2D>("PlayerSprites/playerIdleSouthLightOn"));
            newPlayer.GetComponent<Animator>().AddAnimation("IdleRightLightOff", Content.Load<Texture2D>("PlayerSprites/playerIdleEastLightOff"));
            newPlayer.GetComponent<Animator>().AddAnimation("IdleRightLightOn", Content.Load<Texture2D>("PlayerSprites/playerIdleEastLightOn"));
            newPlayer.GetComponent<Animator>().AddAnimation("IdleUpLightOff", Content.Load<Texture2D>("PlayerSprites/playerIdleNorthLightOff"));
            newPlayer.GetComponent<Animator>().AddAnimation("IdleUpLightOn", Content.Load<Texture2D>("PlayerSprites/playerIdleNorthLightOn"));
            newPlayer.GetComponent<Animator>().PlayAnimation("IdleDownLightOn");
            newPlayer.Transform.Scale = 0.3f;

            newPlayer.Observer = new DeathObserver();
            newPlayer.Observer.AddListener(this);

            AddObject(newPlayer);
        }


        public void HearFromObserver(IObserver observer)
        {
            //currently appears under the shader :/
            UIManager.AddUIObject(TextFactory.CreateTextObject(() => { return "Game Over"; }, Color.White, GameWorld.ScreenSize / 2, 2f));
        }

        /// <summary>
        /// Gathers <see cref="LightEmitter"/>s and <see cref="ShadowCaster"/>s and converts it to shader data. - Malthe
        /// </summary>
        /// <returns>A List of <see cref="LightEmitter"/>s and their corrosponding <see cref="ShadowInterval"/>s.</returns>
        public List<(LightEmitter lightEmitters, List<ShadowInterval> shadowIntervals)> GetShaderData()
        {
            List<LightEmitter> lightEmitters = new List<LightEmitter>();
            List<ShadowCaster> shadowCasters = new List<ShadowCaster>();
            List<(LightEmitter lightEmitters, List<ShadowInterval> shadowIntervals)> shadows = new List<(LightEmitter lightEmitters, List<ShadowInterval> shadowIntervals)>();
            foreach (GameObject gameObject in activeGameObjects)
            {
                Component component;
                if ((component = gameObject.GetComponent<LightEmitter>()) is not null)
                {
                    lightEmitters.Add((LightEmitter)component);
                }
                else if ((component = gameObject.GetComponent<ShadowCaster>()) is not null)
                {
                    shadowCasters.Add((ShadowCaster)component);
                }

            }

            foreach (LightEmitter light in lightEmitters)
            {
                List<ShadowInterval> shadowIntervals = new List<ShadowInterval>();
                foreach (ShadowCaster shadowCaster in shadowCasters)
                {
                    if ((shadowCaster.GameObject.Transform.WorldPosition - light.GameObject.Transform.WorldPosition).Length() < light.LightRadius * GameWorld.ScreenSize.X)
                    {
                        shadowIntervals.Add(new ShadowInterval(shadowCaster, light));
                    }
                }
                shadows.Add((light, shadowIntervals));
            }

            return shadows;
        }

        public void CheckForWin()
        {
            if (ItemsCollected == 5 && !gameWon)
            {
                activeGameObjects.Clear();
                UIManager.ActiveUIObjects.Clear();
                gameWon = true;
                string win = "You win";

                UIManager.AddUIObject(TextFactory.CreateTextObject(() => { return win; }, Color.White, new Vector2(GameWorld.ScreenSize.X / 2, GameWorld.ScreenSize.Y / 2), 2f));
            }
        }

        /// <summary>
        /// Spawns a light refill in a random direction outside the screen borders, lights spawn twice as fast if the player has low health
        /// </summary>
        public void SpawnLightRefill()
        {
            Vector2 newPosition = Vector2.Zero;
            switch (GameWorld.Rnd.Next(0, 4))
            {
                case 0:
                    newPosition = new Vector2(Player.Instance.Transform.WorldPosition.X - (GameWorld.ScreenSize.X / 2) - 40, GameWorld.Rnd.Next((int)(Player.Instance.Transform.WorldPosition.Y - (GameWorld.ScreenSize.Y / 2)), (int)(Player.Instance.Transform.WorldPosition.Y + (GameWorld.ScreenSize.Y / 2))));
                    break;
                case 1:
                    newPosition = new Vector2(Player.Instance.Transform.WorldPosition.X + (GameWorld.ScreenSize.X / 2) + 40, GameWorld.Rnd.Next((int)(Player.Instance.Transform.WorldPosition.Y - (GameWorld.ScreenSize.Y / 2)), (int)(Player.Instance.Transform.WorldPosition.Y + (GameWorld.ScreenSize.Y / 2))));
                    break;
                case 2:
                    newPosition = new Vector2(GameWorld.Rnd.Next((int)(Player.Instance.Transform.WorldPosition.X - (GameWorld.ScreenSize.X / 2)), (int)(Player.Instance.Transform.WorldPosition.X + (GameWorld.ScreenSize.X / 2))), Player.Instance.Transform.WorldPosition.Y - (GameWorld.ScreenSize.Y / 2) - 40);
                    break;
                case 3:
                    newPosition = new Vector2(GameWorld.Rnd.Next((int)(Player.Instance.Transform.WorldPosition.X - (GameWorld.ScreenSize.X / 2)), (int)(Player.Instance.Transform.WorldPosition.X + (GameWorld.ScreenSize.X / 2))), Player.Instance.Transform.WorldPosition.Y + (GameWorld.ScreenSize.Y / 2) + 40);
                    break;
            }
            AddObject(LightPool.Instance.GetObject(newPosition));
            if (Player.Instance.Life < 20)
            {
                TimeLineManager.AddEvent(lightSpawnRate / 2 * 1000, SpawnLightRefill);
            }
            else
            {
                TimeLineManager.AddEvent(lightSpawnRate * 1000, SpawnLightRefill);
            }
        }
        public void SpawnLightEater()
        {
            Vector2 newPosition = Vector2.Zero;
            switch (GameWorld.Rnd.Next(0, 4))
            {
                case 0:
                    newPosition = new Vector2(Player.Instance.Transform.WorldPosition.X - (GameWorld.ScreenSize.X / 2) - 40, GameWorld.Rnd.Next((int)(Player.Instance.Transform.WorldPosition.Y - (GameWorld.ScreenSize.Y / 2)), (int)(Player.Instance.Transform.WorldPosition.Y + (GameWorld.ScreenSize.Y / 2))));
                    break;
                case 1:
                    newPosition = new Vector2(Player.Instance.Transform.WorldPosition.X + (GameWorld.ScreenSize.X / 2) + 40, GameWorld.Rnd.Next((int)(Player.Instance.Transform.WorldPosition.Y - (GameWorld.ScreenSize.Y / 2)), (int)(Player.Instance.Transform.WorldPosition.Y + (GameWorld.ScreenSize.Y / 2))));
                    break;
                case 2:
                    newPosition = new Vector2(GameWorld.Rnd.Next((int)(Player.Instance.Transform.WorldPosition.X - (GameWorld.ScreenSize.X / 2)), (int)(Player.Instance.Transform.WorldPosition.X + (GameWorld.ScreenSize.X / 2))), Player.Instance.Transform.WorldPosition.Y - (GameWorld.ScreenSize.Y / 2) - 40);
                    break;
                case 3:
                    newPosition = new Vector2(GameWorld.Rnd.Next((int)(Player.Instance.Transform.WorldPosition.X - (GameWorld.ScreenSize.X / 2)), (int)(Player.Instance.Transform.WorldPosition.X + (GameWorld.ScreenSize.X / 2))), Player.Instance.Transform.WorldPosition.Y + (GameWorld.ScreenSize.Y / 2) + 40);
                    break;
            }
            AddObject(EnemyFactory.CreateEnemy(newPosition, EnemyType.LightEater));

            TimeLineManager.AddEvent(GameWorld.Rnd.Next((int)LightEaterEnemy.MinTimeBetweenLighteaters,(int)LightEaterEnemy.MaxTimeBetweenLighteaters) * 1000, SpawnLightEater);
        }
    }
}
