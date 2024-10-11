using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MenuPrincipale : MonoBehaviourPunCallbacks, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     public void OnPointerEnter(PointerEventData pointerEventData){
        gameObject.GetComponent<TextMeshProUGUI>().color = Color.green;
    }

    public void OnPointerExit(PointerEventData pointerEventData){
        gameObject.GetComponent<TextMeshProUGUI>().color = Color.white;
    }

    public void OnPointerClick(PointerEventData pointerEventData){
        if(PhotonNetwork.IsMasterClient){
            // Chiudi la stanza e disconnetti tutti i giocatori
            PhotonNetwork.CurrentRoom.IsOpen = false;
            PhotonNetwork.CurrentRoom.IsVisible = false;
             Debug.Log(PhotonNetwork.InRoom);
            PhotonNetwork.LeaveRoom();  // Disconnette tutti i giocatori dalla stanza
        }
        else
        {
            // Se non è il Master Client, disconnetti solo il giocatore corrente
            PhotonNetwork.LeaveRoom();
        }
        }

        // Funzione chiamata quando il giocatore ha lasciato la stanza
    public override void OnLeftRoom()
    {
       
        // Una volta disconnesso, torna alla scena del menu
        PhotonNetwork.Disconnect();
    }

    // Funzione chiamata quando il giocatore è stato disconnesso da Photon
    public override void OnDisconnected(DisconnectCause cause)
    {
       
        // Carica la scena del menu dopo la disconnessione
        SceneManager.LoadScene("MenuPrincipale");
    }

    }
