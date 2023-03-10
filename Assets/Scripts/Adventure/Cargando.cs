using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Cargando : MonoBehaviour
{
    public int TiempoDeEspera=10;
    private float Resultado=0;
    private float Tiempo;
    public Text texto;
    private void Start()
    {
        int nivelACargar = CargaNivel.siguienteNivel;
        StartCoroutine(IniciarCarga(nivelACargar));
    }
    void Update()
    {
        Contador();
    }
    void Contador(){
        Tiempo+=1*Time.deltaTime;
        Resultado=TiempoDeEspera-Tiempo;
        if(Resultado>1)
        {
            texto.text="Espera " +Resultado.ToString("f0")+" para continuar";
        }
    }

    IEnumerator IniciarCarga(int nivel)

    {
       
        yield return new WaitForSeconds(TiempoDeEspera);
        AsyncOperation operacion = SceneManager.LoadSceneAsync(nivel);
        operacion.allowSceneActivation = false;

        while(!operacion.isDone)
        {
            if(operacion.progress >= 0.9f)
            {
                texto.text = "Presiona una tecla o Clickea para continuar";
                if(Input.anyKey)
                {
                    operacion.allowSceneActivation = true;
                }
            }

            yield return null;
        }
    }
    


}
