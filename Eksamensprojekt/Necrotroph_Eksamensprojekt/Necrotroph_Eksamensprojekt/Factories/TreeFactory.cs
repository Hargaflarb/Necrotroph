using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace Necrotroph_Eksamensprojekt.Factories
{
    public class TreeFactory
    {
        #region Fields
        #endregion
        #region Properties
        #endregion
        #region Constructors
        #endregion
        #region Methods
        public GameObject CreateTree(Vector2 position)
        {
            return new Tree();
        }
        #endregion
    }
}
