using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon.StructWrapping;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChangeMicrophone : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public TextMeshProUGUI attendi;
    public GameObject prefabGiocatore;
    // Start is called before the first frame update
    void Start()
    {
        attendi.enabled = false;
    }

    public void OnPointerEnter(PointerEventData pointerEventData){
        Color color = gameObject.GetComponent<Image>().color;
        color = Color.green;
        color.a = 0.25F;
        gameObject.GetComponent<Image>().color = color;
    }

    public void OnPointerExit(PointerEventData pointerEventData){
        Color color=gameObject.GetComponent<Image>().color;
        color = Color.white;
        color.a = 0.25F;
        gameObject.GetComponent<Image>().color = color;
    }
    
    public void OnPointerClick(PointerEventData pointerEventData){
        attendi.enabled = true;
        StartCoroutine(WaitForSomething());
        
    }

    private IEnumerator WaitForSomething(){
        yield return new WaitUntil(() => Input.anyKeyDown);
        // Ora controlliamo quale tasto è stato premuto
        foreach (KeyCode key in System.Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(key))
            {
                prefabGiocatore.GetComponent<activeVoiceChat>().key = key;
                transform.parent.gameObject.GetComponent<TextMeshProUGUI>().text = key.ToString();
                attendi.enabled = false;
                break;
            }
        }
    }
}
