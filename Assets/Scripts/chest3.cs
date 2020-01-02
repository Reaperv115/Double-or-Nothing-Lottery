//using System.Collections;
//using System.Collections.Generic;
//using TMPro;
//using UnityEngine;
//using UnityEngine.UI;

//public class chest3 : base_chest
//{
//    base_chest base_Chest;

//    // Start is called before the first frame update
//    void Start()
//    {
//        rewardIndicator = GameObject.Find("reward").GetComponent<TextMeshProUGUI>();
//        base_Chest = GetComponent<base_chest>();
//        animator = GetComponent<Animator>();
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        if (timer >= 2.0f)
//            rewardIndicator.text = "";
//        else
//        {
//            if (base_Chest.isPicked)
//            {
//                rewardIndicator.text = " + " + reward.ToString();
//                _chestState = ChestStates.open;
//                timer += Time.deltaTime;
//            }
//            else
//            {
//                _chestState = ChestStates.close;
//            }

//        }
//        switch (_chestState)
//        {
//            case ChestStates.open:
//                animator.SetTrigger("open");
//                break;
//            case ChestStates.close:
//                animator.SetTrigger("close");
//                break;
//            default:
//                break;
//        }
//    }
//}
