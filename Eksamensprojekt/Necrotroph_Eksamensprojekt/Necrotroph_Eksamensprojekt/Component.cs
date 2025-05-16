using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Necrotroph_Eksamensprojekt
{
    public abstract class Component
    {
        #region Fields
        private GameObject gameObject;
        #endregion
        #region Properties
        #endregion
        #region Constructors
        #endregion
        #region Methods
        public void Execute();
        #endregion
    }
}
