using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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
        public Rectangle Hitbox
        {
            get
            {
                return new Rectangle((int)(Transform.Position.X - Transform.Size.X / 2f),
                    (int)(Transform.Position.Y - Transform.Size.Y / 2f),
                    (int)Transform.Size.X,
                    (int)Transform.Size.Y);
            }
        }

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

#if DEBUG
            DrawRectangle(spriteBatch);
#endif
        }
        public virtual void OnCollision(GameObject otherObject)
        {
            foreach (Component component in components)
            {
                component.OnCollision(otherObject);
            }
        }

        private void DrawRectangle(SpriteBatch spriteBatch)
        {
            Rectangle topLine = new Rectangle(Hitbox.X, Hitbox.Y, Hitbox.Width, 1);
            Rectangle bottomLine = new Rectangle(Hitbox.X, Hitbox.Y + Hitbox.Height, Hitbox.Width, 1);
            Rectangle rightLine = new Rectangle(Hitbox.X + Hitbox.Width, Hitbox.Y, 1, Hitbox.Height);
            Rectangle leftLine = new Rectangle(Hitbox.X, Hitbox.Y, 1, Hitbox.Height);
            Rectangle center = new Rectangle((int)Transform.Position.X - 1, (int)Transform.Position.Y - 1, 3, 3);

            //spriteBatch.Draw(pixel, topLine, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            //spriteBatch.Draw(pixel, bottomLine, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            //spriteBatch.Draw(pixel, rightLine, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            //spriteBatch.Draw(pixel, leftLine, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            //spriteBatch.Draw(pixel, center, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
        }

        public bool CheckCollision(GameObject otherObject)
        {
            return Hitbox.Intersects(otherObject.Hitbox);
        }

        #endregion
    }
}
