using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movimiento : MonoBehaviour
{
    [SerializeField] private CharacterController ControladorJugador;
    [SerializeField] private float Gravedad = -9.8f;


    //VARIABLES USADAS EN TODO tanto para rigid como controller
    private Vector3 EntradaMovimientoJugador;
    private float CamaraRotacionX;
    private Vector3 FuerzaVelocidad;
    private Vector2 EntradaMouseJugador;
    [SerializeField] private Transform CamaraJugador;
    [SerializeField] private float Velocidad;
    [SerializeField] private float Sensibilidad;
    // [SerializeField] private float FuerzaSalto;

    void Update()
    {
        EntradaMovimientoJugador = new Vector3(Input.GetAxis("Horizontal"),0f, Input.GetAxis("Vertical"));
        EntradaMouseJugador = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        MoverJugador();
        MoverJugadorCamara();
    }



    private void MoverJugador()
    {
        //ES CON CharacterController
        Vector3 MoverVector = transform.TransformDirection(EntradaMovimientoJugador);
        ControladorJugador.Move(MoverVector * Velocidad * Time.deltaTime);
        ControladorJugador.Move(FuerzaVelocidad * Time.deltaTime);

        if (ControladorJugador.isGrounded)
        {
            FuerzaVelocidad.y = -1f;
        }
        else
        {
            FuerzaVelocidad.y -=Gravedad *-2f*Time.deltaTime;
        }

       //ES CON Rigidbody.
       //Vector3 MoverVector = transform.TransformDirection(EntradaMovimientoJugador) * Velocidad;
       //CuerpoJugador.velocity = new Vector3(MoverVector.x, CuerpoJugador.velocity.y, MoverVector.z);
    }
    private void MoverJugadorCamara()
    {
        CamaraRotacionX -= EntradaMouseJugador.y * Sensibilidad;
        transform.Rotate(0f, EntradaMouseJugador.x * Sensibilidad, 0f);
        CamaraJugador.transform.localRotation = Quaternion.Euler(CamaraRotacionX,0f,0f);

    }

    
}
