using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class AggiornaLobby : MonoBehaviour
{
    private Player [] players;
    public TextMeshProUGUI [] nickname;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        players = PhotonNetwork.PlayerList;
        for(int i=0;i<players.Length;i++){
            nickname[i].text = players[i].NickName;
        }
        transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().text = PhotonNetwork.CurrentRoom.PlayerCount.ToString();
    }
}
