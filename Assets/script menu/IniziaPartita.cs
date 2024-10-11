using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine.UI;

public class IniziaPartita : MonoBehaviourPunCallbacks
{
  void Awake (){
    gameObject.GetComponent<Button>().onClick.AddListener(StartGame);
  }

  void StartGame(){
    PhotonNetwork.LoadLevel("Caricamento");
  }
}
