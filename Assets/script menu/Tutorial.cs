using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tutorial : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    private GameObject tutorial;
    public void Start(){
        tutorial = GameObject.FindGameObjectWithTag("Tutorial");
        tutorial.SetActive(false);
    }
   public void OnPointerEnter(PointerEventData pointerEventData){
    gameObject.GetComponent<TextMeshProUGUI>().color=Color.green;
   }

   public void OnPointerExit(PointerEventData pointerEventData){
     gameObject.GetComponent<TextMeshProUGUI>().color=Color.white;
   }

    public void OnPointerClick(PointerEventData pointerEventData){
        GameObject.FindGameObjectWithTag("Menu").SetActive(false);
        tutorial.SetActive(true);
        gameObject.GetComponent<TextMeshProUGUI>().color=Color.white;
        }

}
