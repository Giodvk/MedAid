using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using UnityEngine.AI;
using UnityEditor;
using System.Threading;

public class Restart : MonoBehaviour
{
    public GameObject paziente;
    public GameObject domandePaziente;
    public GameObject interact;
    public GameObject pallino1;
    public GameObject pallino2;
    private Vector3 origin;

    public GameObject visitare;
    
    public Canvas visit;

    public Transform [] waypoints;

    private int currentWaypointIndex = 0; // Waypoint attuale

    public GameObject doorStudio;
    private bool restart;

    public DecidiEsito libro;

    private GameObject bott;

    public Transform [] waypointsUscita;

    private GameObject patient;

    public TimerController timer;
    // Start is called before the first frame update
    void Start()
    {
        origin = new Vector3(2.8f, 1.94f, 5.53f);
    }

    // Update is called once per frame
    void Update()
    {
        if(GameObject.FindGameObjectWithTag("Bot")==null){
            if(paziente==null){
                paziente = Resources.Load<GameObject>("Assets/Bot/maleBot");
            }
            Instantiate(paziente,origin, Quaternion.identity);
            patient = GameObject.FindGameObjectWithTag("patient");
            bott =  GameObject.FindGameObjectWithTag("Bot");
            if(domandePaziente!=null){
                foreach(Transform child in domandePaziente.transform){
                    child.gameObject.GetComponent<FaiDomanda>().paziente = paziente;
                }
            }
            if(interact != null){
                interact.GetComponent<ApriDomande>().paziente = paziente;
            }
            if(pallino1 != null && pallino2 != null){
                pallino1.GetComponent<apriOcchi>().skinnedMeshRenderer = bott.GetComponent<SkinnedMeshRenderer>();
                pallino2.GetComponent<apriOcchi>().skinnedMeshRenderer = bott.GetComponent<SkinnedMeshRenderer>();
            }
            if( libro != null){
                libro.paziente = bott.GetComponent<BotInteraction>();
            }
            BotInteraction bot = bott.GetComponent<BotInteraction>();
            patient.GetComponent<prova>().waypoints = waypointsUscita;
            patient.GetComponent<prova>().doorStudio = doorStudio;
            if(visitare !=null && visit != null){
                visitare.GetComponent<VisitaOculistica>().VisitCamera = paziente.transform.GetChild(3).GetComponent<Camera>();
                visitare.GetComponent<VisitaOculistica>().VisitCamera.enabled = false;
                visit.worldCamera =visitare.GetComponent<VisitaOculistica>().VisitCamera;
            }
            restart = true;
            currentWaypointIndex = 0;
     }
     if(restart){
        print("parto");
        followPath(patient);
     }
    }

     private void followPath(GameObject bot)
    {   
        doorStudio.GetComponent<DoorScript.Door>().open = true;
        NavMeshAgent agent = bot.GetComponent<NavMeshAgent>();
        if(currentWaypointIndex < waypoints.Length) // Se ci sono ancora waypoints da raggiungere
        {
            bot.GetComponent<Animator>().SetBool("walking",true);
            agent.SetDestination(waypoints[currentWaypointIndex].position);
            agent.isStopped = false;
            // Se il bot è vicino al waypoint, passa al successivo
             if (!agent.pathPending && agent.remainingDistance < 0.5f)
            {
                currentWaypointIndex++;
            }
        }else{
            // Azioni da eseguire quando il bot ha raggiunto la destinazione finale
            agent.isStopped = true; // Ferma il movimento
            patient.GetComponent<Animator>().SetBool("walking",false);
            Debug.Log("Bot ha raggiunto la destinazione finale.");
            doorStudio.GetComponent<DoorScript.Door>().OpenDoor();
            restart = false;
            timer.GoOn = true;
            }
        }
        /*
        private void SetWay(Transform [] waypoints){
            Transform [] newWay = new Transform[4];
            int i = 0;
            for (i=0;i<3;i++){
                newWay[i] = waypoints[i];
            }
            paziente.transform.parent.gameObject.GetComponent<prova>().waypoints = newWay;
        }*/
    }
