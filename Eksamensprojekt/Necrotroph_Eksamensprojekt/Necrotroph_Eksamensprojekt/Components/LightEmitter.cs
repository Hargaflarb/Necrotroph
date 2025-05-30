using Necrotroph_Eksamensprojekt.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Necrotroph_Eksamensprojekt.Components
{
    public class LightEmitter : Component
    {
        #region Fields
        private float lightRadius;
        private RenderTarget2D shadowTarget;
        private static Effect shaderShadowEffect;

        #endregion
        #region Properties
        public float LightRadius { get => lightRadius; set => lightRadius = value; }
        public float X { get => GameObject.Transform.ScreenPosition.X / (GameWorld.Instance.GraphicsDevice.PresentationParameters.BackBufferWidth + 50); }
        public float Y { get => (GameObject.Transform.ScreenPosition.Y - gameObject.GetComponent<SpriteRenderer>().Sprite.Height * gameObject.Transform.Scale / 2) / GameWorld.Instance.GraphicsDevice.PresentationParameters.BackBufferHeight; }
        public static Effect ShaderShadowEffect { get => shaderShadowEffect; set => shaderShadowEffect = value; }


        #endregion
        #region Constructors
        public LightEmitter(GameObject gameObject, float radius) : base(gameObject)
        {
            LightRadius = radius;
            GraphicsDevice device = GameWorld.Instance.GraphicsDevice;
            shadowTarget = new RenderTarget2D(device, device.PresentationParameters.BackBufferHeight, device.PresentationParameters.BackBufferHeight);
        }
        #endregion
        #region Methods
        public float NormalizedDistanceToLight(ShadowCaster shadow)
        {
            return ((shadow.GameObject.Transform.ScreenPosition- GameObject.Transform.ScreenPosition) / (LightRadius * GameWorld.Instance.GraphicsDevice.PresentationParameters.BackBufferWidth * 2)).Length();
        }


        /// <summary>
        /// Using a list of shadowIntervals, it draws the corrosponnding shadows to the lights RenderTarget.
        /// </summary>
        public void DrawShadowsToTarget(SpriteBatch spriteBatch, List<ShadowInterval> shadows)
        {
            //shaderShadowEffect.Parameters["lightPositions"].SetValue(new Vector2(X,Y));

            GameWorld.Instance.GraphicsDevice.SetRenderTarget(shadowTarget);
            GameWorld.Instance.GraphicsDevice.Clear(new Color(0, 0, 0, 0));

            spriteBatch.Begin(blendState: BlendState.Additive, effect: ShaderShadowEffect);
            foreach (ShadowInterval shadow in shadows)
            {
                spriteBatch.Draw(GameObject.Pixel, shadowTarget.Bounds, shadow.ToDataPass());
            }
            spriteBatch.End();
        }

        public void DrawToLightMask(SpriteBatch spriteBatch)
        {
            float resizedRadius = LightRadius * GameWorld.Instance.GraphicsDevice.PresentationParameters.BackBufferWidth * 2;
            Vector2 size = new Vector2((int)resizedRadius, (int)resizedRadius);
            Point position = (GameObject.Transform.ScreenPosition - (size / 2)).ToPoint();
            spriteBatch.Draw(shadowTarget, new Rectangle(position, size.ToPoint()), Color.White);

            //spriteBatch.Draw(shadowTarget, new Vector2(0, 0), Color.White);
        }

        #endregion
    }
}
