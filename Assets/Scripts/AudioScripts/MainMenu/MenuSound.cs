using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSound : MonoBehaviour
{
    AudioSource Main_music;
    public AudioClip LoopClip;
    public AudioClip NightClip;
    public bool playNight = false;

    // Start is called before the first frame update
    void Start()
    {
        Main_music = gameObject.GetComponent<AudioSource>();    
    }

    // Update is called once per frame
    void Update()
    {
        if(playNight && Main_music.clip == LoopClip || !playNight && Main_music.clip == NightClip)
            Main_music.Stop();

        if(!Main_music.isPlaying)
        {
            if(playNight)
            {
                Main_music.clip = NightClip;
                Main_music.Play();
            }
            else
            {
                Main_music.clip = LoopClip;
                Main_music.Play();
            }
        }
    }
}
