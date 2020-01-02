using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CurrentBalance : MonoBehaviour
{
    public float currentBalance = 10.00f;

    [SerializeField]
    TextMeshProUGUI currentbalance;
    // Start is called before the first frame update
    void Start() => currentbalance.text = "Current Balance: " + currentBalance;

    // Update is called once per frame
    void Update() => currentbalance.text = "C.B.: " + currentBalance;
}
