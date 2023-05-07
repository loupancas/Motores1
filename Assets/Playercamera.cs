using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playercamera : MonoBehaviour
{
    public float sensitivityX = 1.0f;
    public float sensitivityY = 1.0f;

    public Transform cameraorientation;

    float Xrotation;
    float Yrotation;

    private void Start()
    {
        //configuracion del mouse en la pantalla
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

    private void Update()
    {
        //obtengo la posicion del mouse
        float mousex = Input.GetAxisRaw("Mouse X") * Time.fixedDeltaTime * sensitivityX;
        float mousey = Input.GetAxisRaw("Mouse Y") * Time.fixedDeltaTime * sensitivityY;

        Yrotation += mousex;
        Xrotation -= mousey;

        //clamp para que la rotacion en X no supere los 90 grados
        Xrotation = Mathf.Clamp(Xrotation, -90f, 90f);

        //rotar camara y orietacion

        transform.rotation = Quaternion.Euler(Xrotation, Yrotation, 0);
        cameraorientation.rotation=Quaternion.Euler(0,Yrotation, 0);
    }


}
