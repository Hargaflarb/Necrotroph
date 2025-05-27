using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Necrotroph_Eksamensprojekt.Components;
using Necrotroph_Eksamensprojekt.GameObjects;
using Necrotroph_Eksamensprojekt.ObjectPools;

namespace Necrotroph_Eksamensprojekt.Enemies
{
    public static class SeekerEnemyManager
    {
        #region Fields
        //Total time when the seeker... well, seeks (assuming the player doesn't move)
        private static float huntTime = 10;
        //How long the player has to be moving before they get got
        private static float timeBeforeDeath = 6;
        private static int mainTimerID;
        private static int walkTimerID;
        private static float walkTime;
        private static int maxTimeBetweenSeekers = 120;
        private static int minTimeBetweenSeekers = 30;
        private static bool timerStarted;
        private static Random rnd = new Random();
        #endregion
        #region Properties
        #endregion
        #region Constructors
        #endregion
        #region Methods
        public static void Update()
        {
            if (!Tree.HasEyes && !timerStarted)
            {
                timerStarted = true;
                TimeLineManager.AddEvent((float)rnd.Next(minTimeBetweenSeekers, maxTimeBetweenSeekers) * 1000, Hunt);
            }
        }
        private static void Hunt()
        {
            //get all trees & set them to have eyes
            Tree.HasEyes = true;
            timerStarted = false;
            mainTimerID = TimeLineManager.AddEvent(huntTime * 1000, ReturnToNormal);
            //get SoundManager & increase player sounds, decrease ambience & enemy sounds
            if (Player.Instance.IsMoving && walkTimerID == 0)
            {
                walkTimerID = TimeLineManager.AddEvent(timeBeforeDeath * 1000, KillPlayer);
            }
            else if (!Player.Instance.IsMoving)
            {
                walkTime = TimeLineManager.GetTime(walkTimerID);
                TimeLineManager.RemoveEvent(walkTimerID);
            }
            else
            {
                walkTimerID = TimeLineManager.AddEvent(walkTime * 1000, KillPlayer);
            }
        }
        public static void ReturnToNormal()
        {
            TimeLineManager.RemoveEvent(mainTimerID);

            //if (!Player.Instance.IsMoving)
            //{
            TimeLineManager.RemoveEvent(walkTimerID);
            Tree.HasEyes = false;
            mainTimerID = 0;
            walkTimerID = 0;
            walkTime = timeBeforeDeath;
            //get SoundManager & return sound to normal
            //}
        }

        public static void KillPlayer()
        {
            Player.Instance.PlayerDeath(EnemyType.Seeker);
        }
        #endregion
    }
}
