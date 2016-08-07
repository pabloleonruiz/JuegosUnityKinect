using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using EaseTools;



public class CountDown : MonoBehaviour {
    float timeLeft = 4;

    public Text text;

    public EaseUI easeUIComponent;


    void Start()
    {
        easeUIComponent.ScaleIn();

    }
    void Update()
    {
        timeLeft -= Time.deltaTime;
        text.text = "" + Mathf.Round(timeLeft);
        if (timeLeft < 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
