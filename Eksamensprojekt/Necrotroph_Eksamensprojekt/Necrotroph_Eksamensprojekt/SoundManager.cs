using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Necrotroph_Eksamensprojekt
{
    public class SoundManager
    {
        #region Fields
        private static SoundManager instance;
        #endregion
        #region Properties
        public static SoundManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SoundManager();
                }
                return instance;
            }
        }
        #endregion
        #region Constructors
        #endregion
        #region Methods
        #endregion
    }
}
