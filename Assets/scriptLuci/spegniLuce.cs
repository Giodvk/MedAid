using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class spegniLuce : MonoBehaviour
{
    private GameObject[] luci;
    private Color emissionColor=Color.white;
    private float emissionIntesityAcceso=1.0F;
    private bool acceso=true;
    // Start is called before the first frame update
    void Start()
    {
        luci=GameObject.FindGameObjectsWithTag("luceAttesa");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnMouseDown(){
        if(acceso){
            transform.parent.Rotate(Vector3.up,50);
            acceso=false;
        }else{
             transform.parent.Rotate(Vector3.up,-50);
             acceso=true;
        }
        foreach(GameObject go in luci){
            Renderer[] render=go.GetComponentsInChildren<Renderer>();
            Light light=go.GetComponentInChildren<Light>();
            if(render[1].material.IsKeywordEnabled("_EMISSION")){
                render[1].material.DisableKeyword("_EMISSION");
                light.intensity=0;
            }else{
                render[1].material.EnableKeyword("_EMISSION");
                Color acceso=emissionColor*emissionIntesityAcceso;
                render[1].material.SetColor("_EmissionColor", acceso);
                light.intensity=2;
            }
        }
    }
}
