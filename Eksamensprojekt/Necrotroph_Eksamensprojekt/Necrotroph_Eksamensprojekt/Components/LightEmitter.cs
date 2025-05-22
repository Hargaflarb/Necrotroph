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
        private float lightRadius;

        public float LightRadius { get => lightRadius; set => lightRadius = value; }
        public float X { get => GameObject.Transform.Position.X / GameWorld.Instance.GraphicsDevice.PresentationParameters.BackBufferWidth; }
        public float Y { get => GameObject.Transform.Position.Y / GameWorld.Instance.GraphicsDevice.PresentationParameters.BackBufferHeight; }

        public LightEmitter(GameObject gameObject, float radius) : base(gameObject)
        {
            LightRadius = radius;
        }

    }
}
