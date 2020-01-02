using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class decrement : MonoBehaviour
{
    //declaring instance of the button to decrement the denomination iterator
    Button Decrement;

    //declaring instance of increment class
    increment _increment;

    // Start is called before the first frame update
    void Start()
    {
        //initializing increment class instance
        _increment = GameObject.Find("increment denomination").GetComponent<increment>();

        //initializing decrement button by grabbing button component
        Decrement = GetComponent<Button>();

        //adding click listener functionality to the decrement button
        Decrement.onClick.AddListener(OnClick);
    }

    public void OnClick()
    {
        //if the denomination iterator is equal to or below 0
        //tell the player else continue iterating
        if (_increment.currentDenom.i <= 0)
        {
            _increment.lowestDenomination.text = "can't go any lower";
            _increment.highestDenomination.text = "";
        }
        else
        {
            _increment.highestDenomination.text = "";
            _increment.lowestDenomination.text = "";
            --_increment.currentDenom.i;
        }
    }
}
