using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Voice;
using Photon.Voice.Unity;
using Photon.Pun;


public class activeVoiceChat : MonoBehaviour
{
    private Recorder mic;
    public KeyCode key = KeyCode.V;

    public GameObject micAcceso;

    public GameObject micSpento;
    private PhotonView photonView;

    void Start(){
        mic = GetComponent<Recorder>();
        if(micSpento){
        micSpento.SetActive(false);
        }
        photonView = GetComponent<PhotonView>();
    }

    void Update(){
        
        if(Input.GetKeyDown(key)){
            ChangeMic();
        }
    }


    void ChangeMic(){
        if(mic==null){
            return;
        }
        if(mic.TransmitEnabled && photonView.IsMine){
            mic.TransmitEnabled = false;
            micSpento.SetActive(true);
            micAcceso.SetActive(false);
        }else if(!mic.TransmitEnabled && photonView.IsMine){
            mic.TransmitEnabled = true;
            micSpento.SetActive(false);
            micAcceso.SetActive(true);
        }
    }
}
