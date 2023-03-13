using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mono.Data.Sqlite;
using System.Data;
public class Inicializar : MonoBehaviour
{
    public List<ListaTotal> ObjetosListado = new List<ListaTotal>();
    public List<ListaDatos> DatosListado = new List<ListaDatos>();
    public Dropdown estadoInput;
    [SerializeField] private string Edad;
    [SerializeField] private int EstadoIndice;
    [SerializeField] private string Estado;
    public int TiempoMensaje=0;
    private string Vacio="";
    public Text Mensaje;
    public Color32 correcto;
    public Color32 sinColor;
    public Color32 incorrecto;
    public GameObject menuMensaje;
    public GameObject MenuRegistro;
    public GameObject MenuPersonaje;
    public GameObject BtnJugar;
    private string dbName = "URI=file:Prueba.db";//nombre del archivo en base de datos
    private void awake() 
    {
        for(int i = 0; i < ObjetosListado.Count; i++)
        {
            ObjetosListado[i].P=PlayerPrefs.GetInt("SeleccionePersonaje"+(i+1))==1;
        }
    }
    void Start() {
        LimpiarCampos();
        Regresar(); 
        ComienzaPrefs();
    }   
    public void registro()
    {
        AsignaPlayerprefs();
        if(DatosListado[0].MisImput.text==""||DatosListado[1].MisImput.text==""||DatosListado[2].MisImput.text==""||DatosListado[3].MisImput.text==""||DatosListado[4].MisImput.text==""||DatosListado[5].MisImput.text=="")
        {
            StartCoroutine("camposVacios");
        }
        else
        {
            Edad=DatosListado[5].MisImput.text;
            if(int.Parse(Edad)>0)
            {
                EstadoIndice=estadoInput.value;
                MenuRegistro.SetActive(false);
                MenuPersonaje.SetActive(true);
                DesactivaImagenes();
            }
            else
            {
                MenuRegistro.SetActive(false);
                menuMensaje.SetActive(true);
                Mensaje.text="Necesitas una edad mayor de 0 años";
            }
        }
    }
    IEnumerator camposVacios()
    {
        MenuRegistro.SetActive(false);
        menuMensaje.SetActive(true);
        Mensaje.text="Rellena todos los campos";
        yield return new WaitForSeconds(TiempoMensaje);
        MenuRegistro.SetActive(true);
        menuMensaje.SetActive(false);
        for (int i = 0; i < DatosListado.Count; i++)
        {
            if(DatosListado[i].MisImput.text=="")
            {
                DatosListado[i].MisImput.image.color=incorrecto;
            }
            else
        {
            DatosListado[i].MisImput.image.color=correcto;
        }
        }
        yield return new WaitForSeconds(TiempoMensaje);
        for (int i = 0; i < DatosListado.Count; i++)
        {
            DatosListado[i].MisImput.image.color = sinColor;
        }
    }
    public void Regresar(){
        MenuRegistro.SetActive(true);
        MenuPersonaje.SetActive(false);
        menuMensaje.SetActive(false);
    }
   void AsignaPlayerprefs() 
   {
    for (int i = 0; i < DatosListado.Count; i++)
    {
        if(i>=DatosListado.Count-1)
        {
            PlayerPrefs.SetString("Indice"+i, DatosListado[i].MisImput.text);
        }
        else
        {
             PlayerPrefs.SetString("Indice"+i, Estado);
        }
    }
    }
    void ComienzaPrefs()
    {
        for (int i = 0; i < DatosListado.Count; i++)
        {
            if(i>=DatosListado.Count-1)
            {
                DatosListado[i].MisImput.text=PlayerPrefs.GetString("Indice"+i);
            }
            else
            {
                Vacio=PlayerPrefs.GetString("Indice"+i);
            }
        }
    }
    private void DesactivaImagenes() 
    {
        for(int i = 0; i < ObjetosListado.Count; i++)
        {
            ObjetosListado[i].Objetos.SetActive(false);
        }
    }
    //--------------------------------------------------------------------------------------------------------------------
    public void btnGen(int numero) 
    {
        DesactivaImagenes();
        perDesactivados();
        ObjetosListado[numero].Objetos.SetActive(true);
        ObjetosListado[numero].P=true;
    }

  
    public void Jugar () {
        Guardar();
        ubicaPersonajes();
        VerificaEstado();
        IngresaDatos();
    }

    private void ubicaPersonajes() 
    {
        for(int i = 0; i < ObjetosListado.Count; i++)
        {
            ObjetosListado[i].Objetos.transform.localScale=new Vector2 (1.2f,1.2f);
        }
    }
    private void perDesactivados()
    {
        BtnJugar.SetActive(true);
        for(int i = 0; i < ObjetosListado.Count; i++)
        {
            ObjetosListado[i].P=false;
        }
    }
    private void VerificaEstado()
    {
        Estado=Vacio;
         using (var connection = new SqliteConnection(dbName))
            {
                connection.Open();
                using (IDbCommand dbCmd= connection.CreateCommand())
                {
                    string sqlQuery = "SELECT * FROM estados WHERE estado_id='" + (EstadoIndice+1) + "'";
                    dbCmd.CommandText=sqlQuery;
                    using(IDataReader reader = dbCmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Estado=reader.GetString(1);
                        }
                    }
                }
            }
    }
    private void IngresaDatos()
    {
          using (var connection = new SqliteConnection(dbName))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "INSERT INTO usuarios(nombre, apellidoP, apellidoM, edad, estado, municipio, localidad) VALUES ('" + DatosListado[0].MisImput.text + "','" + DatosListado[1].MisImput.text + "','" + DatosListado[2].MisImput.text + "','" + DatosListado[3].MisImput.text + "','" + Estado + "','" + DatosListado[4].MisImput.text + "','" + DatosListado[5].MisImput.text + "')";
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
    }
    //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    private void  LimpiarCampos() 
    {
        for (int i = 0; i < DatosListado.Count; i++)
        {
            if(i>=DatosListado.Count-1)
            {
            DatosListado[i].MisImput.text="";
            }
        }
    }
    //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    public void Guardar()
    {
        for(int i = 0; i < ObjetosListado.Count; i++)
        {
           PlayerPrefs.SetInt("SeleccionePersonaje"+ObjetosListado[i].P, ObjetosListado[i].P ? 1 : 0);
        }
    }
    //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    public void CerrarJuego()
    {
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #else 
        Application.Quit();
    #endif
    }
}
//----Clases serializables para mandar llamar arriba
[System.Serializable] public class ListaTotal
{
    public GameObject Objetos; 
    public bool P;
}

[System.Serializable] public class ListaDatos
{
    public InputField MisImput;
    public int NPrueba;
}