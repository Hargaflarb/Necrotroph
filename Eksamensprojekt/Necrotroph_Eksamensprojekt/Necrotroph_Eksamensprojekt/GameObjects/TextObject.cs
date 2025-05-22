using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Necrotroph_Eksamensprojekt.GameObjects
{
    public class TextObject : GameObject
    {
        #region Fields
        #endregion
        #region Properties
        #endregion
        #region Constructor
        public TextObject(Vector2 position) : base(position)
        {
            Transform.Position = position;
        }
        #endregion
        #region Methods
        #endregion
    }
}
