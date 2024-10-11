using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using Photon.Voice.Unity;

public class NickManager : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{

    public TextMeshProUGUI title;

    public GameObject nickname;
    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        nickname.SetActive(false);
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    void Update(){

      if(!PhotonNetwork.IsConnected){
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.AutomaticallySyncScene = true;
      }
    }
   public void OnPointerClick(PointerEventData pointerEventData){
    gameObject.GetComponent<TextMeshProUGUI>().color=Color.white;
    title.enabled = false;
    nickname.SetActive(true);
    transform.parent.gameObject.SetActive(false);
   }

   public void OnPointerEnter(PointerEventData pointerEventData){
    gameObject.GetComponent<TextMeshProUGUI>().color=Color.green;
   }

   public void OnPointerExit(PointerEventData pointerEventData){
     gameObject.GetComponent<TextMeshProUGUI>().color=Color.white;
   }


}
