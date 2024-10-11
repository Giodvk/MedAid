using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class CancellaFoto : MonoBehaviour
{
    private screenshot screen;
    // Start is called before the first frame update
    void Start()
    {
        screen = GameObject.FindGameObjectWithTag("fotocamera").GetComponent<screenshot>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnMouseDown(){
        Debug.Log("Cancello");
        if(screen!=null){
        screen.num_photo = 0;
        screen.bacheca = new Vector3(6.516F,4.135F,12.837F);
        GameObject [] foto = GameObject.FindGameObjectsWithTag("foto");
        foreach(GameObject go in foto){
            Destroy(go);
        }
    }
    }
}
