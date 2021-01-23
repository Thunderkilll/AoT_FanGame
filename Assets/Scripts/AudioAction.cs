using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


namespace onboard
{
    public class AudioAction : Actions
    {
        [SerializeField] AudioClip[] audioClips;
        [SerializeField] bool isMusic;
        public bool loadOnStart = false;
        private AudioManager manager;

      
        public override void Act()
        {
            //Debug.Log("Act load this");
            if (isMusic)
                manager.PlayMusic(audioClips[0]);
            if (!isMusic)
                manager.PlaySfx(audioClips[Random.Range(0, audioClips.Length)], transform);
            
      }
        // Use this for initialization
        void Start()
        {
            manager = AudioManager.Instance;
            if (loadOnStart)
            {
               
                Act();
            }
        }

       

    }

}
