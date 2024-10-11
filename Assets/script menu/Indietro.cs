using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Indietro : MonoBehaviour,IPointerClickHandler
{
    public GameObject menu;
    public GameObject riprendi;
    // Start is called before the first frame update
   

   public void OnPointerClick(PointerEventData pointerEventData){
        GameObject.FindGameObjectWithTag(transform.parent.tag).SetActive(false);
        menu.SetActive(true);
        if(riprendi){
            riprendi.SetActive(true);
        }
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.Escape)){
            OnPointerClick(null);
          }
        }
    }
