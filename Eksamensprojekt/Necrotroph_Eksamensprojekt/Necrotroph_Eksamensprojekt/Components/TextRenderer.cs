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
        private Func<string> text;
        private Color textColor;
        private float scale;
        #endregion

        #region Properties
        #endregion
        
        #region Constructor
        public TextRenderer(UIObject uiObject, Func<string> text, Color textColor, float scale) : base(uiObject)
        {
            this.text = text;
            this.textColor = textColor;
            uiObject.Transform.Scale = scale;
        }
        public TextRenderer(UIObject uiObject, string text, Color textColor, float scale) : base(uiObject)
        {
            this.text = () => text;
            this.textColor = textColor;
            uiObject.Transform.Scale = scale;
        }

        #endregion
        #region Methods
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (text == null)
            {
                return;
            }
            spriteBatch.DrawString(TextFactory.SpriteFont, text(), UIObject.Transform.ScreenPosition, textColor, 0, TextFactory.SpriteFont.MeasureString(text()) / 2, uiObject.Transform.Scale, SpriteEffects.None, 1f);
        }
        #endregion
    }
}
