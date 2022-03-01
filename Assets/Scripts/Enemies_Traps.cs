using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies_Traps : Enemies
{
    private void OnParticleCollision(GameObject other) {

        if (other.gameObject.CompareTag("Player")) {
            Debug.Log("Player Died. Game Over!");
            other.GetComponent<Animator>().SetTrigger("Hit");
            other.GetComponent<Player>().ResetToStart();
        }
    }
}
