using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using EaseTools;
using UnityEngine.SceneManagement;

public class ScriptEjercicio6 : MonoBehaviour {
    public int dificultad1 = 0; // Nivel de dificultad
    public int escenario = 0;
    public int score = 0; // Puntuación
    public int aciertos = 0; // Poses completadas correctamente
    public int fallos = 0; // Poses completadas correctamente
    public int lineas = 0;

    public EaseUI easeUIComponent;
    Animator animator;
    private Color colorDefault = new Color32(255, 255, 255, 141); // Color por defecto del avatar
    private float moveTime = 0f; // Tiempo para adoptar la postura
    private float waitTime = 0f; // Tiempo para mantener la postura
    private float porcentaje = 0f; // Porcentaje de similitud para dar por buena la pose
    private int aleatorio = 0; // Numero aleatorio para efectos al completar la pose
    private bool puntuacion = true;
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
    public GameObject oneStar;
    public GameObject twoStar;
    public GameObject threeStar;
    private float startTime = 0f;
    private float elapsedTime = 0f;
    private bool conseguido1 = false, conseguido2 = false;
    private bool actualizado1 = false, actualizado2 = false;
    private bool resultados = true;
    public Text mensajes; // Mensajes para el usuario
    public Text resultado;
    public Text textoBoton;
    public GameObject panel;
    public AudioClip final;
    public AudioClip acierto;
    public AudioClip fallo;
    private int multiplicador;
    private bool presentacion = true;
    private bool presentacion1 = true;
    private bool presentacion2 = false;
    private bool presentacion3 = false;
    private GameObject Suelo, Fondo;
    private bool acabado = false;
    private bool puntuado = true;
    private int oldScore = 0;
    private bool control = true;
    private bool control2 = true;
    private bool control3 = true;

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

        Suelo = GameObject.Find("PavedFloor");
        Fondo = GameObject.Find("Quad");

        animator = GetComponent<Animator>();

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
        //Selección de dificultad
        if (dificultad1 == 0)
        {
            moveTime = 40;
            waitTime = 8;
            porcentaje = 0.3f;
            multiplicador = 30;
        }
        if (dificultad1 == 1)
        {
            moveTime = 20;
            waitTime = 20;
            porcentaje = 0.5f;
            multiplicador = 60;
        }
        if (dificultad1 == 2)
        {
            moveTime = 10;
            waitTime = 40;
            porcentaje = 0.7f;
            multiplicador = 120;
        }
        GameManager.minimo = porcentaje;
        easeUIComponent.MoveIn();
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime = Time.time - startTime;
        ////// Para pruebas //////
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        /////////////////////


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
                        animator.SetBool("Posar6", true);
                        presentacion = false;
                    }

                }
            }
            else
            {
                if (animator.GetCurrentAnimatorStateInfo(0).IsName("Pose_6-1"))
                {
                    // Actualiza el estado del avatar
                    if (!actualizado1)
                    {
                        startTime = Time.time;
                        elapsedTime = Time.time - startTime;

                        // Empieza a funcionar el reloj y la barra de progreso
                        GameObject.Find("Canvas/RelojTiempo").GetComponent<ClockManager>().timeAmt = moveTime;
                        GameObject.Find("Canvas/RelojTiempo").GetComponent<ClockManager>().enabled = true;

                        GameObject.Find("KinectController").GetComponent<PoseDetectorScript>().poseJoints.Add(KinectInterop.JointType.HandRight);
                        GameObject.Find("KinectController").GetComponent<PoseDetectorScript>().poseJoints.Add(KinectInterop.JointType.ShoulderRight);
                        GameObject.Find("KinectController").GetComponent<PoseDetectorScript>().poseJoints.Add(KinectInterop.JointType.ElbowRight);
                        GameObject.Find("KinectController").GetComponent<PoseDetectorScript>().poseJoints.Add(KinectInterop.JointType.FootLeft);
                        GameObject.Find("KinectController").GetComponent<PoseDetectorScript>().poseJoints.Add(KinectInterop.JointType.HipLeft);
                        GameObject.Find("KinectController").GetComponent<PoseDetectorScript>().poseJoints.Add(KinectInterop.JointType.AnkleLeft);

                        // Activa los scripts para iluminar las partes del cuerpo

                        GameObject.Find("modelo/Brazo_izquierdo").GetComponent<ChangeColorIntensityBrazoIzquierdo>().enabled = true;
                        GameObject.Find("modelo/Pierna_derecha").GetComponent<ChangeColorIntensityPiernaDerecha>().enabled = true;

                        mensajes.text = "Imita la pose";

                        actualizado1 = true;
                    }

                    else
                    {
                        // Cumple el porcentaje para conseguir la pose 
                        if (porcentaje <= GameManager.percent && !conseguido1 && control)
                        {
                            startTime = Time.time;
                            elapsedTime = Time.time - startTime;

                            control = false;

                            if (puntuado)
                            {
                                aciertos++;
                                puntuado = false;
                            }
                            mensajes.text = "¡Aguanta!";

                            // Para el reloj
                            GameObject.Find("Canvas/RelojTiempo").GetComponent<ClockManager>().enabled = false;

                            // Activa el tiempo de aguante
                            GameObject.Find("Canvas/RelojAguante").GetComponent<ClockManager>().timeAmt = waitTime;
                            GameObject.Find("Canvas/RelojAguante").GetComponent<ClockManager>().enabled = true;

                            if (puntuacion)
                            {
                                score = score + ((int)(moveTime - elapsedTime) * multiplicador);
                                puntuacion = false;
                            }
                            conseguido1 = true;
                        }

                        // Aviso para mantener la postura
                        if (conseguido1 && control2)
                        {
                            if (GameManager.percent < (porcentaje - 0.2))
                            {
                                // Mensaje avisando: manten la postura
                                mensajes.text = "¡No te muevas!";
                                if (score > oldScore)
                                    score = score - 5;
                            }
                            else
                                mensajes.text = "¡Aguanta!";
                        }

                        // Mantener la postura un tiempo (waitTime)
                        if (elapsedTime >= waitTime && conseguido1 && control2)
                        {
                            control2 = false;
                            mensajes.text = "¡Perfecto!";
                            GetComponent<AudioSource>().clip = acierto;
                            GetComponent<AudioSource>().Play();
                            Random.seed = System.DateTime.Now.Millisecond;
                            aleatorio = Random.Range(0, 2);

                            if (aleatorio == 0)
                                estrellitas.SetActive(true);

                            if (aleatorio == 1)
                                papelitos.SetActive(true);

                            animator.SetBool("6-1Completado", true);
                        }

                        // No se ha logrado hacer la pose en el tiempo elegido (movetime)
                        if (elapsedTime >= moveTime && !conseguido1 && control3)
                        {
                            control3 = false;
                            if (puntuado)
                            {
                                fallos++;
                                puntuado = false;
                            }
                            mensajes.text = "¡Casi!";
                            GetComponent<AudioSource>().clip = fallo;
                            GetComponent<AudioSource>().Play();
                            animator.SetBool("6-1Completado", true);
                        }
                    }
                }

                if (animator.GetCurrentAnimatorStateInfo(0).IsName("Ejercicio_6-2"))
                {
                    GameObject.Find("KinectController").GetComponent<PoseDetectorScript>().poseJoints.Remove(KinectInterop.JointType.HandRight);
                    GameObject.Find("KinectController").GetComponent<PoseDetectorScript>().poseJoints.Remove(KinectInterop.JointType.ShoulderRight);
                    GameObject.Find("KinectController").GetComponent<PoseDetectorScript>().poseJoints.Remove(KinectInterop.JointType.ElbowRight);
                    GameObject.Find("KinectController").GetComponent<PoseDetectorScript>().poseJoints.Remove(KinectInterop.JointType.FootLeft);
                    GameObject.Find("KinectController").GetComponent<PoseDetectorScript>().poseJoints.Remove(KinectInterop.JointType.HipLeft);
                    GameObject.Find("KinectController").GetComponent<PoseDetectorScript>().poseJoints.Remove(KinectInterop.JointType.AnkleLeft);

                    GameObject.Find("modelo/Brazo_izquierdo").GetComponent<ChangeColorIntensityBrazoIzquierdo>().enabled = false;
                    GameObject.Find("modelo/Brazo_izquierdo").GetComponent<Renderer>().materials[0].color = colorDefault;
                    GameObject.Find("modelo/Pierna_derecha").GetComponent<ChangeColorIntensityPiernaDerecha>().enabled = false;
                    GameObject.Find("modelo/Pierna_derecha").GetComponent<Renderer>().materials[0].color = colorDefault;

                    GameObject.Find("Canvas/RelojTiempo").GetComponent<ClockManager>().enabled = false;
                    GameObject.Find("Canvas/RelojAguante").GetComponent<ClockManager>().enabled = false;
                    oldScore = score;

                    puntuacion = true;
                    puntuado = true;

                }

                if (animator.GetCurrentAnimatorStateInfo(0).IsName("Pose_6-2"))
                {

                    // Actualiza el estado del avatar
                    if (!actualizado2)
                    {
                        startTime = Time.time;
                        elapsedTime = Time.time - startTime;

                        control = true;
                        control2 = true;
                        control3 = true;

                        papelitos.SetActive(false);
                        estrellitas.SetActive(false);

                        GameObject.Find("Canvas/RelojAguante").GetComponent<Image>().fillAmount = 1;
                        GameObject.Find("Canvas/RelojTiempo").GetComponent<Image>().fillAmount = 1;

                        GameObject.Find("Canvas/RelojTiempo").GetComponent<ClockManager>().timeAmt = moveTime;
                        GameObject.Find("Canvas/RelojTiempo").GetComponent<ClockManager>().time = moveTime;
                        GameObject.Find("Canvas/RelojTiempo").GetComponent<ClockManager>().enabled = true;

                        GameObject.Find("KinectController").GetComponent<PoseDetectorScript>().poseJoints.Add(KinectInterop.JointType.HandLeft);
                        GameObject.Find("KinectController").GetComponent<PoseDetectorScript>().poseJoints.Add(KinectInterop.JointType.ShoulderLeft);
                        GameObject.Find("KinectController").GetComponent<PoseDetectorScript>().poseJoints.Add(KinectInterop.JointType.ElbowLeft);
                        GameObject.Find("KinectController").GetComponent<PoseDetectorScript>().poseJoints.Add(KinectInterop.JointType.FootRight);
                        GameObject.Find("KinectController").GetComponent<PoseDetectorScript>().poseJoints.Add(KinectInterop.JointType.HipRight);
                        GameObject.Find("KinectController").GetComponent<PoseDetectorScript>().poseJoints.Add(KinectInterop.JointType.AnkleRight);

                        // Activa los scripts para iluminar las partes del cuerpo

                        GameObject.Find("modelo/Brazo_derecho").GetComponent<ChangeColorIntensityBrazoDerecho>().enabled = true;
                        GameObject.Find("modelo/Pierna_izquierda").GetComponent<ChangeColorIntensityPiernaIzquierda>().enabled = true;

                        mensajes.text = "Imita la pose";

                        actualizado2 = true;
                    }

                    else
                    {
                        // Cumple el porcentaje para conseguir la pose 
                        if (porcentaje <= GameManager.percent && !conseguido2 && control)
                        {
                            startTime = Time.time;
                            elapsedTime = Time.time - startTime;

                            control = false;

                            if (puntuado)
                            {
                                aciertos++;
                                puntuado = false;
                            }
                            mensajes.text = "¡Aguanta!";

                            // Para el reloj
                            GameObject.Find("Canvas/RelojTiempo").GetComponent<ClockManager>().enabled = false;

                            // Activa el tiempo de aguante
                            GameObject.Find("Canvas/RelojAguante").GetComponent<ClockManager>().timeAmt = waitTime;
                            GameObject.Find("Canvas/RelojAguante").GetComponent<ClockManager>().time = waitTime;
                            GameObject.Find("Canvas/RelojAguante").GetComponent<ClockManager>().enabled = true;

                            if (puntuacion)
                            {
                                score = score + ((int)(moveTime - elapsedTime) * multiplicador);
                                puntuacion = false;
                            }
                            conseguido2 = true;
                        }

                        // Aviso para mantener la postura
                        if (conseguido2 && control2)
                        {
                            if (GameManager.percent < (porcentaje - 0.2))
                            {
                                // Mensaje avisando: manten la postura
                                mensajes.text = "¡No te muevas!";
                                if (score > oldScore)
                                    score = score - 5;
                            }
                            else
                                mensajes.text = "¡Aguanta!";
                        }

                        // Mantener la postura un tiempo (waitTime)
                        if (elapsedTime >= waitTime && conseguido2 && control2)
                        {
                            control2 = false;
                            mensajes.text = "¡Perfecto!";
                            GetComponent<AudioSource>().Play();

                            Random.seed = System.DateTime.Now.Millisecond;
                            aleatorio = Random.Range(0, 2);

                            if (aleatorio == 0)
                                estrellitas.SetActive(true);
                            if (aleatorio == 1)
                                papelitos.SetActive(true);


                            animator.SetBool("6-2Completado", true);
                        }

                        // No se ha logrado hacer la pose en el tiempo elegido (movetime)
                        if (elapsedTime >= moveTime && !conseguido2 && control3)
                        {
                            control3 = false;
                            if (puntuado)
                            {
                                fallos++;
                                puntuado = false;
                            }
                            mensajes.text = "¡Casi!";
                            animator.SetBool("6-2Completado", true);
                        }
                    }

                }
                if (animator.GetCurrentAnimatorStateInfo(0).IsName("Ejercicio_6-3"))
                {
                    GameObject.Find("Canvas/RelojAguante").GetComponent<Image>().fillAmount = 1;
                    GameObject.Find("Canvas/RelojTiempo").GetComponent<Image>().fillAmount = 1;

                    papelitos.SetActive(false);
                    estrellitas.SetActive(false);
                    GameObject.Find("KinectController").GetComponent<PoseDetectorScript>().poseJoints.Remove(KinectInterop.JointType.HandLeft);
                    GameObject.Find("KinectController").GetComponent<PoseDetectorScript>().poseJoints.Remove(KinectInterop.JointType.ShoulderLeft);
                    GameObject.Find("KinectController").GetComponent<PoseDetectorScript>().poseJoints.Remove(KinectInterop.JointType.ElbowLeft);
                    GameObject.Find("KinectController").GetComponent<PoseDetectorScript>().poseJoints.Remove(KinectInterop.JointType.FootRight);
                    GameObject.Find("KinectController").GetComponent<PoseDetectorScript>().poseJoints.Remove(KinectInterop.JointType.HipRight);
                    GameObject.Find("KinectController").GetComponent<PoseDetectorScript>().poseJoints.Remove(KinectInterop.JointType.AnkleRight);

                    GameObject.Find("modelo/Brazo_derecho").GetComponent<ChangeColorIntensityBrazoDerecho>().enabled = false;
                    GameObject.Find("modelo/Pierna_izquierda").GetComponent<ChangeColorIntensityPiernaIzquierda>().enabled = false;
                    GameObject.Find("modelo/Brazo_derecho").GetComponent<Renderer>().materials[0].color = colorDefault;
                    GameObject.Find("modelo/Pierna_izquierda").GetComponent<Renderer>().materials[0].color = colorDefault;

                    if (resultados)
                    {
                        GetComponent<AudioSource>().clip = final;
                        GetComponent<AudioSource>().Play();
                        resultado.text = string.Format("Puntos: {0:F0} ", score);
                        resultados = false;
                        if (score > 100 && score < 500)
                            oneStar.SetActive(true);
                        if (score >= 500 && score < 1500)
                            twoStar.SetActive(true);
                        if (score >= 1500)
                            threeStar.SetActive(true);

                        panel.SetActive(true);
                        startTime = Time.time;

                        acabado = true;

                        animator.SetBool("Posar6", false);

                    }
                }
            }
        }
    }
}


