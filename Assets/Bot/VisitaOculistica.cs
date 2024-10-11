
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class VisitaOculistica : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{   
   
    public Camera MainCamera;
    public Camera VisitCamera;

    public GameObject UIGioco;
    // Start is called before the first frame update
    void Start()
    {
        VisitCamera.enabled = false;
        
    }

   void Update(){
    if(VisitCamera.enabled && Input.GetKeyDown(KeyCode.E)){
        VisitCamera.enabled = false;
        MainCamera.enabled = true;
        UIGioco.SetActive(true);
       
    }
   }

    public void OnPointerEnter(PointerEventData pointerEventData){
        gameObject.GetComponent<TextMeshProUGUI>().color = Color.red;
    }

    public void OnPointerExit(PointerEventData pointerEventData){
        gameObject.GetComponent<TextMeshProUGUI>().color = Color.white;
    }

    public void OnPointerClick(PointerEventData pointerEventData){
        MainCamera.enabled = false;
        VisitCamera.enabled = true;
        UIGioco.SetActive(false);
        gameObject.GetComponent<TextMeshProUGUI>().color = Color.white;
    }
}