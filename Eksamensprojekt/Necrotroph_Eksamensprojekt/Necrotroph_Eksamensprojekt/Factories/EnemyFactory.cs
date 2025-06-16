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
        #region StalkerHidingSprites
        private static Texture2D stalkerHideLeft1Stage1;
        private static Texture2D stalkerHideLeft1Stage2;
        private static Texture2D stalkerHideLeft1Stage3;
        private static Texture2D stalkerHideLeft1Stage4;
        private static Texture2D stalkerHideLeft1Stage5;
        private static Texture2D stalkerHideLeft2Stage1;
        private static Texture2D stalkerHideLeft2Stage2;
        private static Texture2D stalkerHideLeft2Stage3;
        private static Texture2D stalkerHideLeft2Stage4;
        private static Texture2D stalkerHideLeft2Stage5;
        private static Texture2D stalkerHideRight1Stage1;
        private static Texture2D stalkerHideRight1Stage2;
        private static Texture2D stalkerHideRight1Stage3;
        private static Texture2D stalkerHideRight1Stage4;
        private static Texture2D stalkerHideRight1Stage5;
        private static Texture2D stalkerHideRight2Stage1;
        private static Texture2D stalkerHideRight2Stage2;
        private static Texture2D stalkerHideRight2Stage3;
        private static Texture2D stalkerHideRight2Stage4;
        private static Texture2D stalkerHideRight2Stage5;
        #endregion
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
                    newEnemy.AddComponent<SpriteRenderer>(hunterSprite, new Vector2(0.8f, 0.6f), new Vector2(0.5f, 0.5f));
                    /*newEnemy.AddComponent<Animator>();
                    newEnemy.GetComponent<Animator>().AddAnimation("Walk", hunterSprite);
                    newEnemy.GetComponent<Animator>().PlayAnimation("Walk");*/
                    newEnemy.AddComponent<Movable>();
                    newEnemy.Transform.Scale = 0.2f;
                    return newEnemy;
                case EnemyType.Seeker:
                    throw new NotImplementedException();
                case EnemyType.LightEater:
                    newEnemy = new LightEaterEnemy(position);
                    newEnemy.AddComponent<SpriteRenderer>(lightEaterSprite,new Vector2(0.6f,0.8f),new Vector2(0.5f,0.6f));
                    newEnemy.AddComponent<Movable>();
                    newEnemy.AddComponent<LightEmitter>(0.05f,new Vector2(0,40));
                    newEnemy.Transform.Scale = 0.2f;
                    return newEnemy;
                case EnemyType.Stalker:
                    newEnemy = new LurkerEnemy(position);
                    newEnemy.AddComponent<SpriteRenderer>(stalkerSprite);
                    newEnemy.AddComponent<Animator>();
                    #region AddAllTheSprites
                    newEnemy.GetComponent<Animator>().AddAnimation("Run", stalkerSprite);
                    newEnemy.GetComponent<Animator>().AddAnimation("HideLeft1Stage1", stalkerHideLeft1Stage1);
                    newEnemy.GetComponent<Animator>().AddAnimation("HideLeft1Stage2", stalkerHideLeft1Stage2);
                    newEnemy.GetComponent<Animator>().AddAnimation("HideLeft1Stage3", stalkerHideLeft1Stage3);
                    newEnemy.GetComponent<Animator>().AddAnimation("HideLeft1Stage4", stalkerHideLeft1Stage4);
                    newEnemy.GetComponent<Animator>().AddAnimation("HideLeft1Stage5", stalkerHideLeft1Stage5);
                    newEnemy.GetComponent<Animator>().AddAnimation("HideLeft2Stage1", stalkerHideLeft2Stage1);
                    newEnemy.GetComponent<Animator>().AddAnimation("HideLeft2Stage2", stalkerHideLeft2Stage2);
                    newEnemy.GetComponent<Animator>().AddAnimation("HideLeft2Stage3", stalkerHideLeft2Stage3);
                    newEnemy.GetComponent<Animator>().AddAnimation("HideLeft2Stage4", stalkerHideLeft2Stage4);
                    newEnemy.GetComponent<Animator>().AddAnimation("HideLeft2Stage5", stalkerHideLeft2Stage5);
                    newEnemy.GetComponent<Animator>().AddAnimation("HideRight1Stage1", stalkerHideRight1Stage1);
                    newEnemy.GetComponent<Animator>().AddAnimation("HideRight1Stage2", stalkerHideRight1Stage2);
                    newEnemy.GetComponent<Animator>().AddAnimation("HideRight1Stage3", stalkerHideRight1Stage3);
                    newEnemy.GetComponent<Animator>().AddAnimation("HideRight1Stage4", stalkerHideRight1Stage4);
                    newEnemy.GetComponent<Animator>().AddAnimation("HideRight1Stage5", stalkerHideRight1Stage5);
                    newEnemy.GetComponent<Animator>().AddAnimation("HideRight2Stage1", stalkerHideRight2Stage1);
                    newEnemy.GetComponent<Animator>().AddAnimation("HideRight2Stage2", stalkerHideRight2Stage2);
                    newEnemy.GetComponent<Animator>().AddAnimation("HideRight2Stage3", stalkerHideRight2Stage3);
                    newEnemy.GetComponent<Animator>().AddAnimation("HideRight2Stage4", stalkerHideRight2Stage4);
                    newEnemy.GetComponent<Animator>().AddAnimation("HideRight2Stage5", stalkerHideRight2Stage5);
                    #endregion
                    newEnemy.AddComponent<Movable>();
                    newEnemy.Transform.Scale = 1f;
                    return newEnemy;
            }
            return null;
        }
        #endregion
    }
}
