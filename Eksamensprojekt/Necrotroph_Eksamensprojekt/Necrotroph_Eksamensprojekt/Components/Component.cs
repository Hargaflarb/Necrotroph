using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Necrotroph_Eksamensprojekt.GameObjects;

namespace Necrotroph_Eksamensprojekt.Components
{
    public abstract class Component
    {
        #region Fields
        protected GameObject gameObject;
        protected UIObject uiObject;

        #endregion
        #region Properties
        public GameObject GameObject { get => gameObject; private set => gameObject = value; }
        public UIObject UIObject { get => uiObject; private set => uiObject = value; }
        #endregion
        #region Constructors
        public Component(GameObject gameObject)
        {
            this.gameObject = gameObject;
        }
        public Component(UIObject uiObject)
        {
            this.uiObject = uiObject;
        }
        #endregion
        #region Methods

        public virtual void Awake() { }
        public virtual void Start() { }
        public virtual void Update(GameTime gameTime)
        {

        }
        public virtual void Draw(SpriteBatch spriteBatch) { }
        public virtual void OnCollision(GameObject otherObject) { }

        #endregion
    }
}
