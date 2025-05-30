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
using Necrotroph_Eksamensprojekt.GameObjects;

namespace Necrotroph_Eksamensprojekt.Factories
{
    public static class EnemyFactory
    {
        #region Fields
        private static Texture2D hunterSprite;
        private static Texture2D[] hunterWalkAnim;
        private static Texture2D seekerSprite;
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
            seekerSprite = contentManager.Load<Texture2D>("noImageFound");
            lightEaterSprite = contentManager.Load<Texture2D>("noImageFound");
            stalkerSprite = contentManager.Load<Texture2D>("noImageFound");
        }
        public static GameObject CreateEnemy(Vector2 position, EnemyType enemy)
        {
            GameObject newEnemy;
            //oooo  <- What does this mean???
            switch (enemy)
            {
                case EnemyType.Hunter:
                    newEnemy = HunterEnemy.Instance;
                    newEnemy.AddComponent<SpriteRenderer>(hunterSprite, 1f, new Vector2(0.8f, 0.6f), new Vector2(0.5f, 0.5f));
                    /*newEnemy.AddComponent<Animator>();
                    newEnemy.GetComponent<Animator>().AddAnimation("Walk", hunterSprite);
                    newEnemy.GetComponent<Animator>().PlayAnimation("Walk");*/
                    newEnemy.AddComponent<Movable>();
                    //newEnemy.AddComponent<Collider>();
                    newEnemy.Transform.Scale = 0.3f;
                    return newEnemy;
                case EnemyType.Seeker:
                    break;
                case EnemyType.LightEater:
                    break;
                case EnemyType.Stalker:
                    break;
            }
            return null;
        }
        #endregion
    }
}
