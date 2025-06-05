using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Necrotroph_Eksamensprojekt.Commands
{
    //Echo
    public interface ICommand
    {
        void Execute();

        void Undo();
    }
}
