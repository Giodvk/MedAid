using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sole : MonoBehaviour
{
    public Light directionalLight;
    public float dayDurationInSeconds = 86400f; // Durata di un giorno in secondi (24 ore)

    void Update()
    {
        float timeOfDay = (float) DateTime.Now.TimeOfDay.TotalSeconds;
        float rotationAngle = (timeOfDay / dayDurationInSeconds) * 360f;

        // Ruota la luce direzionale per simulare il movimento del sole
        directionalLight.transform.localRotation = Quaternion.Euler(rotationAngle - 90f, 170f, 0f);

        // Puoi anche aggiungere un'altra luce per la notte (luna) e regolare le intensità.
    }
}
