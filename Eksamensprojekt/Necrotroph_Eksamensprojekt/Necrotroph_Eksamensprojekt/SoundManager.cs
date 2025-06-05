using Necrotroph_Eksamensprojekt.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Necrotroph_Eksamensprojekt
{
    /// <summary>
    /// Emma
    /// </summary>
    public class SoundManager
    {
        #region Fields
        private static SoundManager instance;
        private Dictionary<string, (SoundEffect, float, float, bool)> soundEffects;
        private Dictionary<string, (Song, float)> ambience;
        private Dictionary<int, (string, SoundEffectInstance, Vector2)> activeSFX;
        private int ID = 1;
        private AudioListener audioListener;
        #endregion
        #region Properties
        public static SoundManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SoundManager();
                }
                return instance;
            }
        }
        #endregion
        #region Constructors
        private SoundManager()
        {
            soundEffects = new Dictionary<string, (SoundEffect, float, float, bool)>();
            ambience = new Dictionary<string, (Song, float)>();
            activeSFX = new Dictionary<int, (string, SoundEffectInstance, Vector2)>();
            audioListener = new AudioListener();
            audioListener.Position = new Vector3(Player.Instance.Transform.ScreenPosition.X, Player.Instance.Transform.ScreenPosition.Y, 0);
        }
        #endregion
        #region Methods
        /// <summary>
        /// Add a new type of SoundEffect to be played later
        /// </summary>
        /// <param name="name">The name of the SoundEffect (must be unique)</param>
        /// <param name="effect">The actual sound</param>
        /// <param name="volume">How loud is the SoundEffect</param>
        /// <param name="loops">Whether the SoundEffect should loop once played (fx. walkcycles)</param>
        public void AddSFX(string name, SoundEffect effect, float volume, bool loops)
        {
            if (!soundEffects.ContainsKey(name))
            {
                soundEffects.Add(name, (effect, volume, 100, loops));
            }
        }
        /// <summary>
        /// Add a new type of SoundEffect to be played later
        /// </summary>
        /// <param name="name">The name of the SoundEffect (must be unique)</param>
        /// <param name="effect">The actual sound</param>
        /// <param name="volume">How loud is the SoundEffect</param>
        /// <param name="range">How far away should the SoundEffect be heard</param>
        /// <param name="loops">Whether the SoundEffect should loop once played (fx. walkcycles)</param>
        public void AddSFX(string name, SoundEffect effect, float volume, float range, bool loops)
        {
            if (!soundEffects.ContainsKey(name))
            {
                soundEffects.Add(name, (effect, volume, range, loops));
            }
        }
        /// <summary>
        /// Add a new track of ambience/song to be played later
        /// </summary>
        /// <param name="name">The name of the ambience/song (must be unique)</param>
        /// <param name="song">The actual sound</param>
        /// <param name="volume">How loud is the ambience/song</param>
        public void AddAmbience(string name, Song song, float volume)
        {
            if (!ambience.ContainsKey(name))
            {
                ambience.Add(name, (song, volume));
            }
        }
        /// <summary>
        /// Stops the old ambience and switches to a new one
        /// </summary>
        /// <param name="name">The name of the new ambience</param>
        public void PlayAmbience(string name)
        {
            MediaPlayer.Play(ambience[name].Item1);
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = ambience[name].Item2;
        }
        /// <summary>
        /// Creates and plays a new SoundEffect instance
        /// </summary>
        /// <param name="name">The name of the SoundEffect type</param>
        /// <param name="position">Where the sound is placed (ScreenPosition)</param>
        /// <returns></returns>
        public int PlaySFX(string name, Vector2 position)
        {
            float differenceX = position.X - Player.Instance.Transform.ScreenPosition.X;
            float differenceY = position.Y - Player.Instance.Transform.ScreenPosition.Y;
            SoundEffectInstance newEffect = soundEffects[name].Item1.CreateInstance();
            newEffect.IsLooped = soundEffects[name].Item4;

            //if the sound is too far away to hear
            if (differenceX > soundEffects[name].Item3)
            {
                newEffect.Volume = 0;
            }
            else
            {
                float pan = MathF.Abs(differenceX);
                //make the volume depend on distance
                newEffect.Volume = (soundEffects[name].Item2 / Vector2.Distance(new Vector2(differenceX, differenceY), Player.Instance.Transform.ScreenPosition));
                newEffect.Pan = pan;
            }
            activeSFX.Add(ID, (name, newEffect, position));
            ID++;
            return ID - 1;
        }
        /// <summary>
        /// Removes a specific active SoundEffect
        /// </summary>
        /// <param name="ID"></param>
        public void StopSFX(int ID)
        {
            if (activeSFX.ContainsKey(ID))
            {
                activeSFX[ID].Item2.Stop();
                activeSFX[ID].Item2.Dispose();
                activeSFX.Remove(ID);
            }
        }
        /// <summary>
        /// Remove all active SoundEffects of the same type
        /// </summary>
        /// <param name="name">The name of the SoundEffects</param>
        public void StopSFX(string name)
        {
            List<int> tempIDs = new List<int>();
            foreach (KeyValuePair<int, (string, SoundEffectInstance, Vector2)> pair in activeSFX)
            {
                if (pair.Value.Item1 == name)
                {
                    tempIDs.Add(ID);
                }
            }
            foreach (int entry in tempIDs)
            {
                activeSFX[entry].Item2.Stop();
                activeSFX[entry].Item2.Dispose();
                activeSFX.Remove(entry);
            }
        }
        /// <summary>
        /// Pause a specific active SoundEffect without removing it from the world
        /// </summary>
        /// <param name="ID"></param>
        public void PauseSFX(int ID)
        {
            if (activeSFX.ContainsKey(ID))
            {
                activeSFX[ID].Item2.Stop();
            }
        }

        /// <summary>
        /// Pause all active SoundEffects of the same type, without removing them from the world
        /// </summary>
        /// <param name="name">The name of the SoundEffects</param>
        public void PauseSFX(string name)
        {
            foreach (KeyValuePair<int, (string, SoundEffectInstance, Vector2)> pair in activeSFX)
            {
                if (pair.Value.Item1 == name)
                {
                    pair.Value.Item2.Stop();
                }
            }
        }
        /// <summary>
        /// Resumes a paused SoundEffect
        /// </summary>
        /// <param name="ID"></param>
        public void ResumeSFX(int ID)
        {
            if (activeSFX.ContainsKey(ID))
            {
                activeSFX[ID].Item2.Play();
            }
        }
        /// <summary>
        /// Resumes pauesd SoundEffects of the same type
        /// </summary>
        /// <param name="name">The name of the SoundEffects</param>
        public void ResumeSFX(string name)
        {
            foreach (KeyValuePair<int, (string, SoundEffectInstance, Vector2)> pair in activeSFX)
            {
                if (pair.Value.Item1 == name)
                {
                    pair.Value.Item2.Play();
                }
            }
        }
        /// <summary>
        /// Stop the current music, leaving eerie silence
        /// </summary>
        public void StopMusic()
        {
            MediaPlayer.Stop();
        }
        /// <summary>
        /// Change the volume of a specific SoundEffect
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="volume"></param>
        public void ChangeSFXVolume(int ID, float volume)
        {
            if (activeSFX.ContainsKey(ID))
            {
                float differenceX = activeSFX[ID].Item3.X - Player.Instance.Transform.ScreenPosition.X;
                float differenceY = activeSFX[ID].Item3.Y - Player.Instance.Transform.ScreenPosition.Y;
                activeSFX[ID].Item2.Volume = (volume / Vector2.Distance(new Vector2(differenceX, differenceY), Player.Instance.Transform.ScreenPosition));
            }
        }
        /// <summary>
        /// Change the volume of all SoundEffects of ´the same type
        /// </summary>
        /// <param name="name"></param>
        /// <param name="volume"></param>
        public void ChangeSFXVolume(string name, float volume)
        {
            foreach (KeyValuePair<int, (string, SoundEffectInstance, Vector2)> pair in activeSFX)
            {
                if (pair.Value.Item1 == name)
                {
                    float differenceX = pair.Value.Item3.X - Player.Instance.Transform.ScreenPosition.X;
                    float differenceY = pair.Value.Item3.Y - Player.Instance.Transform.ScreenPosition.Y;
                    pair.Value.Item2.Volume = (volume / Vector2.Distance(new Vector2(differenceX, differenceY), Player.Instance.Transform.ScreenPosition));
                }
            }
        }
        /// <summary>
        /// Change the position of an active SoundEffect, since SoundEffects do not move with the screen (yet)
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="position"></param>
        public void ChangeSFXPosition(int ID, Vector2 position)
        {
            float differenceX = position.X - Player.Instance.Transform.ScreenPosition.X;
            float differenceY = position.Y - Player.Instance.Transform.ScreenPosition.Y;
            SoundEffectInstance effect = activeSFX[ID].Item2;
            string retainName = activeSFX[ID].Item1;
            activeSFX.Remove(ID);
            activeSFX.Add(ID, (retainName, effect, position));
            //if the sound is too far away to hear
            if (differenceX > soundEffects[activeSFX[ID].Item1].Item3)
            {
                effect.Volume = 0;
            }
            else
            {
                float pan = differenceX;
                //make the volume depend on distance
                effect.Volume = (soundEffects[activeSFX[ID].Item1].Item3 / Vector2.Distance(new Vector2(differenceX, differenceY), Player.Instance.Transform.ScreenPosition));
                effect.Pan = pan;
            }
        }
        #endregion
    }
}
