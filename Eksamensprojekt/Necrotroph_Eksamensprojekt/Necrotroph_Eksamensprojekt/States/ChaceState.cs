using Necrotroph_Eksamensprojekt.Components;
using Necrotroph_Eksamensprojekt.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Necrotroph_Eksamensprojekt.States
{
    /// <summary>
    /// Malthe
    /// </summary>
    public abstract class ChaceState
    {
        protected IChaceStateImplementation chacingObject;
        protected Transform chacedTransform;
        protected Transform chacingTransform;
        protected Movable chacingMovable;

        public ChaceState(IChaceStateImplementation chacingObject, Transform chacingTransform, Movable chacingMovable)
        {
            this.chacingObject = chacingObject;
            this.chacingTransform = chacingTransform;
            this.chacingMovable = chacingMovable;

        }

        public virtual void Enter(GameObject chacedGameObject)
        {
            chacedTransform = chacedGameObject.Transform;
        }
        public abstract void Execute();
        public virtual void Exit()
        {

        }
    }
}
