using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Necrotroph_Eksamensprojekt.Components
{
    public class Animation
    {
        #region Fields
        private Texture2D[] frames;
        private float frameRate;
        #endregion
        #region Properties
        #endregion
        #region Constructors
        public Animation(Texture2D[] frames)
        {
            this.frames = frames;
        }
        #endregion
        #region Methods
        public Texture2D GetFrame()
        {
            //add code here
            return null;
        }
        #endregion
    }
}
