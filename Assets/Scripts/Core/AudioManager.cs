using System;
using MetaLabs.Interface;
using MetaLabs.Models;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MetaLabs.Core
{
    public class AudioManager : MonoBehaviour, IAudioManager
    {
        public Sound[] sounds;
        public static AudioManager Instance;
        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            DontDestroyOnLoad(gameObject);
            Instance = this;
            foreach (Sound s in sounds)
            {
                s.source = gameObject.AddComponent<AudioSource>();
                s.source.clip = s.clip;
                s.source.volume = s.volume;
                s.source.pitch = s.pitch;
                s.source.loop = s.loop;
            }
        }

        private void Update()
        {
            if (SceneManager.GetActiveScene().name != "EnteryRoom")
            {
                Pause("theme");
            }
            
        }

        public void Pause(string soundName)
        {
            Sound s = Array.Find(sounds, sound => sound.name == soundName);
            s.source.Pause();
        }

        public void Play(string soundName)
        {
            Sound s = Array.Find(sounds, sound => sound.name == soundName);
            s.source.Play();
        }
        public void Stop(string soundName)
        {
            Sound s = Array.Find(sounds, sound => sound.name == soundName);
            s.source.Stop();
        }
    }
}