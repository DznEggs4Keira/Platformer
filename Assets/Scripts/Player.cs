using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Rigidbody2D playerRB;
    [SerializeField] Animator playerAnim;
    [SerializeField] SpriteRenderer playerSR;
    [SerializeField] float _speed = 5f;
    [SerializeField] float _jumpForce = 200f;
    private Vector2 _startPosition;

    //you can avoid an if entirely with
    //bool walking = horizontal != 0; - horizontal != 0 would be true, so anything other than that would be false
    //bool flipX = horizontal < 0; - horizontal < 0 would be true, so anything other than that would be false

    // Start is called before the first frame update
    void Start() {
        _startPosition = transform.position;
    }

    // Update is called once per frame
    void Update() {
        var horizontal = Input.GetAxis("Horizontal") * _speed;

        if (Mathf.Abs(horizontal) >= 1) {
            playerRB.velocity = new Vector2(horizontal, playerRB.velocity.y);
            Debug.Log($"velocity = {playerRB.velocity}");
        }

        playerAnim.SetBool("isWalking", horizontal != 0);
        if(horizontal != 0) playerSR.flipX = horizontal < 0;

        if (Input.GetButtonDown("Jump")) {
            playerRB.AddForce(Vector2.up * _jumpForce);
        }
    }

    internal void ResetToStart() {
        transform.position = _startPosition;
    }
}
