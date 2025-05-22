using Necrotroph_Eksamensprojekt.Components;
using Necrotroph_Eksamensprojekt.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Necrotroph_Eksamensprojekt.Factories
{
    public class MemorabiliaFactory
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
        public static GameObject CreateMemorabilia()
        {
            GameObject newMemoryObject = new Memorabilia(new Vector2(500, 500));
            newMemoryObject.AddComponent<SpriteRenderer>(noImage, 2f);
            newMemoryObject.Transform.Scale = 10;
            return newMemoryObject;
        }
        #endregion
    }
}
