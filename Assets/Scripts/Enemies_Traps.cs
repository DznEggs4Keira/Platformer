using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies_Traps : Enemies
{
    protected override void Start() {
        _enemy = EnemyTypes.Traps;
    }

    protected override void OnParticleCollision(GameObject other) {

        base.OnParticleCollision(other);

        if (other.gameObject.CompareTag("Player")) {
            Debug.Log("Player Died. Game Over! From Lava Hit");
            other.GetComponent<Animator>().SetTrigger("Hit");
            other.GetComponent<Player>().ResetToStart();
        }
    }
}
