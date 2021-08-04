using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies_Slime : Enemies
{
    // Slime
    [SerializeField] Sprite _deathSprite;

    void OnCollisionEnter2D(Collision2D collision) {
        //if (isDead) return;

        if (collision.gameObject.CompareTag("Player")) {
            
            if(collision.contacts[0].normal.y <= -0.5f) {
                StartCoroutine(Die());
            } else {
                collision.transform.GetComponent<Animator>().SetTrigger("Hit");
                collision.transform.GetComponent<Player>().ResetToStart();
            }
        }
    }

    IEnumerator Die() {
        var sr = GetComponent<SpriteRenderer>();
        sr.sprite = _deathSprite;

        GetComponent<Animator>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Rigidbody2D>().simulated = false;
        //isDead = true;

        float alpha = 1f;
        while (alpha > 0) {

            //null means to wait till end of frame
            yield return null;
            alpha -= Time.deltaTime;
            sr.color = new Color(1, 1, 1, alpha);
        }

    }
}
