using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilPlantSound : MonoBehaviour
{
    AudioSource aSource;
    public AudioClip charge;
    public AudioClip death;
    public AudioClip hurt;
    public AudioClip impact;
    public AudioClip shoot;
    public AudioClip deathHeadfall;
    public bool playCharge = false;
    public bool playDeath = false;
    public bool playHurt = false;
    public bool playImpact = false;
    public bool playShoot = false;
    public bool playDeathHeadfall = false;

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
                playDeath = false;
                aSource.Play();
            }
            if(playCharge)
            {
                aSource.clip = charge;
                playCharge = false;
                aSource.Play();
            }
            if(playHurt)
            {
                aSource.clip = hurt;
                playHurt = false;
                aSource.Play();
            }
            if(playImpact)
            {
                aSource.clip = impact;
                playImpact = false;
                aSource.Play();
            }
            if(playShoot)
            {
                aSource.clip = shoot;
                playShoot = false;
                aSource.Play();
            }
            if(playDeathHeadfall)
            {
                aSource.clip = deathHeadfall;
                playDeathHeadfall = false;
                aSource.Play();
            }
        }
    }
}
