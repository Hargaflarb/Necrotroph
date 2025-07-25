﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Necrotroph_Eksamensprojekt.GameObjects;
using Necrotroph_Eksamensprojekt.Commands;
using Necrotroph_Eksamensprojekt.Components;
using Necrotroph_Eksamensprojekt.Factories;
using Necrotroph_Eksamensprojekt.ObjectPools;

namespace Necrotroph_Eksamensprojekt.Components
{
    //emma
    public class SpriteRenderer : Component
    {
        #region Fields
        private Texture2D sprite;
        private Color colour = Color.White;
        private Vector2 origin;
        private SpriteEffects flipped = SpriteEffects.None;
        #endregion
        #region Properties
        public Texture2D Sprite { get => sprite; set => sprite = value; }
        public Color Colour { get => colour; set => colour = value; }
        public bool Flipped 
        {
            set
            {
                if (value)
                {
                    flipped = SpriteEffects.FlipHorizontally;
                }
                else
                {
                    flipped = SpriteEffects.None;
                }
            }
        }
        public float Layer 
        {
            get
            {
                return MathF.Min((gameObject.Transform.ScreenPosition.Y / GameWorld.ScreenSize.Y) * 0.9f, 0.9f);
            }
        }

        #endregion
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameObject">What object is the SpriteRenderer attached to (should be automatically filled out with AddComponent</param>
        /// <param name="sprite">What sprite is the first one being shown</param>
        /// <param name="layer">Which draw layer is it on (higher means closer)</param>
        public SpriteRenderer(GameObject gameObject, Texture2D sprite, float layer) : base(gameObject)
        {
            this.sprite = sprite;
            this.gameObject.Transform.Size = sprite.Bounds.Size.ToVector2();
            origin = new Vector2(sprite.Width / 2, sprite.Height / 2);
        }

        public SpriteRenderer(GameObject gameObject, Texture2D sprite, float layer, Vector2 hitboxSizeScale, Vector2 originPlacement) : base(gameObject)
        {
            this.sprite = sprite;
            this.gameObject.Transform.Size = sprite.Bounds.Size.ToVector2() * hitboxSizeScale;
            this.origin = new Vector2(sprite.Width * originPlacement.X, sprite.Height * originPlacement.Y);
        }

        public SpriteRenderer(GameObject gameObject, float layer) : base(gameObject)
        {
            this.gameObject = gameObject;
            //this.gameObject.Transform.Size = hitboxSize;
            origin = new Vector2(sprite.Width / 2, sprite.Height / 2);
        }


        #endregion
        #region Methods

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (sprite == null)
            {
                return;
            }

            spriteBatch.Draw(sprite, gameObject.Transform.ScreenPosition, null, colour, gameObject.Transform.Rotation, origin, gameObject.Transform.Scale, flipped, Layer);
        }
        #endregion
    }
}
