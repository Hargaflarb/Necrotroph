using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Necrotroph_Eksamensprojekt.GameObjects
{
    /// <summary>
    /// Manages everything to do with UI so that they're not GameObjects
    /// </summary>
    public class UIManager
    {
        #region Fields
        private List<UIObject> activeUIObjects;
        private List<UIObject> uiObjectsToAdd;
        private List<UIObject> uiObjectsToRemove;

        private static UIManager instance;
        #endregion
        #region Properties
        public List<UIObject> ActiveUIObjects { get => activeUIObjects; set => activeUIObjects = value; }
        public List<UIObject> UIObjectsToAdd { get => uiObjectsToAdd; set => uiObjectsToAdd = value; }
        public List<UIObject> UIObjectsToRemove { get => uiObjectsToRemove; set => uiObjectsToRemove = value; }
        public static UIManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new UIManager();
                }
                return instance;
            }
        }
        #endregion
        #region Constructor
        private UIManager()
        {
            activeUIObjects = new List<UIObject>();
            uiObjectsToAdd = new List<UIObject>();
            uiObjectsToRemove = new List<UIObject>();
        }
        #endregion
        #region Methods
        public void AddUIObject(UIObject uiObject)
        {
            uiObject.Awake();
            uiObjectsToAdd.Add(uiObject);
        }
        public void RemoveUIObject(UIObject uiObject)
        {
            uiObjectsToRemove.Add(uiObject);
        }
        public void AddAndRemoveUIObjects()
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
