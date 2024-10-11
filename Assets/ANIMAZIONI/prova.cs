using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements.Experimental;

public class prova : MonoBehaviour
{
   public Transform[] waypoints; // Lista dei Waypoints
    private int currentWaypointIndex = 0; // Waypoint attuale
    private NavMeshAgent agent;

    private Animator animator;
    
    private bool isVisiting = true; // Stato di visita
    public bool Visit{
        get{return this.isVisiting;}

        set{this.isVisiting = value;}
    }

    public GameObject doorStudio;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!isVisiting) // Inizia a seguire il percorso se la visita è terminata
        {
            FollowPath();
           
        }
    }

    public void EndVisit()
    {
        isVisiting = false; // Cambia stato, il bot smette di visitare e inizia a seguire il percorso
    }

    void FollowPath()
    {   
        doorStudio.GetComponent<DoorScript.Door>().open = true;
        if (currentWaypointIndex < waypoints.Length) // Se ci sono ancora waypoints da raggiungere
        {
            animator.SetBool("walking",true);
            agent.SetDestination(waypoints[currentWaypointIndex].position);
            agent.isStopped = false;

            // Se il bot è vicino al waypoint, passa al successivo
            if (!agent.pathPending && agent.remainingDistance < 0.5f)
            {
                currentWaypointIndex++;
            }
        }
        else
        {
            // Azioni da eseguire quando il bot ha raggiunto la destinazione finale
            agent.isStopped = true; // Ferma il movimento
            animator.SetBool("walking",false);
            Debug.Log("Bot ha raggiunto la destinazione finale.");
            doorStudio.GetComponent<DoorScript.Door>().OpenDoor();
            Destroy(gameObject);
          
        }
    }
}
