using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player")) {

            if (collision.contacts[0].normal.y > 0) {
                TakeHit();
            }
        }
    }

    void TakeHit() {

        GetComponent<ParticleSystem>().Play();

        GetComponent<Collider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
    }
}
