using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C1_PersonajeAnimaciones : MonoBehaviour
{

    public Animator anim;
    public GameObject Player;
    public GameObject SegPos;
    public GameObject Sentarse;
    public GameObject Pararse;

    void activaPlayer()
    {
        Player.SetActive(true);
    }
    void desactivaPlayer()
    {
        Player.SetActive(false);
    }

  void OnTriggerEnter(Collider other)
    {
        if(other.tag=="ActivaSentado")
        {
            ActivadoresDesactivados();
            anim.SetTrigger("Sentado");
            Player.transform.position = new Vector3 (47.82f,2.378f,3.3f);
            Player.transform.Rotate(0f, 90.0f, 0.0f);     
        }
        if(other.tag=="ActivaParado")
        {
            ActivadoresDesactivados();
            anim.SetTrigger("Parado");
            Player.transform.position = new Vector3 (47.82f,2.378f,3.9f);
        }
    }
    void ActivadoresDesactivados()
    {
        Sentarse.SetActive(false);
        Pararse.SetActive(false);
    }

  
    // Start is called before the first frame update

}
