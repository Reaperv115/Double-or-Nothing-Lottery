using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class increment : MonoBehaviour
{
    //declaring textbox objects to indicate when you are on the lowest and highest possible denominations
     [SerializeField] public TextMeshProUGUI highestDenomination, lowestDenomination;

    //creating public instance of denomination class
    public denomination currentDenom;

    //declaring instance of increment button object
    Button Increment;
    
    // Start is called before the first frame update
    void Start()
    {
        //initializing increment button by grabbing Button component from the button gameobject
        Increment = GetComponent<Button>();

        //adding click listener functionality to increment button
        Increment.onClick.AddListener(OnClick);

        //initializing currentDenomination class instance
        currentDenom = GameObject.Find("current denomination").GetComponent<denomination>();
    }

    public void OnClick()
    {
        //if the iterator for the currentDenomination class is greater than or equal to 3 tell the player
        //else iterate through the array
        if (currentDenom.i >= 3)
        {
            highestDenomination.text = "can't go any higher";
            lowestDenomination.text = "";
        }
        else
        {
            lowestDenomination.text = "";
            highestDenomination.text = "";
            ++currentDenom.i;
        }

    }
}
