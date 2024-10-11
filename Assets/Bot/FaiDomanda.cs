using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class FaiDomanda : MonoBehaviour,IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public GameObject Paneldomande;
    public GameObject risposta;
    public GameObject paziente;
    private Bot domande;
    private bool isSelected=false;
    private static bool isTalking=false;
    private GameObject heart;
    // Start is called before the first frame update
    void Start()
    {
        domande=paziente.GetComponent<Bot>();
        risposta.SetActive(false);
        heart = risposta.transform.GetChild(1).gameObject;
        heart.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKeyDown && !isTalking){
            risposta.SetActive(false);
            heart.SetActive(false);
        }
    }

    public void OnPointerEnter(PointerEventData pointerEventData){
        isSelected=true;
        gameObject.GetComponent<TextMeshProUGUI>().color=Color.red;
    }

    public void OnPointerExit(PointerEventData pointerEventData){
        isSelected=false;
        gameObject.GetComponent<TextMeshProUGUI>().color=Color.white;
    }

    public void OnPointerClick(PointerEventData pointerEventData){
        if(isSelected && !isTalking){
            isTalking=true;
            if(paziente){
                risposta.SetActive(true);
                if( paziente.GetComponent<BotInteraction>()){
                paziente.GetComponent<BotInteraction>().enabled = false;
                }else if(paziente.GetComponent<ConsigliAlMedico>()){
                    paziente.GetComponent<ConsigliAlMedico>().enabled = false;
                }
                String answer = gameObject.GetComponent<TextMeshProUGUI>().text;
                 StartCoroutine(WriteAnswer(answer));
            }else{
                Debug.LogError("Paziente non assegnato");
            }
        }
    }

    public IEnumerator WriteAnswer(string question){
        string answer=domande.GetRisposta(question);
        TextMeshProUGUI pezzo=risposta.GetComponentInChildren<TextMeshProUGUI>();
        pezzo.text = "";
        foreach(char c in answer){
            pezzo.text+=c;
            yield return new WaitForSeconds(0.05f);
        }
        isTalking=false;
        heart.SetActive(true);
         if( paziente.GetComponent<BotInteraction>()){
                paziente.GetComponent<BotInteraction>().enabled = true;
                }else if(paziente.GetComponent<ConsigliAlMedico>()){
                    paziente.GetComponent<ConsigliAlMedico>().enabled = true;
                }
}
}
