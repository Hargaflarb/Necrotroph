using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Necrotroph_Eksamensprojekt.Components
{
    public class Animation
    {
        #region Fields
        private Texture2D[] frames;
        private float frameRate;
        private float sinceLastFrame;
        private int currentFrame;
        private bool loops = true;
        #endregion
        #region Properties
        public float FrameRate { get => frameRate; }
        public Texture2D[] Frames { get => frames; }
        public bool Loops { get => loops; }
        #endregion
        #region Constructors
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="frames">The frame data</param>
        /// <param name="frameRate">How many times a second the frames should change</param>
        /// <param name="loops">Whether the animation should loop</param>
        public Animation(Texture2D[] frames,float frameRate, bool loops)
        {
            this.frames = frames;
            this.frameRate = frameRate;
            this.loops = loops;
        }
        public Animation(Texture2D frames)
        {
            this.frames = new Texture2D[] { frames };
            loops = false;
        }
        #endregion
        #region Methods
        public Texture2D GetFrame(GameTime gameTime)
        {
            //timer things
            sinceLastFrame += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (sinceLastFrame >= 1 / frameRate)
            {
                sinceLastFrame = 0;
                currentFrame += 1;
                if (currentFrame >= frames.Length && loops)
                {
                    currentFrame = 0;
                }
            }
            return frames[currentFrame];
        }
        public Texture2D GetFrame(int frame)
        {
            return frames[frame];
        }
        #endregion
    }
}
