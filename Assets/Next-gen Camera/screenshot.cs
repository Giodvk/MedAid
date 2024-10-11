using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class screenshot : MonoBehaviour
{
    public GameObject prefabFoto;
    private GameObject player;
    [HideInInspector]
    public bool Grabbed=false;
    private screenshot instance;
    private Camera foto;

    private bool Take;
    private Vector3 offset=new Vector3(0,-0.2F,0.5F);
    private Quaternion rot;
    private Vector3 origin;
    [HideInInspector]
    public Vector3 bacheca;
    [HideInInspector]
    public int num_photo=0;

    private float maxFov = 90F;
    private float minFov = 15F;
    private int zoomSpeed = 10;

    public Camera visitare;
    public AudioSource source;
    public AudioClip clip;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        foto=gameObject.GetComponentInChildren<Camera>();
        player=GameObject.FindGameObjectWithTag("MainCamera");
        source.clip = clip;
        origin=transform.parent.position;
        rot=transform.parent.rotation;
        bacheca = new Vector3(6.516F,4.135F,12.837F);

    }

    private void TakeScreenshot(int height, int width){
        Take=true;
        source.Play();
        foto.targetTexture = RenderTexture.GetTemporary(width,height,16);
    }

    private void OnPostRender(){
        if(Take && num_photo<6){
            Take = false;
            RenderTexture renderTexture = foto.targetTexture;

            Texture2D renderResult = new Texture2D(renderTexture.width, renderTexture.height,TextureFormat.ARGB32, false);
            Rect rect= new Rect(0, 0, renderTexture.width, renderTexture.height);
            renderResult.ReadPixels(rect,0,0);
            renderResult.Apply();

            CreatePhoto(renderResult);

            Debug.Log("Screenshot fatto");
            RenderTexture.ReleaseTemporary(renderTexture);
            foto.targetTexture = null;
            foto.enabled = false; // Disabilita e riabilita la fotocamera per assicurarsi che non ci siano conflitti
            foto.enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Grabbed){
        float scrollWheel = Input.GetAxis("Mouse ScrollWheel");
        foto.fieldOfView-=scrollWheel*zoomSpeed;
        foto.fieldOfView = Math.Clamp(foto.fieldOfView,minFov,maxFov);
        foto.transform.GetChild(0).gameObject.GetComponent<Camera>().fieldOfView = foto.fieldOfView;
        }
        if(Input.GetKeyDown(KeyCode.Space) && Grabbed){
            Debug.Log("Faccio foto");
            TakeScreenshot(500,500);
            visitare.enabled = false;

        }
        if(Input.GetKeyDown(KeyCode.Q) && !Grabbed){
            grabCamera();
        }else if(Input.GetKeyDown(KeyCode.Q) && Grabbed)
        {
            Grabbed=false;
            transform.parent.position=origin;
            transform.parent.rotation=rot;
            transform.parent.parent=null;
        }
        }

    

    private void grabCamera(){
        Transform mainCameraTransform = player.transform;
        transform.parent.position = mainCameraTransform.position + mainCameraTransform.forward * offset.z + mainCameraTransform.right * offset.x + mainCameraTransform.up * offset.y;
        transform.parent.parent = player.transform;
        transform.parent.rotation=mainCameraTransform.rotation * Quaternion.Euler(0, 180, 0);
        Grabbed = true;
       
    }

    private void CreatePhoto(Texture2D foto){
        if(prefabFoto!=null){
            GameObject photo = Instantiate(prefabFoto,bacheca, Quaternion.identity);
            Renderer render = photo.transform.GetChild(1).GetComponent<Renderer>();
            photo.transform.Rotate(0,-87.734F,0);
            num_photo+=1;
            if(num_photo==3){
                bacheca.z = 12.837F;
                bacheca.y = 3.629F;
            }else if(num_photo<6){
                bacheca.z-=0.6F;
            }
            if(render!=null){
                render.material.mainTexture = foto;
            }else{
                Debug.LogError("Non è stato assegnato il materiale al prefab della foto");
            }
        }else{
            Debug.LogError("Prefab della foto non assegnato");
        }
    }
}


