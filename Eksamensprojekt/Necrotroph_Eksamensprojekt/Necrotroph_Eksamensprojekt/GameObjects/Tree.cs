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
<<<<<<< HEAD:Eksamensprojekt/Necrotroph_Eksamensprojekt/Necrotroph_Eksamensprojekt/GameObjects/Tree.cs
        public Tree(Vector2 position) : base(position)
        {

=======
        public Tree(Vector2 position) : base(position) 
        {
            Transform.Scale = 20f;
            AddComponent<SpriteRenderer>(GameWorld.Instance.Content.Load<Texture2D>("noImageFound"), 1f);
            AddComponent<Collider>();
>>>>>>> Main-2:Eksamensprojekt/Necrotroph_Eksamensprojekt/Necrotroph_Eksamensprojekt/Tree.cs
        }
        #endregion
        #region Methods
        #endregion
    }
}
