using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using Photon.Realtime;
using TMPro;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class InterrompiPartita : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    private GameObject UIgioco;
    private GameObject menu;
    private bool isOpen = false;
    private GameObject player;

    public GameObject Player{
        get{ return this.player;}

        set{ this.player = value;}
    }
    private CursorLockMode original;
    private bool canRotate;
    // Start is called before the first frame update
    void Start()
    {
        
        UIgioco = GameObject.FindGameObjectWithTag("UIGioco");
        menu = GameObject.FindGameObjectWithTag("Menu");
        menu.SetActive(false);
        gameObject.GetComponent<TextMeshProUGUI>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !isOpen){
            openMenu();
            UIgioco.SetActive(false);
        }else if(Input.GetKeyDown(KeyCode.Escape) && isOpen){
           closeMenu();
           UIgioco.SetActive(true);
        }
    }

    public void openMenu(){
        Debug.Log("Menu aperto");
            menu.SetActive(true);
            isOpen = true;
            Cursor.visible = true;
            original = Cursor.lockState;
            Cursor.lockState = CursorLockMode.None;
            gameObject.GetComponent<TextMeshProUGUI>().enabled = true;
            canRotate = player.GetComponent<rotation>().enabled;
            Time.timeScale = 0f;
    }

    public void closeMenu(){
            menu.SetActive(false);
            Debug.Log("Menu chiuso");
            isOpen = false;
            Time.timeScale = 1f;
            gameObject.GetComponent<TextMeshProUGUI>().enabled = false;
            if(original!=CursorLockMode.None){

                Cursor.lockState = CursorLockMode.Locked;

            }
            if(canRotate){
                Debug.Log("Si muove");
                player.GetComponent<rotation>().enabled = true;

            }
    }

    public void OnPointerClick(PointerEventData pointerEventData){
        closeMenu();
        UIgioco.SetActive(true);
    }

    public void OnPointerEnter(PointerEventData pointerEventData){
        gameObject.GetComponent<TextMeshProUGUI>().color = Color.green;
    }

    public void OnPointerExit(PointerEventData pointerEventData){
        gameObject.GetComponent<TextMeshProUGUI>().color = Color.white;
    }
}


