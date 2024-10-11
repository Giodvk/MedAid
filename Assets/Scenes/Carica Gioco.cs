using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CaricaGioco : MonoBehaviour
{
  public TextMeshProUGUI premi;
  private const byte SCENE_SYNC_EVENT = 1;
  void Start()
    {
        premi.enabled = false;
        PhotonNetwork.NetworkingClient.EventReceived += OnEvent;
        StartCoroutine(LoadSceneAsync());
    }

    void OnDestroy()
    {
        // Disregistra l'handler dell'evento quando l'oggetto viene distrutto
        PhotonNetwork.NetworkingClient.EventReceived -= OnEvent;
    }

    IEnumerator LoadSceneAsync()
    {
        // Avvia il caricamento asincrono della scena
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("MainScene");

        // Impedisci alla scena di caricarsi automaticamente
        asyncLoad.allowSceneActivation = false;

        // Continua a visualizzare l'animazione finché la scena non è completamente caricata
        while (!asyncLoad.isDone)
        {
            // Se la scena è quasi pronta, puoi eventualmente fare qualcosa qui
            if (asyncLoad.progress >= 0.9f)
            {
                premi.enabled = true;
                // Ad esempio, potresti mostrare un messaggio "Premi un tasto per continuare"
                // Aspetta che l'utente prema un tasto, se vuoi controllare manualmente quando attivare la scena
                if (Input.anyKeyDown && PhotonNetwork.IsMasterClient)
                {
                    // Attiva la scena caricata
                    asyncLoad.allowSceneActivation = true;
                    PhotonNetwork.RaiseEvent(SCENE_SYNC_EVENT, null, new RaiseEventOptions { Receivers = ReceiverGroup.All }, SendOptions.SendReliable);
                }
            }

            // Continua l'aggiornamento del frame e delle animazioni
            yield return null;
        }
    }
    private void OnEvent(EventData photonEvent)
    {
        // Controlla se l'evento è quello di sincronizzazione della scena
        if (photonEvent.Code == SCENE_SYNC_EVENT)
        {
            // Attiva la scena caricata
            SceneManager.LoadScene("MainScene");
        }
    }

}
