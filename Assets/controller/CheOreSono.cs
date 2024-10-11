using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheOreSono : MonoBehaviour
{
    public Transform hourHand;
    public Transform minuteHand;
    public Transform secondHand;



    void Update()
    {
        DateTime currentTime = DateTime.Now;
        // Sincronizza le lancette con l'ora reale
        UpdateClock(currentTime);
    }

    void UpdateClock(DateTime time)
    {
        // Calcola la rotazione per ogni lancetta
        float hourRotation = (time.Hour % 12) * 30f + (time.Minute / 2f); // 30° per ogni ora + la frazione delle mezze ore
        float minuteRotation = time.Minute * 6f; // 6° per ogni minuto
        float secondRotation = time.Second * 6f; // 6° per ogni secondo
        // Applica la rotazione alle lancette
        hourHand.localRotation = Quaternion.Euler(0f, 90f, hourRotation);
        minuteHand.localRotation = Quaternion.Euler(0f, 90f, minuteRotation);
        secondHand.localRotation = Quaternion.Euler(0f, 90f, secondRotation);
    }
}
