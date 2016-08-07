using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using EaseTools;

public class presentacionPosar7 : MonoBehaviour {

    public int escenario = 0;

    public EaseUI easeUIComponent;
    public EaseUI easeUIComponent2;
    Animator animator;
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
    private bool pantalla1 = true;
    private bool pantalla2 = false;
    private bool pantalla3 = false;
    private bool pantalla4 = false;
    private bool pantalla5 = false;
    private bool pantalla6 = false;
    private bool pantalla7 = false;
    private bool pantalla8 = false;
    private bool pantalla9 = false;
    private float startTime = 0f;
    private float elapsedTime = 0f;
    public Text titulo;
    public Text descripcion;
    public RenderTexture imagen;
    public RenderTexture modelo;
    public RenderTexture jugador;
    public RenderTexture fondo;
    public GameObject estrellitas;
    public GameObject papelitos;
    public GameObject relojTiempo;
    public GameObject relojAguante;
    public GameObject barraPose;
    public GameObject panel;
    private ModelGestureListener gestureListener; // reference to the gesture listener
    private GameObject Suelo, Fondo;

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

        gestureListener = ModelGestureListener.Instance;

        // Control de animacion
        animator = GetComponent<Animator>();

        Suelo = GameObject.Find("PavedFloor");
        Fondo = GameObject.Find("Quad");

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
        easeUIComponent.ScaleIn();
        easeUIComponent2.MoveIn();
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime = Time.time - startTime;
        if (gestureListener.IsRaiseHand() || Input.GetKeyDown(KeyCode.Q))
        {
            SceneManager.LoadScene("transicion-posar7");
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
            titulo.text = "Modelo jugador";
            descripcion.text = "Al moverte mueves el modelo de la derecha.";
            GameObject.Find("Canvas/Window/RawImage").GetComponent<RawImage>().texture = jugador;
            easeUIComponent2.ScaleOut();
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
            animator.SetBool("Posar1", true);
            GameObject.Find("modelo/Brazo_izquierdo").GetComponent<ChangeColorIntensityBrazoIzquierdo>().enabled = true;
            GameObject.Find("modelo/Brazo_derecho").GetComponent<ChangeColorIntensityBrazoDerecho>().enabled = true;
            titulo.text = "Modelo pose";
            descripcion.text = "Copia el modelo de la izquierda. ¡Mira como se iluminan los brazos!";
            GameObject.Find("Canvas/Window/RawImage").GetComponent<RawImage>().texture = modelo;
            if (elapsedTime > 8)
            {
                startTime = Time.time;
                elapsedTime = Time.time - startTime;
                pantalla3 = false;
                pantalla4 = true;
                GameObject.Find("modelo/Brazo_izquierdo").GetComponent<ChangeColorIntensityBrazoIzquierdo>().enabled = false;
                GameObject.Find("modelo/Brazo_derecho").GetComponent<ChangeColorIntensityBrazoDerecho>().enabled = false;
            }
        }
        if (pantalla4)
        {
            GameObject.Find("Canvas/RelojTiempo").GetComponent<ClockManager>().enabled = true;
            titulo.text = "Reloj de tiempo";
            descripcion.text = "Tiempo para conseguir la pose. ¡Consigue mas puntos al hacerlo rapido!";
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
            GameObject.Find("Canvas/RelojAguante").GetComponent<ClockManager>().enabled = true;
            titulo.text = "Reloj de aguante";
            descripcion.text = "Aguanta la pose para conseguir mas puntos!";
            relojAguante.SetActive(true);
            if (elapsedTime > 8)
            {
                startTime = Time.time;
                elapsedTime = Time.time - startTime;
                pantalla5 = false;
                pantalla6 = true;
                relojAguante.SetActive(false);
                GameObject.Find("Canvas/RelojAguante").GetComponent<ClockManager>().enabled = false;
                GameObject.Find("Canvas/RelojAguante").GetComponent<Image>().fillAmount = 1;
            }
        }
        if (pantalla6)
        {
            titulo.text = "Barra de pose";
            descripcion.text = "Cuando este llena conseguiras la pose.";
            barraPose.SetActive(true);
            if (elapsedTime > 8)
            {
                startTime = Time.time;
                elapsedTime = Time.time - startTime;
                pantalla6 = false;
                pantalla7 = true;
                barraPose.SetActive(false);
            }
        }
        if (pantalla7)
        {
            titulo.text = "Confeti y estrellas";
            descripcion.text = "Al lograr una pose apareceran.";
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
                pantalla7 = false;
                pantalla8 = true;
            }

        }
        if (pantalla8)
        {
            titulo.text = "Puntuacion final";
            descripcion.text = "¡Seguro que consigues muchos puntos!";

            panel.SetActive(true);

            if (elapsedTime > 8)
            {
                estrellitas.SetActive(false);
                startTime = Time.time;
                elapsedTime = Time.time - startTime;
                pantalla8 = false;
                pantalla9 = true;
            }

        }
        if (pantalla9)
        {
            panel.SetActive(false);
            GameObject.Find("Canvas/Window/RawImage").GetComponent<RawImage>().texture = imagen;
            titulo.text = "Todo listo";
            descripcion.text = "Ya sabes como se juega. ¡Ahora a jugar!";
            if (elapsedTime > 4)
            {
                SceneManager.LoadScene("transicion-posar7");
            }
        }

    }
}
