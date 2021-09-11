using UnityEngine;

public class SpikesScript : MonoBehaviour
{
    public AudioSource SpikesAudio;

    private bool spiked;

    private BoxCollider boxCollider;

    private Animator animator;

    public int delay;

    // Start is called before the first frame update
    void Start()
    {
        spiked = true;
        boxCollider = gameObject.GetComponent<BoxCollider>();
        animator = gameObject.GetComponent<Animator>();
        SpikesAudio = transform.parent.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (spiked)
        {
            Invoke("spikeIn", delay);
            spiked = false;
        }
    }

    void spikeOut()
    {
        animator.SetBool("SpikeOut", true);
        boxCollider.enabled = true;
        SpikesAudio.Play();
        Invoke("spikeIn", 2f);
    }

    void spikeIn()
    {
        animator.SetBool("SpikeOut", false);
        boxCollider.enabled = false;
        Invoke("spikeOut", 2f);
    }


}
