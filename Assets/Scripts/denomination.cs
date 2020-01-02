using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class denomination : MonoBehaviour
{
    //text box to show currently selected denomination
    TextMeshProUGUI denominationAmount;

    //denomination options
    public float[] denominationPossibilites = { 0.25f, 0.50f, 1.00f, 5.00f };

    //iterator for iterating through denomination possibilities
    public int i = 0;

   //initializing text box object
    void Start() => denominationAmount = GetComponent<TextMeshProUGUI>();

    //updating denomination text box to show currently selected denomination
    void Update() => denominationAmount.text = "Denomination: " + denominationPossibilites[i];
}
