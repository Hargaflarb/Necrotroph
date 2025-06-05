using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Necrotroph_Eksamensprojekt.Commands;
using Necrotroph_Eksamensprojekt.Components;
using Necrotroph_Eksamensprojekt.Enemies;
using Necrotroph_Eksamensprojekt.Factories;
using Necrotroph_Eksamensprojekt.GameObjects;
using Necrotroph_Eksamensprojekt.ObjectPools;

namespace Necrotroph_Eksamensprojekt.Observer
{
    //emma
    public class DeathObserver : IObserver
    {
        #region Fields
        private List<IListener> listeners = new List<IListener>();
        #endregion
        #region Properties
        #endregion
        #region Constructors
        #endregion
        #region Methods
        public void Update()
        {
            foreach (IListener listener in listeners)
            {
                listener.HearFromObserver(this);
            }
        }
        public void AddListener(IListener listener)
        {
            listeners.Add(listener);
        }
        #endregion
    }
}
