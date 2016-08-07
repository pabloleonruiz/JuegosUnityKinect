using UnityEngine;
using System.Collections;

public class ChangeColorIntensityBrazoIzquierdo : MonoBehaviour {
    public Color colorStart = Color.red;
    public Color colorEnd = Color.green;
    public Renderer rend;
    // Use this for initialization

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        rend.material.color = Color.Lerp(colorStart, colorEnd, GameManagerBrazoIzquierdo.percent);
    }
}