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
        #endregion
        #region Properties
        public float LightRadius { get => lightRadius; set => lightRadius = value; }
        public float X { get => GameObject.Transform.ScreenPosition.X / GameWorld.Instance.GraphicsDevice.PresentationParameters.BackBufferWidth; }
        public float Y { get => GameObject.Transform.ScreenPosition.Y / GameWorld.Instance.GraphicsDevice.PresentationParameters.BackBufferHeight; }
        #endregion
        #region Constructors
        public LightEmitter(GameObject gameObject, float radius) : base(gameObject)
        {
            LightRadius = radius;
        }
        #endregion
        #region Methods
        #endregion
    }
}
