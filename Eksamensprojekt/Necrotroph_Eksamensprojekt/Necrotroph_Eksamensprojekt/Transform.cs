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
        private Vector2 worldPosition;
        private float rotation;
        private float scale;
        private Vector2 size;
        #endregion
        #region Properties
        public Vector2 WorldPosition { get => worldPosition; set => worldPosition = value; }
        public Vector2 Position
        {
            get
            {
                return (WorldPosition - GameWorld.Player.Transform.WorldPosition) + (GameWorld.ScreenSize / 2);
            }
        }
        public float Rotation { get => rotation; set => rotation = value; }
        public float Scale { get => scale; set => scale = value; }
        public Vector2 Size { get => size * scale; set => size = value; }
        #endregion
        #region Constructors
        public Transform(Vector2 worldPosition, float scale = 1, float rotation = 0)
        {
            this.worldPosition = worldPosition;
            this.scale = scale;
            this.rotation = rotation;
            this.size = Vector2.One;
        }
        public Transform(Vector2 worldPosition, Vector2 size, float scale = 1, float rotation = 0)
        {
            this.worldPosition = worldPosition;
            this.scale = scale;
            this.rotation = rotation;
            this.size = size;
        }
        #endregion
        #region Methods
        #endregion
    }
}
