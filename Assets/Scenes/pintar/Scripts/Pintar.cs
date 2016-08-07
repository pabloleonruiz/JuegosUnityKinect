using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using EaseTools;
using UnityEngine.SceneManagement;

public class Pintar : MonoBehaviour
{
    public int dificultad1 = 0;
    public int escenario = 0;
    public int lineas = 0;

    public EaseUI easeUIComponent;
    public EaseUI easeUIComponent2;
    public Texture background; //Fondos
    public Texture floor; //Suelos
    public Texture background2;
    public Texture floor2;
    public Texture background3;
    public Texture floor3;
    public Texture background4;
    public Texture floor4;
    public Texture background5;
    public Texture floor5;
    public Texture background6;
    public Texture background7;
    public LineRenderer lineaRoja;
    public LineRenderer lineaAzul;
    public LineRenderer lineaVerde;
    private float startTime = 0f;
    private float elapsedTime = 0f;
    public Text mensajes; 
    public Text textoBoton;
    private GameObject Suelo, Fondo, Kinect;
    public Texture pincelrojo,pincelazul,pincelverde;
    private bool presentacion = true;
    private bool presentacion1 = true;
    private bool presentacion2 = false;
    private bool presentacion3 = false;
    private GameObject LapizAzul,LapizVerde,LapizRojo;
    public Material[] rojoActivo = new Material[5];
    public Material[] rojoNoActivo = new Material[5];
    public Material[] azulActivo = new Material[5];
    public Material[] azulNoActivo = new Material[5];
    public Material[] verdeActivo = new Material[5];
    public Material[] verdeNoActivo = new Material[5];

    // Use this for initialization
    void Start()
    {
        //Bug de encendido de la camara entre escenas
        if (GameManager.activo == false)
        {
            GameManager.intentos++;
            if (GameManager.intentos < 5)
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            else
                GameManager.intentos = 0;
        }

        // Semilla para el numero aleatorio
        Random.seed = System.DateTime.Now.Millisecond;

        // Objetos del escenario
        Suelo = GameObject.Find("PavedFloor");
        Fondo = GameObject.Find("Quad");
        Kinect = GameObject.Find("KinectController");
        LapizAzul = GameObject.Find("LapizAzul");
        LapizVerde = GameObject.Find("LapizVerde");
        LapizRojo = GameObject.Find("LapizRojo");

        if(dificultad1==1)
        {
            mensajes.text = "Cierra la mano \n derecha para pintar";
            Kinect.GetComponent<HandOverlayer>().enabled = false;
            Kinect.GetComponent<HandOverlayer>().isLeftHanded = false;
            Kinect.GetComponent<HandOverlayer>().enabled = true;
        }

        //Seleccion de cuadricula
        if (lineas == 1)
            GameObject.Find("Canvas/Lineas").GetComponent<RawImage>().enabled = true;

        // Seleccion de fondo y suelo
        if (escenario == 0)
        {
            Suelo.GetComponent<Renderer>().material.mainTexture = floor;
            Fondo.GetComponent<Renderer>().material.mainTexture = background;
        }

        if (escenario == 1)
        {
            Suelo.GetComponent<Renderer>().material.mainTexture = floor2;
            Fondo.GetComponent<Renderer>().material.mainTexture = background2;
        }
        if (escenario == 2)
        {
            Suelo.GetComponent<Renderer>().material.mainTexture = floor3;
            Fondo.GetComponent<Renderer>().material.mainTexture = background3;
        }
        if (escenario == 3)
        {
            Suelo.GetComponent<Renderer>().material.mainTexture = floor4;
            Fondo.GetComponent<Renderer>().material.mainTexture = background4;
        }
        if (escenario == 4)
        {
            Suelo.GetComponent<Renderer>().material.mainTexture = floor5;
            Fondo.GetComponent<Renderer>().material.mainTexture = background5;
        }
        if (escenario == 5)
        {
            Suelo.GetComponent<Renderer>().enabled = false;
            Fondo.GetComponent<Renderer>().material.mainTexture = background6;
        }
        if (escenario == 6)
        {
            Suelo.GetComponent<Renderer>().enabled = false;
            Fondo.GetComponent<Renderer>().material.mainTexture = background7;
        }

        easeUIComponent.MoveIn();
        startTime = Time.time;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        //Bug de encendido de la camara entre escenas
        if (GameManager.activo == false)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        elapsedTime = Time.time - startTime;
        if (presentacion)
        {
            if (presentacion1)
            {
                if (elapsedTime > 3)
                {
                    startTime = Time.time;
                    elapsedTime = Time.time - startTime;
                    textoBoton.text = "LISTOS";
                    easeUIComponent.ScaleIn();
                    presentacion1 = false;
                    presentacion2 = true;
                }
            }
            if (presentacion2)
            {

                if (elapsedTime > 3)
                {
                    startTime = Time.time;
                    elapsedTime = Time.time - startTime;
                    textoBoton.text = "¡YA!";
                    easeUIComponent.ScaleIn();
                    easeUIComponent.RotateOut();
                    presentacion2 = false;
                    presentacion3 = true;

                }

            }
            if (presentacion3)
            {
                easeUIComponent2.MoveIn();

                if (elapsedTime > 2)
                {
                    startTime = Time.time;
                    elapsedTime = Time.time - startTime;
                    easeUIComponent.MoveOut();
                    presentacion3 = false;
                    presentacion = false;
                    Kinect.GetComponent<LinePainter>().enabled = true;
                    LapizAzul.GetComponent<Renderer>().materials = azulActivo;

                }

            }
        }
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (!presentacion)
        {
            if (other.gameObject.tag == "rojo")
            {
                Kinect.GetComponent<LinePainter>().enabled = false;
                Kinect.GetComponent<LinePainter>().linePrefab = lineaRoja;
                Kinect.GetComponent<HandOverlayer>().gripHandTexture = pincelrojo;
                Kinect.GetComponent<LinePainter>().enabled = true;
                LapizRojo.GetComponent<Renderer>().materials = rojoActivo;
                LapizAzul.GetComponent<Renderer>().materials = azulNoActivo;
                LapizVerde.GetComponent<Renderer>().materials = verdeNoActivo;
            }
            if (other.gameObject.tag == "verde")
            {
                Kinect.GetComponent<LinePainter>().enabled = false;
                Kinect.GetComponent<LinePainter>().linePrefab = lineaVerde;
                Kinect.GetComponent<HandOverlayer>().gripHandTexture = pincelverde;
                Kinect.GetComponent<LinePainter>().enabled = true;
                LapizAzul.GetComponent<Renderer>().materials = azulNoActivo;
                LapizVerde.GetComponent<Renderer>().materials = verdeActivo;
                LapizRojo.GetComponent<Renderer>().materials = rojoNoActivo;
            }
            if (other.gameObject.tag == "azul")
            {
                Kinect.GetComponent<LinePainter>().enabled = false;
                Kinect.GetComponent<LinePainter>().linePrefab = lineaAzul;
                Kinect.GetComponent<HandOverlayer>().gripHandTexture = pincelazul;
                Kinect.GetComponent<LinePainter>().enabled = true;
                LapizAzul.GetComponent<Renderer>().materials = azulActivo;
                LapizVerde.GetComponent<Renderer>().materials = verdeNoActivo;
                LapizRojo.GetComponent<Renderer>().materials = rojoNoActivo;
            }
            if (other.gameObject.tag == "borrar")
            {
                Kinect.GetComponent<LinePainter>().DeleteLastLine();
            }
            if (other.gameObject.tag == "salir")
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
            }
        }

    }
    
}