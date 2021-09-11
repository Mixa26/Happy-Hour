using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxScript : MonoBehaviour
{
    public GameObject cubeFractured;

    private AudioSource boxBreakAudio;
    public AudioClip boxBreakAudioClip;
    private void Awake()
    {
        if (gameObject.CompareTag("BoxFractured"))
        {
            boxBreakAudio = gameObject.AddComponent<AudioSource>();
            boxBreakAudio.clip = boxBreakAudioClip;
            boxBreakAudio.Play();
            Destroy(gameObject, 2);
        }
    }

    private void Update()
    {
        if (transform.position.y < -5)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.CompareTag("Box") && other.CompareTag("Hammer"))
        {
            Vector3 position = transform.position;
            position.y -= 0.53f;
            Instantiate(cubeFractured, position, transform.rotation);
            Destroy(gameObject);
            other.GetComponent<HammerScript>().destroySelf();
        }
    }
}
