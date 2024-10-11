using System.Collections;
using System.Collections.Generic;
using CameraDoorScript;
using Photon.Pun;
using UnityEngine;

public class SetUpPlayer : MonoBehaviour
{
    private GameObject textPorta;
    private GameObject micAttivo;
    private GameObject micSpento;
    // Start is called before the first frame update
    void Start()
    {
        textPorta = GameObject.FindGameObjectWithTag("door");
        micAttivo = GameObject.FindGameObjectWithTag("micAcceso");
        micSpento = GameObject.FindGameObjectWithTag("micSpento");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [PunRPC]
    void SyncPlayerSetup(int viewID){


    PhotonView photonView = PhotonView.Find(viewID);
    if (photonView == null)
    {
        Debug.LogError("PhotonView with viewID: " + viewID + " not found!");
        return;
    }

    GameObject character = photonView.gameObject;

    if (character != null)
    {
        GameObject camera = character.transform.GetChild(2).gameObject;
        camera.GetComponent<CameraOpenDoor>().text = textPorta;
        camera.SetActive(false);
    }
        character.GetComponent<activeVoiceChat>().micAcceso = micAttivo;
        character.GetComponent<activeVoiceChat>().micSpento = micSpento;
    }
}
