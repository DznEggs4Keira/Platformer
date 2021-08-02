using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FlagProgression : MonoBehaviour
{
    [SerializeField] Animator flagAnim;
    [SerializeField] string _sceneName;
    [SerializeField] float _delay = 2f;

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            
            //play flag wave
            flagAnim.SetTrigger("Raise");

            //load new lovel
            StartCoroutine(LoadLevelAfterDelay());
        } else {
            return;
        }
    }

    IEnumerator LoadLevelAfterDelay() {

        yield return new WaitForSeconds(_delay);

        SceneManager.LoadScene(_sceneName);
    }
}
