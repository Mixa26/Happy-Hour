using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crab : MonoBehaviour
{
    public Object walkableObject;
    private float walkableObjectPosX;
    private float walkableObjectSizeX;

    private Vector3 crabPos;
    private float facing = 1;
    private float crabSpeed = 2.5f;
    private float crabSizeX;

    // Start is called before the first frame update
    void Start()
    {
        walkableObjectPosX = GameObject.Find(walkableObject.name.ToString()).GetComponent<Transform>().position.x;
        walkableObjectSizeX = GameObject.Find(walkableObject.name.ToString()).GetComponent<Collider>().bounds.size.x;

        crabPos = transform.position;
        crabSizeX = GetComponent<Collider>().bounds.extents.x;

        GetComponent<Animator>().SetBool("IsWalking", true);
    }

    // Update is called once per frame
    void Update()
    {
        crabPos = transform.position;
        crabPos.x += crabSpeed * facing * Time.deltaTime;
        transform.position = crabPos;

        if (crabPos.x + crabSizeX / 2 > walkableObjectPosX + walkableObjectSizeX / 2)
        {
            //movement right
            facing = -1;
        }
        else if (crabPos.x - crabSizeX / 2 < walkableObjectPosX - walkableObjectSizeX / 2)
        {
            //movement left
            facing = 1;
        }
    }
}
