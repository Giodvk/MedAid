using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Movement : MonoBehaviourPun, IPunObservable
{
    public int MovementSpeed = 5;
    [HideInInspector]
    public bool Walking = false;
    private Rigidbody rb;
    private Animator animator;

    private bool back = false;

    private Vector3 latestPosition;
    private Quaternion latestRotation;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        animator.applyRootMotion = false; // Disabilita il root motion
    }

    

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 movement = Vector3.zero;
        Walking = false;
        back = false;
        if(!photonView.IsMine){
            return;
        }
        
       if(Input.GetKey(KeyCode.W))
        {
            movement += transform.forward;
            Walking = true;
        }
        if (Input.GetKey(KeyCode.S))
        {
            movement += -transform.forward;
            back = true;
        }
        if (Input.GetKey(KeyCode.A))
        {
            movement += -transform.right;
        }
        if (Input.GetKey(KeyCode.D))
        {
            movement += transform.right;
        }
       

        movement = movement.normalized;
        Vector3 newPosition = rb.position + movement * MovementSpeed * Time.fixedDeltaTime;
        rb.MovePosition(newPosition); // Muovi il personaggio
        animator.SetBool("isWalking", Walking);
        animator.SetBool("walkingBack",back);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // Se è il client locale, invia la posizione e rotazione del Rigidbody
            stream.SendNext(rb.position);
            stream.SendNext(rb.rotation);
        }
        else
        {
            // Se è un client remoto, ricevi la posizione e rotazione
            latestPosition = (Vector3)stream.ReceiveNext();
            latestRotation = (Quaternion)stream.ReceiveNext();
        }
    }

    
}
