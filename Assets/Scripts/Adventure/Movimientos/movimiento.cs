using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;

public class movimiento : MonoBehaviour
{
    public List<ListaAnimaciones> AnimLista = new List<ListaAnimaciones>();
    public Animator anim;
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

    float hor;
    float ver;
    // [SerializeField] private float FuerzaSalto;
    private void Start() {
        AsignaNumeros();
        animaciones();
    }
    void Update()
    {
        animaciones();
        EntradaMovimientoJugador = new Vector3(Input.GetAxis("Horizontal"),0f, Input.GetAxis("Vertical"));
        EntradaMouseJugador = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        MoverJugador();
        MoverJugadorCamara();
    }
    
    void animaciones()
    {
        float hor = Input.GetAxisRaw("Horizontal");
        float ver = Input.GetAxisRaw("Vertical");
        if (hor==0&&ver==0)
        {
            sinAnim();
        }
        if (hor==0&&ver==1)
        {
            anim.SetBool(AnimLista[0].nomAnim,true);
        }
        if (hor==0&&ver==-1)
        {
            anim.SetBool(AnimLista[1].nomAnim,true);
        }
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
        CamaraRotacionX = Mathf.Clamp(CamaraRotacionX, -45, 45);
        CamaraJugador.transform.localRotation = Quaternion.Euler(CamaraRotacionX,0f,0f);

    }
    void AsignaNumeros()
    {
        for (int i = 0; i < AnimLista.Count; i++)
        {
            AnimLista[i].numAnim=i;
        }
    }
    private void sinAnim() {
        for (int i = 0; i < AnimLista.Count; i++)
        {
            anim.SetBool(AnimLista[i].nomAnim,false);
        }
    }
}
[System.Serializable] public class ListaAnimaciones
{
    public string nomAnim;
    public int numAnim;
}
