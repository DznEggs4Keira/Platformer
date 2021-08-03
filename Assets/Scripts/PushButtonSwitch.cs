using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class PushButtonSwitch : MonoBehaviour
{
    [SerializeField] SpriteRenderer buttonSR;
    [SerializeField] Sprite _downSprite;
    [SerializeField] UnityEvent _onPressed;
    [SerializeField] UnityEvent _onReleased;
    [SerializeField] float _delay;
    [SerializeField] int _playerNumber = 1;
    Sprite _upSprite;

    void Awake() {
        _upSprite = buttonSR.sprite;

        StartCoroutine(BecomeReleased());
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            if (collision.GetComponent<Player>().PlayerNumber != _playerNumber) return;

            BecomePressed();
        }
    }

    void BecomePressed() {
        //switch sprite to down
        buttonSR.sprite = _downSprite;

        _onPressed?.Invoke();
    }

    void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            if (collision.GetComponent<Player>().PlayerNumber != _playerNumber) return;

            StartCoroutine(BecomeReleased(_delay));
        }
    }

    IEnumerator BecomeReleased(float delay = 0f) {

        yield return new WaitForSeconds(delay);

        //switch sprite to up
        buttonSR.sprite = _upSprite;

        _onReleased?.Invoke();
    }
}
