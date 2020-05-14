using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilHeadSound : MonoBehaviour
{
    AudioSource aSource;
    public AudioClip charge;
    public AudioClip death;
    public AudioClip bite;
    public bool playCharge = false;
    public bool playDeath = false;
    public bool playBite = false;

    // Start is called before the first frame update
    void Start()
    {
        aSource = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!aSource.isPlaying)
        {
            if(playDeath)
            {
                aSource.clip = death;
                aSource.Play();
                playDeath = false;
            }
            if(playCharge)
            {
                aSource.clip = charge;
                aSource.Play();
                playCharge = false;
            }
            if(playBite)
            {
                aSource.clip = bite;
                aSource.Play();
                playBite = false;
            }
        }
    }
}
