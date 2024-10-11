using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ApriDomande : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject domande;
    public GameObject paziente;
    // Start is called before the first frame update
    void Awake()
    {
        paziente = GameObject.FindGameObjectWithTag("Bot");
        domande.SetActive(false);
    }


    public void OnPointerEnter(PointerEventData pointerEventData){
        gameObject.GetComponent<TextMeshProUGUI>().color = Color.red;
    }

    public void OnPointerExit(PointerEventData pointerEventData){
        gameObject.GetComponent<TextMeshProUGUI>().color = Color.white;
    }

    public void OnPointerClick(PointerEventData pointerEventData){
        gameObject.GetComponent<TextMeshProUGUI>().color = Color.white;
        OpenDomande();
        paziente.GetComponent<BotInteraction>().questionPanel = domande;
    }

    public void OpenDomande(){
        domande.SetActive(true);
        transform.parent.gameObject.SetActive(false);
    }

}

