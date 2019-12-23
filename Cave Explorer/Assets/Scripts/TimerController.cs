using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerController : MonoBehaviour
{
    private UnityEngine.UI.Text text;

    private float time;


    // Start is called before the first frame update
    void Start()
    {
        text = this.GetComponent<UnityEngine.UI.Text>();
        text.text = "";
    }

    // Update is called once per frame
    void Update()
    {

        time += Time.deltaTime;

        var minutes = time / 60; //Divide the guiTime by sixty to get the minutes.
        var seconds = time % 60;//Use the euclidean division for the seconds.
        var fraction = (time * 100) % 100;

        //update the label value
        text.text = string.Format("{0:00} : {1:00} : {2:000}", minutes, seconds, fraction);
    }
}
