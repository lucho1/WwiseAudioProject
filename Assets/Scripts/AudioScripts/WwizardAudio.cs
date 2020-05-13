using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WwizardAudio : MonoBehaviour
{
    public AudioClip runesChargeBegin;
    public AudioClip runesChargeEnd;
    public AudioClip poofClip;
    AudioSource aSource;

    public bool playOnChargeBegin = false;
    public bool playOnChargeEnd = false;
    public bool playPoof = false;

    // Start is called before the first frame update
    void Start()
    {
        aSource = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playPoof)
        {
            aSource.clip = poofClip;
            playPoof = false;            
            aSource.Play();
        }

        if(playOnChargeBegin)
        {
            aSource.clip = runesChargeBegin;
            playOnChargeBegin = false;            
            aSource.Play();
        }

        if(playOnChargeEnd)
        {
            aSource.clip = runesChargeEnd;  
            playOnChargeEnd = false;          
            aSource.Play();
        }
    }
}
