using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Necrotroph_Eksamensprojekt.Commands
{
    public class LoadCommand : ICommand
    {
        public void Execute()
        {
            SaveManager.ExecuteLoad();
        }

        public void Undo()
        {
            
        }
    }
}
