using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Necrotroph_Eksamensprojekt
{
    /// <summary>
    /// Emma
    /// </summary>
    public class SFXInstance
    {
        #region Fields
        private SoundEffect effect;
        private Vector2 worldPosition;
        private float range;
        #endregion
        #region Properties
        public bool Repeating { get; set; } = false;
        #endregion
        #region Constructors
        public SFXInstance(SoundEffect effect,Vector2 worldPosition) 
        { 
        
        }
        #endregion
        #region Methods
        public void Play()
        {

        }
        public void Stop()
        {

        }
        #endregion
    }
}
