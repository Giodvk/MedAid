using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class apriOcchi : MonoBehaviour, IDragHandler
{
    public SkinnedMeshRenderer skinnedMeshRenderer;
    public String blendShapeName;
    private int blendShapeIndex;
    private RectTransform rectTransform;
    private Slider slider;


    void Start()
    {
        // Ottiene i componenti necessari
        rectTransform = GetComponent<RectTransform>();
        slider = GetComponentInParent<Slider>();
        blendShapeIndex = skinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(blendShapeName);
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
        UpdateBlendShape();
    }

    void UpdateBlendShape()
    {
        float distance =slider.value*10;
        float blendShapeValue = Mathf.Clamp(distance,0,100);
        skinnedMeshRenderer.SetBlendShapeWeight(blendShapeIndex, blendShapeValue);
    }
}

