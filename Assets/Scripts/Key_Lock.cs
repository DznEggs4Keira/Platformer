using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Key_Lock : MonoBehaviour
{
    [SerializeField] UnityEvent onUnlocked;

    public void Unlock() {
        Debug.Log("Unlocked");

        onUnlocked.Invoke();
    }
}
