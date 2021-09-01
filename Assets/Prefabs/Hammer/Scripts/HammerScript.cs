using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HammerScript : MonoBehaviour
{
    private LevelMenager levelMenager;
    private AudioSource hammerAudio;

    public AudioClip hammerAudioClip;

    // Start is called before the first frame update
    void Start()
    {
        levelMenager = GameObject.Find("LevelMenager").GetComponent<LevelMenager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            levelMenager.hammers++;
            levelMenager.numberOfHammers.GetComponent<Text>().text = levelMenager.hammers.ToString();
            playHammerSound();
            Destroy(gameObject);
        }
    }
    public void playHammerSound()
    {
        hammerAudio = levelMenager.gameObject.AddComponent<AudioSource>();
        hammerAudio.clip = hammerAudioClip;
        hammerAudio.Play();
    }
}
