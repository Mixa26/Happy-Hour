using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HammerScript : MonoBehaviour
{
    private LevelMenager levelMenager;

    private GameObject player;

    private AudioSource hammerAudio;
    public AudioClip hammerAudioClip;
    private AudioSource hammerThrowAudio;
    public AudioClip hammerThrowAudioClip;

    private bool thrown;
    private int facing;

    private int cameraRenderDistance = 22;

    [HideInInspector]
    public bool hit;

    // Start is called before the first frame update
    void Start()
    {
        levelMenager = GameObject.Find("LevelMenager").GetComponent<LevelMenager>();
        player = GameObject.Find("Player");

        hammerAudio = levelMenager.gameObject.AddComponent<AudioSource>();
        hammerAudio.clip = hammerAudioClip;
    }

    private void Awake()
    {
        hammerThrowAudio = gameObject.AddComponent<AudioSource>();
        hammerThrowAudio.clip = hammerThrowAudioClip;
    }

    private void Update()
    {
        if (thrown)
        {
            Vector3 hammerPosition = gameObject.transform.position;
            hammerPosition.x += 10f * Time.deltaTime * facing;
            gameObject.transform.position = hammerPosition; 
            if (gameObject.transform.position.x > player.transform.position.x + cameraRenderDistance / 2 + 3 || gameObject.transform.position.x < player.transform.position.x - cameraRenderDistance / 2)
            {
                Destroy(gameObject);
            }
        }

        if (hit)
        {
            Destroy(gameObject);      
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            levelMenager.hammers++;
            levelMenager.numberOfHammers.GetComponent<Text>().text = levelMenager.hammers.ToString();
            hammerAudio.Play();
            Destroy(gameObject);
        }
    }

    public void throwHammer(int facing)
    {
        this.thrown = true;
        this.facing = facing;
        gameObject.GetComponent<Animator>().SetBool("Thrown", true);
        hammerThrowAudio.Play();
    }
    
    public void destroySelf()
    {
        Destroy(gameObject);
    }
}
