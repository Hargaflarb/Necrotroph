using Necrotroph_Eksamensprojekt.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Necrotroph_Eksamensprojekt
{
    public class SoundManager
    {
        #region Fields
        private static SoundManager instance;
        private Dictionary<string, (SoundEffect, float,float)> soundEffects;
        private Dictionary<string, (Song, float)> ambience;
        private Dictionary<string, SoundEffectInstance> activeSFX;
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
            soundEffects = new Dictionary<string, (SoundEffect, float, float)>();
            ambience = new Dictionary<string, (Song, float)>();
            activeSFX = new Dictionary<string, SoundEffectInstance>();
        }
        #endregion
        #region Methods
        public void AddSFX(string name, SoundEffect effect, float volume,float range)
        {
            soundEffects.Add(name, (effect, volume,range));
        }
        public void AddSong(string name, Song song, float volume)
        {
            ambience.Add(name, (song, volume));
        }
        public void PlaySong(string name)
        {
            MediaPlayer.Play(ambience[name].Item1);
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = ambience[name].Item2;
        }
        public void PlaySFX(string name, Vector2 position)
        {
            float differenceX = position.X - Player.Instance.Transform.ScreenPosition.X;
            float pan = differenceX;

            //use soundeffectinstance
            SoundEffectInstance newEffect = soundEffects[name].Item1.CreateInstance();
            newEffect.Volume = soundEffects[name].Item2;
            newEffect.Pan = pan;
            activeSFX.Add(name, newEffect);
        }
        public void StopSFX(string name)
        {
            if (activeSFX.ContainsKey(name))
            {
                activeSFX[name].Stop();
                activeSFX[name].Dispose();
                activeSFX.Remove(name);
            }
        }
        public void PauseSFX(string name)
        {
            if (activeSFX.ContainsKey(name))
            {
                activeSFX[name].Pause();
            }
        }
        public void ResumeSFX(string name)
        {
            if (activeSFX.ContainsKey(name))
            {
                activeSFX[name].Resume();
            }
        }
        public void StopMusic(string name)
        {
            MediaPlayer.Stop();
        }
        public void ChangeSFXVolume(string name,float volume)
        {
            if (activeSFX.ContainsKey(name))
            {
                activeSFX[name].Volume = volume;
            }
        }
        #endregion
    }
}
