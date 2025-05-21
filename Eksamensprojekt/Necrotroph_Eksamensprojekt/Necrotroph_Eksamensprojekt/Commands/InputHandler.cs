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
    /// <summary>
    /// Handles all the key presses of the player. Will eventually be able to configure keybinds
    /// </summary>
    public static class InputHandler
    {
        #region Fields
        private static Dictionary<Keys,ICommand> heldCommandKeys;
        private static Dictionary<Keys,ICommand> pushCommandKeys;
        private static Dictionary<Keys, ICommand> unclickedCommandKeys;
        private static KeyboardState lastKeyboardState;
        private static bool pressed = true;
        #endregion
        #region Properties
        #endregion
        #region Constructors
        static InputHandler()
        {
            heldCommandKeys = new Dictionary<Keys, ICommand>();
            pushCommandKeys = new Dictionary<Keys, ICommand>();
            unclickedCommandKeys = new Dictionary<Keys, ICommand>();
            lastKeyboardState = new KeyboardState();
        }

        #endregion
        #region Methods
        /// <summary>
        /// For buttons that you hold, such as WASD
        /// </summary>
        /// <param name="key"></param>
        /// <param name="command"></param>
        public static void AddHeldKeyCommand(Keys key,ICommand command)
        {
            heldCommandKeys.Add(key, command);
        }
        /// <summary>
        /// Buttons that only trigger code when you press it, not as long as you hold it
        /// </summary>
        /// <param name="key"></param>
        /// <param name="command"></param>
        public static void AddPressedKeyCommand(Keys key, ICommand command)
        {
            pushCommandKeys.Add(key, command);
        }
        /// <summary>
        /// For buttons that need to run their command when you stop clicking the button
        /// </summary>
        /// <param name="key"></param>
        /// <param name="command"></param>
        public static void AddUnclickedCommand(Keys key, ICommand command)
        {
            unclickedCommandKeys.Add(key, command);
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
                if (pushCommandKeys.TryGetValue(pressedKey, out ICommand downCommand) & !lastKeyboardState.IsKeyDown(pressedKey))
                {
                    downCommand.Execute();
                }
            }
            foreach(KeyValuePair<Keys, ICommand> unclickedKey in unclickedCommandKeys)
            {
                if (!Keyboard.GetState().IsKeyDown(unclickedKey.Key) & !lastKeyboardState.IsKeyDown(unclickedKey.Key))
                {
                    unclickedKey.Value.Undo();
                }
            }
        }

        #endregion
    }
}
