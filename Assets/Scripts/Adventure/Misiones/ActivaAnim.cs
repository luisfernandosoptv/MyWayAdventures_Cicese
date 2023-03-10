using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivaAnim : MonoBehaviour
{

        public Animator animaciones;
        
        Vector3 Origen;
        Vector3 Destino;
        public GameObject Player;
        public Transform PosSentado; // la posición objetivo a la que se moverá el personaje
        public Transform PosParado;
        public float movementSpeed; // velocidad de movimiento del personaje
        bool posicionaPersonaje1;


    void TomarTelefono()
    {

        animaciones.SetTrigger("TomarTelefono");
    }
        void Sentarse()
    {
        animaciones.SetTrigger("Sentado");
        Player.transform.position=PosSentado.position;
    }
    void Pararse()
    {
        animaciones.SetTrigger("Pararse");
        posicionaPersonaje1=true;
    }
        void Peinarse()
    {
        animaciones.SetTrigger("Peinarse");
    }
    
    private void Start() {
        posicionaPersonaje1=false;
    }

    private void Update()
    {
        
    
        if(posicionaPersonaje1==true)
        {
            Destino = PosParado.position-Player.transform.position;
            Destino.Normalize();// Normaliza la dirección para asegurar que el personaje se mueva con velocidad constante
            float distance = Vector3.Distance(Player.transform.position,PosParado.position);// Calcula la distancia entre la posición actual del personaje y la posición objetivo
                if(Player.transform.position!=PosParado.position)
                {
                    Player.transform.position += Destino * movementSpeed * Time.deltaTime;// Mueve el personaje hacia la posición objetivo con Time.deltaTime
                    Debug.Log(Player.transform.position);
                    if (distance < 0.1f)// Si el personaje ha llegado a la posición objetivo, detén su movimiento
                    {
                        movementSpeed=0;// Detiene el movimiento del personaje
                        posicionaPersonaje1=false;
                    }
                }

        }
        
    }

}
