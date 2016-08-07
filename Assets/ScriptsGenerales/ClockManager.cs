using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ClockManager : MonoBehaviour {

    Image fillImg;
    public float timeAmt;
    public float time;
  //  public Text timeText;

    // Use this for initialization
    void Start()
    {
        fillImg = this.GetComponent<Image>();
        time = timeAmt;
    }

    // Update is called once per frame
    void Update()
    {
        if (time > 0)
        {
            time -= Time.deltaTime;
            fillImg.fillAmount = time / timeAmt;
 //           timeText.text = "Time : " + time.ToString("F");
        }
    }
}