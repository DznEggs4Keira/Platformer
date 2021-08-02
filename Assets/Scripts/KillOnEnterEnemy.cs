using UnityEngine;

public class KillOnEnterEnemy: Enemies {
    void OnTriggerEnter2D(Collider2D collision) {
        switch (_enemy) {
            case EnemyTypes.Fly:
            case EnemyTypes.Traps: {
                    if (collision.gameObject.CompareTag("Player")) {
                        Debug.Log("Player Died. Game Over!");
                        collision.GetComponent<Player>().ResetToStart();
                    }
                    break;
                }
        }
        
    }

    void OnCollisionEnter2D(Collision2D collision) {
        switch(_enemy) {
            case EnemyTypes.Slime: {
                    if (collision.gameObject.CompareTag("Player")) {
                        Debug.Log("Player Died. Game Over!");
                        collision.transform.GetComponent<Player>().ResetToStart();
                    }
                    break;
                }
        }
        
    }
}
