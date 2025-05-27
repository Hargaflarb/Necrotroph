using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Necrotroph_Eksamensprojekt.Observer
{
    public interface ISubject
    {
        #region Fields
        #endregion
        #region Properties
        #endregion
        #region Constructors
        #endregion
        #region Methods
        public void AttachObserver(IObserver observer);
        public void DetachObserver(IObserver observer);

        public void NotifyObserver();
        #endregion
    }
}
