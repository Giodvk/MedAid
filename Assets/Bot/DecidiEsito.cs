using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon.StructWrapping;
using Photon.Pun;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DecidiEsito : MonoBehaviour
{   
    public GameObject UIGioco;
    public GameObject MainCamera;
    private TextMeshProUGUI esito;
    public TextMeshProUGUI interact;
    public Image handImage;
    [HideInInspector]
    public BotInteraction paziente;
    private ConsigliAlMedico medico;
    public int DistanceInteract;
    public Camera finale;
    // Start is called before the first frame update
    void Start()
    {
        esito = GameObject.FindGameObjectWithTag("esito").GetComponent<TextMeshProUGUI>();
        medico = GameObject.FindGameObjectWithTag("BotMedico").GetComponent<ConsigliAlMedico>();
        paziente = GameObject.FindGameObjectWithTag("Bot").GetComponent<BotInteraction>();
        finale.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
    if(PhotonNetwork.IsMasterClient){
        if(finale.enabled){
            if(Input.GetKeyDown(KeyCode.E)){
                finale.enabled = false;
                Cursor.lockState = CursorLockMode.Locked;
                MainCamera.transform.parent.gameObject.GetComponent<Movement>().enabled = true;
                MainCamera.GetComponent<rotation>().enabled = true;
                UIGioco.SetActive(true);
            }
        }else{
            Debug.Log(paziente);
        RaycastHit hit;
        if(Physics.Raycast(MainCamera.transform.position, MainCamera.transform.forward, out hit, DistanceInteract) && paziente != null){
            
            if(hit.collider.CompareTag("libroEsito")){
                Debug.Log("Pronto per l'esito");
                paziente.enabled = false;
                Debug.Log("ci sono");
                medico.enabled = false;
                interact.enabled = true;
                handImage.enabled = true;
                if(Input.GetKeyDown(KeyCode.E)){
                    ChangeCamera();
                }
            }else{
                   paziente.enabled = true;
                   medico.enabled = true;
                   
            }
        }else{
            
            interact.enabled = false;
            handImage.enabled = false;
            
        }
    }
    }
    }
    private void ChangeCamera(){
        finale.enabled = true;
        Cursor.lockState = CursorLockMode.None;
        MainCamera.transform.parent.gameObject.GetComponent<Movement>().enabled = false;
        MainCamera.GetComponent<rotation>().enabled = false;
        interact.enabled = false;
        handImage.enabled = false;
    }

}
