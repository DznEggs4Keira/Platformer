using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Player 
    [Header("Player")]
    [SerializeField] int playerNum = 1;

    public int PlayerNumber { get { return playerNum; } }

    //Serializable Components
    [Header("Cache Components")]
    [SerializeField] Rigidbody2D playerRB;
    [SerializeField] Animator playerAnim;
    [SerializeField] SpriteRenderer playerSR;
    [SerializeField] Transform playerFeet;

    //Serializable private fields
    [Header("Movement")]
    [SerializeField] float _speed = 5f;
    [SerializeField] float _slipFactor = 1f;
    [Header("Jump")]
    [SerializeField] float _jumpVelocity = 10f;
    [SerializeField] int _maxJumps = 2;
    [SerializeField] float _downPull = 0.1f;
    [SerializeField] float _maxJumpDuration = 0.1f;

    //private fields
    Vector2 _startPosition;
    int _jumpsRemaining;
    bool isGrounded = true;
    bool _isOnSlipperySurface;
    float _fallTimer;
    float _jumpTimer;
    float _horizontal;

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

        UpdatAnimator();

        CalculateIsGrounded();

        // Get the Input Movement from the player
        _horizontal = Input.GetAxis($"P{playerNum}Horizontal") * _speed;

        if (_isOnSlipperySurface) {
            SlipHorizontalMovement();
        } else {
            UpdateHorizontalMovement();
        }

        PerformJumpingCalculations();
    }

    void PerformJumpingCalculations() {

        //JUMP GOING UP
        //Jump and Double Jump
        if (Input.GetButtonDown($"P{playerNum}Jump") && _jumpsRemaining > 0) {

            _jumpsRemaining--;
            Debug.Log($"Jumps Remaining: {_jumpsRemaining}");
            playerRB.velocity = new Vector2(playerRB.velocity.x, _jumpVelocity);
            //playerRB.AddForce(Vector2.up * _jumpForce);
            _fallTimer = 0f;
            _jumpTimer = 0f;

        } //Hold Jump
          else if (Input.GetButton($"P{playerNum}Jump") && _jumpTimer <= _maxJumpDuration) {
              playerRB.velocity = new Vector2(playerRB.velocity.x, _jumpVelocity);
              _fallTimer = 0;
          }

        //we don't wanna increment this in the if because that will just add a few milliseconds in and we will get a third jump on hold
        //we don't care if the jump timer keeps incrementing as long as it is reset when we start the jump
        _jumpTimer += Time.deltaTime;

        // JUMP COMING DOWN
        //Checking and applying motion for grounding the player after the jumping
        //have been falling!!
        if (isGrounded && _fallTimer > 0) {

            _fallTimer = 0f;
            _jumpsRemaining = _maxJumps;

        } else {

            _fallTimer += Time.deltaTime;
            var downForce = _downPull * _fallTimer * _fallTimer;
            playerRB.velocity = new Vector2(playerRB.velocity.x, playerRB.velocity.y - downForce);
        }
    }

    void UpdateHorizontalMovement() {
        
        //Move Player based on movement
        playerRB.velocity = new Vector2(_horizontal, playerRB.velocity.y);
        //Debug.Log($"velocity = {playerRB.velocity}");
    }

    void SlipHorizontalMovement() {

        var desiredVelocity = new Vector2(_horizontal, playerRB.velocity.y);
        var smoothVelocity = Vector2.Lerp(playerRB.velocity, desiredVelocity, Time.deltaTime / _slipFactor);
        //Move Player based on movement
        playerRB.velocity = smoothVelocity;
        //Debug.Log($"velocity = {playerRB.velocity}");
    }

    void UpdatAnimator() {
        // Set the animation of player based on movement
        playerAnim.SetBool("isJumping", !isGrounded);

        playerAnim.SetBool("isWalking", _horizontal != 0);
        if (_horizontal != 0) playerSR.flipX = _horizontal < 0;
    }

    void CalculateIsGrounded() {
        //Check if feet are on the Ground with OverlapCircle Raycast
        var hit = Physics2D.OverlapCircle(playerFeet.position, 0.1f, LayerMask.GetMask("Ground"));
        isGrounded = hit != null;

        //shorthand
        //_isOnSlipperySurface = hit?.CompareTag("Slippery") ?? false;

        if(hit != null) {
            _isOnSlipperySurface = hit.CompareTag("Slippery");
        } else {
            _isOnSlipperySurface = false;
        }
    }

    internal void ResetToStart() {
        playerRB.position = _startPosition;
    }

    internal void TeleportTo(Vector3 position) {
        playerRB.position = position;
        playerRB.velocity = Vector2.zero;
    }
}
