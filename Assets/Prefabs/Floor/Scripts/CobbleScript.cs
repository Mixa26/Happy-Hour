using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CobbleScript : MonoBehaviour
{
    private AudioSource hammerHitRockAudio;
    public AudioClip hammerHitRockAudioClip;

    private bool hit;

    private void Awake()
    {
        hammerHitRockAudio = gameObject.AddComponent<AudioSource>();
        hammerHitRockAudio.clip = hammerHitRockAudioClip;
        hit = false;
    }

    private void Update()
    {
        if (hit)
        {
            hammerHitRockAudio.Play();
            hit = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {      
        if (other.CompareTag("Hammer"))
        {
            other.GetComponent<HammerScript>().hit = true;
            hit = true;         
        }
    }
}
