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

namespace Necrotroph_Eksamensprojekt.GameObjects
{
    public class Tree : GameObject
    {
        #region Fields
        #endregion
        #region Properties
        #endregion
        #region Constructors
        public Tree(Vector2 position) : base(position) 
        {
            //AddComponent<SpriteRenderer>(GameWorld.Instance.Content.Load<Texture2D>("noImageFound"), 1f, new Vector2(50,50));
            //AddComponent<Collider>();
            Transform.Scale = 0.2f;
        }
        #endregion
        #region Methods
        #endregion
    }
}
