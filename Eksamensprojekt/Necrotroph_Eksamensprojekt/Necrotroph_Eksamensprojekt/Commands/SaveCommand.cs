using Necrotroph_Eksamensprojekt.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Necrotroph_Eksamensprojekt.Commands
{
    public class SaveCommand : ICommand
    {
        public void Execute()
        {
            SaveManager.ExecuteSave();
        }

        public void Undo()
        {
            
        }
    }
}
