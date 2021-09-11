using UnityEngine.UI;
using UnityEngine;

public class HammerStashScript : MonoBehaviour
{
    private LevelMenager levelMenager;

    private AudioSource hammerAudio;
    public AudioClip hammerAudioClip;

    // Start is called before the first frame update
    void Start()
    {
        levelMenager = GameObject.Find("LevelMenager").GetComponent<LevelMenager>();
        hammerAudio = gameObject.AddComponent<AudioSource>();
        hammerAudio.clip = hammerAudioClip;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        { 
            if (!(levelMenager.hammers >= 5))
            {
                levelMenager.hammers = 5;
                levelMenager.numberOfHammers.GetComponent<Text>().text = levelMenager.hammers.ToString();
                hammerAudio.Play();
            }
        }
    }
}
