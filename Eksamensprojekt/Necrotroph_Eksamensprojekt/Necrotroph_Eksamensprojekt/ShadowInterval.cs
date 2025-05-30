using Necrotroph_Eksamensprojekt.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Necrotroph_Eksamensprojekt
{
    public struct ShadowInterval
    {
        private float upperAngle;
        private float lowerAngle;
        private float angleOffset;
        private float distance;

        public ShadowInterval(ShadowCaster shadowCaster, LightEmitter light)
        {
            distance = light.NormalizedDistanceToLight(shadowCaster);
            float nondistance = shadowCaster.CalculateDistanceToLight(light);
            float BaseAngle = shadowCaster.CalculateLightToShadowAngle(light);
            float AngleIntervalSize = shadowCaster.CalculateAngle(nondistance);
            // double negativity

            angleOffset = -(BaseAngle - AngleIntervalSize);
            upperAngle = BaseAngle + AngleIntervalSize + angleOffset;


            //if (BaseAngle + AngleIntervalSize > MathF.PI * 2)
            //{
            //    angleOffset = -((MathF.PI * 2) % (BaseAngle + AngleIntervalSize));
            //}
            //else if (BaseAngle - AngleIntervalSize < 0)
            //{
            //    // double negativity
            //    angleOffset = -(BaseAngle - AngleIntervalSize);
            //}
            //else
            //{
            //    angleOffset = 0;
            //}
            //upperAngle = BaseAngle + AngleIntervalSize;// + angleOffset;
            //lowerAngle = BaseAngle - AngleIntervalSize;// + angleOffset;
        }

        public float UpperAngle { get => upperAngle; }
        public float LowerAngle { get => lowerAngle; }
        public float AngleOffset { get => angleOffset; }
        public float Distance { get => distance; }

        public Color ToDataPass()
        {
            float Pi = 6.28318530718f;
            return new Color(UpperAngle / (Pi), (AngleOffset / (Pi)) + 0.5f, Distance);
        }
    }

}
