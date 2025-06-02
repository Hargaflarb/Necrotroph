using Necrotroph_Eksamensprojekt.Components;
using Necrotroph_Eksamensprojekt.GameObjects;
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
        private Vector2 direction;
        private float speed = 600;
        private float normalSpeed;
        #endregion

        #region Constructor
        public SprintCommand()
        {
            //normalSpeed = Player.Instance.Speed;
        }
        #endregion

        #region Methods
        public void Execute()
        {
            Player.Instance.GetComponent<Movable>().Speed = speed;
        }

        public void Undo()
        {
            //Player.Instance.Speed = normalSpeed;
        }
        #endregion
    }
}
