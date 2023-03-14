using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivaAnim : MonoBehaviour
{
    public Animator animaciones;
    Vector3 Origen;
    Vector3 Destino;
    public List<ObjetosListado> ObjetosListado = new List<ObjetosListado>();
    public List<ListaAnim> ListaAnimaciones = new List<ListaAnim>();
    public int IdPosPlayer;
    public int IdPosSentado;
    public int IdPosParado;
    public int AnimSentado; 
    public int AnimParado;
    public float movementSpeed; 
    private bool posicionaPersonaje1;
    private void Start() 
    {
        posicionaPersonaje1=false;
        for (int i = 0; i < ObjetosListado.Count; i++)
        {
            ObjetosListado[i].numero=i;
        }
        for (int i = 0; i < ListaAnimaciones.Count; i++)
        {
            ListaAnimaciones[i].numero=i;
        }
    }
    public void AnimGeneral(int numAnim)
    {
        animaciones.SetTrigger(ListaAnimaciones[numAnim].animNombre);
        if(ListaAnimaciones[numAnim].numero==AnimSentado)
        {
            ObjetosListado[IdPosPlayer].Objetos.transform.position=ObjetosListado[IdPosSentado].Objetos.transform.position;
        }
        if(ListaAnimaciones[numAnim].numero==AnimParado){
            posicionaPersonaje1=true;
        }
    }
    private void Update()
    {
        CambiaPosParado();
    }
    public void JugadorControl(int idObjetos,bool Activador) {
        ObjetosListado[idObjetos].Objetos.SetActive(Activador);
    }
    private void CambiaPosParado()
    {
        if(posicionaPersonaje1==true)
        {
            Destino = ObjetosListado[IdPosParado].Objetos.transform.position-ObjetosListado[IdPosPlayer].Objetos.transform.position;
            Destino.Normalize();// Normaliza la dirección para asegurar que el personaje se mueva con velocidad constante
            float distance = Vector3.Distance(ObjetosListado[IdPosPlayer].Objetos.transform.position,ObjetosListado[IdPosParado].Objetos.transform.position);// Calcula la distancia entre la posición actual del personaje y la posición objetivo
                if(ObjetosListado[IdPosPlayer].Objetos.transform.position!=ObjetosListado[IdPosParado].Objetos.transform.position)
                {
                    ObjetosListado[IdPosPlayer].Objetos.transform.position += Destino * movementSpeed * Time.deltaTime;// Mueve el personaje hacia la posición objetivo con Time.deltaTime
                    Debug.Log(ObjetosListado[IdPosPlayer].Objetos.transform.position);
                    if (distance < 0.1f)// Si el personaje ha llegado a la posición objetivo, detén su movimiento
                    {
                        movementSpeed=0;// Detiene el movimiento del personaje
                        posicionaPersonaje1=false;
                    }
                }
        }   
    }
}
[System.Serializable] public class ListaTotal
{
    public GameObject Objetos; 
    public int numero;
}
[System.Serializable] public class ListaAnim
{
    public string animNombre; 
    public int numero;
}
