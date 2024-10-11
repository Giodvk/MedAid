using System.Collections;
using System.Collections.Generic;
using Photon.Pun; // Importa Photon
using UnityEngine;

public class spegniLuceStudio : MonoBehaviourPun
{
    private GameObject[] luci;
    private Color emissionColor = Color.white;
    private float emissionIntesityAcceso = 1.0F;
    private bool acceso = true;

    void Start()
    {
        // Trova tutte le luci con il tag "luceStudio"
        luci = GameObject.FindGameObjectsWithTag("luceStudio");
    }

    // Questo metodo viene chiamato quando il giocatore clicca sull'oggetto
    public void OnMouseDown()
    {
        // Chiamata RPC per accendere/spegnere le luci su tutti i client
        photonView.RPC("ToggleLights", RpcTarget.All);
    }

    // Metodo RPC che viene eseguito su tutti i client
    [PunRPC]
    void ToggleLights()
    {
        // Cambia lo stato di acceso/spento
        if (acceso)
        {
            transform.parent.Rotate(Vector3.up, 50);
            acceso = false;
        }
        else
        {
            transform.parent.Rotate(Vector3.up, -50);
            acceso = true;
        }

        // Per ogni luce trovata, aggiorna lo stato (accende o spegne)
        foreach (GameObject go in luci)
        {
            Renderer[] renderers = go.GetComponentsInChildren<Renderer>();
            Light light = go.GetComponentInChildren<Light>();

            if (renderers[1].material.IsKeywordEnabled("_EMISSION"))
            {
                // Spegni la luce
                renderers[1].material.DisableKeyword("_EMISSION");
                light.intensity = 0;
            }
            else
            {
                // Accendi la luce
                renderers[1].material.EnableKeyword("_EMISSION");
                Color accesoColor = emissionColor * emissionIntesityAcceso;
                renderers[1].material.SetColor("_EmissionColor", accesoColor);
                light.intensity = 2;
            }
        }
    }
}