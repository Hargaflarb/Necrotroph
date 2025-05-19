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
    public class EnemyFactory
    {
        #region Fields
        #endregion
        #region Properties
        #endregion
        #region Constructors
        #endregion
        #region Methods
        public GameObject CreateEnemy(Vector2 position, EnemyType enemy)
        {
            //oooo
            return new HunterEnemy(position);
        }
        #endregion
    }
}
