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
        private Color lightColor;

        public float LightRadius { get => lightRadius; set => lightRadius = value; }

        public LightEmitter(GameObject gameObject, float radius) : base(gameObject)
        {
            LightRadius = radius;
            lightColor = Color.White;
        }

    }
}
