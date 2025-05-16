using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Necrotroph_Eksamensprojekt.Components
{
    public class SpriteRenderer : Component
    {
        #region Fields
        private Texture2D sprite;
        private Color colour = Color.White;
        #endregion
        #region Properties
        #endregion
        #region Constructors
        public SpriteRenderer(GameObject gameObject, Texture2D sprite)
        {
            this.sprite = sprite;
            this.gameObject = gameObject;
        }

        #endregion
        #region Methods
        public override void Execute()
        {

        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, gameObject.Transform.Position, null, colour, gameObject.Transform.Rotation, Vector2.Zero, gameObject.Transform.Scale, SpriteEffects.None, 0);
        }
        #endregion
    }
}
