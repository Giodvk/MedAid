using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Impostazioni : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    private GameObject menuImp;
    public GameObject riprendi;

    private GameObject principale;
    public void Awake(){
        menuImp = GameObject.FindGameObjectWithTag("imp");
        menuImp.SetActive(false);
        principale =  GameObject.FindGameObjectWithTag("Menu");
    }

   public void OnPointerEnter(PointerEventData pointerEventData){

    gameObject.GetComponent<TextMeshProUGUI>().color = Color.green;

   }

   public void OnPointerExit(PointerEventData pointerEventData){

    gameObject.GetComponent<TextMeshProUGUI>().color = Color.white;

   }

   public void OnPointerClick(PointerEventData pointerEventData){
    principale.SetActive(false);
    menuImp.SetActive(true);
    gameObject.GetComponent<TextMeshProUGUI>().color = Color.white;
    if(riprendi!=null){
        riprendi.SetActive(false);
    }
   }
}
