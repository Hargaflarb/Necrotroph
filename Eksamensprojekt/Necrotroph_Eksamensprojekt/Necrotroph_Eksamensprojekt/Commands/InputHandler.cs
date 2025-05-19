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
        static InputHandler()
        {
            heldCommandKeys = new Dictionary<Keys, ICommand>();
            pushCommandKeys = new Dictionary<Keys, ICommand>();
        }

        #endregion
        #region Methods
        public static void AddHeldKeyCommand(Keys key,ICommand command)
        {
            heldCommandKeys.Add(key, command);
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
        public static void HandleInput()
        {
            KeyboardState keyState = Keyboard.GetState();

            foreach(Keys pressedKey in keyState.GetPressedKeys())
            {
                if (heldCommandKeys.TryGetValue(pressedKey, out ICommand holdCommand))
                {
                    holdCommand.Execute();
                }
            }
        }

        #endregion
    }
}
