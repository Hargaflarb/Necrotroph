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
        private Vector2 worldPosition = Vector2.Zero;
        #endregion
        #region Properties
        public Vector2 Position { get => position; set => position = value; }
        public float Rotation { get => rotation; set => rotation = value; }
        public float Scale { get => scale; set => scale = value; }
        public Vector2 Size { get => size * scale; set => size = value; }
        public Vector2 WorldPosition { get => worldPosition; set => worldPosition = value; }
        #endregion
        #region Constructors
        public Transform(Vector2 position, Vector2 worldPosition)
        {
            this.position = position;
            this.scale = 1.0f;
            this.rotation = 0.0f;
            this.size = Vector2.One;
            this.worldPosition = worldPosition;
        }
        public Transform(Vector2 position, Vector2 worldPosition, float scale)
        {
            this.position = position;
            this.scale = scale;
            this.rotation = 0.0f;
            this.size = Vector2.One;
            this.worldPosition = worldPosition;
        }
        public Transform(Vector2 position, Vector2 worldPosition, float scale, float rotation)
        {
            this.position = position;
            this.scale = scale;
            this.rotation = rotation;
            this.size = Vector2.One;
            this.worldPosition = worldPosition;
        }
        public Transform(Vector2 position, Vector2 worldPosition, float scale, float rotation,Vector2 size)
        {
            this.position = position;
            this.scale = scale;
            this.rotation = rotation;
            this.size = size;
            this.worldPosition = worldPosition;
        }
        #endregion
        #region Methods
        #endregion
    }
}
