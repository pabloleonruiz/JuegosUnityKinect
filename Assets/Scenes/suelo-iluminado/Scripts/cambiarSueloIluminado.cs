using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class cambiarSueloIluminado : MonoBehaviour {

	// Update is called once per frame
	void Update () {
        SceneManager.LoadScene("suelo-iluminado");
    }
}
