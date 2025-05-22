using Necrotroph_Eksamensprojekt.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Necrotroph_Eksamensprojekt.GameObjects
{
    public class Memorabilia : GameObject
    {
        public Memorabilia(Vector2 position) : base(position)
        {
            Transform.WorldPosition = position;
        }
    }
}
