using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InnerAmbienceScript : MonoBehaviour
{
    AudioSource aSource;
    public AudioClip ambience_loop1;
    public AudioClip ambience_loop2;
    public AudioClip night_loop;
    public bool playNightLoop = false;

    // Start is called before the first frame update
    void Start()
    {
        aSource = gameObject.GetComponent<AudioSource>();
        aSource.clip = ambience_loop1;
    }

    // Update is called once per frame
    void Update()
    {
        if(!playNightLoop && aSource.clip == night_loop || playNightLoop && aSource.clip != night_loop)
            aSource.Stop();

        if(!aSource.isPlaying)
        {
            if(playNightLoop)
            {
                //aSource.volume = 1.0f;
                aSource.clip = night_loop;
                aSource.Play();
            }
            else
            {
                //aSource.volume = 0.4f;

                if(aSource.clip == night_loop)
                {
                    aSource.clip = ambience_loop2;
                }
                
                if(aSource.clip == ambience_loop1)
                {
                    aSource.clip = ambience_loop2;
                    aSource.Play();
                }

                if(aSource.clip == ambience_loop2)
                {
                    aSource.clip = ambience_loop1;
                    aSource.Play();
                }
            }
        }
    }
}
