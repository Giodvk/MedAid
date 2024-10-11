using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerController : MonoBehaviour
{   
    private int I=0;
    private int minutes;
    private int seconds;
    private TextMeshProUGUI text;
    private bool goOn;

    private bool stop;
    public bool GoOn{
        get{return this.goOn;}

        set{this.goOn = value;}
    }

    public bool Stop{
        get{return this.stop;}

        set{this.stop = value;}
    }
    // Start is called before the first frame update
    void Start()
    {
        text = gameObject.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
     if(goOn){
        StartCoroutine(StartTimer());
        goOn = false;
        I+=1;
     }   
    }

    public IEnumerator StartTimer(){
        minutes = 5;
        seconds = 00;
        text.text = minutes.ToString()+":"+seconds.ToString();
        while(minutes>0 && !stop){
            if(seconds == 00){
                seconds = 59;
                minutes -= 1;
                text.text = minutes.ToString()+":"+seconds.ToString();
            }
            else{
                seconds-=1;
                text.text = minutes.ToString()+":"+seconds.ToString();
            }
            yield return new WaitForSeconds(1);
        }
        stop = false;
    }
}
