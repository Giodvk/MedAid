using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon.StructWrapping;
using CameraDoorScript;
using TMPro;
using UnityEngine.UI;
public class StartTheGame : MonoBehaviourPunCallbacks
{
    public GameObject[] playerPrefab;  // The player prefab that you’ll instantiate
    public Transform[] spawnPoints;  // Predefined spawn points for the players

    public ConsigliAlMedico assistant;

    public DecidiEsito book;
    public VisitaOculistica visit;


    public decisione decide;

    public GameObject porta;

    public InterrompiPartita menu;

    public GameObject InteractPaziente;
    public Image handIcon;
    public TextMeshProUGUI interaction;

    public BotInteraction botInteraction;

    public GameObject micAttivo;

    public GameObject micSpento;

    public Startgame presentazione;

    void Start()
    {
        if (PhotonNetwork.IsConnected)
        {
            int randomindex = Random.Range(0, playerPrefab.Length);
            // Spawn player characters for all connected players
            SpawnPlayers(randomindex);
        }
    }

    void SpawnPlayers(int index)
{
    if (PhotonNetwork.LocalPlayer != null)
    {
        Vector3 spawnPosition = spawnPoints[PhotonNetwork.LocalPlayer.ActorNumber % spawnPoints.Length].position;
        GameObject character = PhotonNetwork.Instantiate(playerPrefab[index].name, spawnPosition, Quaternion.Euler(0,-90,0));
        
        SetForPlayer(character);
    }
}



    void SetForPlayer(GameObject character){
        if(character.GetPhotonView().IsMine){
            GameObject camera = character.transform.GetChild(2).gameObject;
            presentazione.player = character;
            assistant.playerCamera = camera.GetComponent<Camera>();
            book.MainCamera = camera;
            visit.MainCamera = camera.GetComponent<Camera>();
            decisione.MainCamera = camera;
            menu.Player = camera;
            camera.GetComponent<CameraOpenDoor>().text = porta;
            botInteraction.playerCamera = camera.GetComponent<Camera>();
            botInteraction.enabled = true;
            assistant.enabled = true;
            book.enabled = true;
            character.GetComponent<activeVoiceChat>().micAcceso = micAttivo;
            character.GetComponent<activeVoiceChat>().micSpento = micSpento;

            // Invia un RPC agli altri giocatori
            StartCoroutine(WaitAndSendRPC(character.GetPhotonView(), character.GetPhotonView().ViewID));
        }

    }


    IEnumerator WaitAndSendRPC(PhotonView photonView, int viewID)
    {
        // Attendi fino a quando il ViewID non è valido
        while (viewID == 0)
        {
            yield return null; // Aspetta un frame
        }

        // Invia l'RPC solo quando il ViewID è valido
        photonView.RPC("SyncPlayerSetup", RpcTarget.OthersBuffered, viewID);
    }


     
}
