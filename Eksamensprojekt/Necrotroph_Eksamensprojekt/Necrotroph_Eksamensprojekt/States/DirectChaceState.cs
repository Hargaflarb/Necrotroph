using Necrotroph_Eksamensprojekt.Components;
using Necrotroph_Eksamensprojekt.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Necrotroph_Eksamensprojekt.States
{
    public class DirectChaceState : ChaceState
    {

        public DirectChaceState(IChaceStateImplementation chacingObject, Transform chacingTransform, Movable chasingMovable) : base(chacingObject, chacingTransform, chasingMovable)
        {

        }

        public override void Execute()
        {
            Vector2 direction = chacedTransform.WorldPosition - chacingTransform.WorldPosition;
            chacingMovable.Direction = direction;
            if ((float)Math.Sqrt(Math.Pow(direction.X, 2) + Math.Pow(direction.Y, 2)) > 300)
            {
                chacingObject.ChangeChaceState(new PathfindState(chacingObject, chacingTransform, chacingMovable));
            }
        }

    }
}
