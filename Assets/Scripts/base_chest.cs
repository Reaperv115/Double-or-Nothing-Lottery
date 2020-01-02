using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class base_chest : MonoBehaviour
{
    public float reward = 0.0f;
    public float timer = 0.0f;

    public bool playParticles = false;
    public bool isPicked;

    

    public Animator animator;

    Ray ray;
    RaycastHit hit;

    public enum ChestStates
    {
        open, close
    } 
    public ChestStates _chestState;
    public void setState(ChestStates cheststate) { _chestState = cheststate; }
    public ChestStates getState() { return _chestState; }


    // Start is called before the first frame update
    void Start()
    {
        transform.gameObject.SetActive(false);
        isPicked = false;
        
    }

    void Update()
    {
        

        //if (isPicked)
        //{
        //    _chestState = ChestStates.open;
        //}
        //else if (isPicked && _chestState == ChestStates.open)
        //{
        //    return;
        //}
        //else
        //{
        //    _chestState = ChestStates.close;
        //}
        //switch (_chestState)
        //{
        //    case ChestStates.open:
        //        chestPicked();
        //        break;
        //    case ChestStates.close:
        //        animator.SetTrigger("close");
        //        break;
        //    default:
        //        break;
        //}
    }

    //void chestPicked()
    //{
    //    rewardIndicator.text = " + " + reward.ToString();
    //    animator.SetTrigger("open");
    //}

}
