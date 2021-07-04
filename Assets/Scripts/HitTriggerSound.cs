using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitTriggerSound : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource audio;
    public AudioClip attack;
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play(){
    	audio.PlayOneShot(attack, 1);
    }
}
