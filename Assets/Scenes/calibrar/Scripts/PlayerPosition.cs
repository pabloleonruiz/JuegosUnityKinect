using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using EaseTools;



public class PlayerPosition : MonoBehaviour
{
    public EaseUI easeUIComponent;
    private KinectManager manager = null;
    private Vector3 userMeshPos = Vector3.zero;
    public GameObject IzquierdaActiva;
    public GameObject DerechaActiva;
    public GameObject ArribaActiva;
    public GameObject AbajoActiva;
    public GameObject IzquierdaNoActiva;
    public GameObject DerechaNoActiva;
    public GameObject ArribaNoActiva;
    public GameObject AbajoNoActiva;
    public Text mensajes; // Mensajes para el usuario                 
    private ModelGestureListener gestureListener; // reference to the gesture listener
    private bool calibrado=false;

    // Use this for initialization
    void Start()
    {
        easeUIComponent.ScaleIn();
        manager = KinectManager.Instance;
        // get the gestures listener
        gestureListener = ModelGestureListener.Instance;
        DerechaActiva.SetActive(false);
        IzquierdaActiva.SetActive(false);
        ArribaActiva.SetActive(false);
        AbajoActiva.SetActive(false);
        GameObject.Find("Canvas/UICanvas/Pasar").SetActive(false);
        GameObject.Find("Canvas/UICanvas/Problema").SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.activo == false)
        {
            GameObject.Find("Canvas/UICanvas/Problema").SetActive(true);
        }
        else
        {
            if (calibrado)
            {
                GameObject.Find("Canvas/UICanvas/Pasar").SetActive(true);

                if (gestureListener.IsRaiseHand())
                {

                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                }
            }
            else
            {
                GameObject.Find("Canvas/UICanvas/Pasar").SetActive(false);
            }
            DerechaActiva.SetActive(false);
            DerechaNoActiva.SetActive(true);
            IzquierdaActiva.SetActive(false);
            IzquierdaNoActiva.SetActive(true);
            ArribaActiva.SetActive(false);
            ArribaNoActiva.SetActive(true);
            AbajoActiva.SetActive(false);
            AbajoNoActiva.SetActive(true);

            userMeshPos = manager.GetUserPosition(manager.GetPrimaryUserID());
            mensajes.text = "Sigue las flechas para calibrar";
            if (Input.GetKeyDown(KeyCode.Q))
            {
                SceneManager.LoadScene(1);

            }
            if (userMeshPos.x < -0.2)
            {
                DerechaActiva.SetActive(true);
                DerechaNoActiva.SetActive(false);
            }

            if (userMeshPos.z < 2.7)
            {
                ArribaActiva.SetActive(true);
                ArribaNoActiva.SetActive(false);
            }

            if (userMeshPos.x > 0.2)
            {
                IzquierdaActiva.SetActive(true);
                IzquierdaNoActiva.SetActive(false);
            }

            if (userMeshPos.z > 3.4)
            {
                AbajoActiva.SetActive(true);
                AbajoNoActiva.SetActive(false);
            }

            if (userMeshPos.x > -0.2 && userMeshPos.x < 0.2 && userMeshPos.z > 2.7 && userMeshPos.z < 3.4)
            {
                mensajes.text = "Calibrado";
                calibrado = true;
            }
            else
                calibrado = false;
        }
    }
}