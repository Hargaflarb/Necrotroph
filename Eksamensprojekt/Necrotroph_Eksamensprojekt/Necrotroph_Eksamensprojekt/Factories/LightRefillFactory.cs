using Microsoft.Xna.Framework.Content;
using Necrotroph_Eksamensprojekt.Components;
using Necrotroph_Eksamensprojekt.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Necrotroph_Eksamensprojekt.Factories
{
    public static class LightRefillFactory
    {
        #region Fields
        private static Texture2D sprite;
        #endregion
        #region Properties
        #endregion
        #region Constructors
        #endregion
        #region Methods
        public static void LoadContent(ContentManager content)
        {
            sprite = content.Load<Texture2D>("lightPickup");
            int id = SoundManager.Instance.PlaySFX("PlayerPickUpLight", Player.Instance.Transform.ScreenPosition);
            SoundManager.Instance.PauseSFX(id);
        }

        public static LightRefill Create(Vector2 position)
        {
            LightRefill light = new LightRefill(position);
            light.AddComponent<Pickupable>();
            light.AddComponent<SpriteRenderer>(sprite, 1f);
            light.AddComponent<LightEmitter>(0.05f);
            light.Transform.Scale = 0.5f;
            return light;
        }
        #endregion
    }
}
