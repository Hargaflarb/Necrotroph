using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Necrotroph_Eksamensprojekt.Components;

namespace Necrotroph_Eksamensprojekt
{
    public abstract class GameObject
    {
        #region Fields
        private List<Component> components;
        private Transform transform;
        #endregion
        #region Properties
        public Transform Transform { get => transform; }
        #endregion
        #region Constructors
        public GameObject(Vector2 position)
        {
            components = new List<Component>();
            transform = new Transform(position);
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
        public T RemoveComponent<T>() where T : Component
        {
            components.Remove((T)components.Find(x => x.GetType() == typeof(T)));
            return null;
        }
        public virtual void Awake()
        {
            
        }
        public virtual void Start()
        {

        }
        public virtual void Update(GameTime gameTime)
        {

        }
        #endregion
    }
}
