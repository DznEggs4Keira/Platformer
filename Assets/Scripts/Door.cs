using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    [SerializeField] Canvas _doorCanvas;
    [SerializeField] Sprite _openDoorTop;
    [SerializeField] Sprite _openDoorMid;

    [SerializeField] SpriteRenderer _doorTopRenderer;
    [SerializeField] SpriteRenderer _doorMidRenderer;
    [SerializeField] Door _exit;

    [SerializeField] int _requiredCoins = 3;

    bool _doorOpen;

    // Start is called before the first frame update
    void Start()
    {
        _doorOpen = false;
        _doorMidRenderer = GetComponent<SpriteRenderer>();
        _doorTopRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_doorOpen == false && Coin.CoinsCollected >= _requiredCoins) {
            OpenDoor();
        }
    }

    void OpenDoor() {
        _doorMidRenderer.sprite = _openDoorMid;
        _doorTopRenderer.sprite = _openDoorTop;

        _doorOpen = true;

        if (_doorCanvas != null) {
            _doorCanvas.enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (_doorOpen == false) return;

        if(collision.CompareTag("Player")) {
            TeleportPlayer(collision.GetComponent<Player>());
        }
    }

    void TeleportPlayer(Player player) {
        if(_exit != null) {
            player.TeleportTo(_exit.transform.position);
        }
    }
}
