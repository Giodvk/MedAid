using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon.StructWrapping;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SearchCode : MonoBehaviourPunCallbacks 
{
    private TMP_InputField nick;

    public TextMeshProUGUI numPlayer;

    public TextMeshProUGUI [] players;

    public GameObject lobby;
    void Awake(){
        Button button = transform.GetChild(2).gameObject.GetComponent<Button>();
        button.onClick.AddListener(SearchButton);
        nick = transform.GetChild(3).gameObject.GetComponent<TMP_InputField>();
        
    }

    public void JoinRoomWithCode()
    {
        string enteredRoomCode = gameObject.GetComponent<TMP_InputField>().text;  // Prendi il codice inserito dall'utente
        string nickname = nick.text;
        if (!string.IsNullOrEmpty(enteredRoomCode) && !string.IsNullOrEmpty(nickname))
        {
            Debug.Log(enteredRoomCode);
            PhotonNetwork.JoinRoom(enteredRoomCode);  // Prova ad unirsi alla stanza con quel codice
        }
        else
        {
            Debug.LogWarning("Inserisci un codice stanza valido.");
        }
    }

    void Update(){
        if(gameObject.activeSelf && Input.GetKeyDown(KeyCode.Return)){
            Debug.Log("codice preso");
            JoinRoomWithCode();
        }
    }
     public override void OnJoinedRoom()
    {
        Debug.Log("Entrato nella lobby di Photon.");
        transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = "";
        numPlayer.text = PhotonNetwork.CurrentRoom.PlayerCount.ToString();
        lobby.SetActive(true);
        int index = PhotonNetwork.CurrentRoom.PlayerCount - 1;
        PhotonNetwork.PlayerList[index].NickName = nick.text;
        gameObject.SetActive(false);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("Connessione fallita. Codice: " + returnCode + " Messaggio: " + message);
        transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = "Codice errato";
    }


    public void SearchButton(){
        JoinRoomWithCode();
    }
}
