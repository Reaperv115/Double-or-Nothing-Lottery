using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MMPlay : MonoBehaviour
{
    Button mn_mnu_playbutton;

    // Start is called before the first frame update
    void Start()
    {
        mn_mnu_playbutton = GetComponent<Button>();
        mn_mnu_playbutton.onClick.AddListener(OnClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        SceneManager.LoadScene("Scenes/SampleScene");
    }
}
