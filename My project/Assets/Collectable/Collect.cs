using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Collect : MonoBehaviour
{
    static TMP_Text collectText;
    static int amount = 0;
    static int collected = 0;
    private void Awake()
    {
        amount = 0;
        collected = 0;
    }
    private void Start()
    {
        if(collectText == null)
        {
            collectText = GameObject.Find("CollectText").GetComponent<TMP_Text>();
        }
        amount++;
        collectText.text = $"Collected: {collected} / {amount}";
    }

    private void OnDestroy()
    {
        collected++;
        collectText.text = $"Collected: {collected} / {amount}";
    }
}
