using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CercaLobby : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
   public GameObject code;

   void Start(){
    code.SetActive(false);
   }
   public void OnPointerExit(PointerEventData pointerEventData){
    gameObject.GetComponent<TextMeshProUGUI>().color=Color.white;
   }

   public void OnPointerEnter(PointerEventData pointerEventData){
    gameObject.GetComponent<TextMeshProUGUI>().color=Color.green;
   }

   public void OnPointerClick(PointerEventData pointerEventData){
     gameObject.GetComponent<TextMeshProUGUI>().color=Color.white;
     code.SetActive(true);
     transform.parent.gameObject.SetActive(false);
   }
}
