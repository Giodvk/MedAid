using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CameraDoorScript
{
public class CameraOpenDoor : MonoBehaviour {
	public float DistanceOpen=3;
	public GameObject text;
	private TextMeshProUGUI interazione;
	// Use this for initialization
	void Start () {
		if(text)
		interazione=text.GetComponent<TextMeshProUGUI>();
	}
	
	// Update is called once per frame
	void Update () {
		RaycastHit hit;
		if (Physics.Raycast (transform.position, transform.forward, out hit, DistanceOpen) && interazione) {
				if (hit.transform.GetComponent<DoorScript.Door> ()) {
				interazione.enabled = true;
				if (Input.GetKeyDown(KeyCode.E)){
					hit.transform.GetComponent<DoorScript.Door> ().OpenDoor();
				if(interazione.text=="Aprire"){
					interazione.text="Chiudere";
				}else{
					interazione.text="Aprire";
				}
				}
			}else{
				interazione.enabled = false;
			}
		}else if(interazione){
			interazione.enabled = false;
		}
	}
}
}
