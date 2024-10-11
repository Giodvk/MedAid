using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConsigliAlMedico : MonoBehaviour
{
    public Camera playerCamera;
    public float interactionDistance = 1f;
    public Image handIcon;
    public TextMeshProUGUI interactionText;
    public GameObject questionPanel;
    private rotation mouseLook;
    private bool isOpen=false;

    private screenshot foto;

    private void Start()
    {
        handIcon.enabled = false;
        interactionText.enabled = false;
        questionPanel.SetActive(false);
        mouseLook=playerCamera.GetComponent<rotation>();
        foto = GameObject.FindGameObjectWithTag("fotocamera").GetComponent<screenshot>();
        if(mouseLook == null){
            Debug.LogError("MouseLook script is not found on the player camera.");
        }
    }

    void Update()
    {
        
        if(isOpen){
            mouseLook.enabled=false;

            if(Input.GetKey(KeyCode.Backspace)){
                CloseMenu();
                foto.enabled = true;
            }
        }else{
            Ray ray=playerCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            
            if(Physics.Raycast(ray,out hit,interactionDistance)){
                if(hit.collider.CompareTag("BotMedico")){
                    handIcon.enabled = true;
                    interactionText.enabled = true;
                    if(Input.GetKey(KeyCode.E)){
                    OpenMenu();
                    foto.enabled = false;
                    }
                }else if(hit.collider.CompareTag("Bot")){
                    return;
                }else{
                    handIcon.enabled = false;
                    interactionText.enabled = false;
                }
            }else{
                handIcon.enabled = false;
                interactionText.enabled = false;
            }
        }
    }

    void OpenMenu(){
        questionPanel.SetActive(true);
        isOpen = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible=true;
        handIcon.enabled = false;
        interactionText.enabled = false;
    }

    void CloseMenu(){
        questionPanel.SetActive(false);
        isOpen = false;
        Cursor.lockState = CursorLockMode.Locked;
        mouseLook.enabled=true;
    }
}
