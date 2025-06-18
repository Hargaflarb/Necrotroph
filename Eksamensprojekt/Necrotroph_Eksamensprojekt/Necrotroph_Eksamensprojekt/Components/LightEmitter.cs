using Necrotroph_Eksamensprojekt.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Necrotroph_Eksamensprojekt.Components
{
    /// <summary>
    /// Malthe
    /// </summary>
    public class LightEmitter : Component
    {
        #region Fields
        private float lightRadius;
        private RenderTarget2D shadowTarget;
        private static Effect shaderShadowEffect;
        private Vector2 offset;

        #endregion
        #region Properties
        public float LightRadius { get => lightRadius; set => lightRadius = value; }
        public float X { get => GameObject.Transform.ScreenPosition.X / (GameWorld.Instance.GraphicsDevice.PresentationParameters.BackBufferWidth + 50) + offset.X; }
        public float Y { get => (GameObject.Transform.ScreenPosition.Y - (gameObject.GetComponent<SpriteRenderer>().Sprite.Height * gameObject.Transform.Scale) / 2) / GameWorld.Instance.GraphicsDevice.PresentationParameters.BackBufferHeight + offset.Y; }
        public static Effect ShaderShadowEffect { get => shaderShadowEffect; set => shaderShadowEffect = value; }
        public Vector2 Offset { get => offset; set => offset = value; }

        #endregion
        #region Constructors
        public LightEmitter(GameObject gameObject, float radius) : base(gameObject)
        {
            LightRadius = radius;
            GraphicsDevice device = GameWorld.Instance.GraphicsDevice;
            shadowTarget = new RenderTarget2D(device, device.PresentationParameters.BackBufferHeight, device.PresentationParameters.BackBufferHeight);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="radius"></param>
        /// <param name="offset">If the light doesn't emit frm the center of the gameobject</param>
        public LightEmitter(GameObject gameObject, float radius, Vector2 offset) : base(gameObject)
        {
            LightRadius = radius;
            GraphicsDevice device = GameWorld.Instance.GraphicsDevice;
            shadowTarget = new RenderTarget2D(device, device.PresentationParameters.BackBufferHeight, device.PresentationParameters.BackBufferHeight);
            this.offset = Vector2.Zero;// offset;
        }
        #endregion
        #region Methods
        public float NormalizedDistanceToLight(ShadowCaster shadow)
        {
            return ((shadow.GameObject.Transform.ScreenPosition - GameObject.Transform.ScreenPosition + offset) / (LightRadius * GameWorld.Instance.GraphicsDevice.PresentationParameters.BackBufferWidth * 2)).Length();
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
            Point position = (GameObject.Transform.ScreenPosition - (size / 2) + offset).ToPoint();
            spriteBatch.Draw(shadowTarget, new Rectangle(position, size.ToPoint()), Color.White);

            //spriteBatch.Draw(shadowTarget, new Vector2(0, 0), Color.White);
        }

        #endregion
    }
}
