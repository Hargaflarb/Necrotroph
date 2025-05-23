using Necrotroph_Eksamensprojekt.Components;
using Necrotroph_Eksamensprojekt.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Necrotroph_Eksamensprojekt.Factories
{
    public static class MemorabiliaFactory
    {
        #region Fields
        private static Texture2D noImage;
        #endregion
        #region Properties
        #endregion
        #region Constructor
        #endregion
        #region Methods
        public static void LoadContent(ContentManager contentManager)
        {
            noImage = contentManager.Load<Texture2D>("noImageFound");
        }
        public static GameObject CreateMemorabilia(Vector2 position)
        {
            GameObject newMemoryObject = new Memorabilia(position);
            newMemoryObject.AddComponent<SpriteRenderer>(noImage, 1f);
            newMemoryObject.AddComponent<Pickupable>();
            newMemoryObject.Transform.Scale = 10;
            return newMemoryObject;
        }
        #endregion
    }
}
