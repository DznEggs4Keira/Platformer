using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Rigidbody2D playerRB;
    [SerializeField] Animator playerAnim;
    [SerializeField] SpriteRenderer playerSR;
    [SerializeField] Transform playerFeet;
    [SerializeField] float _speed = 5f;
    [SerializeField] float _jumpVelocity = 10f;
    [SerializeField] int _maxJumps = 2;
    [SerializeField] float _downPull = 0.1f;
    [SerializeField] float _maxJumpDuration = 0.1f;

    Vector2 _startPosition;
    int _jumpsRemaining;
    bool isGrounded = true;
    float _fallTimer;
    float _jumpTimer;

    //you can avoid an if entirely with
    //bool walking = horizontal != 0; - horizontal != 0 would be true, so anything other than that would be false
    //bool flipX = horizontal < 0; - horizontal < 0 would be true, so anything other than that would be false

    // Start is called before the first frame update
    void Start() {
        _startPosition = transform.position;
        _jumpsRemaining = _maxJumps;
    }

    // Update is called once per frame
    void Update() {

        //Check if feet are on the Ground with OverlapCircle Raycast
        var hit = Physics2D.OverlapCircle(playerFeet.position, 0.1f, LayerMask.GetMask("Ground"));
        isGrounded = hit != null;

        // Get the Input Movement from the player
        var horizontal = Input.GetAxis("Horizontal") * _speed;

        //Move Player based on movement
        if (Mathf.Abs(horizontal) >= 1) {
            playerRB.velocity = new Vector2(horizontal, playerRB.velocity.y);
            Debug.Log($"velocity = {playerRB.velocity}");
        }

        // Set the animation of player based on movement
        playerAnim.SetBool("isWalking", horizontal != 0);
        if(horizontal != 0) playerSR.flipX = horizontal < 0;


        //Jump and Double Jump
        if (Input.GetButtonDown("Jump") && _jumpsRemaining > 0) {

            playerRB.velocity = new Vector2(playerRB.velocity.x, _jumpVelocity);
            //playerRB.AddForce(Vector2.up * _jumpForce);
            _jumpsRemaining--;
            _fallTimer = 0f;
            _jumpTimer = 0f;

        } else if(Input.GetButton("Jump") && _jumpTimer <= _maxJumpDuration) {

            playerRB.velocity = new Vector2(playerRB.velocity.x, _jumpVelocity);
            _fallTimer = 0;
            _jumpTimer += Time.deltaTime;
        }

        //Hold Jump


        //Checking and applying motion for grounding the player after the jumping
        if (isGrounded) {

            _fallTimer = 0f;
            _jumpsRemaining = _maxJumps;

        } else {

            _fallTimer += Time.deltaTime;
            var downForce = _downPull * _fallTimer * _fallTimer;
            playerRB.velocity = new Vector2(playerRB.velocity.x, playerRB.velocity.y - downForce);
        }
    }

    internal void ResetToStart() {
        transform.position = _startPosition;
    }
}
