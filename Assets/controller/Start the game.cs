using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Startgame : MonoBehaviour
{
    public GameObject player=null;
    private GameObject main;

    public GameObject Presentation;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitForObjectNotNull());
    }

    // Update is called once per frame
    void Update()
    {
        if(!Presentation.activeSelf){
            player.GetComponent<Movement>().enabled = true;
           
            gameObject.SetActive(false);
        }
    }

    IEnumerator WaitForObjectNotNull()
    {
        // Continua ad aspettare finché targetObject è null
        while (player == null)
        {
            // Stampa il log o fai qualsiasi cosa qui se vuoi monitorare lo stato
            Debug.Log("Aspettando che targetObject non sia più null...");
            
            // Aspetta fino al prossimo frame
            yield return null;
        }

        // Quando targetObject non è più null, esegui il codice successivo
        Debug.Log("targetObject non è più null, continua con l'esecuzione!");

        player = GameObject.FindGameObjectWithTag("Player");
        main = GameObject.FindGameObjectWithTag("MainCamera");
        player.GetComponent<Movement>().enabled = false;
        
        Presentation.SetActive(true);
    }
}
