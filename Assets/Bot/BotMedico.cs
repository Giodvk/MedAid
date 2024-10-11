using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotMedico : MonoBehaviour, Bot
{
    private Dictionary<string, string> risposte= new Dictionary<string, string>()
    {
        {"Mi sai dire quali sono i sintomi della retinopatia?", "Certo, i sintomi della retinopatia diabetica sono: difficoltà nella percezione dei colori, ipovisione, vista offuscata, macchie o fili scuri che galleggiano davanti agli occhi (miodesopsie) e nel caso peggio cecità"},
        {"Questo paziente ha avuto episodi di offuscamento visivo, è positivo?", "Non è detto la persona potrebbe soffrire di miopia, astigmatismo e cataratta per esempio, controlla bene la membrana degli occhi e poni altre domande, non limitarti alla vista."},
        {"Risulta saggio chiedere al paziente se è daltonico?", "Certo, se il paziente ha dei problemi a percepire dei colori ed è stata diagnostica come non daltonica potrebbe essere soggetto alla retinopatia, quindi sì è una domanda utile"}
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
