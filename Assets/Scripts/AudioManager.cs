using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;




//manage sounds in the scene
namespace onboard
{
    public class AudioManager : MonoBehaviour
    {
        #region Properties
    
        [Header("Audio Mixer settings")]
        [Tooltip("Audio mixer group to be controlled")]
        [SerializeField] AudioMixerGroup sfxGroup, musicGroup;
        [Tooltip("number of music instances at once")]
        [SerializeField] int audioSourceInstances = 5;

        public static AudioManager Instance { get; private set; }
        bool isPlayed = false;
        private Queue<AudioSource> sfxLib = new Queue<AudioSource>();
        private AudioSource musicPlayer;
        #endregion

        #region default methods
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
                if (musicPlayer != null)
                {
                    musicPlayer.volume = 0.123f;
                }
            }
            else
            {
                Destroy(gameObject);
            }
        }

        // Use this for initialization
        void Start()
        {
            Init();
        }

        void Init()
        {
            for (int i = 0; i < audioSourceInstances; i++)
            {
                sfxLib.Enqueue(AudioSourceInstantiate(sfxGroup, false,"AudioSource" + i.ToString("00")));
            }

            musicPlayer = AudioSourceInstantiate(musicGroup, false, "MusicSource");
        }

        #endregion

        #region custom methods
        AudioSource AudioSourceInstantiate(AudioMixerGroup group, bool sfx, string name = "AudioSource")
        {
            AudioSource audio = new GameObject(name).AddComponent<AudioSource>();
            audio.outputAudioMixerGroup = group;
            audio.spatialBlend = sfx ? 1f : 0f;

            audio.loop = sfx;

            audio.transform.SetParent(transform);

            return audio;
        }

        public void PlaySfx(AudioClip clip, Transform source = null)
        {
            AudioSource audio;

            if (sfxLib.Count == 0)
            {
                audio = AudioSourceInstantiate(sfxGroup, true, "AudioSource" + audioSourceInstances++);
            }
            else
            {
                audio = sfxLib.Dequeue();
            }

            audio.transform.position = source != null ? source.position : Vector3.zero;

            audio.clip = clip;
            audio.Play();

            audio.transform.SetAsLastSibling(); //to illustrate the dequeue/enqueue process

            sfxLib.Enqueue(audio);
        }

        public void PlayMusic(AudioClip music)
        {
            if (musicPlayer.clip == music)
            {
                return;
            }
            if (musicPlayer.isPlaying)
            {
                StartCoroutine(WaitForMusicToEnd(music));
            }
            else
            {
                musicPlayer.clip = music;
                musicPlayer.Play();
            }


        }

        IEnumerator WaitForMusicToEnd(AudioClip c)
        {
            yield return new WaitForSeconds(c.length);
            musicPlayer.clip = c;
            musicPlayer.Play();
        }
        public void StopMusic(AudioClip music)
        {
            if (musicPlayer.clip == music)
                return;

            musicPlayer.clip = music;
            musicPlayer.Stop();
        }
        #endregion

    }
}

