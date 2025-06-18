using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Necrotroph_Eksamensprojekt.States
{
    public interface IChaceStateImplementation
    {
        public ChaceState CurrentChaceState { get; protected set; }
        void ChangeChaceState(ChaceState state);
    }
}
