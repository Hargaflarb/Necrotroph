using Necrotroph_Eksamensprojekt.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Necrotroph_Eksamensprojekt
{
    public static class DeathObserver
    {
        #region Fields
        #endregion
        #region Properties
        #endregion
        #region Constructors
        #endregion
        #region Methods
        public static void CheckDeath()
        {
            if (Player.Instance.Life <= 0)
            {

            }
        }
        private static void CallDeath()
        {

        }
        #endregion
    }
}
