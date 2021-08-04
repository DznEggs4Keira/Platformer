using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public static int CoinsCollected;
    
    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.CompareTag("Player")) {
            PickupCoin();
        }
    }

    void PickupCoin() {
        gameObject.SetActive(false);
        CoinsCollected++;
        //Debug.Log(CoinsCollected);
    }
}
