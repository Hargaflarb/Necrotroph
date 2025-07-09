using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Necrotroph_Eksamensprojekt.Commands
{
    public class CustomCommand : ICommand
    {
        private Action execute;
        private Action undo = () => { };

        public CustomCommand(Action execute)
        {
            this.execute = execute;
        }
        public CustomCommand(Action execute, Action undo) : this(execute)
        {
            this.undo = undo;
        }


        public void Execute() => execute();

        public void Undo() => undo();
    }
}
