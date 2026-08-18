using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CarStatus : MonoBehaviour
{
    [Header("TEXTOS DO MONITOR")]
    [SerializeField] private TMP_Text textoPressao;
    [SerializeField] private TMP_Text textoEmissao;
    [SerializeField] private TMP_Text textoTemperatura;
    [SerializeField] private TMP_Text textoGasolina;
    [SerializeField] private TMP_Text textoPotencia;

    [Header("SLIDERS")]
    [SerializeField] private Slider sliderPressao;
    [SerializeField] private Slider sliderEmissao;
    [SerializeField] private Slider sliderTemperatura;
    [SerializeField] private Slider sliderGasolina;
    [SerializeField] private Slider sliderPotencia;

    [Header("VALORES")]
    [SerializeField] private float pressao;
    [SerializeField] private float emissao;
    [SerializeField] private float temperatura;
    [SerializeField] private float gasolina;
    [SerializeField] private float potencia;

    [SerializeField] private float minValue = 0f;
    [SerializeField] private float maxValue = 60f;

    private void Start()
    {
        // Configura os Sliders
        ConfigurarSlider(sliderPressao);
        ConfigurarSlider(sliderEmissao);
        ConfigurarSlider(sliderTemperatura);
        ConfigurarSlider(sliderGasolina);
        ConfigurarSlider(sliderPotencia);

        // Sorteia os valores iniciais
        pressao = Random.Range(minValue, maxValue + 1);
        emissao = Random.Range(minValue, maxValue + 1);
        temperatura = Random.Range(minValue, maxValue + 1);
        gasolina = Random.Range(minValue, maxValue + 1);
        potencia = Random.Range(minValue, maxValue + 1);

        // Atualiza tudo
        AtualizarTudo();
    }

    private void ConfigurarSlider(Slider slider)
    {
        if (slider != null)
        {
            slider.minValue = minValue;
            slider.maxValue = maxValue;
        }
    }

    // =========================
    // PRESSÃO
    // =========================

    public void AtualizarPressao(float valor)
    {
        pressao = Mathf.Clamp(valor, minValue, maxValue);

        if (sliderPressao != null)
            sliderPressao.SetValueWithoutNotify(pressao);

        if (textoPressao != null)
            textoPressao.text = pressao.ToString("0");
    }

    // =========================
    // EMISSÃO
    // =========================

    public void AtualizarEmissao(float valor)
    {
        emissao = Mathf.Clamp(valor, minValue, maxValue);

        if (sliderEmissao != null)
            sliderEmissao.SetValueWithoutNotify(emissao);

        if (textoEmissao != null)
            textoEmissao.text = emissao.ToString("0");
    }

    // =========================
    // TEMPERATURA
    // =========================

    public void AtualizarTemperatura(float valor)
    {
        temperatura = Mathf.Clamp(valor, minValue, maxValue);

        if (sliderTemperatura != null)
            sliderTemperatura.SetValueWithoutNotify(temperatura);

        if (textoTemperatura != null)
            textoTemperatura.text = temperatura.ToString("0");
    }

    // =========================
    // GASOLINA
    // =========================

    public void AtualizarGasolina(float valor)
    {
        gasolina = Mathf.Clamp(valor, minValue, maxValue);

        if (sliderGasolina != null)
            sliderGasolina.SetValueWithoutNotify(gasolina);

        if (textoGasolina != null)
            textoGasolina.text = gasolina.ToString("0");
    }

    // =========================
    // POTÊNCIA
    // =========================

    public void AtualizarPotencia(float valor)
    {
        potencia = Mathf.Clamp(valor, minValue, maxValue);

        if (sliderPotencia != null)
            sliderPotencia.SetValueWithoutNotify(potencia);

        if (textoPotencia != null)
            textoPotencia.text = potencia.ToString("0");
    }

    // =========================
    // ATUALIZAR TUDO
    // =========================

    private void AtualizarTudo()
    {
        AtualizarPressao(pressao);
        AtualizarEmissao(emissao);
        AtualizarTemperatura(temperatura);
        AtualizarGasolina(gasolina);
        AtualizarPotencia(potencia);
    }
}