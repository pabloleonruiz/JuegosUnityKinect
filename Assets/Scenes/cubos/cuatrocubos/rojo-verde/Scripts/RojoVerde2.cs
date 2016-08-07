﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using EaseTools;
using UnityEngine.SceneManagement;


public class RojoVerde2 : MonoBehaviour {
    public int dificultad1 = 0;
    public int dificultad2 = 0;
    public int escenario = 0;
    public int score = 0; // Puntuación
    public int aciertos = 0;
    public int fallos = 0;
    public int lineas = 0;

    public EaseUI easeUIComponent;
    public GameObject estrellitas;
    public GameObject papelitos;
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
    public GameObject panel;
    public GameObject oneStar;
    public GameObject twoStar;
    public GameObject threeStar;
    public Text resultado;
    public Text mensajes; // Mensajes para el usuario
    public Text textoBoton;
    public AudioClip final;
    public AudioClip acierto;
    public AudioClip fallo;

    private Color colorVerde = new Color32(0, 197, 26, 120); // Color verde
    private Color colorRojo = new Color32(253, 76, 76, 120); // Color rojo
    private int maxIteraciones = 1;
    private int iteraciones = 0;
    private float moveTime = 0f; // Tiempo para tocar el pilar
    private float startTime = 0f;
    private float elapsedTime = 0f;
    private bool DerechaAbajoActiva = false;
    private bool IzquierdaAbajoActiva = false;
    private bool DerechaArribaActiva = false;
    private bool IzquierdaArribaActiva = false;
    private bool conseguido = false;
    private int aleatorio;
    private int aleatorio2;
    private bool espera = false;
    private float tiempoEspera = 0;
    private GameObject DerechaAba, DerechaArri, IzquierdaAba, IzquierdaArri, Suelo, Fondo, Reloj;
    private int multiplicador;
    private int aleatorioTemp;
    private bool presentacion = true;
    private bool presentacion1 = true;
    private bool presentacion2 = false;
    private bool presentacion3 = false;
    private bool acabado = false;
    private bool finJuego = false;

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

        // Numero aleatorio de 0 a 3
        aleatorio = Random.Range(0, 4);
        aleatorioTemp = aleatorio;
        startTime = Time.time;

        // Objetos del escenario
        DerechaAba = GameObject.Find("Pilares/DerechaAbajo");
        DerechaArri = GameObject.Find("Pilares/DerechaArriba");
        IzquierdaArri = GameObject.Find("Pilares/IzquierdaArriba");
        IzquierdaAba = GameObject.Find("Pilares/IzquierdaAbajo");
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

        // Primera seleccion del bloque verde
        if (aleatorio == 0)
        {
            DerechaAba.GetComponent<Renderer>().materials[0].color = colorRojo;
            DerechaArri.GetComponent<Renderer>().materials[0].color = colorRojo;
            IzquierdaArri.GetComponent<Renderer>().materials[0].color = colorRojo;
            IzquierdaAba.GetComponent<Renderer>().materials[0].color = colorVerde;
            IzquierdaAbajoActiva = true;
        }

        if (aleatorio == 1)
        {
            DerechaAba.GetComponent<Renderer>().materials[0].color = colorRojo;
            DerechaArri.GetComponent<Renderer>().materials[0].color = colorRojo;
            IzquierdaArri.GetComponent<Renderer>().materials[0].color = colorVerde;
            IzquierdaAba.GetComponent<Renderer>().materials[0].color = colorRojo;
            IzquierdaArribaActiva = true;
        }

        if (aleatorio == 2)
        {
            DerechaAba.GetComponent<Renderer>().materials[0].color = colorRojo;
            DerechaArri.GetComponent<Renderer>().materials[0].color = colorVerde;
            IzquierdaArri.GetComponent<Renderer>().materials[0].color = colorRojo;
            IzquierdaAba.GetComponent<Renderer>().materials[0].color = colorRojo;
            DerechaArribaActiva = true;
        }

        if (aleatorio == 3)
        {
            DerechaAba.GetComponent<Renderer>().materials[0].color = colorVerde;
            DerechaArri.GetComponent<Renderer>().materials[0].color = colorRojo;
            IzquierdaArri.GetComponent<Renderer>().materials[0].color = colorRojo;
            IzquierdaAba.GetComponent<Renderer>().materials[0].color = colorRojo;
            DerechaAbajoActiva = true;
        }

        if(dificultad1==0)
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

        if (dificultad2 == 1)
        {
            DerechaAba.GetComponent<Transform>().position= new Vector3((DerechaAba.GetComponent<Transform>().position.x) + 0.2f,
                DerechaAba.GetComponent<Transform>().position.y, DerechaAba.GetComponent<Transform>().position.z);

            DerechaArri.GetComponent<Transform>().position = new Vector3((DerechaArri.GetComponent<Transform>().position.x) + 0.2f,
                DerechaArri.GetComponent<Transform>().position.y, DerechaArri.GetComponent<Transform>().position.z);

            IzquierdaAba.GetComponent<Transform>().position = new Vector3((IzquierdaAba.GetComponent<Transform>().position.x) - 0.2f,
              IzquierdaAba.GetComponent<Transform>().position.y, IzquierdaAba.GetComponent<Transform>().position.z);

            IzquierdaArri.GetComponent<Transform>().position = new Vector3((IzquierdaArri.GetComponent<Transform>().position.x) - 0.2f,
              IzquierdaArri.GetComponent<Transform>().position.y, IzquierdaArri.GetComponent<Transform>().position.z);
        }

        if (dificultad2 == 2)
        {
            DerechaAba.GetComponent<Transform>().position = new Vector3((DerechaAba.GetComponent<Transform>().position.x) + 0.3f,
                DerechaAba.GetComponent<Transform>().position.y, DerechaAba.GetComponent<Transform>().position.z);

            DerechaArri.GetComponent<Transform>().position = new Vector3((DerechaArri.GetComponent<Transform>().position.x) + 0.4f,
                DerechaArri.GetComponent<Transform>().position.y, DerechaArri.GetComponent<Transform>().position.z);

            IzquierdaAba.GetComponent<Transform>().position = new Vector3((IzquierdaAba.GetComponent<Transform>().position.x) - 0.3f,
                IzquierdaAba.GetComponent<Transform>().position.y, IzquierdaAba.GetComponent<Transform>().position.z);

            IzquierdaArri.GetComponent<Transform>().position = new Vector3((IzquierdaArri.GetComponent<Transform>().position.x) - 0.4f,
                IzquierdaArri.GetComponent<Transform>().position.y, IzquierdaArri.GetComponent<Transform>().position.z);
        }

        easeUIComponent.MoveIn();
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime = Time.time - startTime;
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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
                        mensajes.text = "¡A por el verde!";
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
                        Reloj.GetComponent<ClockManager>().enabled = false;
                        resultado.text = string.Format("Puntos: {0:F0} ", score);
                        if (score > 100 && score < 500)
                            oneStar.SetActive(true);
                        if (score >= 500 && score < 1500)
                            twoStar.SetActive(true);
                        if (score >= 1500)
                            threeStar.SetActive(true);

                        panel.SetActive(true);
                        finJuego = true;
                        acabado = true;
                    }
                }
                if (conseguido && espera)
                {
                    estrellitas.SetActive(false);
                    papelitos.SetActive(false);
                    mensajes.text = "¡A por el verde!";
                    Random.seed = System.DateTime.Now.Millisecond;
                    do
                    {
                        aleatorio = Random.Range(0, 4);
                    } while (aleatorio == aleatorioTemp);
                    aleatorioTemp = aleatorio;
                    startTime = Time.time;
                    if (aleatorio == 0)
                    {
                        DerechaAba.GetComponent<Renderer>().materials[0].color = colorRojo;
                        DerechaArri.GetComponent<Renderer>().materials[0].color = colorRojo;
                        IzquierdaArri.GetComponent<Renderer>().materials[0].color = colorRojo;
                        IzquierdaAba.GetComponent<Renderer>().materials[0].color = colorVerde;
                        IzquierdaAbajoActiva = true;
                    }

                    if (aleatorio == 1)
                    {
                        DerechaAba.GetComponent<Renderer>().materials[0].color = colorRojo;
                        DerechaArri.GetComponent<Renderer>().materials[0].color = colorRojo;
                        IzquierdaArri.GetComponent<Renderer>().materials[0].color = colorVerde;
                        IzquierdaAba.GetComponent<Renderer>().materials[0].color = colorRojo;
                        IzquierdaArribaActiva = true;
                    }

                    if (aleatorio == 2)
                    {
                        DerechaAba.GetComponent<Renderer>().materials[0].color = colorRojo;
                        DerechaArri.GetComponent<Renderer>().materials[0].color = colorVerde;
                        IzquierdaArri.GetComponent<Renderer>().materials[0].color = colorRojo;
                        IzquierdaAba.GetComponent<Renderer>().materials[0].color = colorRojo;
                        DerechaArribaActiva = true;
                    }

                    if (aleatorio == 3)
                    {
                        DerechaAba.GetComponent<Renderer>().materials[0].color = colorVerde;
                        DerechaArri.GetComponent<Renderer>().materials[0].color = colorRojo;
                        IzquierdaArri.GetComponent<Renderer>().materials[0].color = colorRojo;
                        IzquierdaAba.GetComponent<Renderer>().materials[0].color = colorRojo;
                        DerechaAbajoActiva = true;
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
                    // Para el reloj
                    startTime = Time.time;
                    elapsedTime = Time.time - startTime;
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
                    acabado = true;

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
                    // Para el reloj
                    startTime = Time.time;
                    elapsedTime = Time.time - startTime;
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
                    acabado = true;

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
                    // Para el reloj
                    startTime = Time.time;
                    elapsedTime = Time.time - startTime;
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
                    acabado = true;

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
                    startTime = Time.time;
                    elapsedTime = Time.time - startTime;

                    panel.SetActive(true);
                    acabado = true;

                }
            }

        }
    }
}