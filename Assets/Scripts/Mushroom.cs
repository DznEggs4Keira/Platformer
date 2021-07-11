using UnityEngine;

public class Mushroom : MonoBehaviour {

    [SerializeField] float _bounceVelocity = 20;

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player")) {

            var playerRB = collision.gameObject.GetComponent<Rigidbody2D>();

            if (playerRB != null) {
                playerRB.velocity = new Vector2(playerRB.velocity.x, _bounceVelocity);
            }
        }
    }
}
