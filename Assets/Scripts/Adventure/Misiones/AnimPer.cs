using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimPer : MonoBehaviour
{
    public GameObject ActTomarTel;
    public GameObject ActSentado;
    public GameObject ActParado;
    public GameObject ActPeinarse;

    public Animator animaciones;
    void OnTriggerEnter(Collider other)
    {
        // if (other.CompareTag("ActTomarTelefono"))
        // {
        //     DesactivaObjetos();
        //     animaciones.SetTrigger("TomarTelefono");
        // }
                
        // if (other.CompareTag("ActSentado"))
        // {
        //     DesactivaObjetos();
        //     animaciones.SetTrigger("Sentado");
        // }
        
        // if (other.CompareTag("ActParado"))
        // {
        //     DesactivaObjetos();
        //     animaciones.SetTrigger("Pararse");
        // }
        // if (other.CompareTag("ActPeinarse"))
        // {
        //     DesactivaObjetos();
        //     animaciones.SetTrigger("Peinarse");
        // }
    }
    public void DesactivaObjetos()
    {
        ActParado.SetActive(false);
        ActSentado.SetActive(false);
        ActTomarTel.SetActive(false);
        ActPeinarse.SetActive(false);
    }
}
