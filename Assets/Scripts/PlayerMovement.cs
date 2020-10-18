using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D playerRb;
    public float moveSpeed = 10;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical= Input.GetAxis("Vertical");

        Vector2 move = new Vector2(horizontal, vertical);

        playerRb.MovePosition(playerRb.position + move * Time.fixedDeltaTime * moveSpeed);

    }
}
