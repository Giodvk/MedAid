using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Sensibilità : MonoBehaviour, IDragHandler
{
    public TextMeshProUGUI sensitivity;
    private RectTransform rectTransform;
    private Slider slider;

    public GameObject prefabCamera;

    void Start()
    {
        // Ottiene i componenti necessari
        rectTransform = GetComponent<RectTransform>();
        slider = GetComponentInParent<Slider>();
    }

    // Questo metodo viene chiamato quando l'utente trascina il pallino
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 localPoint;

        // Converte la posizione del mouse in coordinate locali relative allo Slider
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            slider.fillRect, eventData.position, eventData.pressEventCamera, out localPoint
        );

        // Calcola il valore dello Slider basato sulla posizione locale
        float percentage = Mathf.InverseLerp(slider.fillRect.rect.xMin, slider.fillRect.rect.xMax, localPoint.x);

        // Imposta il valore dello Slider
        slider.value = Mathf.Lerp(slider.minValue, slider.maxValue, percentage);
        if(sensitivity!=null){
            int sens= (int)slider.value;
            sensitivity.text=sens.ToString();
        }

        if(prefabCamera!=null){
            rotation camera = prefabCamera.GetComponent<rotation>();
            print(camera);
            camera.mouseSensitivityH = (int) slider.value * 2 * 100;
            camera.mouseSensitivityV = (int) slider.value * 2 * 100;
        }
    }
}

