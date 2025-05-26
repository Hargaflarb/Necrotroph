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
    public static class TreeFactory
    {
        #region Fields
        private static Texture2D tree1;
        private static Texture2D tree2;
        private static Texture2D tree3;
        private static Random rnd = new Random();
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
        }
        public static Tree CreateTree(Vector2 position)
        {
            Tree newTree = new Tree(position);
            //randomly select sprite
            switch (rnd.Next(1, 4))
            {
                case 1:
                    newTree.AddComponent<SpriteRenderer>(tree1, 1f, new Vector2(0.6f, 0.2f), new Vector2(0.6f, 0.9f));
                    break;
                case 2:
                    newTree.AddComponent<SpriteRenderer>(tree2, 1f, new Vector2(0.6f, 0.2f), new Vector2(0.6f, 0.9f));
                    break;
                case 3:
                    newTree.AddComponent<SpriteRenderer>(tree3, 1f, new Vector2(0.6f, 0.1f), new Vector2(0.6f, 0.95f));
                    break;
            }
            newTree.AddComponent<Collider>();
            return newTree;
        }
        #endregion
    }
}
