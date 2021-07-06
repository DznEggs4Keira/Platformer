using UnityEngine;

public class KillOnEnter: MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            Debug.Log("Player Died. Game Over!");
            collision.GetComponent<Player>().ResetToStart();
        }
    }
}
