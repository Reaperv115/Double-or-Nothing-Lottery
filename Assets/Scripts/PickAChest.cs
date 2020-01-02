using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.ParticleSystem;

public class PickAChest : MonoBehaviour
{
    //variables for using raycasting
    RaycastHit hit;
    Ray ray;


    //to determine if particles for the 
    //selected chest should be played
    bool playParticles = false;

    //timer for displaying the amount in the chest
    public float timer = 0.0f;
    float biggestwincelebrationTimer = 0.0f;

    //total amount won that game
    public float totalEarnings = 0.00f;

    //Text boxes for displaying the amount that was in the selected chest
    //and for indicating and eealing with Double or Nothing
    public TextMeshProUGUI DoubleorNothingOption;
    TextMeshProUGUI DoubleorNothingIndication;
    TextMeshProUGUI amountWon;
    TextMeshProUGUI wanttoplayAgain;
    TextMeshProUGUI rewardIndicator;

    //instance of the CurrentBalance class
    CurrentBalance currentBalance;

    //instance of the 
    //play class
    play Play;

    //Buttons for trying your luck, or knowing when to stop
    public Button IwantdoubleorNothing, IdontwantdoubleorNothing, IwanttoplayAgain, IdontwanttoplayAgain;

    ParticleSystem.MainModule colorforbiggestWin;
    // Start is called before the first frame update
    void Start()
    {
        //initializing game object instance variables
        currentBalance = GameObject.Find("current balance").GetComponent<CurrentBalance>();
        Play = GameObject.Find("Play").GetComponent<play>();
        amountWon = GameObject.Find("amount won").GetComponent<TextMeshProUGUI>();
        DoubleorNothingIndication = GameObject.Find("double or nothing").GetComponent<TextMeshProUGUI>();
        DoubleorNothingOption = GameObject.Find("double or nothing option").GetComponent<TextMeshProUGUI>();
        IwantdoubleorNothing = GameObject.Find("yes double or nothing").GetComponent<Button>();
        IdontwantdoubleorNothing = GameObject.Find("no double or nothing").GetComponent<Button>();
        wanttoplayAgain = GameObject.Find("Play Again?").GetComponent<TextMeshProUGUI>();
        rewardIndicator = GameObject.Find("reward").GetComponent<TextMeshProUGUI>();
        IwanttoplayAgain = GameObject.Find("I want to play again").GetComponent<Button>();
        IdontwanttoplayAgain = GameObject.Find("I dont want to play again").GetComponent<Button>();
        //adding listeners to  buttons
        IwantdoubleorNothing.onClick.AddListener(OnTryforDoubleorNothing);
        IdontwantdoubleorNothing.onClick.AddListener(OnNoDoubleorNothing);
        IwanttoplayAgain.onClick.AddListener(OnPlayAgain);
        IdontwanttoplayAgain.onClick.AddListener(OnQuit);

        IwantdoubleorNothing.interactable = false;
        IdontwantdoubleorNothing.interactable = false;
        IwanttoplayAgain.interactable = false;
        IdontwanttoplayAgain.interactable = false;

        IwantdoubleorNothing.gameObject.SetActive(false);
        IdontwantdoubleorNothing.gameObject.SetActive(false);
        IwanttoplayAgain.gameObject.SetActive(false);
        IdontwanttoplayAgain.gameObject.SetActive(false);

        wanttoplayAgain.text = "";

    }

    // Update is called once per frame
    void Update()
    {
        //making the start of the ray where my mouse is
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (timer >= 5.0f)
            rewardIndicator.text = "";
        else
            timer += Time.deltaTime;
        

        if (Physics.Raycast(ray, out hit))
        {
            //if mouse is over a chest, play the particle system on the chest
            if (hit.transform.name.Contains("chest") || hit.transform.name == "jackal")
            {
                Debug.Log(hit.transform.name);
                ParticleSystem particleSystem = hit.transform.gameObject.GetComponent<ParticleSystem>();
                if (playParticles)
                {
                    particleSystem.transform.position = new Vector3(hit.transform.position.x, hit.transform.position.y, hit.transform.position.z);
                    if (particleSystem.isPlaying) { }
                    else
                        particleSystem.Play(true);
                }
                playParticles = true;
            }

           if (Input.GetMouseButtonDown(0))
           {
               
               if (hit.transform.name.Contains("chest"))
               {
                    timer = 0.0f;
                   Animator anim;
                   anim = hit.transform.gameObject.GetComponent<Animator>();
                   //play the 'open animation' for the chest that's been clicked
                   anim.SetTrigger("open");
                   amountinChest(hit.transform.gameObject.GetComponent<base_chest>().reward);
                   //set the isPicked variable for the chest to true so that the chest can't be picked again.
                   hit.transform.gameObject.GetComponent<base_chest>().isPicked = true;

                    rewardIndicator.text = '+' + hit.transform.gameObject.GetComponent<base_chest>().reward.ToString();
                   //add the amount inside the chest to the totalEarning variable
                   totalEarnings += hit.transform.gameObject.GetComponent<base_chest>().reward;
                   //setting the amountWon text to display how much you've won this game
                   amountWon.text = totalEarnings.ToString();
               }
               if (hit.transform.name == "jackal")
               {
                   //setting the color of the fountains in the back to be black
                   var main1 = GameObject.Find("isthechestGood1").GetComponent<ParticleSystem>().main;
                   main1.startColor = Color.black;
                   var main2 = GameObject.Find("isthechestGood2").GetComponent<ParticleSystem>().main;
                   main2.startColor = Color.black;


                   //resetting chests in prep for next game
                   resetChests();

                   //check if the player wants to try for double or nothing
                   DoubleorNothingOption.text = "Do you want to try for double or nothing?";
                   IwantdoubleorNothing.gameObject.SetActive(true);
                   IwantdoubleorNothing.interactable = true;
                   IdontwantdoubleorNothing.gameObject.SetActive(true);
                   IdontwantdoubleorNothing.interactable = true;
               }

               if (hit.transform.name == "double")
               {
                   //setting the isPicked variable for the selected chest equal to true
                   hit.transform.gameObject.GetComponent<base_chest>().isPicked = true;

                  Animator animator = hit.transform.gameObject.GetComponent<Animator>();
                   animator.SetTrigger("open");

                   //setting colors of fountains to yellow in celebration of getting double their money
                   var DoNVictory1 = GameObject.Find("isthechestGood1").GetComponent<ParticleSystem>().main;
                   DoNVictory1.startColor = Color.yellow;
                   var DoNVictory2 = GameObject.Find("isthechestGood2").GetComponent<ParticleSystem>().main;
                   DoNVictory2.startColor = Color.yellow;

                    currentBalance.currentBalance = totalEarnings;

                   //updating amountWon text to show new amount earned
                   amountWon.text = totalEarnings.ToString();

                    wanttoplayAgain.text = "Do you want to plat again?";
                    IwanttoplayAgain.interactable = true;
                    IdontwanttoplayAgain.interactable = true;
                    IwanttoplayAgain.gameObject.SetActive(true);
                    IdontwanttoplayAgain.gameObject.SetActive(true);
                    DoubleorNothingIndication.text = "";
                    wanttoplayAgain.text = "Do you want to play again?";
               }
               else if (hit.transform.name == "nothing")
               {
                   //setting the isPicked variable for the selected chest equal to true
                   hit.transform.gameObject.GetComponent<base_chest>().isPicked = true;

                    Animator animator = hit.transform.gameObject.GetComponent<Animator>();
                    animator.SetTrigger("open");

                    //setting the fountain color to black because the player is getting nothing this game
                    var DoNVictory1 = GameObject.Find("isthechestGood1").GetComponent<ParticleSystem>().main;
                   DoNVictory1.startColor = Color.black;
                   var DoNVictory2 = GameObject.Find("isthechestGood2").GetComponent<ParticleSystem>().main;
                   DoNVictory2.startColor = Color.black;

                    wanttoplayAgain.text = "Do you want to plat again?";
                    IwanttoplayAgain.interactable = true;
                    IdontwanttoplayAgain.interactable = true;
                    IwanttoplayAgain.gameObject.SetActive(true);
                    IdontwanttoplayAgain.gameObject.SetActive(true);
                    DoubleorNothingIndication.text = "";
                    wanttoplayAgain.text = "Do you want to play again?";
                }
           }
            
        }
        else
        {
            //looping through all particle systems and if the PS is alive, kill it
            for (int i = 0; i < Play.chests.Count; ++i)
            {
                if (Play.chests[i].GetComponent<ParticleSystem>().IsAlive())
                {
                    ParticleSystem particleSystem = Play.chests[i].GetComponent<ParticleSystem>();
                    particleSystem.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
                }
            }
        }

    }


    void amountinChest(float reward)
    {
        //if the amount in the chest is greater than 0, turn the fountain green, else turn it red
        if (reward > 0.00f)
        {
            var main1 = GameObject.Find("isthechestGood1").GetComponent<ParticleSystem>().main;
            main1.startColor = Color.green;
            var main2 = GameObject.Find("isthechestGood2").GetComponent<ParticleSystem>().main;
            main2.startColor = Color.green;

        }
        else
        {
            var main1 = GameObject.Find("isthechestGood1").GetComponent<ParticleSystem>().main;
            main1.startColor = Color.red;
            var main2 = GameObject.Find("isthechestGood2").GetComponent<ParticleSystem>().main;
            main2.startColor = Color.red;
        }
        
    }

    void resetChests()
    {
        //looping through all chests, closing them then setting their isPicked variable to false
        for (int i = 0; i < Play.chests.Count; ++i)
        {
            if (Play.chests[i].GetComponent<base_chest>().isPicked)
            {
                Animator animator = Play.chests[i].GetComponent<Animator>();
                animator.SetTrigger("close");
                Play.chests[i].GetComponent<base_chest>().isPicked = false;
            }
            
        }
    }

    public void OnTryforDoubleorNothing()
    {
        //initiate double or nothing
        DoubleorNothing();
        IwantdoubleorNothing.gameObject.SetActive(false);
        IdontwantdoubleorNothing.gameObject.SetActive(false);
        DoubleorNothingOption.text = "";
    }

    void DoubleorNothing()
    {
        //determining which chests are going to be used for Doube or Nothing
        int chest1 = Random.Range(0, Play.chests.Count);
        int chest2 = Random.Range(0, Play.chests.Count);
        float totalAmount = totalEarnings * 2;

        //if the chests are the same number, simply increment chest2 by one
        if (chest2 == chest1)
            ++chest2;
        
        //deactivating all chests
        for (int i = 0; i < Play.chests.Count; ++i) { Play.chests[i].SetActive(false); }

        if (Play.chests[chest1].GetComponent<base_chest>().isPicked)
        {
            Animator animator = Play.chests[chest1].GetComponent<Animator>();
            animator.SetTrigger("close");
            Play.chests[chest1].GetComponent<base_chest>().isPicked = false;
        }
        if (Play.chests[chest2].GetComponent<base_chest>().isPicked)
        {
            Animator animator = Play.chests[chest2].GetComponent<Animator>();
            animator.SetTrigger("close");
            Play.chests[chest2].GetComponent<base_chest>().isPicked = false;
        }

        //activating the 2 chests that will be used
        Play.chests[chest1].SetActive(true);
        Play.chests[chest2].SetActive(true);

        //determining which chest will be the chest with double, and which will be the chest with nothing
        //as well as amessage explaining the rules
        int chestthatgivesyouDouble = Random.Range(0, 2);
        if (chestthatgivesyouDouble == 0)
        {
            Play.chests[chest1].GetComponent<base_chest>().reward = 0;
            Play.chests[chest1].gameObject.name = "nothing";

            Play.chests[chest2].GetComponent<base_chest>().reward = totalAmount;
            Play.chests[chest2].gameObject.name = "double";
        }
        else
        {
            Play.chests[chest1].GetComponent<base_chest>().reward = totalAmount;
            Play.chests[chest1].gameObject.name = "double";

            Play.chests[chest2].GetComponent<base_chest>().reward = 0;
            Play.chests[chest2].gameObject.name = "nothing";
        }

        DoubleorNothingIndication.text = "DOUBLE OR NOTHING. 1 chest gives you everything and 1 chest gives you nothing. Choose carefully.";
    }

    void restartGame()
    {
        //deactivating double or nothing features
        IwantdoubleorNothing.gameObject.SetActive(false);
        IdontwantdoubleorNothing.gameObject.SetActive(false);
        DoubleorNothingOption.text = "";
        DoubleorNothingIndication.text = "";


        //re-enabling features to prepare for next game
        Play.incButton.interactable = true;
        Play.incButton.gameObject.SetActive(true);

        Play.decButton.interactable = true;
        Play.decButton.gameObject.SetActive(true);

        Play.playButton.interactable = true;
        Play.playButton.gameObject.SetActive(true);

        wanttoplayAgain.text = "";
        IwanttoplayAgain.interactable = false;
        IwanttoplayAgain.gameObject.SetActive(false);
        IdontwanttoplayAgain.interactable = false;
        IdontwanttoplayAgain.gameObject.SetActive(false);

        for (int i = 0; i < Play.chests.Count; ++i) 
        {
            if (Play.chests[i].GetComponent<base_chest>().isPicked)
            {
                Animator animator = Play.chests[i].GetComponent<Animator>();
                animator.SetTrigger("close");
                Play.chests[i].GetComponent<base_chest>().isPicked = false;
            }
        }
    }

    public void OnNoDoubleorNothing()
    {
        //adding the total amount earned that game and restarting the game
        currentBalance.currentBalance += totalEarnings;
        wanttoplayAgain.text = "Do you want to play again?";
        DoubleorNothingOption.text = "";
        IwantdoubleorNothing.gameObject.SetActive(false);
        IdontwantdoubleorNothing.gameObject.SetActive(false);

        IwanttoplayAgain.interactable = true;
        IwanttoplayAgain.gameObject.SetActive(true);
        IdontwanttoplayAgain.interactable = true;
        IdontwanttoplayAgain.gameObject.SetActive(true);
    }

    public void OnPlayAgain()
    {
        restartGame();
    }

   public void OnQuit()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
