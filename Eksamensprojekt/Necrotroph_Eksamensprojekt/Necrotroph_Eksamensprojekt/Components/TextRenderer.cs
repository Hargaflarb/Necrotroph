using Necrotroph_Eksamensprojekt.Factories;
using Necrotroph_Eksamensprojekt.GameObjects;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Necrotroph_Eksamensprojekt.Components
{
    public class TextRenderer : Component
    {
        #region Fields
        private SpriteFont spriteFont;
        private string text;
        private Color textColor;
        #endregion
        #region Properties
        #endregion
        #region Constructor
        public TextRenderer(UIObject uiObject, string text, Color textColor) : base(uiObject)
        {
            this.text = text;
            this.textColor = textColor;
        }
        #endregion
        #region Methods
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (text == null)
            {
                return;
            }
            spriteBatch.DrawString(TextFactory.SpriteFont, text, UIObject.Transform.Position, textColor);
        }
        #endregion
    }
}
