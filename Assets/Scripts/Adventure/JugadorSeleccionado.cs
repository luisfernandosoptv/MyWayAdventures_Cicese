using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class JugadorSeleccionado : MonoBehaviour
{
    public List<ListaTotal> ObjetosListado = new List<ListaTotal>();
    public int Activo;
    void Start()
    {
        ActivaPer();
        CachaPrefs();
    }
    private void CachaPrefs(){
        for (int i = 0; i < ObjetosListado.Count; i++)
        {
            ObjetosListado[i].P = PlayerPrefs.GetInt("SeleccionePersonaje"+ObjetosListado[i].Activo)==1;
             if(ObjetosListado[i].P==true)
            {
                ObjetosListado[i].Objetos.SetActive(true);
            }
            else
        {
            Destroy(ObjetosListado[i].Objetos);
        } 
        }
    }
    private void ActivaPer()
    {
        for (int i = 0; i < ObjetosListado.Count; i++)
        {
            ObjetosListado[i].Activo=i;
        }
    }
    [System.Serializable] public class ListaTotal
{
    public GameObject Objetos; 
    public bool P;
    public int Activo;
}
}