using Necrotroph_Eksamensprojekt.Commands;
using Necrotroph_Eksamensprojekt.Components;
using Necrotroph_Eksamensprojekt.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Necrotroph_Eksamensprojekt.GameObjects
{
    /// <summary>
    /// Malthe
    /// </summary>
    public class UIButton : UIObject
    {
        private string text;
        private Color color;
        private float scale;
        private Action action;

        public UIButton(Vector2 position, Vector2 size, string text, Action action) : base(position)
        {
            Transform.Size = size;
            this.text = text;
            color = Color.White;
            scale = 1f;
            this.action = action;

            AddComponent<TextRenderer>(this.text, color, scale);
            //AddComponent<SpriteRenderer>(GameWorld.Instance.Content.Load<Texture2D>("butan"), 1f);
        }

        public override void Update(GameTime gameTime)
        {
            IsButtonPressed(InputHandler.CurrentMouseState);
            base.Update(gameTime);
        }

        /// <summary>
        /// checks if the button was pressed and runs it's action.
        /// </summary>
        /// <param name="mousePosition">The position of the mouse when it was pressed.</param>
        public void IsButtonPressed(MouseState mouseState)
        {
            if (mouseState.LeftButton == ButtonState.Pressed & InputHandler.LastMouseState != InputHandler.CurrentMouseState)
            {
                Vector2 dif = Transform.ScreenPosition - mouseState.Position.ToVector2();
                if (MathF.Abs(dif.X) < Transform.Size.X & MathF.Abs(dif.Y) < Transform.Size.Y)
                {
                    action();
                }
            }
        }
    }
}
