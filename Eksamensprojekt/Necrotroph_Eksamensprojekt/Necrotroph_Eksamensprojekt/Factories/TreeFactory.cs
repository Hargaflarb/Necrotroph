using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Necrotroph_Eksamensprojekt.Components;
using Necrotroph_Eksamensprojekt.GameObjects;


namespace Necrotroph_Eksamensprojekt.Factories
{
    //emma
    public static class TreeFactory
    {
        #region Fields
        private static Texture2D tree1;
        private static Texture2D tree2;
        private static Texture2D tree3;
        private static Texture2D seeker1;
        private static Texture2D seeker2;
        private static Texture2D seeker3;
        #endregion
        #region Properties
        #endregion
        #region Constructors
        #endregion
        #region Methods
        public static void LoadContent(ContentManager content)
        {
            tree1 = content.Load<Texture2D>("TreeSprites/Tree1");
            tree2 = content.Load<Texture2D>("TreeSprites/Tree2");
            tree3 = content.Load<Texture2D>("TreeSprites/SmallTree");
            seeker1 = content.Load<Texture2D>("TreeSprites/Seeker1");
            seeker2 = content.Load<Texture2D>("TreeSprites/Seeker2");
            seeker3 = content.Load<Texture2D>("TreeSprites/SmallSeeker");
        }
        public static Tree CreateTree(Vector2 position)
        {
            Tree newTree = new Tree(position);
            Animator animator = newTree.AddComponent<Animator>();
            //randomly select sprite
            switch (GameWorld.Rnd.Next(1, 4))
            {
                case 1: //big 1
                    newTree.AddComponent<SpriteRenderer>(tree1, new Vector2(0.5f, 0.15f), new Vector2(0.5f, 0.85f));
                    newTree.AddComponent<ShadowCaster>(80);
                    animator.AddAnimation("Normal", tree1);
                    animator.AddAnimation("Seek", seeker1);
                    break;
                case 2: //big 2
                    newTree.AddComponent<SpriteRenderer>(tree2, new Vector2(0.5f, 0.15f), new Vector2(0.5f, 0.85f));
                    newTree.AddComponent<ShadowCaster>(80);
                    animator.AddAnimation("Normal", tree2);
                    animator.AddAnimation("Seek", seeker2);
                    break;
                case 3: //small
                    newTree.AddComponent<SpriteRenderer>(tree3, new Vector2(0.5f, 0.1f), new Vector2(0.5f, 0.95f));
                    newTree.AddComponent<ShadowCaster>(20);
                    animator.AddAnimation("Normal", tree3);
                    animator.AddAnimation("Seek", seeker3);
                    break;
            }
            if (Tree.HasEyes)
            {
                animator.PlayAnimation("Seek");
            }
            else
            {
                animator.PlayAnimation("Normal");
            }
            newTree.AddComponent<Collider>();
            return newTree;
        }
        #endregion
    }
}
