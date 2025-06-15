using Necrotroph_Eksamensprojekt.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Necrotroph_Eksamensprojekt.Commands
{
    public class LightBurstCommand : ICommand
    {
        public void Execute()
        {
            Player.Instance.LightBurst();
        }

        public void Undo()
        {
            throw new NotImplementedException();
        }
    }
}
