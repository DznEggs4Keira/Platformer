using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Springboard : MonoBehaviour
{
    [SerializeField] float _bounceVelocity = 20;
    [SerializeField] Sprite _downSprite;
    private Sprite _upSprite;

    SpriteRenderer springboardSR;

    void Awake() {
        springboardSR = GetComponent<SpriteRenderer>();
        _upSprite = springboardSR.sprite;
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player")) {

            var playerRB = collision.gameObject.GetComponent<Rigidbody2D>();

            if (playerRB != null) {
                playerRB.velocity = new Vector2(playerRB.velocity.x, _bounceVelocity);
                springboardSR.sprite = _downSprite;
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
           springboardSR.sprite = _upSprite;
        }
    }
}
