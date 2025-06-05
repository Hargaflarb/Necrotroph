using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Necrotroph_Eksamensprojekt.Observer
{
    //emma
    public interface ISubject
    {
        #region Properties
        #endregion
        #region Methods
        public void AttachObserver(IObserver observer);
        public void DetachObserver(IObserver observer);

        public void NotifyObserver();
        #endregion
    }
}
