using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotPaziente : MonoBehaviour, Bot
{   
    [HideInInspector]
    public bool malato = false;
    private Dictionary<string, string> risposte= new Dictionary<string, string>()
    {
        {"Come stai?", "Sto bene, grazie!"},
        {"Le è stato mai diagnosticato il daltonismo?", "No, ho fatto varie visite oculistiche prima d'ora ma non mi è stato diagnosticato"},
        {"Riesce a vedere chiaramente il quadro dietro di me?", "Si, è un quadro che raffigura un albero e delle foglie rosse"},
        {"Buongiorno qual è il motivo della visita?", "Ultimamente ho avuto degli episodi dove perdevo la percezione visiva."}
    };
    // Start is called before the first frame update
    void Start()
    {
        
    }

    
    public string GetRisposta(string domanda){
        if(risposte.ContainsKey(domanda)){
            return risposte[domanda];
        }
        return "Non ti so rispondere";
    }

}
