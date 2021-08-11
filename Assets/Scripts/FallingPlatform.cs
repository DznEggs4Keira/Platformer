using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    bool _playerInside;
    bool _falling;
    Coroutine _coroutine;
    HashSet<Player> _playersInTrigger = new HashSet<Player>();
    Vector3 _initialPosition;
    float _wiggleTimer = 0;

    [Header("Falling Platform Options")]
    [Tooltip("Reset the wiggle timer when no players on the platform")]
    [SerializeField] bool _resetOnEmpty;
    [SerializeField] float _fallSpeed = 7f;
    [Range(0.1f, 5)][SerializeField] float _fallAfterSceonds = 3f;
    [Range(0.005f, 0.1f)] [SerializeField] float shakeX = 0.005f;
    [Range(0.005f, 0.1f)] [SerializeField] float shakeY = 0.005f;

    private void Start() {
        _initialPosition = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        var player = collision.GetComponent<Player>();

        if (player == null) return;

        _playersInTrigger.Add(player);
        _playerInside = true;

        if(_playersInTrigger.Count == 1) {
            _coroutine = StartCoroutine(WiggleAndFall());
        }
    }

    IEnumerator WiggleAndFall() {
        Debug.Log("Waiting to wiggle");
        yield return new WaitForSeconds(0.25f);
        Debug.Log("Wiggle");
        //_wiggleTimer = 0;

        while(_wiggleTimer < _fallAfterSceonds) {

            float randomX = UnityEngine.Random.Range(-shakeX, shakeY);
            float randomY = UnityEngine.Random.Range(-shakeX, shakeY);

            transform.position = _initialPosition + new Vector3(randomX, randomY);

            float randomDelay = UnityEngine.Random.Range(0.005f, 0.1f);

            yield return new WaitForSeconds(randomDelay);
            _wiggleTimer += randomDelay;

        }

        Debug.Log("Fall");
        _falling = true;
        foreach (var collider in GetComponents<Collider2D>()) {
            collider.enabled = false;
        }

        float fallTimer = 0;

        while (fallTimer < 3f) {
            transform.position += Vector3.down * Time.deltaTime * _fallSpeed;
            fallTimer += Time.deltaTime;
            Debug.Log(fallTimer);
            yield return null;
        }

        Destroy(gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (_falling) return;

        var player = collision.GetComponent<Player>();

        if (player == null) return;

        _playersInTrigger.Remove(player);
        if(_playersInTrigger.Count == 0) {

            _playerInside = false;
            //Debug.Log("Stopping Coroutine");
            StopCoroutine(_coroutine);
        }

        if (_resetOnEmpty)
            _wiggleTimer = 0f;
    }
}
