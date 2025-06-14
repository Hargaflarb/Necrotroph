using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Necrotroph_Eksamensprojekt.Components;
using Necrotroph_Eksamensprojekt.Enemies;
using Necrotroph_Eksamensprojekt.GameObjects;

namespace Necrotroph_Eksamensprojekt.Factories
{
    //emma
    public static class EnemyFactory
    {
        #region Fields
        private static Texture2D hunterSprite;
        private static Texture2D[] hunterWalkAnim;
        private static Texture2D lightEaterSprite;
        private static Texture2D stalkerSprite;
        #endregion
        #region Properties
        #endregion
        #region Constructors
        #endregion
        #region Methods
        public static void LoadContent(ContentManager contentManager)
        {
            hunterSprite = contentManager.Load<Texture2D>("HunterSprites/hunterSouthWest");
            lightEaterSprite = contentManager.Load<Texture2D>("LightEaterSprites/lighteaterSouthEast");
            stalkerSprite = contentManager.Load<Texture2D>("noImageFound");
        }
        public static GameObject CreateEnemy(Vector2 position, EnemyType enemy)
        {
            GameObject newEnemy;
            //oooo  <- What does this mean???
            switch (enemy)
            {
                case EnemyType.Hunter:
                    HunterEnemy.Position = position;
                    newEnemy = HunterEnemy.Instance;
                    newEnemy.AddComponent<SpriteRenderer>(hunterSprite, 1f, new Vector2(0.8f, 0.6f), new Vector2(0.5f, 0.5f));
                    /*newEnemy.AddComponent<Animator>();
                    newEnemy.GetComponent<Animator>().AddAnimation("Walk", hunterSprite);
                    newEnemy.GetComponent<Animator>().PlayAnimation("Walk");*/
                    newEnemy.AddComponent<Movable>();
                    newEnemy.Transform.Scale = 0.2f;
                    return newEnemy;
                case EnemyType.Seeker:
                    throw new NotImplementedException();
                    break;
                case EnemyType.LightEater:
                    newEnemy = new LightEaterEnemy(position);
                    newEnemy.AddComponent<SpriteRenderer>(lightEaterSprite,1f,new Vector2(0.6f,0.8f),new Vector2(0.5f,0.6f));
                    newEnemy.AddComponent<Movable>();
                    newEnemy.AddComponent<LightEmitter>(0.05f);
                    newEnemy.Transform.Scale = 0.2f;
                    return newEnemy;
                    break;
                case EnemyType.Stalker:
                    break;
            }
            return null;
        }
        #endregion
    }
}
