using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Necrotroph_Eksamensprojekt.Components;
using Necrotroph_Eksamensprojekt.GameObjects;

namespace Necrotroph_Eksamensprojekt.Commands
{
    //Echo
    public class WalkCommand : ICommand
    {
        #region Fields
        private Player player;
        private Vector2 direction;
        #endregion
        #region Properties
        #endregion
        #region Constructors
        public WalkCommand(Player player, Vector2 direction)
        {
            this.player = player;
            this.direction = direction;
        }
        #endregion
        #region Methods

        public void Execute()
        {
            //((Movable)Player.Instance.GetComponent<Movable>()).Move(direction, Player.Instance.Speed);
            Player.Instance.GetComponent<Movable>().Direction += direction;
        }

        public void Undo()
        {
            if (Player.Instance.GetComponent<Movable>().Direction == direction)
            {
                Player.Instance.GetComponent<Movable>().Direction = Vector2.Zero;
            }
        }
        #endregion
    }
}
