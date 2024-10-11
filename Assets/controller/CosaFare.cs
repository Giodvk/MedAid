using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CosaFare : MonoBehaviour
{
    private String discorso="Benvenuto, mi presento, io sono Jarvis il tuo assistente e oggi ti aiuterò a diagnosticare la retinopatia diabetica."+
    " La retinopatia diabetica è una malattia che colpisce direttamente la vista di una persona, essa è causata direttamente dal diabete che viene"+
    " trascurato causando nel tempo delle alterazioni nei vasi sanguigni nella retina dell'occhio danneggiandoli. Il tuo compito sarà capire per ogni"+
    " paziente che visiterai se esso ne è affetto. Non ti preoccupare perchè ti farò una infarinatura di tutti i sintomi e potrai richiederli quando vorrai."+
    " I sintomi della retinopatia diabetica sono macchie o fili scuri che galleggiano davanti agli occhi, vista offuscata, difficoltà nel percepire i colori e" +
    " perdita dell'acutezza visiva. Ora che ti ho dato tutte le spiegazioni sta a te entrare in azione, per ogni paziente avrai 5 minuti per decidere l'esito dal libro, in qualsiasi momento non esitare a fare domande.";

    private bool isTalking = true;
    public TextMeshProUGUI testo;
    // Start is called before the first frame update
    public TimerController timer;
    void Start()
    {
        Debug.Log(discorso.Length);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKeyDown && !isTalking){
            timer.GoOn = true;
            gameObject.SetActive(false);
        }
    }

    void OnEnable(){
      StartCoroutine(Chiarisci());
    }


    public IEnumerator Chiarisci(){
    // Prima parte del discorso
    testo.text = "";

    // Scrittura progressiva del primo blocco di testo
    foreach(string s in DivideStringIntoParts(discorso, 9)){
        foreach(char c in s){
        testo.text += c;
        yield return new WaitForSeconds(0.05f);// Usa una velocità di scrittura più rapida
        }
        // Aspetta che venga premuto un tasto prima di passare alla seconda parte  
        yield return new WaitUntil(() => Input.anyKeyDown);
         // Pulizia del testo prima di mostrare quella nuova
        testo.text = "";
    }

    // Flag per indicare che il discorso è finito
    isTalking = false;
}



public static List<string> DivideStringIntoParts(string input, int parts)
    {
        List<string> result = new List<string>();
        
        // Lunghezza di ogni parte
        int partSize = input.Length / parts;
        int remaining = input.Length % parts;

        int startIndex = 0;
        
        for (int i = 0; i < parts; i++)
        {
            int currentPartSize = partSize;
            
            // Se c'è un resto, aggiungilo alla parte attuale
            if (remaining > 0)
            {
                currentPartSize++;
                remaining--;
            }

            // Prendi la sottostringa dalla stringa originale
            string part = input.Substring(startIndex, currentPartSize);
            result.Add(part);
            startIndex += currentPartSize;
        }

        return result;
    }
}
