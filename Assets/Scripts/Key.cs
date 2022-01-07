using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField] Key_Lock _KeyLock;

    private void OnTriggerEnter2D(Collider2D collision) {
        var player = collision.GetComponent<Player>();
        if(player != null) {
            //if already has a key, don't let the player hold another one
            if(player.GetComponentInChildren<Key>()) return;

            transform.SetParent(player.transform);
            transform.localPosition = Vector3.up;
        }

        var keyLock = collision.GetComponent<Key_Lock>();
        if(keyLock != null && keyLock == _KeyLock) {
            keyLock.Unlock();
            Destroy(gameObject);
        }
    }
}
