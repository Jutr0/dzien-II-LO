using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalkeeperMovement : MonoBehaviour
{
    Vector3 moveDir;
    public float speed = 4;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(transform.position.x < -2)
        {
            moveDir = Vector3.right;
        }
        else if(transform.position.x > 2)
        {
            moveDir = Vector3.left;
        }
        transform.position += moveDir * Time.deltaTime * speed;

    }
}
