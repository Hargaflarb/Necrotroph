using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Necrotroph_Eksamensprojekt.Commands;
using Necrotroph_Eksamensprojekt.Components;
using Necrotroph_Eksamensprojekt.Factories;
using Necrotroph_Eksamensprojekt.GameObjects;
using Necrotroph_Eksamensprojekt.ObjectPools;

namespace Necrotroph_Eksamensprojekt.Components
{
    //emma
    public class Animator : Component
    {
        #region Fields
        private int currentIndex;
        private SpriteRenderer spriteRenderer;
        private Dictionary<string, Animation> animations;
        private Animation currentAnimation;
        //timer thing
        private float timeSinceLastFrame = 0;
        #endregion
        #region Properties
        public string CurrentAnimationName { get { return animations.FirstOrDefault(x => x.Value == currentAnimation).Key; } }
        #endregion
        #region Constructors
        public Animator(GameObject gameObject) : base(gameObject)
        {
            animations = new Dictionary<string, Animation>();
        }
        #endregion
        #region Methods
        /// <summary>
        /// Play animation from the ones added to the animator
        /// </summary>
        /// <param name="animationName">The name of the animation</param>
        public void PlayAnimation(string animationName)
        {
            if (animations.ContainsKey(animationName))
            {
                currentAnimation = animations[animationName];
                currentIndex = 0;
            }
        }
        /// <summary>
        /// Add animation containing one frame
        /// </summary>
        /// <param name="animationName">The name of the new animation (make sure it's not the same as another on this animator)</param>
        /// <param name="animation">The frame data</param>
        public void AddAnimation(string animationName, Texture2D animation)
        {
            animations.Add(animationName, new Animation(animation));
        }
        /// <summary>
        /// Add animation containing multiple frames
        /// </summary>
        /// <param name="animationName">The name of the new animation (make sure it's not the same as another on this animator)</param>
        /// <param name="animation">The frame data</param>
        /// <param name="frameRate">How many times a second the frames should switch</param>
        /// <param name="loops">Whether the animation should loop back to the start once it ends</param>
        public void AddAnimation(string animationName, Texture2D[] animation, float frameRate, bool loops)
        {
            animations.Add(animationName, new Animation(animation, frameRate, loops));
        }

        /// <summary>
        /// Calls the SpriteRenderer to change the frame
        /// </summary>
        public override void Update(GameTime gameTime)
        {
            timeSinceLastFrame += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (timeSinceLastFrame >= 1 / currentAnimation.FrameRate && currentAnimation.FrameRate != 0)
            {
                timeSinceLastFrame = 0;
                currentIndex += 1;
                if (currentIndex >= currentAnimation.Frames.Length && currentAnimation.Loops)
                {
                    currentIndex = 0;
                }
                else if (currentIndex >= currentAnimation.Frames.Length && !currentAnimation.Loops)
                {
                    currentIndex = currentAnimation.Frames.Length - 1;
                }
            }
            gameObject.GetComponent<SpriteRenderer>().Sprite = currentAnimation.GetFrame(currentIndex);
        }


        #endregion
    }
}
