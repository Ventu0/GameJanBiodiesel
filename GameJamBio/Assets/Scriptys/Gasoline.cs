using UnityEngine;
using UnityEngine.UI;

public class Gasoline : MonoBehaviour
{
    [SerializeField] Slider gasolineBar;
    [SerializeField] int totalGasoline;
    float timer = 0;

    void Start()
    {
        totalGasoline = 60;
        gasolineBar.value = totalGasoline;
    }
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= 1) //a cada 1 seg
        {
            totalGasoline -= 1;
            gasolineBar.value = totalGasoline;
            timer = 0;
        }
    }
}
