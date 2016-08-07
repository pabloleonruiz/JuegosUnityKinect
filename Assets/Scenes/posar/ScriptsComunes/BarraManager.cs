using ProgressBar;
using System.Collections;
using UnityEngine;

public class BarraManager : MonoBehaviour
{
    ProgressBarBehaviour BarBehaviour;

    IEnumerator Start()
    {
        BarBehaviour = GetComponent<ProgressBarBehaviour>();
        float UpdateDelay = 0.2f;
        while (true)
        {
            yield return new WaitForSeconds(UpdateDelay);
            BarBehaviour.Value = GameManager.percent * 100/GameManager.minimo;
        }
    }
}
