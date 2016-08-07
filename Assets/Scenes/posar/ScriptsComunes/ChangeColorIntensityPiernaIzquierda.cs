using UnityEngine;
using System.Collections;

public class ChangeColorIntensityPiernaIzquierda : MonoBehaviour {
    public Color colorStart = Color.red;
    public Color colorEnd = Color.green;
    public Renderer rend;
    public bool modificador;
    // Use this for initialization

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (modificador)
        {
            if (GameManagerPiernaDerecha.percent >= 0.9)
            {
                rend.material.color = Color.Lerp(colorStart, colorEnd, GameManagerPiernaDerecha.percent);
            }

            if (GameManagerPiernaDerecha.percent < 0.9 && GameManagerPiernaDerecha.percent >= 0.7)
            {
                rend.material.color = Color.Lerp(colorStart, colorEnd, GameManagerPiernaDerecha.percent - 0.4f);


            }

            if (GameManagerPiernaDerecha.percent < 0.7)
            {
                rend.material.color = Color.Lerp(colorStart, colorEnd, GameManagerPiernaDerecha.percent - 0.6f);
            }
        }

        else
        {
            rend.material.color = Color.Lerp(colorStart, colorEnd, GameManagerPiernaDerecha.percent);
        }
    }
}