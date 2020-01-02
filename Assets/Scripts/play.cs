using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class play : MonoBehaviour
{
    //the list of available chests to choose from
    [SerializeField]public List<GameObject> chests = new List<GameObject>();

    [SerializeField] TextMeshProUGUI amountWon, noMoney;
    public Button playButton, decButton, incButton;
    CurrentBalance currentBalance;
    denomination denom;
    increment inc;
    
    //array of cash amounts for each chest
    float[] money = new float[] { 0.00f, 0.00f, 0.00f, 0.00f, 0.00f, 0.00f, 0.00f, 0.00f, 0.00f };
    float startingAmount;
    public float max;
    float totalWin;

    //the number that decides which chest is the losing chest
    int jackal;


    PickAChest pickaChest;

    // Start is called before the first frame update
    void Start()
    {
        playButton = GetComponent<Button>();
        playButton.onClick.AddListener(OnClick);

        incButton = GameObject.Find("increment denomination").GetComponent<Button>();
        inc = incButton.GetComponent<increment>();

        decButton = GameObject.Find("decrement denomination").GetComponent<Button>();

        currentBalance = GameObject.Find("current balance").GetComponent<CurrentBalance>();
        denom = GameObject.Find("current denomination").GetComponent<denomination>();
        
        max = money[0];


        pickaChest = GameObject.Find("Main Camera").GetComponent<PickAChest>();
        
    }

   public void OnClick()
    {
        playButton.interactable = false;
        playButton.gameObject.SetActive(false);

        incButton.interactable = false;
        incButton.gameObject.SetActive(false);

        decButton.interactable = false;
        decButton.gameObject.SetActive(false);
        
        currentBalance.currentBalance -= denom.denominationPossibilites[denom.i];
        amountWon.text = startingAmount.ToString();

        pickaChest.totalEarnings = 0.0f;

        for (int i = 0; i < chests.Count; ++i)
        {
            if (chests[i].name == "jackal")
                chests[i].name = "chest" + i;
        }

        jackal = Random.Range(0, chests.Count);
        chests[jackal].transform.name = "jackal";
        chests[jackal].transform.tag = "jackalChest";

        for (int i = 0; i < chests.Count; ++i)
        {
            if (chests[i].name == "jackal")
                continue;
            else
                chests[i].name = "chest" + i;
        }

        totalWin = Random.Range(0, 1001);
        

        for (; totalWin > 0.00f;)
        {
            for (int i = 0; i < money.Length; ++i)
            {
                //if there's at least only 5 cents left
                if (totalWin <= 0.05f)
                {
                    int j = Random.Range(0, chests.Count);
                    //and the jackal chest is the first chest in the array then increment j by 1 to the next chest
                    if (j == 0 && chests[0].transform.name == "jackal")
                    {
                        money[++j] += totalWin;
                        totalWin = 0.00f;
                    }
                    //or the jackal chest is the last chest in the array then decrement j by 1 to the second to last chest
                    else if (j == chests.Count && chests[j].transform.name == "jackal")
                    {
                        money[--j] += totalWin;
                        totalWin = 0.00f;
                    }
                    //if neither of those 2 then give the rest to the randomly selected chest and set totalWin to 0
                    else
                    {
                        money[j] += totalWin;
                        totalWin = 0.00f;
                    }
                }
                //distributing cash as normal
                else
                {
                    if (i == 0 &&chests[i].transform.name == "jackal")
                    {
                        money[++i] += 20;
                        totalWin -= 20.00f;
                    }
                    //or the jackal chest is the last chest in the array then decrement j by 1 to the second to last chest
                    else if (i == chests.Count && chests[i].transform.name == "jackal")
                    {
                        money[--i] += 20;
                        totalWin -= 20.00f;
                    }
                    //if neither of those 2 then give the rest to the randomly selected chest and set totalWin to 0
                    else
                    {
                        money[i] += 20.0f;
                        totalWin -= 20.00f;
                    }
                }
            }
            
        }

        for (int i = 0; i < money.Length; ++i)
        {
            if (money[i] > max)
                max = money[i];
        }

        for (int i = 0; i < chests.Count; ++i)
        {
            chests[i].GetComponent<base_chest>().reward = money[i];
            chests[i].gameObject.SetActive(true);

            if (chests[i].gameObject.GetComponent<base_chest>().isPicked)
            {
                chests[i].gameObject.GetComponent<base_chest>().isPicked = false;
                Animator animator = chests[i].gameObject.GetComponent<Animator>();
                animator.SetTrigger("close");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //telling the player their current balance is less than the wanted denomination
        if (inc.currentDenom.denominationPossibilites[denom.i] > currentBalance.currentBalance)
        {
            noMoney.text = "current balance must be greater than current denomination";
            playButton.interactable = false;
        }
        else
        {
            noMoney.text = "";
            playButton.interactable = true;
            playButton.gameObject.SetActive(true);
        }
    }
}
