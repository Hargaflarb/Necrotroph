using Necrotroph_Eksamensprojekt.Components;
using SharpDX.MediaFoundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Necrotroph_Eksamensprojekt.GameObjects
{
    public abstract class UIObject
    {
        #region Fields
        private List<Component> components;
        private UITransform transform;
        #endregion
        #region Properties
        public UITransform Transform { get => transform; set => transform = value; }
        public bool Active { get; set; }
        #endregion
        #region Constructor
        public UIObject(Vector2 position)
        {
            components = new List<Component>();
            Transform = new UITransform(position);
        }

        #endregion
        #region Methods
        public T AddComponent<T>(params object[] additionalParameters) where T : Component
        {
            Type componentType = typeof(T);
            try
            {
                object[] allParameters = new object[1 + additionalParameters.Length];
                allParameters[0] = this;
                Array.Copy(additionalParameters, 0, allParameters, 1, additionalParameters.Length);

                T component = (T)Activator.CreateInstance(componentType, allParameters);
                components.Add(component);
                return component;
            }
            catch (Exception)
            {
                throw new InvalidOperationException($"Klassen {componentType.Name} har ikke en konstruktør der matcher de leverede parametre.");
            }
        }
        public T GetComponent<T>() where T : Component
        {
            return (T)components.Find(x => x.GetType() == typeof(T));
        }
        public bool RemoveComponent<T>() where T : Component
        {
            return components.Remove((T)components.Find(x => x.GetType() == typeof(T)));
        }
        
        public virtual void Awake()
        {
            foreach (Component component in components)
            {
                component.Awake();
            }
        }
        public virtual void Start()
        {
            Active = true;
            foreach (Component component in components)
            {
                component.Start();
            }
        }
        public virtual void Update(GameTime gameTime)
        {
            foreach (Component component in components)
            {
                component.Update(gameTime);
            }
        }
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            foreach (Component component in components)
            {
                component.Draw(spriteBatch);
            }
        }
        #endregion
    }
}
