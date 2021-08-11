using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UICoinsCollected : MonoBehaviour
{
    TMP_Text _text;
    void Start()
    {
        _text = GetComponent<TMP_Text>();        
    }

    // Update is called once per frame
    void Update()
    {
        _text.SetText(Coin.CoinsCollected.ToString());
    }
}
