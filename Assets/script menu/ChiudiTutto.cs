using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEditor;

public class ChiudiTutto : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData pointerEventData){
    gameObject.GetComponent<TextMeshProUGUI>().color=Color.green;
   }

   public void OnPointerExit(PointerEventData pointerEventData){
     gameObject.GetComponent<TextMeshProUGUI>().color=Color.white;
   }

    public void OnPointerClick(PointerEventData pointerEventData){
        Application.Quit();
    }


}
