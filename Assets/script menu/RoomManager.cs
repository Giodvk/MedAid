
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;



public class RoomManager : MonoBehaviourPunCallbacks
{
    public TextMeshProUGUI roomCodeDisplay;  // Text per visualizzare il codice stanza
    private string roomCode;
    public GameObject lobby;

    private TMP_InputField nickname;
    private Button button;

    public TextMeshProUGUI numPlayer;
    void Awake()
    {
        nickname = transform.GetChild(0).gameObject.GetComponent<TMP_InputField>();
        button = transform.GetChild(1).gameObject.GetComponent<Button>();
        button.onClick.AddListener(CreateRoom);
    }
    
      public void CreateRoom()
    {
        roomCode = GenerateRoomCode(6);  // Genera un codice di 6 caratteri
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 3;  // Imposta il limite massimo di giocatori

        PhotonNetwork.CreateRoom(roomCode, roomOptions);  // Crea la stanza con il codice
        roomCodeDisplay.text = roomCode;  // Mostra il codice stanza al creatore
       
    }

    // Genera un codice stanza alfanumerico casuale
    private string GenerateRoomCode(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        char[] stringChars = new char[length];
        for (int i = 0; i < length; i++)
        {
            stringChars[i] = chars[Random.Range(0, chars.Length)];
        }
        return new string(stringChars);
    }

    // Chiamato quando si entra nella stanza
    public override void OnJoinedRoom()
    {
        lobby.SetActive(true);
        // Qui puoi caricare la scena di gioco o far partire la logica di gioco
        numPlayer.text = PhotonNetwork.CurrentRoom.PlayerCount.ToString();
        gameObject.SetActive(false);
        PhotonNetwork.PlayerList[0].NickName = nickname.text;
    }

    // Se non riesci a creare la stanza (es. codice già usato), rigenera un nuovo codice
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.LogWarning("Creazione stanza fallita: " + message);
        CreateRoom();  // Prova a creare di nuovo la stanza con un altro codice
    }

}