using Necrotroph_Eksamensprojekt.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Necrotroph_Eksamensprojekt
{
    /// <summary>
    /// Manages everything to do with UI so that they're not GameObjects
    /// </summary>
    public static class UIManager
    {
        #region Fields
        private static List<UIObject> activeUIObjects;
        private static List<UIObject> uiObjectsToAdd;
        private static List<UIObject> uiObjectsToRemove;
        #endregion
        
        #region Properties
        public static List<UIObject> ActiveUIObjects { get => activeUIObjects; set => activeUIObjects = value; }
        public static List<UIObject> UIObjectsToAdd { get => uiObjectsToAdd; set => uiObjectsToAdd = value; }
        public static List<UIObject> UIObjectsToRemove { get => uiObjectsToRemove; set => uiObjectsToRemove = value; }
        #endregion

        #region Constructor
        static UIManager()
        {
            activeUIObjects = new List<UIObject>();
            uiObjectsToAdd = new List<UIObject>();
            uiObjectsToRemove = new List<UIObject>();
        }
        #endregion

        #region Methods
        public static void AddUIObject(UIObject uiObject)
        {
            uiObject.Awake();
            uiObjectsToAdd.Add(uiObject);
        }
        public static void RemoveUIObject(UIObject uiObject)
        {
            uiObjectsToRemove.Add(uiObject);
        }
        public static void AddAndRemoveUIObjects()
        {
            foreach (UIObject uiObject in uiObjectsToAdd)
            {
                uiObject.Start();
                activeUIObjects.Add(uiObject);
            }
            uiObjectsToAdd.Clear();
            foreach (UIObject uiObject in uiObjectsToRemove)
            {
                if (activeUIObjects.Contains(uiObject))
                {
                    activeUIObjects.Remove(uiObject);
                }
            }
            uiObjectsToRemove.Clear();
        }
        #endregion
    }
}
