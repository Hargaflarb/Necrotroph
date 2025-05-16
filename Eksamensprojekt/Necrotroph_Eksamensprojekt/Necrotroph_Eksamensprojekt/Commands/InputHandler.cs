using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Necrotroph_Eksamensprojekt.Commands
{
    public class InputHandler
    {
        #region Fields
        private Dictionary<Keys,ICommand> heldCommandKeys;
        private Dictionary<Keys,ICommand> pushCommandKeys;
        #endregion
        #region Properties
        #endregion
        #region Constructors
        #endregion
        #region Methods
        public void AddHeldKeyCommand(Keys key,ICommand command)
        {

        }
        public void AddPressedKeyCommand(Keys key, ICommand command)
        {

        }
        public void EditHeldKeyCommand(Keys key, ICommand command)
        {

        }
        public void EditPressedKeyCommand(Keys key, ICommand command)
        {

        }
        public void RemoveHeldKeyCommand(ICommand command)
        {

        }
        public void RemovePressedKeyCommand(ICommand command)
        {

        }
        #endregion
    }
}
