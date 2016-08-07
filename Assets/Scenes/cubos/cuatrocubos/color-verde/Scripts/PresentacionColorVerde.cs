using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using EaseTools;

public class PresentacionColorVerde : MonoBehaviour {
    public int escenario = 0; // Elegir pantalla (0 a 6)

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

    public RenderTexture cuboRojo;
    public RenderTexture cuboVerde;
    public RenderTexture imagen;
    public RenderTexture fondo;

    public Text titulo;
    public Text descripcion;

    public GameObject estrellitas;
    public GameObject papelitos;
    public GameObject relojTiempo;
    public GameObject panel;

    private Color colorVerde = new Color32(0, 197, 26, 120); // Color verde
    private Color colorRojo = new Color32(253, 76, 76, 120); // Color rojo
    private Color colorAmarillo = new Color32(226, 229, 29, 120); // Color amarillo
    private Color colorAzul = new Color32(0, 213, 255, 120); // Color azul
    private bool pantalla1 = true;
    private bool pantalla2 = false;
    private bool pantalla3 = false;
    private bool pantalla4 = false;
    private bool pantalla5 = false;
    private bool pantalla6 = false;
    private bool pantalla7 = false;
    private float startTime = 0f;
    private float elapsedTime = 0f;
    private GameObject DerechaAba, DerechaArri, IzquierdaAba, IzquierdaArri, Suelo, Fondo, Reloj;
    private ModelGestureListener gestureListener; // reference to the gesture listener

    // Use this for initialization
    void Start () {
        //Bug de encendido de la camara entre escenas
        if (GameManager.activo == false)
        {
            GameManager.intentos++;
            if (GameManager.intentos < 5)
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            else
                GameManager.intentos = 0;
        }

        gestureListener = ModelGestureListener.Instance;

        // Objetos del escenario
        DerechaAba = GameObject.Find("Pilares/DerechaAbajo");
        DerechaArri = GameObject.Find("Pilares/DerechaArriba");
        IzquierdaArri = GameObject.Find("Pilares/IzquierdaArriba");
        IzquierdaAba = GameObject.Find("Pilares/IzquierdaAbajo");
        Suelo = GameObject.Find("PavedFloor");
        Fondo = GameObject.Find("Quad");
        Reloj = GameObject.Find("Canvas/RelojTiempo");

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
        DerechaArri.GetComponent<Renderer>().materials[0].color = colorVerde;
        DerechaAba.GetComponent<Renderer>().materials[0].color = colorAzul;
        IzquierdaArri.GetComponent<Renderer>().materials[0].color = colorRojo;
        IzquierdaAba.GetComponent<Renderer>().materials[0].color = colorAmarillo;
        easeUIComponent.ScaleIn();
        easeUIComponent2.MoveIn();
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update () {
        elapsedTime = Time.time - startTime;

        if (gestureListener.IsRaiseHand() || Input.GetKeyDown(KeyCode.Q))
        {
            SceneManager.LoadScene("transicion-color-verde4");
        }

        if (pantalla1)
        {
            titulo.text = "Imagen real";
            descripcion.text = "¡Eres tu! Estas dentro del juego.";
            GameObject.Find("Canvas/Window/RawImage").GetComponent<RawImage>().texture = imagen;

            if (elapsedTime > 8)
            {
                startTime = Time.time;
                elapsedTime = Time.time - startTime;
                pantalla1 = false;
                pantalla2 = true;
            }
        }
        if (pantalla2)
        {
            titulo.text = "Cubo verde";
            descripcion.text = "¡Toca todos los cubos verdes!";
            easeUIComponent2.ScaleOut();
            GameObject.Find("Canvas/Window/RawImage").GetComponent<RawImage>().texture = cuboVerde;
            if (elapsedTime > 8)
            {
                startTime = Time.time;
                elapsedTime = Time.time - startTime;
                pantalla2 = false;
                pantalla3 = true;
            }
        }
        if (pantalla3)
        {
            titulo.text = "Cubo rojo";
            descripcion.text = "No toques lo cubos de un color distinto al verde";
            GameObject.Find("Canvas/Window/RawImage").GetComponent<RawImage>().texture = cuboRojo;
            if (elapsedTime > 8)
            {
                startTime = Time.time;
                elapsedTime = Time.time - startTime;
                pantalla3 = false;
                pantalla4 = true;
            }
        }
        if (pantalla4)
        {
            Reloj.GetComponent<ClockManager>().enabled = true;
            titulo.text = "Reloj de tiempo";
            descripcion.text = "Limite de tiempo para tocar el cubo. ¡Consigue mas puntos al hacerlo rapido!";
            GameObject.Find("Canvas/Window/RawImage").GetComponent<RawImage>().texture = fondo;
            relojTiempo.SetActive(true);
            if (elapsedTime > 8)
            {
                startTime = Time.time;
                elapsedTime = Time.time - startTime;
                pantalla4 = false;
                pantalla5 = true;
                relojTiempo.SetActive(false);
                GameObject.Find("Canvas/RelojTiempo").GetComponent<ClockManager>().enabled = false;
                GameObject.Find("Canvas/RelojTiempo").GetComponent<Image>().fillAmount = 1;

            }
        }
        if (pantalla5)
        {
            titulo.text = "Confeti y estrellas";
            descripcion.text = "Al lograr una pose, apareceran confeti o estrellas en la pantalla.";
            if (elapsedTime < 4)
                papelitos.SetActive(true);
            if (elapsedTime > 4 && elapsedTime < 8)
            {
                papelitos.SetActive(false);
                estrellitas.SetActive(true);
            }
            if (elapsedTime > 8)
            {
                estrellitas.SetActive(false);
                startTime = Time.time;
                elapsedTime = Time.time - startTime;
                pantalla5 = false;
                pantalla6 = true;
            }

        }
        if (pantalla6)
        {
            titulo.text = "Puntuacion final";
            descripcion.text = "Al terminar conseguiras una puntuacion. ¡Seguro que consigues muchos puntos!";

            panel.SetActive(true);

            if (elapsedTime > 8)
            {
                estrellitas.SetActive(false);
                startTime = Time.time;
                elapsedTime = Time.time - startTime;
                pantalla6 = false;
                pantalla7 = true;
            }

        }
        if (pantalla7)
        {
            panel.SetActive(false);
            GameObject.Find("Canvas/Window/RawImage").GetComponent<RawImage>().texture = imagen;
            titulo.text = "Todo listo";
            descripcion.text = "Ya sabes como se juega. ¡A jugar!";
            if (elapsedTime > 4)
            {
                SceneManager.LoadScene("transicion-color-verde4");
            }
        }
    }
}

