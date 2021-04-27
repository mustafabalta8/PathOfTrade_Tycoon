using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSettings : MonoBehaviour
{
    [SerializeField] AudioClip[] BackgroundSounds;
    AudioSource MyAudioSource;
    // Start is called before the first frame update
    void Start()
    {
       // MyAudioSource = GetComponent<AudioSource>();
        //PlaySounds();
    }

    // Update is called once per frame
    void Update()
    { 
        
    }
    public void PlaySounds()
    {
        foreach(AudioClip BackgroundSound in BackgroundSounds)
        {
            MyAudioSource.PlayOneShot(BackgroundSound);
        }  
        
    }
}
