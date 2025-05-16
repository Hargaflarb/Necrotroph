using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Necrotroph_Eksamensprojekt
{
    public class Transform
    {
        #region Fields
        private Vector2 position;
        private float rotation;
        private float scale;
        private Vector2 size;
        #endregion
        #region Properties
        public Vector2 Position { get => position; set => position = value; }
        public float Rotation { get => rotation; set => rotation = value; }
        public float Scale { get => scale; set => scale = value; }
        public Vector2 Size { get => size; set => size = value; }
        #endregion
        #region Constructors
        public Transform(Vector2 position)
        {
            this.position = position;
            this.scale = 1.0f;
            this.rotation = 0.0f;
            this.size = Vector2.One;
        }
        public Transform(Vector2 position, float scale)
        {
            this.position = position;
            this.scale = scale;
            this.rotation = 0.0f;
            this.size = Vector2.One;
        }
        public Transform(Vector2 position, float scale, float rotation)
        {
            this.position = position;
            this.scale = scale;
            this.rotation = rotation;
            this.size = Vector2.One;
        }
        public Transform(Vector2 position, float scale, float rotation,Vector2 size)
        {
            this.position = position;
            this.scale = scale;
            this.rotation = rotation;
            this.size = size;
        }

        public Vector2 Position1 { get => position; set => position = value; }
        #endregion
        #region Methods
        #endregion
    }
}
