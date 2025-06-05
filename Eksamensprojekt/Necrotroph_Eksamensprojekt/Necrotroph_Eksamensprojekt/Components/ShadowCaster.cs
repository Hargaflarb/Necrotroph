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
    public class ShadowCaster : Component
    {
        private float objectRadius;

        public ShadowCaster(GameObject gameObject, float Radius) : base(gameObject)
        {
            objectRadius = Radius;
        }

        public float CalculateLightToShadowAngle(LightEmitter light)
        {
            Vector2 dif = GameObject.Transform.ScreenPosition - light.GameObject.Transform.ScreenPosition;
            return MathF.Atan2(dif.Y, dif.X);
        }
        public float CalculateDistanceToLight(LightEmitter light)
        {
            return (light.GameObject.Transform.ScreenPosition - GameObject.Transform.ScreenPosition).Length();
        }
        public float CalculateAngle(float lightDistance)
        {
            // i can use Asin here because the angle with never go above 90 degrees.
            // it would have to be inside the shadowcaster's radius.
            return MathF.Asin(objectRadius / lightDistance);
        }
    }
}
