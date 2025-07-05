using Necrotroph_Eksamensprojekt.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Necrotroph_Eksamensprojekt.GameObjects
{
    public class Barriar : GameObject
    {

        public Barriar(Vector2 position, Vector2 size) : base(position)
        {
            AddComponent<Collider>(size);
        }


    }
}
