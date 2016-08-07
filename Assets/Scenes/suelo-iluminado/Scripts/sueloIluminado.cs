using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using EaseTools;
using UnityEngine.SceneManagement;

public class sueloIluminado : MonoBehaviour {
    public int dificultad1 = 0;
    public int dificultad2 = 0;
    public int escenario = 0;
    public int score = 0; // Puntuación
    public int aciertos = 0;
    public int fallos = 0;
    public int lineas = 0;

    public EaseUI easeUIComponent;
    public Material transparentMaterial;
    public Material iluminateMaterial;
    public GameObject estrellitas;
    public GameObject papelitos;
    private int maxIteraciones = 1;
    private int iteraciones = 0;
    private float moveTime = 0f; // Tiempo para tocar el pilar
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
    public GameObject oneStar;
    public GameObject twoStar;
    public GameObject threeStar;
    private float startTime = 0f;
    private float elapsedTime = 0f;
    private bool DerechaAbajoActiva = false;
    private bool DerechaMedioAbajoActiva = false;
    private bool DerechaMedioArribaActiva = false;
    private bool IzquierdaMedioAbajoActiva = false;
    private bool IzquierdaAbajoActiva = false;
    private bool IzquierdaMedioArribaActiva = false;
    private bool DerechaArribaActiva = false;
    private bool IzquierdaArribaActiva = false;
    private bool MedioActiva = false;
    private bool conseguido = false;
    private int aleatorio;
    private int aleatorio2;
    public Text resultado;
    public Text textoBoton;
    public GameObject panel;
    public Text mensajes; // Mensajes para el usuario
    private bool espera = false;
    private float tiempoEspera = 0;
    public AudioClip final;
    public AudioClip acierto;
    public AudioClip fallo;
    private GameObject DerechaAba, DerechaMedioAba, DerechaMedioArri, DerechaArri, IzquierdaAba, IzquierdaMedioAba, IzquierdaMedioArri, IzquierdaArri, Medio, Suelo, Fondo, Reloj;
    private int multiplicador;
    private int aleatorioTemp;
    private bool presentacion = true;
    private bool presentacion1 = true;
    private bool presentacion2 = false;
    private bool presentacion3 = false;
    private bool finJuego = false;
    private bool acabado = false;

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

        startTime = Time.time;
        // Objetos del escenario
        DerechaAba = GameObject.Find("Pilares/DerechaAbajo");
        DerechaMedioAba = GameObject.Find("Pilares/DerechaMedioAbajo");
        DerechaMedioArri = GameObject.Find("Pilares/DerechaMedioArriba");
        DerechaArri = GameObject.Find("Pilares/DerechaArriba");
        IzquierdaArri = GameObject.Find("Pilares/IzquierdaArriba");
        IzquierdaMedioArri = GameObject.Find("Pilares/IzquierdaMedioArriba");
        IzquierdaMedioAba = GameObject.Find("Pilares/IzquierdaMedioAbajo");
        IzquierdaAba = GameObject.Find("Pilares/IzquierdaAbajo");
        Medio = GameObject.Find("Pilares/Medio");

        Suelo = GameObject.Find("PavedFloor");
        Fondo = GameObject.Find("Quad");
        Reloj = GameObject.Find("Canvas/RelojTiempo");

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

        if (dificultad2 == 0)
        {
            mensajes.text = "Con los dos pies";
            // Numero aleatorio de 0 a 7
            aleatorio = Random.Range(0, 8);
            aleatorioTemp = aleatorio;
        }
        if (dificultad2 == 1)
        {
            mensajes.text = "Solo con el izquierdo";
            Medio.GetComponent<Renderer>().enabled = false;
            DerechaMedioArri.GetComponent<Renderer>().enabled = false;
            do
            {
                // Numero aleatorio de 0 a 7
                aleatorio = Random.Range(0, 8);
                aleatorioTemp = aleatorio;
            } while (aleatorioTemp == 5);
        }
        if (dificultad2 == 2)
        {
            mensajes.text = "Solo con el derecho";
            Medio.GetComponent<Renderer>().enabled = false;
            IzquierdaMedioArri.GetComponent<Renderer>().enabled = false;
            do
            {
                // Numero aleatorio de 0 a 7
                aleatorio = Random.Range(0, 8);
                aleatorioTemp = aleatorio;
            } while (aleatorioTemp == 6);
        }
        if (dificultad2 == 3)
        {
            mensajes.text = "Saltando";
            // Numero aleatorio de 0 a 7
            aleatorio = Random.Range(0, 8);
            aleatorioTemp = aleatorio;
        }
        // Primera seleccion del bloque iluminado
        if (aleatorio == 0)
        {
            DerechaAba.GetComponent<Renderer>().material = transparentMaterial;
            DerechaArri.GetComponent<Renderer>().material = transparentMaterial;
            IzquierdaArri.GetComponent<Renderer>().material = transparentMaterial;
            IzquierdaAba.GetComponent<Renderer>().material = iluminateMaterial;
            DerechaMedioAba.GetComponent<Renderer>().material = transparentMaterial;
            DerechaMedioArri.GetComponent<Renderer>().material = transparentMaterial;
            IzquierdaMedioArri.GetComponent<Renderer>().material = transparentMaterial;
            IzquierdaMedioAba.GetComponent<Renderer>().material = transparentMaterial;
            Medio.GetComponent<Renderer>().material = transparentMaterial;
            IzquierdaAbajoActiva = true;
            Behaviour halo = (Behaviour)IzquierdaAba.GetComponent("Halo");
            halo.enabled = true;
        }

        if (aleatorio == 1)
        {
            DerechaAba.GetComponent<Renderer>().material = transparentMaterial;
            DerechaArri.GetComponent<Renderer>().material = transparentMaterial;
            IzquierdaArri.GetComponent<Renderer>().material = iluminateMaterial;
            IzquierdaAba.GetComponent<Renderer>().material = transparentMaterial;
            DerechaMedioAba.GetComponent<Renderer>().material = transparentMaterial;
            DerechaMedioArri.GetComponent<Renderer>().material = transparentMaterial;
            IzquierdaMedioArri.GetComponent<Renderer>().material = transparentMaterial;
            IzquierdaMedioAba.GetComponent<Renderer>().material = transparentMaterial;
            Medio.GetComponent<Renderer>().material = transparentMaterial;
            IzquierdaArribaActiva = true;
            Behaviour halo = (Behaviour)IzquierdaArri.GetComponent("Halo");
            halo.enabled = true;
        }

        if (aleatorio == 2)
        {
            DerechaAba.GetComponent<Renderer>().material = transparentMaterial;
            DerechaArri.GetComponent<Renderer>().material = iluminateMaterial;
            IzquierdaArri.GetComponent<Renderer>().material = transparentMaterial;
            IzquierdaAba.GetComponent<Renderer>().material = transparentMaterial;
            DerechaMedioAba.GetComponent<Renderer>().material = transparentMaterial;
            DerechaMedioArri.GetComponent<Renderer>().material = transparentMaterial;
            IzquierdaMedioArri.GetComponent<Renderer>().material = transparentMaterial;
            IzquierdaMedioAba.GetComponent<Renderer>().material = transparentMaterial;
            Medio.GetComponent<Renderer>().material = transparentMaterial;
            DerechaArribaActiva = true;
            Behaviour halo = (Behaviour)DerechaArri.GetComponent("Halo");
            halo.enabled = true;
        }

        if (aleatorio == 3)
        {
            DerechaAba.GetComponent<Renderer>().material = iluminateMaterial;
            DerechaArri.GetComponent<Renderer>().material = transparentMaterial;
            IzquierdaArri.GetComponent<Renderer>().material = transparentMaterial;
            IzquierdaAba.GetComponent<Renderer>().material = transparentMaterial;
            DerechaMedioAba.GetComponent<Renderer>().material = transparentMaterial;
            DerechaMedioArri.GetComponent<Renderer>().material = transparentMaterial;
            IzquierdaMedioArri.GetComponent<Renderer>().material = transparentMaterial;
            IzquierdaMedioAba.GetComponent<Renderer>().material = transparentMaterial;
            Medio.GetComponent<Renderer>().material = transparentMaterial;
            DerechaAbajoActiva = true;
            Behaviour halo = (Behaviour)DerechaAba.GetComponent("Halo");
            halo.enabled = true;
        }
        if (aleatorio == 4)
        {
            DerechaAba.GetComponent<Renderer>().material = transparentMaterial;
            DerechaArri.GetComponent<Renderer>().material = transparentMaterial;
            IzquierdaArri.GetComponent<Renderer>().material = transparentMaterial;
            IzquierdaAba.GetComponent<Renderer>().material = transparentMaterial;
            DerechaMedioAba.GetComponent<Renderer>().material = iluminateMaterial;
            DerechaMedioArri.GetComponent<Renderer>().material = transparentMaterial;
            IzquierdaMedioArri.GetComponent<Renderer>().material = transparentMaterial;
            IzquierdaMedioAba.GetComponent<Renderer>().material = transparentMaterial;
            Medio.GetComponent<Renderer>().material = transparentMaterial;
            DerechaMedioAbajoActiva = true;
            Behaviour halo = (Behaviour)DerechaMedioAba.GetComponent("Halo");
            halo.enabled = true;
        }
        if (aleatorio == 5)
        {
            DerechaAba.GetComponent<Renderer>().material = transparentMaterial;
            DerechaArri.GetComponent<Renderer>().material = transparentMaterial;
            IzquierdaArri.GetComponent<Renderer>().material = transparentMaterial;
            IzquierdaAba.GetComponent<Renderer>().material = transparentMaterial;
            DerechaMedioAba.GetComponent<Renderer>().material = transparentMaterial;
            DerechaMedioArri.GetComponent<Renderer>().material = iluminateMaterial;
            IzquierdaMedioArri.GetComponent<Renderer>().material = transparentMaterial;
            IzquierdaMedioAba.GetComponent<Renderer>().material = transparentMaterial;
            Medio.GetComponent<Renderer>().material = transparentMaterial;
            DerechaMedioArribaActiva = true;
            Behaviour halo = (Behaviour)DerechaMedioArri.GetComponent("Halo");
            halo.enabled = true;
        }
        if (aleatorio == 6)
        {
            DerechaAba.GetComponent<Renderer>().material = transparentMaterial;
            DerechaArri.GetComponent<Renderer>().material = transparentMaterial;
            IzquierdaArri.GetComponent<Renderer>().material = transparentMaterial;
            IzquierdaAba.GetComponent<Renderer>().material = transparentMaterial;
            DerechaMedioAba.GetComponent<Renderer>().material = transparentMaterial;
            DerechaMedioArri.GetComponent<Renderer>().material = transparentMaterial;
            IzquierdaMedioArri.GetComponent<Renderer>().material = iluminateMaterial;
            IzquierdaMedioAba.GetComponent<Renderer>().material = transparentMaterial;
            Medio.GetComponent<Renderer>().material = transparentMaterial;
            IzquierdaMedioArribaActiva = true;
            Behaviour halo = (Behaviour)IzquierdaMedioArri.GetComponent("Halo");
            halo.enabled = true;
        }
        if (aleatorio == 7)
        {
            DerechaAba.GetComponent<Renderer>().material = transparentMaterial;
            DerechaArri.GetComponent<Renderer>().material = transparentMaterial;
            IzquierdaArri.GetComponent<Renderer>().material = transparentMaterial;
            IzquierdaAba.GetComponent<Renderer>().material = transparentMaterial;
            DerechaMedioAba.GetComponent<Renderer>().material = transparentMaterial;
            DerechaMedioArri.GetComponent<Renderer>().material = transparentMaterial;
            IzquierdaMedioArri.GetComponent<Renderer>().material = transparentMaterial;
            IzquierdaMedioAba.GetComponent<Renderer>().material = iluminateMaterial;
            Medio.GetComponent<Renderer>().material = transparentMaterial;
            IzquierdaMedioAbajoActiva = true;
            Behaviour halo = (Behaviour)IzquierdaMedioAba.GetComponent("Halo");
            halo.enabled = true;
        }

        // Nivel de dificultad
        if (dificultad1 == 0)
        {
            maxIteraciones = 8;
            moveTime = 20;
            tiempoEspera = 3;
            multiplicador = 24;
        }

        if (dificultad1 == 1)
        {
            maxIteraciones = 15;
            moveTime = 10;
            tiempoEspera = 2;
            multiplicador = 24;
        }

        if (dificultad1 == 2)
        {
            maxIteraciones = 25;
            moveTime = 5;
            tiempoEspera = 1;
            multiplicador = 25;
        }
        easeUIComponent.MoveIn();
    }

    // Update is called once per frame
    void Update()
    {

        elapsedTime = Time.time - startTime;
        if(Input.GetKeyDown(KeyCode.Q))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        }

        if (acabado)
        {
            if (elapsedTime > 8)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                //Terapiam.GamePlayManager.EndActivity();
            }
        }
        else
        {
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

                    if (elapsedTime > 2)
                    {
                        startTime = Time.time;
                        elapsedTime = Time.time - startTime;
                        easeUIComponent.MoveOut();
                        presentacion3 = false;
                        presentacion = false;
                        // Empieza a funcionar el reloj y la barra de progreso
                        Reloj.GetComponent<ClockManager>().timeAmt = moveTime;
                        Reloj.GetComponent<ClockManager>().enabled = true;
                        mensajes.text = "¡Pisa el iluminado!";
                    }

                }
            }
            else
            {
                if (!conseguido && elapsedTime >= moveTime && !finJuego)
                {
                    fallos++;
                    if (iteraciones < maxIteraciones - 1)
                    {
                        iteraciones++;
                        conseguido = true;
                        // Para el reloj
                        Reloj.GetComponent<ClockManager>().enabled = false;
                        mensajes.text = "¡Casi!";
                        GetComponent<AudioSource>().clip = fallo;
                        GetComponent<AudioSource>().Play();
                    }
                    else
                    {
                        startTime = Time.time;
                        elapsedTime = Time.time - startTime;
                        finJuego = true;
                        acabado = true;
                        Reloj.GetComponent<ClockManager>().enabled = false;
                        resultado.text = string.Format("Puntos: {0:F0} ", score);
                        if (score > 100 && score < 500)
                            oneStar.SetActive(true);
                        if (score >= 500 && score < 1500)
                            twoStar.SetActive(true);
                        if (score >= 1500)
                            threeStar.SetActive(true);

                        panel.SetActive(true);
                    }
                }
                if (conseguido && espera)
                {
                    estrellitas.SetActive(false);
                    papelitos.SetActive(false);
                    Random.seed = System.DateTime.Now.Millisecond;

                    //Desactivar los halos
                    Behaviour halo1 = (Behaviour)Medio.GetComponent("Halo");
                    halo1.enabled = false;
                    Behaviour halo2 = (Behaviour)IzquierdaMedioAba.GetComponent("Halo");
                    halo2.enabled = false;
                    Behaviour halo3 = (Behaviour)IzquierdaMedioArri.GetComponent("Halo");
                    halo3.enabled = false;
                    Behaviour halo4 = (Behaviour)DerechaMedioArri.GetComponent("Halo");
                    halo4.enabled = false;
                    Behaviour halo5 = (Behaviour)DerechaMedioAba.GetComponent("Halo");
                    halo5.enabled = false;
                    Behaviour halo6 = (Behaviour)DerechaAba.GetComponent("Halo");
                    halo6.enabled = false;
                    Behaviour halo7 = (Behaviour)DerechaArri.GetComponent("Halo");
                    halo7.enabled = false;
                    Behaviour halo8 = (Behaviour)IzquierdaArri.GetComponent("Halo");
                    halo8.enabled = false;
                    Behaviour halo9 = (Behaviour)IzquierdaAba.GetComponent("Halo");
                    halo9.enabled = false;

                    if (dificultad2 == 0)
                    {
                        // Numero aleatorio de 0 a 8
                        do
                        {
                            aleatorio = Random.Range(0, 9);
                        } while (aleatorio == aleatorioTemp);
                        aleatorioTemp = aleatorio;
                    }
                    if (dificultad2 == 1)
                    {
                        // Numero aleatorio de 0 a 7
                        do
                        {
                            // Numero aleatorio de 0 a 7
                            do
                            {
                                aleatorio = Random.Range(0, 8);

                            } while (aleatorio == aleatorioTemp || aleatorio == 5);
                        } while (aleatorio == aleatorioTemp);
                        aleatorioTemp = aleatorio;
                    }
                    if (dificultad2 == 2)
                    {
                        // Numero aleatorio de 0 a 7
                        do
                        {
                            aleatorio = Random.Range(0, 8);

                        } while (aleatorio == aleatorioTemp || aleatorio == 6);
                        aleatorioTemp = aleatorio;
                    }
                    if(dificultad2 == 3)
                    {
                        // Numero aleatorio de 0 a 8
                        do
                        {
                            aleatorio = Random.Range(0, 9);
                        } while (aleatorio == aleatorioTemp);
                        aleatorioTemp = aleatorio;
                    }
                    mensajes.text = "¡Pisa el iluminado!";
                    startTime = Time.time;
                    if (aleatorio == 0)
                    {
                        DerechaAba.GetComponent<Renderer>().material = transparentMaterial;
                        DerechaArri.GetComponent<Renderer>().material = transparentMaterial;
                        IzquierdaArri.GetComponent<Renderer>().material = transparentMaterial;
                        IzquierdaAba.GetComponent<Renderer>().material = iluminateMaterial;
                        DerechaMedioAba.GetComponent<Renderer>().material = transparentMaterial;
                        DerechaMedioArri.GetComponent<Renderer>().material = transparentMaterial;
                        IzquierdaMedioArri.GetComponent<Renderer>().material = transparentMaterial;
                        IzquierdaMedioAba.GetComponent<Renderer>().material = transparentMaterial;
                        Medio.GetComponent<Renderer>().material = transparentMaterial;
                        IzquierdaAbajoActiva = true;
                        Behaviour halo = (Behaviour)IzquierdaAba.GetComponent("Halo");
                        halo.enabled = true;
                    }

                    if (aleatorio == 1)
                    {
                        DerechaAba.GetComponent<Renderer>().material = transparentMaterial;
                        DerechaArri.GetComponent<Renderer>().material = transparentMaterial;
                        IzquierdaArri.GetComponent<Renderer>().material = iluminateMaterial;
                        IzquierdaAba.GetComponent<Renderer>().material = transparentMaterial;
                        DerechaMedioAba.GetComponent<Renderer>().material = transparentMaterial;
                        DerechaMedioArri.GetComponent<Renderer>().material = transparentMaterial;
                        IzquierdaMedioArri.GetComponent<Renderer>().material = transparentMaterial;
                        IzquierdaMedioAba.GetComponent<Renderer>().material = transparentMaterial;
                        Medio.GetComponent<Renderer>().material = transparentMaterial;
                        IzquierdaArribaActiva = true;
                        Behaviour halo = (Behaviour)IzquierdaArri.GetComponent("Halo");
                        halo.enabled = true;
                    }

                    if (aleatorio == 2)
                    {
                        DerechaAba.GetComponent<Renderer>().material = transparentMaterial;
                        DerechaArri.GetComponent<Renderer>().material = iluminateMaterial;
                        IzquierdaArri.GetComponent<Renderer>().material = transparentMaterial;
                        IzquierdaAba.GetComponent<Renderer>().material = transparentMaterial;
                        DerechaMedioAba.GetComponent<Renderer>().material = transparentMaterial;
                        DerechaMedioArri.GetComponent<Renderer>().material = transparentMaterial;
                        IzquierdaMedioArri.GetComponent<Renderer>().material = transparentMaterial;
                        IzquierdaMedioAba.GetComponent<Renderer>().material = transparentMaterial;
                        Medio.GetComponent<Renderer>().material = transparentMaterial;
                        DerechaArribaActiva = true;
                        Behaviour halo = (Behaviour)DerechaArri.GetComponent("Halo");
                        halo.enabled = true;
                    }

                    if (aleatorio == 3)
                    {
                        DerechaAba.GetComponent<Renderer>().material = iluminateMaterial;
                        DerechaArri.GetComponent<Renderer>().material = transparentMaterial;
                        IzquierdaArri.GetComponent<Renderer>().material = transparentMaterial;
                        IzquierdaAba.GetComponent<Renderer>().material = transparentMaterial;
                        DerechaMedioAba.GetComponent<Renderer>().material = transparentMaterial;
                        DerechaMedioArri.GetComponent<Renderer>().material = transparentMaterial;
                        IzquierdaMedioArri.GetComponent<Renderer>().material = transparentMaterial;
                        IzquierdaMedioAba.GetComponent<Renderer>().material = transparentMaterial;
                        Medio.GetComponent<Renderer>().material = transparentMaterial;
                        DerechaAbajoActiva = true;
                        Behaviour halo = (Behaviour)DerechaAba.GetComponent("Halo");
                        halo.enabled = true;
                    }
                    if (aleatorio == 4)
                    {
                        DerechaAba.GetComponent<Renderer>().material = transparentMaterial;
                        DerechaArri.GetComponent<Renderer>().material = transparentMaterial;
                        IzquierdaArri.GetComponent<Renderer>().material = transparentMaterial;
                        IzquierdaAba.GetComponent<Renderer>().material = transparentMaterial;
                        DerechaMedioAba.GetComponent<Renderer>().material = iluminateMaterial;
                        DerechaMedioArri.GetComponent<Renderer>().material = transparentMaterial;
                        IzquierdaMedioArri.GetComponent<Renderer>().material = transparentMaterial;
                        IzquierdaMedioAba.GetComponent<Renderer>().material = transparentMaterial;
                        Medio.GetComponent<Renderer>().material = transparentMaterial;
                        DerechaMedioAbajoActiva = true;
                        Behaviour halo = (Behaviour)DerechaMedioAba.GetComponent("Halo");
                        halo.enabled = true;
                    }
                    if (aleatorio == 5)
                    {
                        DerechaAba.GetComponent<Renderer>().material = transparentMaterial;
                        DerechaArri.GetComponent<Renderer>().material = transparentMaterial;
                        IzquierdaArri.GetComponent<Renderer>().material = transparentMaterial;
                        IzquierdaAba.GetComponent<Renderer>().material = transparentMaterial;
                        DerechaMedioAba.GetComponent<Renderer>().material = transparentMaterial;
                        DerechaMedioArri.GetComponent<Renderer>().material = iluminateMaterial;
                        IzquierdaMedioArri.GetComponent<Renderer>().material = transparentMaterial;
                        IzquierdaMedioAba.GetComponent<Renderer>().material = transparentMaterial;
                        Medio.GetComponent<Renderer>().material = transparentMaterial;
                        DerechaMedioArribaActiva = true;
                        Behaviour halo = (Behaviour)DerechaMedioArri.GetComponent("Halo");
                        halo.enabled = true;
                    }
                    if (aleatorio == 6)
                    {
                        DerechaAba.GetComponent<Renderer>().material = transparentMaterial;
                        DerechaArri.GetComponent<Renderer>().material = transparentMaterial;
                        IzquierdaArri.GetComponent<Renderer>().material = transparentMaterial;
                        IzquierdaAba.GetComponent<Renderer>().material = transparentMaterial;
                        DerechaMedioAba.GetComponent<Renderer>().material = transparentMaterial;
                        DerechaMedioArri.GetComponent<Renderer>().material = transparentMaterial;
                        IzquierdaMedioArri.GetComponent<Renderer>().material = iluminateMaterial;
                        IzquierdaMedioAba.GetComponent<Renderer>().material = transparentMaterial;
                        Medio.GetComponent<Renderer>().material = transparentMaterial;
                        IzquierdaMedioArribaActiva = true;
                        Behaviour halo = (Behaviour)IzquierdaMedioArri.GetComponent("Halo");
                        halo.enabled = true;
                    }
                    if (aleatorio == 7)
                    {
                        DerechaAba.GetComponent<Renderer>().material = transparentMaterial;
                        DerechaArri.GetComponent<Renderer>().material = transparentMaterial;
                        IzquierdaArri.GetComponent<Renderer>().material = transparentMaterial;
                        IzquierdaAba.GetComponent<Renderer>().material = transparentMaterial;
                        DerechaMedioAba.GetComponent<Renderer>().material = transparentMaterial;
                        DerechaMedioArri.GetComponent<Renderer>().material = transparentMaterial;
                        IzquierdaMedioArri.GetComponent<Renderer>().material = transparentMaterial;
                        IzquierdaMedioAba.GetComponent<Renderer>().material = iluminateMaterial;
                        Medio.GetComponent<Renderer>().material = transparentMaterial;
                        IzquierdaMedioAbajoActiva = true;
                        Behaviour halo = (Behaviour)IzquierdaMedioAba.GetComponent("Halo");
                        halo.enabled = true;
                    }
                    if (aleatorio == 8)
                    {
                        DerechaAba.GetComponent<Renderer>().material = transparentMaterial;
                        DerechaArri.GetComponent<Renderer>().material = transparentMaterial;
                        IzquierdaArri.GetComponent<Renderer>().material = transparentMaterial;
                        IzquierdaAba.GetComponent<Renderer>().material = transparentMaterial;
                        DerechaMedioAba.GetComponent<Renderer>().material = transparentMaterial;
                        DerechaMedioArri.GetComponent<Renderer>().material = transparentMaterial;
                        IzquierdaMedioArri.GetComponent<Renderer>().material = transparentMaterial;
                        IzquierdaMedioAba.GetComponent<Renderer>().material = transparentMaterial;
                        Medio.GetComponent<Renderer>().material = iluminateMaterial;
                        MedioActiva = true;
                        Behaviour halo = (Behaviour)Medio.GetComponent("Halo");
                        halo.enabled = true;
                    }
                    conseguido = false;
                    espera = false;
                    Reloj.GetComponent<Image>().fillAmount = 1;
                    Reloj.GetComponent<ClockManager>().timeAmt = moveTime;
                    Reloj.GetComponent<ClockManager>().time = moveTime;
                    Reloj.GetComponent<ClockManager>().enabled = true;
                }
                if (conseguido && !espera)
                {
                    elapsedTime = Time.time - startTime;
                    if (elapsedTime >= tiempoEspera)
                    {
                        espera = true;
                    }
                }
            }
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (!presentacion)
        {
            if (other.gameObject.tag == "rightUp" && DerechaArribaActiva && !conseguido)
            {
                aciertos++;
                DerechaArribaActiva = false;
                if (iteraciones < maxIteraciones - 1)
                {
                    iteraciones++;
                    conseguido = true;
                    score = score + (int)(moveTime - elapsedTime) * multiplicador;
                    // Para el reloj
                    Reloj.GetComponent<ClockManager>().enabled = false;
                    mensajes.text = "¡Bien!";
                    GetComponent<AudioSource>().clip = acierto;
                    GetComponent<AudioSource>().Play();
                    startTime = Time.time;
                    aleatorio2 = Random.Range(0, 2);

                    if (aleatorio2 == 0)
                        estrellitas.SetActive(true);

                    if (aleatorio2 == 1)
                        papelitos.SetActive(true);

                }
                else
                {
                    startTime = Time.time;
                    elapsedTime = Time.time - startTime;
                    acabado = true;

                    // Para el reloj
                    GetComponent<AudioSource>().clip = final;
                    GetComponent<AudioSource>().Play();
                    Reloj.GetComponent<ClockManager>().enabled = false;
                    resultado.text = string.Format("Puntos: {0:F0} ", score);
                    if (score > 100 && score < 500)
                        oneStar.SetActive(true);
                    if (score >= 500 && score < 1500)
                        twoStar.SetActive(true);
                    if (score >= 1500)
                        threeStar.SetActive(true);

                    panel.SetActive(true);
                }

            }

            if (other.gameObject.tag == "leftUp" && IzquierdaArribaActiva && !conseguido)
            {
                aciertos++;
                IzquierdaArribaActiva = false;
                if (iteraciones < maxIteraciones - 1)
                {
                    iteraciones++;
                    conseguido = true;
                    score = score + (int)(moveTime - elapsedTime) * multiplicador;
                    // Para el reloj
                    Reloj.GetComponent<ClockManager>().enabled = false;
                    mensajes.text = "¡Bien!";
                    GetComponent<AudioSource>().clip = acierto;
                    GetComponent<AudioSource>().Play();
                    startTime = Time.time;
                    aleatorio2 = Random.Range(0, 2);

                    if (aleatorio2 == 0)
                        estrellitas.SetActive(true);

                    if (aleatorio2 == 1)
                        papelitos.SetActive(true);

                }
                else
                {
                    startTime = Time.time;
                    elapsedTime = Time.time - startTime;
                    acabado = true;

                    // Para el reloj
                    GetComponent<AudioSource>().clip = final;
                    GetComponent<AudioSource>().Play();
                    Reloj.GetComponent<ClockManager>().enabled = false;
                    resultado.text = string.Format("Puntos: {0:F0} ", score);
                    if (score > 100 && score < 500)
                        oneStar.SetActive(true);
                    if (score >= 500 && score < 1500)
                        twoStar.SetActive(true);
                    if (score >= 1500)
                        threeStar.SetActive(true);

                    panel.SetActive(true);
                }
            }



            if (other.gameObject.tag == "leftDown" && IzquierdaAbajoActiva && !conseguido)
            {
                aciertos++;
                IzquierdaAbajoActiva = false;
                if (iteraciones < maxIteraciones - 1)
                {
                    iteraciones++;
                    conseguido = true;
                    score = score + (int)(moveTime - elapsedTime) * multiplicador;
                    // Para el reloj
                    Reloj.GetComponent<ClockManager>().enabled = false;
                    mensajes.text = "¡Bien!";
                    GetComponent<AudioSource>().clip = acierto;
                    GetComponent<AudioSource>().Play();
                    aleatorio2 = Random.Range(0, 2);

                    if (aleatorio2 == 0)
                        estrellitas.SetActive(true);

                    if (aleatorio2 == 1)
                        papelitos.SetActive(true);

                    startTime = Time.time;

                }
                else
                {
                    startTime = Time.time;
                    elapsedTime = Time.time - startTime;
                    acabado = true;

                    // Para el reloj
                    GetComponent<AudioSource>().clip = final;
                    GetComponent<AudioSource>().Play();
                    Reloj.GetComponent<ClockManager>().enabled = false;
                    resultado.text = string.Format("Puntos: {0:F0} ", score);
                    if (score > 100 && score < 500)
                        oneStar.SetActive(true);
                    if (score >= 500 && score < 1500)
                        twoStar.SetActive(true);
                    if (score >= 1500)
                        threeStar.SetActive(true);

                    panel.SetActive(true);
                }
            }

            if (other.gameObject.tag == "rightDown" && DerechaAbajoActiva && !conseguido)
            {
                aciertos++;
                DerechaAbajoActiva = false;
                if (iteraciones < maxIteraciones - 1)
                {
                    iteraciones++;
                    conseguido = true;
                    score = score + (int)(moveTime - elapsedTime) * multiplicador;
                    // Para el reloj
                    Reloj.GetComponent<ClockManager>().enabled = false;
                    mensajes.text = "¡Bien!";
                    GetComponent<AudioSource>().clip = acierto;
                    GetComponent<AudioSource>().Play();
                    aleatorio2 = Random.Range(0, 2);

                    if (aleatorio2 == 0)
                        estrellitas.SetActive(true);

                    if (aleatorio2 == 1)
                        papelitos.SetActive(true);

                    startTime = Time.time;

                }
                else
                {
                    startTime = Time.time;
                    elapsedTime = Time.time - startTime;
                    acabado = true;

                    // Para el reloj
                    GetComponent<AudioSource>().clip = final;
                    GetComponent<AudioSource>().Play();
                    Reloj.GetComponent<ClockManager>().enabled = false;
                    resultado.text = string.Format("Puntos: {0:F0} ", score);
                    if (score > 100 && score < 500)
                        oneStar.SetActive(true);
                    if (score >= 500 && score < 1500)
                        twoStar.SetActive(true);
                    if (score >= 1500)
                        threeStar.SetActive(true);

                    panel.SetActive(true);
                }

            }

            if (other.gameObject.tag == "rightMiddleDown" && DerechaMedioAbajoActiva && !conseguido)
            {
                aciertos++;
                DerechaMedioAbajoActiva = false;
                if (iteraciones < maxIteraciones - 1)
                {
                    iteraciones++;
                    conseguido = true;
                    score = score + (int)(moveTime - elapsedTime) * multiplicador;
                    // Para el reloj
                    Reloj.GetComponent<ClockManager>().enabled = false;
                    mensajes.text = "¡Bien!";
                    GetComponent<AudioSource>().clip = acierto;
                    GetComponent<AudioSource>().Play();
                    aleatorio2 = Random.Range(0, 2);

                    if (aleatorio2 == 0)
                        estrellitas.SetActive(true);

                    if (aleatorio2 == 1)
                        papelitos.SetActive(true);

                    startTime = Time.time;

                }
                else
                {
                    startTime = Time.time;
                    elapsedTime = Time.time - startTime;
                    acabado = true;

                    // Para el reloj
                    GetComponent<AudioSource>().clip = final;
                    GetComponent<AudioSource>().Play();
                    Reloj.GetComponent<ClockManager>().enabled = false;
                    resultado.text = string.Format("Puntos: {0:F0} ", score);
                    if (score > 100 && score < 500)
                        oneStar.SetActive(true);
                    if (score >= 500 && score < 1500)
                        twoStar.SetActive(true);
                    if (score >= 1500)
                        threeStar.SetActive(true);

                    panel.SetActive(true);
                }

            }

            if (other.gameObject.tag == "rightMiddleUp" && DerechaMedioArribaActiva && !conseguido)
            {
                aciertos++;
                DerechaMedioArribaActiva = false;
                if (iteraciones < maxIteraciones - 1)
                {
                    iteraciones++;
                    conseguido = true;
                    score = score + (int)(moveTime - elapsedTime) * multiplicador;
                    // Para el reloj
                    Reloj.GetComponent<ClockManager>().enabled = false;
                    mensajes.text = "¡Bien!";
                    GetComponent<AudioSource>().clip = acierto;
                    GetComponent<AudioSource>().Play();
                    aleatorio2 = Random.Range(0, 2);

                    if (aleatorio2 == 0)
                        estrellitas.SetActive(true);

                    if (aleatorio2 == 1)
                        papelitos.SetActive(true);

                    startTime = Time.time;

                }
                else
                {
                    startTime = Time.time;
                    elapsedTime = Time.time - startTime;
                    acabado = true;

                    // Para el reloj
                    GetComponent<AudioSource>().clip = final;
                    GetComponent<AudioSource>().Play();
                    Reloj.GetComponent<ClockManager>().enabled = false;
                    resultado.text = string.Format("Puntos: {0:F0} ", score);
                    if (score > 100 && score < 500)
                        oneStar.SetActive(true);
                    if (score >= 500 && score < 1500)
                        twoStar.SetActive(true);
                    if (score >= 1500)
                        threeStar.SetActive(true);

                    panel.SetActive(true);
                }

            }

            if (other.gameObject.tag == "leftMiddleDown" && IzquierdaMedioAbajoActiva && !conseguido)
            {
                aciertos++;
                IzquierdaMedioAbajoActiva = false;
                if (iteraciones < maxIteraciones - 1)
                {
                    iteraciones++;
                    conseguido = true;
                    score = score + (int)(moveTime - elapsedTime) * multiplicador;
                    // Para el reloj
                    Reloj.GetComponent<ClockManager>().enabled = false;
                    mensajes.text = "¡Bien!";
                    GetComponent<AudioSource>().clip = acierto;
                    GetComponent<AudioSource>().Play();
                    aleatorio2 = Random.Range(0, 2);

                    if (aleatorio2 == 0)
                        estrellitas.SetActive(true);

                    if (aleatorio2 == 1)
                        papelitos.SetActive(true);

                    startTime = Time.time;

                }
                else
                {
                    startTime = Time.time;
                    elapsedTime = Time.time - startTime;
                    acabado = true;

                    // Para el reloj
                    GetComponent<AudioSource>().clip = final;
                    GetComponent<AudioSource>().Play();
                    Reloj.GetComponent<ClockManager>().enabled = false;
                    resultado.text = string.Format("Puntos: {0:F0} ", score);
                    if (score > 100 && score < 500)
                        oneStar.SetActive(true);
                    if (score >= 500 && score < 1500)
                        twoStar.SetActive(true);
                    if (score >= 1500)
                        threeStar.SetActive(true);

                    panel.SetActive(true);
                }
            }

            if (other.gameObject.tag == "leftMiddleUp" && IzquierdaMedioArribaActiva && !conseguido)
            {
                aciertos++;
                IzquierdaMedioArribaActiva = false;
                if (iteraciones < maxIteraciones - 1)
                {
                    iteraciones++;
                    conseguido = true;
                    score = score + (int)(moveTime - elapsedTime) * multiplicador;
                    // Para el reloj
                    Reloj.GetComponent<ClockManager>().enabled = false;
                    mensajes.text = "¡Bien!";
                    GetComponent<AudioSource>().clip = acierto;
                    GetComponent<AudioSource>().Play();
                    aleatorio2 = Random.Range(0, 2);

                    if (aleatorio2 == 0)
                        estrellitas.SetActive(true);

                    if (aleatorio2 == 1)
                        papelitos.SetActive(true);

                    startTime = Time.time;

                }
                else
                {
                    startTime = Time.time;
                    elapsedTime = Time.time - startTime;
                    acabado = true;

                    // Para el reloj
                    GetComponent<AudioSource>().clip = final;
                    GetComponent<AudioSource>().Play();
                    Reloj.GetComponent<ClockManager>().enabled = false;
                    resultado.text = string.Format("Puntos: {0:F0} ", score);
                    if (score > 100 && score < 500)
                        oneStar.SetActive(true);
                    if (score >= 500 && score < 1500)
                        twoStar.SetActive(true);
                    if (score >= 1500)
                        threeStar.SetActive(true);

                    panel.SetActive(true);
                }
            }
            if (other.gameObject.tag == "Middle" && MedioActiva && !conseguido)
            {
                MedioActiva = false;
                aciertos++;
                if (iteraciones < maxIteraciones - 1)
                {
                    iteraciones++;
                    conseguido = true;
                    score = score + (int)(moveTime - elapsedTime) * multiplicador;
                    // Para el reloj
                    Reloj.GetComponent<ClockManager>().enabled = false;
                    mensajes.text = "¡Bien!";
                    GetComponent<AudioSource>().clip = acierto;
                    GetComponent<AudioSource>().Play();
                    aleatorio2 = Random.Range(0, 2);

                    if (aleatorio2 == 0)
                        estrellitas.SetActive(true);

                    if (aleatorio2 == 1)
                        papelitos.SetActive(true);

                    startTime = Time.time;

                }
                else
                {
                    startTime = Time.time;
                    elapsedTime = Time.time - startTime;
                    acabado = true;

                    // Para el reloj
                    GetComponent<AudioSource>().clip = final;
                    GetComponent<AudioSource>().Play();
                    Reloj.GetComponent<ClockManager>().enabled = false;
                    resultado.text = string.Format("Puntos: {0:F0} ", score);
                    if (score > 100 && score < 500)
                        oneStar.SetActive(true);
                    if (score >= 500 && score < 1500)
                        twoStar.SetActive(true);
                    if (score >= 1500)
                        threeStar.SetActive(true);
                    panel.SetActive(true);
                }
            }
        }
    }
}
