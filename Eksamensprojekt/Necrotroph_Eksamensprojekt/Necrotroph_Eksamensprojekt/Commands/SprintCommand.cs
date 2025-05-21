using Necrotroph_Eksamensprojekt.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Necrotroph_Eksamensprojekt.Commands
{
    /// <summary>
    /// Used by the player to go real fast. Don't use it for other things
    /// </summary>
    public class SprintCommand : ICommand
    {
        #region Fields
        private Player player;
        private Vector2 direction;
        private float speed;
        private bool sprinting;
        #endregion

        #region Constructor
        public SprintCommand(Player player)
        {
            this.player = player;
        }
        #endregion

        #region Methods
        public void Execute()
        {
            Player.Instance.GetComponent<Movable>().Sprint(600);
        }

        public void Undo()
        {
            Player.Instance.Speed = 300;
        }
        #endregion
    }
}
