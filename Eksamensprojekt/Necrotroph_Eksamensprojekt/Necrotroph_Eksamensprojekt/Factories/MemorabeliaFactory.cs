using Necrotroph_Eksamensprojekt.Components;
using Necrotroph_Eksamensprojekt.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Necrotroph_Eksamensprojekt.Factories
{
    //Echo
    public static class MemorabeliaFactory
    {
        #region Fields
        private static Texture2D sprite;
        #endregion
        #region Properties
        #endregion
        #region Constructor
        #endregion
        #region Methods
        public static void LoadContent(ContentManager contentManager)
        {
            sprite = contentManager.Load<Texture2D>("memory");
        }
        public static GameObject CreateMemorabilia(Vector2 position)
        {
            GameObject newMemoryObject = new Memorabilia(position);
            newMemoryObject.AddComponent<SpriteRenderer>(sprite);
            newMemoryObject.AddComponent<Pickupable>();
            newMemoryObject.Transform.Scale = 0.1f;
            return newMemoryObject;
        }
        #endregion
    }
}
