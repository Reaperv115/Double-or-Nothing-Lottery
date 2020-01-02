using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class title : MonoBehaviour
{
    TextMeshProUGUI Title;

    // Start is called before the first frame update
    void Start()
    {
        Title = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        Title.text = "Welcome to Double Or Nothing Lottery!";
    }
}
