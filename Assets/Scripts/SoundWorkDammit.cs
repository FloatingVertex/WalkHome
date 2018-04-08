using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundWorkDammit : MonoBehaviour
{
    private AudioSource source;
	// Use this for initialization
	void Start ()
    {
        source = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        source.Play();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        source.Stop();
    }
}
