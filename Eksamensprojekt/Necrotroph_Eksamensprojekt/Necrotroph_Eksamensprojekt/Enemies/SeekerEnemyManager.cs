using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Necrotroph_Eksamensprojekt.Components;
using Necrotroph_Eksamensprojekt.GameObjects;
using Necrotroph_Eksamensprojekt.ObjectPools;
using Necrotroph_Eksamensprojekt.Observer;

namespace Necrotroph_Eksamensprojekt.Enemies
{
    //emma
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
        private static int appearSFX;
        private static int disappearSFX;
        #endregion
        #region Properties
        #endregion
        #region Constructors
        #endregion
        #region Methods
        public static void Update()
        {
            if (appearSFX == 0)
            {
                appearSFX = SoundManager.Instance.PlaySFX("SeekerActivate",GameWorld.ScreenSize/2);
                disappearSFX = SoundManager.Instance.PlaySFX("SeekerDeactivate", GameWorld.ScreenSize / 2);
                SoundManager.Instance.PauseSFX(appearSFX);
                SoundManager.Instance.PauseSFX(disappearSFX);
            }
            if (!Tree.HasEyes && !timerStarted)
            {
                timerStarted = true;
                int time = GameWorld.Rnd.Next(minTimeBetweenSeekers, maxTimeBetweenSeekers);
                TimeLineManager.AddEvent((float)time * 1000, StartHunt);
            }
            else if (Tree.HasEyes)
            {
                Hunt();
            }
        }
        public static void StartHunt()
        {
            SoundManager.Instance.ResumeSFX(appearSFX);
            SoundManager.Instance.ChangeSFXVolume("PlayerWalk1", 700);
            SoundManager.Instance.ChangeSFXVolume("PlayerWalk2", 700);
            //get all trees & set them to have eyes
            Tree.HasEyes = true;
            timerStarted = false;
            mainTimerID = TimeLineManager.AddEvent(huntTime * 1000, ReturnToNormal);
            //get SoundManager & increase player sounds, decrease ambience & enemy sounds
            walkTimerID = TimeLineManager.AddEvent(timeBeforeDeath * 1000, KillPlayer);
            Hunt();
        }
        private static void Hunt()
        {
            if (!Player.Instance.IsMoving)
            {
                float temp = TimeLineManager.GetTime(walkTimerID);
                if (temp != -1)
                {
                    walkTime = temp / 1000;
                    TimeLineManager.RemoveEvent(walkTimerID);
                    walkTimerID = 0;
                }
            }
            else if (walkTimerID == 0 && Player.Instance.IsMoving)
            {
                walkTimerID = TimeLineManager.AddEvent(walkTime * 1000, KillPlayer);
                Debug.WriteLine(walkTime + " seconds left...");
            }
        }
        public static void ReturnToNormal()
        {
            SoundManager.Instance.ResumeSFX(disappearSFX);
            SoundManager.Instance.ChangeSFXVolume("PlayerWalk1", 200);
            SoundManager.Instance.ChangeSFXVolume("PlayerWalk2", 200);
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
