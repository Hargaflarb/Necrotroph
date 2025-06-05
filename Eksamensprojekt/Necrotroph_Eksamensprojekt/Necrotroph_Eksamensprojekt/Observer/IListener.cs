using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Necrotroph_Eksamensprojekt.Observer
{
    //emma
    public interface IListener
    {
        #region Properties
        #endregion
        #region Methods
        public void HearFromObserver(IObserver observer);
        #endregion
    }
}
