using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovementScript : MonoBehaviour
{
    GameObject player;
    Vector3 offSet;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        offSet = new Vector3(1.5f, 0, -12);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position + offSet;
    }
}
