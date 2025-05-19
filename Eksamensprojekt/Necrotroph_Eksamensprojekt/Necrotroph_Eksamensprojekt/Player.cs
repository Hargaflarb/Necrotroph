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
    public class Player:GameObject
    {
        #region Fields
        private int life;
        #endregion
        #region Properties
        #endregion
        #region Constructors
        public Player(Vector2 position) : base(position) 
        { 
        
        }
        #endregion
        #region Methods
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
        #endregion
    }
}
