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
    public class Animator : Component
    {
        #region Fields
        private int currentIndex;
        private SpriteRenderer spriteRenderer;
        private Dictionary<string, Animation> animations;
        #endregion
        #region Properties
        #endregion
        #region Constructors
        public Animator(GameObject gameObject) : base(gameObject)
        {
        }
        #endregion
        #region Methods
        public void PlayAnimation(string animationName)
        {
            
        }
        public void AddAnimation(string animationName,Texture2D animation) 
        { 
        
        }
        public void AddAnimation(string animationName, Texture2D[] animation)
        {

        }

        #endregion
    }
}
