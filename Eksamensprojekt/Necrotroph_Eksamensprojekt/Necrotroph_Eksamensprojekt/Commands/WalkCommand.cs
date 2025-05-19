using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Necrotroph_Eksamensprojekt.Components;

namespace Necrotroph_Eksamensprojekt.Commands
{
    public class WalkCommand: ICommand
    {
        #region Fields
        private Player player;
        #endregion
        #region Properties
        #endregion
        #region Constructors
        public WalkCommand(Player player, Vector2 direction)
        {
            this.player = player;
        }
        #endregion
        #region Methods
        public void Execute(GameTime gameTime)
        {
            Player.Instance.GetComponent<Movable>().Move(gameTime);
        }

        public void Execute()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
