using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using SojaExiles;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BotInteraction : MonoBehaviour
{
    public Camera playerCamera;
    public float interactionDistance = 1f;
    public Image handIcon;
    public TextMeshProUGUI interactionText;
    public GameObject questionPanel;
    private rotation mouseLook;
    private bool isOpen = false;

    private GameObject questionOriginal;
    private screenshot foto;

    private void Start()
    {

            if (playerCamera == null)
            {
                Debug.LogError("Camera non trovata nel giocatore locale.");
                return;
            }

            // Inizializza il resto solo se è il giocatore locale
            questionOriginal = questionPanel;
            handIcon.enabled = false;
            interactionText.enabled = false;
            questionPanel.SetActive(false);
            mouseLook = playerCamera.GetComponent<rotation>();
            foto = GameObject.FindGameObjectWithTag("fotocamera").GetComponent<screenshot>();

            if (mouseLook == null)
            {
                Debug.LogError("MouseLook script non trovato sulla camera del giocatore.");
            }
    }

    void Update()
    {
        if (isOpen)
        {
            if (Input.GetKey(KeyCode.Backspace))
            {
                CloseMenu();
                foto.enabled = true;
            }
        }
        if(!foto.Grabbed)
        {
            Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, interactionDistance))
            {
                if (hit.collider.CompareTag("Bot"))
                {
                    handIcon.enabled = true;
                    interactionText.enabled = true;
                    if (Input.GetKey(KeyCode.E))
                    {
                        OpenMenu();
                        foto.enabled = false;
                    }
                }
                else
                {
                    handIcon.enabled = false;
                    interactionText.enabled = false;
                }
            }
            else
            {
                handIcon.enabled = false;
                interactionText.enabled = false;
            }
        }
    }

    void OpenMenu()
    {
        questionPanel.SetActive(true);
        isOpen = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        handIcon.enabled = false;
        interactionText.enabled = false;
        mouseLook.enabled = false;
    }

    void CloseMenu()
    {
        questionPanel.SetActive(false);
        isOpen = false;
        Cursor.lockState = CursorLockMode.Locked;
        mouseLook.enabled = true;
        this.questionPanel = questionOriginal;
    }
}