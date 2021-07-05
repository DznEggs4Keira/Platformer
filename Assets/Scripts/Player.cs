using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Rigidbody2D playerRB;
    [SerializeField] Animator playerAnim;
    [SerializeField] SpriteRenderer playerSR;
    [SerializeField] float speed = 5f;
    [SerializeField] float jumpForce = 200f;

    //you can avoid an if entirely with
    //bool walking = horizontal != 0; - horizontal != 0 would be true, so anything other than that would be false
    //bool flipX = horizontal < 0; - horizontal < 0 would be true, so anything other than that would be false

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        var horizontal = Input.GetAxis("Horizontal");

        playerRB.velocity = new Vector2(horizontal * speed, playerRB.velocity.y);
        playerAnim.SetBool("isWalking", horizontal != 0);
        if(horizontal != 0) playerSR.flipX = horizontal < 0;

        if (Input.GetButtonDown("Jump")) {
            playerRB.AddForce(Vector2.up * jumpForce);
        }
    }
}
