using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Necrotroph_Eksamensprojekt.GameObjects
{
    public class TextObject : UIObject
    {
        public TextObject(Vector2 position) : base(position)
        {
            Transform.ScreenPosition = position;
        }
    }
}
