using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class rotation : MonoBehaviour
{
    public float mouseSensitivityH = 100f;
    public float mouseSensitivityV = 100F;
    private float xRotation = 0f;

    private GameObject image;

    void Start()
    {
        // Blocca il cursore al centro dello schermo
        Cursor.lockState = CursorLockMode.Locked;
        image = GameObject.FindGameObjectWithTag("Cursore");
        image.transform.localPosition = Vector3.zero;
    }

    void Update()
    {
        // Ottiene il movimento del mouse
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivityH * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivityV * Time.deltaTime;
        // Calcola la rotazione lungo l'asse X
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 50f);
       
        // Applica la rotazione
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.parent.Rotate(Vector3.up * mouseX);
       

}
}

