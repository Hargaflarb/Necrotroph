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
    /// <summary>
    /// emma
    /// </summary>
    public class Tree : GameObject
    {
        #region Fields
        private bool hadEyes = false;
        #endregion
        #region Properties
        public static bool HasEyes { get; set; } = false;
        #endregion
        #region Constructors
        public Tree(Vector2 position) : base(position)
        {
            //AddComponent<SpriteRenderer>(GameWorld.Instance.Content.Load<Texture2D>("noImageFound"), 1f, new Vector2(50,50));
            //AddComponent<Collider>();
            Transform.Scale = 0.4f;
        }
        #endregion
        #region Methods

        public override void Update(GameTime gameTime)
        {
            if (hadEyes != HasEyes)
            {
                ChangeSprite();
                hadEyes = HasEyes;
            }
            base.Update(gameTime);
        }
        private void ChangeSprite()
        {
            if (HasEyes)
            {
                GetComponent<Animator>().PlayAnimation("Seek");
            }
            else
            {
                GetComponent<Animator>().PlayAnimation("Normal");
            }
        }
        #endregion
    }
}
