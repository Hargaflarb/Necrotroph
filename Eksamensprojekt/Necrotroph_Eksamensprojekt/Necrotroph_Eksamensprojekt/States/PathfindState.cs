using Necrotroph_Eksamensprojekt.Components;
using Necrotroph_Eksamensprojekt.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Necrotroph_Eksamensprojekt.States
{
    public class PathfindState : ChaceState
    {
        private Vector2 nextDestination;

        public PathfindState(IChaceStateImplementation chacingObject, Transform chacingTransform, Movable chasingMovable) : base(chacingObject, chacingTransform, chasingMovable)
        {
            nextDestination = chacingTransform.WorldPosition;
        }

        public override void Execute()
        {
            nextDestination = Map.PathfoundDestination(chacingTransform.WorldPosition, chacedTransform.WorldPosition, nextDestination);
            Vector2 direction = nextDestination - chacingTransform.WorldPosition;
            chacingMovable.Direction = direction;

            Vector2 distance = chacingTransform.WorldPosition - chacedTransform.WorldPosition;
            if (nextDestination == chacedTransform.WorldPosition | (float)Math.Sqrt(Math.Pow(distance.X, 2) + Math.Pow(distance.Y, 2)) < 290)
            {
                chacingObject.ChangeChaceState(new DirectChaceState(chacingObject, chacingTransform, chacingMovable));
            }

        }
    }
}
