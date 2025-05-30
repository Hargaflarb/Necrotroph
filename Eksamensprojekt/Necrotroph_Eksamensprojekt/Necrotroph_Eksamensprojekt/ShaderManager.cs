using Necrotroph_Eksamensprojekt.Components;
using Necrotroph_Eksamensprojekt.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Necrotroph_Eksamensprojekt
{
    public static class ShaderManager
    {
        private static Texture2D shadowSprite;
        private static Effect lightEffect;
        private static Effect shadowEffect;
        private static Effect invertAplha;
        private static RenderTarget2D lightTarget;
        private static RenderTarget2D finalLightTarget;
        private static RenderTarget2D shadowTarget;
        private static Color color = Color.White;


        public static Texture2D ShadowSprite { get => shadowSprite; set => shadowSprite = value; }
        public static Effect LightEffect { get => lightEffect; set => lightEffect = value; }
        public static Effect ShadowEffect { get => shadowEffect; set => shadowEffect = value; }
        public static Effect InvertAplha { get => invertAplha; set => invertAplha = value; }
        public static RenderTarget2D LightTarget { get => lightTarget; set => lightTarget = value; }
        public static RenderTarget2D FinalLightTarget { get => finalLightTarget; set => finalLightTarget = value; }
        public static RenderTarget2D ShadowTarget { get => shadowTarget; set => shadowTarget = value; }
        public static Color Color { get => color; set => color = value; }

        static ShaderManager()
        {
            GraphicsDevice device = GameWorld.Instance.GraphicsDevice;
            LightTarget = new RenderTarget2D(device, device.PresentationParameters.BackBufferWidth, device.PresentationParameters.BackBufferHeight);
            FinalLightTarget = new RenderTarget2D(device, device.PresentationParameters.BackBufferWidth, device.PresentationParameters.BackBufferHeight);
            ShadowTarget = new RenderTarget2D(device, device.PresentationParameters.BackBufferWidth, device.PresentationParameters.BackBufferHeight);
        }


        /// <summary>
        /// Sets the sprite to a given gameObject
        /// </summary>
        /// <param name="spriteName"></param>
        public static void SetSprite()
        {
            ShadowSprite = GameWorld.Instance.Content.Load<Texture2D>("darkshaddow");
            LightEffect = GameWorld.Instance.Content.Load<Effect>("LightingShader");
            //ShadowEffect = GameWorld.Instance.Content.Load<Effect>("ShadowShader");
            InvertAplha = GameWorld.Instance.Content.Load<Effect>("InvertAlpha");
            //ShadowMapSprite = GameWorld.Instance.Content.Load<Texture2D>("shadowMap");
        }

        public static void PrepareShadows(SpriteBatch spriteBatch)
        {
            List<(LightEmitter lightEmitter, List<ShadowInterval> shadowIntervals)> components = InGame.Instance.GetShaderData();

            //-------------

            foreach ((LightEmitter lightEmitter, List<ShadowInterval> shadowIntervals) shadow in components)
            {
                shadow.lightEmitter.DrawShadowsToTarget(spriteBatch, shadow.shadowIntervals);
            }

            //-------------
            GameWorld.Instance.GraphicsDevice.SetRenderTarget(LightTarget);
            GameWorld.Instance.GraphicsDevice.Clear(new Color(0, 0, 0, 0));

            spriteBatch.Begin(blendState: BlendState.Additive, effect: LightEffect);
            foreach ((LightEmitter lightEmitter, List<ShadowInterval> shadowIntervals) shadow in components)
            {
                shadow.lightEmitter.DrawToLightMask(spriteBatch);
            }

            spriteBatch.End();


            //-------------
            GameWorld.Instance.GraphicsDevice.SetRenderTarget(FinalLightTarget);
            GameWorld.Instance.GraphicsDevice.Clear(new Color(0, 0, 0, 0));

            spriteBatch.Begin(blendState: BlendState.Additive, effect: InvertAplha);
            spriteBatch.Draw(LightTarget, new Vector2(0, 0), Color.White);
            spriteBatch.End();


            GameWorld.Instance.GraphicsDevice.SetRenderTarget(null);

        }



        public static void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(FinalLightTarget, Vector2.Zero, null, Color, 0, Vector2.Zero, 1, SpriteEffects.None, 0f);
        }

    }
}
