using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ControleBarra : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private TMP_Text textoValor;

    [SerializeField] private float passo = 10f;

    private void Start()
    {
        slider.minValue = 0f;
        slider.maxValue = 100f;
        slider.value = 50f;
        slider.interactable = false;
        AtualizarTexto();
    }

    public void Aumentar()
    {
        slider.value += passo;
        AtualizarTexto();
    }

    public void Diminuir()
    {
        slider.value -= passo;
        AtualizarTexto();
    }

    private void AtualizarTexto()
    {
        textoValor.text = slider.value.ToString("0");
    }
}