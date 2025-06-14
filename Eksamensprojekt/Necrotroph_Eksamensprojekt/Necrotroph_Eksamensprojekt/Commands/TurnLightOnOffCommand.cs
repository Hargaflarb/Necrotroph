using Necrotroph_Eksamensprojekt.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Necrotroph_Eksamensprojekt.Commands
{
    public class TurnLightOnOffCommand : ICommand
    {

        public void Execute()
        {
            Player.Instance.LightOn = !Player.Instance.LightOn;
        }

        public void Undo()
        {
            throw new NotImplementedException();
        }
    }
}
