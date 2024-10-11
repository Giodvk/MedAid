using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class decisione : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public bool esito;
    private BotPaziente paziente;
    private TextMeshPro result;
    public Camera libro;
    private prova bot;
    public static GameObject MainCamera;
    private TimerController timer;
    // Start is called before the first frame update
    void Start()
    {
        result = GameObject.FindGameObjectWithTag("esito").GetComponent<TextMeshPro>();
        timer = GameObject.FindGameObjectWithTag("time").GetComponent<TimerController>();
        
    }

    void Update(){
        if( GameObject.FindGameObjectWithTag("Bot"))
        paziente = GameObject.FindGameObjectWithTag("Bot").GetComponent<BotPaziente>();
    }
    public void OnPointerEnter(PointerEventData pointerEventData){
        gameObject.GetComponent<TextMeshProUGUI>().color = Color.green;
    }

    public void OnPointerExit(PointerEventData pointerEventData){
        gameObject.GetComponent<TextMeshProUGUI>().color = Color.black;
    }

    public void OnPointerClick(PointerEventData pointerEventData){
        bot = GameObject.FindGameObjectWithTag("Bot").transform.parent.gameObject.GetComponent<prova>();
        gameObject.GetComponent<TextMeshProUGUI>().color = Color.black;
        StartCoroutine(esitoVai());
        if(libro!=null){
            libro.enabled = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        if(bot!=null){
            bot.Visit = false;
        }
        timer.Stop = true;
    }

    private IEnumerator esitoVai(){
        if(this.esito == paziente.malato){
            result.text = "Complimenti, hai correttamente diagnosticato il paziente";
            result.color = Color.green;
            MainCamera.GetComponent<Camera>().enabled = true;
            MainCamera.GetComponent<rotation>().enabled = true;
            MainCamera.transform.parent.gameObject.GetComponent<Movement>().enabled = true;
        }else{
            result.text = "Peccato, hai sbagliato a diagnosticare la retinopatia, controlla meglio con il prossimo";
            result.color = Color.red;
            MainCamera.GetComponent<Camera>().enabled = true;
            MainCamera.GetComponent<rotation>().enabled = true;
            MainCamera.transform.parent.gameObject.GetComponent<Movement>().enabled = true;
        }
        yield return new WaitForSeconds(5);
        result.text = "";
    }
}
