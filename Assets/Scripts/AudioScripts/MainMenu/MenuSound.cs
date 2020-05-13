using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSound : MonoBehaviour
{
    AudioSource Main_music;
    public AudioClip LoopClip;

    // Start is called before the first frame update
    void Start()
    {
        Main_music = gameObject.GetComponent<AudioSource>();
    
    }

    // Update is called once per frame
    void Update()
    {

        if (Main_music.isPlaying==false)
        {
            Main_music.clip = LoopClip;
            Main_music.loop = true;
            Main_music.Play();
        }
    }
}
