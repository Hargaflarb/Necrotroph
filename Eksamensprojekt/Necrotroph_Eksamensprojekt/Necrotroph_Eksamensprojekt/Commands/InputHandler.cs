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
    public static class InputHandler
    {
        #region Fields
        private static Dictionary<Keys,ICommand> heldCommandKeys;
        private static Dictionary<Keys,ICommand> pushCommandKeys;
        #endregion
        #region Properties
        #endregion
        #region Constructors
        #endregion
        #region Methods
        public static void AddHeldKeyCommand(Keys key,ICommand command)
        {

        }
        public static void AddPressedKeyCommand(Keys key, ICommand command)
        {

        }
        public static void EditHeldKeyCommand(Keys key, ICommand command)
        {

        }
        public static void EditPressedKeyCommand(Keys key, ICommand command)
        {

        }
        public static void RemoveHeldKeyCommand(ICommand command)
        {

        }
        public static void RemovePressedKeyCommand(ICommand command)
        {

        }
        #endregion
    }
}
