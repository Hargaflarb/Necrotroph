﻿using Necrotroph_Eksamensprojekt.Components;
using Necrotroph_Eksamensprojekt.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Necrotroph_Eksamensprojekt.Factories
{
    public static class TextFactory
    {
        #region Fields
        private static SpriteFont spriteFont;
        #endregion
        #region Properties
        public static SpriteFont SpriteFont { get => spriteFont; set => spriteFont = value; }
        #endregion
        #region Constructor
        #endregion
        #region Methods
        public static void LoadContent(ContentManager contentManager)
        {
            SpriteFont = contentManager.Load<SpriteFont>("textui");
        }
        public static UIObject CreateTextObject(Func<string> text, Color color, Vector2 position, float scale)
        {
            UIObject newTextObject = new TextObject(position);
            newTextObject.AddComponent<TextRenderer>(text, color, scale);
            return newTextObject;
        }
        #endregion
    }
}
