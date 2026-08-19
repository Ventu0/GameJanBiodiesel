using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CarControlScreen : MonoBehaviour
{
    [Header("Dados do carro")]
    public CarData car;


    // =========================================================
    // TEXTOS
    // =========================================================

    [Header("Textos")]
    public TMP_Text temperaturaText;
    public TMP_Text pressaoText;
    public TMP_Text emissaoText;
    public TMP_Text gasolinaText;
    public TMP_Text potenciaText;


    // =========================================================
    // SLIDERS / BARRAS
    // =========================================================

    [Header("Barras")]
    public Slider temperaturaBarra;
    public Slider pressaoBarra;
    public Slider emissaoBarra;
    public Slider gasolinaBarra;
    public Slider potenciaBarra;


    // =========================================================
    // CONFIGURAÇÃO
    // =========================================================

    [Header("Configuração dos botões")]
    public float quantidadePorClique = 5f;


    // =========================================================
    // START
    // =========================================================

    void Start()
    {
        ConfigurarBarras();
    }


    // =========================================================
    // UPDATE
    // =========================================================

    void Update()
    {
        if (car == null)
            return;


        // =====================================================
        // ATUALIZA OS TEXTOS
        // =====================================================

        if (temperaturaText != null)
        {
            temperaturaText.text =
                "Temperatura - " +
                Mathf.RoundToInt(car.temperatura);
        }


        if (pressaoText != null)
        {
            pressaoText.text =
                "Pressao - " +
                Mathf.RoundToInt(car.pressao);
        }


        if (emissaoText != null)
        {
            emissaoText.text =
                "Emissao - " +
                Mathf.RoundToInt(car.emissao);
        }


        if (gasolinaText != null)
        {
            gasolinaText.text =
                "Gasolina - " +
                Mathf.RoundToInt(car.gasolina);
        }


        if (potenciaText != null)
        {
            potenciaText.text =
                "Potencia - " +
                Mathf.RoundToInt(car.potencia);
        }


        // =====================================================
        // ATUALIZA AS BARRAS
        // =====================================================

        if (temperaturaBarra != null)
        {
            temperaturaBarra.value = car.temperatura;
        }


        if (pressaoBarra != null)
        {
            pressaoBarra.value = car.pressao;
        }


        if (emissaoBarra != null)
        {
            emissaoBarra.value = car.emissao;
        }


        if (gasolinaBarra != null)
        {
            gasolinaBarra.value = car.gasolina;
        }


        if (potenciaBarra != null)
        {
            potenciaBarra.value = car.potencia;
        }
    }


    // =========================================================
    // CONFIGURA AS BARRAS
    // =========================================================

    void ConfigurarBarras()
    {
        if (car == null)
            return;


        if (temperaturaBarra != null)
        {
            temperaturaBarra.minValue = car.valorMinimo;
            temperaturaBarra.maxValue = car.valorMaximo;
            temperaturaBarra.value = car.temperatura;
            temperaturaBarra.interactable = false;
        }


        if (pressaoBarra != null)
        {
            pressaoBarra.minValue = car.valorMinimo;
            pressaoBarra.maxValue = car.valorMaximo;
            pressaoBarra.value = car.pressao;
            pressaoBarra.interactable = false;
        }


        if (emissaoBarra != null)
        {
            emissaoBarra.minValue = car.valorMinimo;
            emissaoBarra.maxValue = car.valorMaximo;
            emissaoBarra.value = car.emissao;
            emissaoBarra.interactable = false;
        }


        if (gasolinaBarra != null)
        {
            gasolinaBarra.minValue = car.valorMinimo;
            gasolinaBarra.maxValue = car.valorMaximo;
            gasolinaBarra.value = car.gasolina;
            gasolinaBarra.interactable = false;
        }


        if (potenciaBarra != null)
        {
            potenciaBarra.minValue = car.valorMinimo;
            potenciaBarra.maxValue = car.valorMaximo;
            potenciaBarra.value = car.potencia;
            potenciaBarra.interactable = false;
        }
    }


    // =========================================================
    // TEMPERATURA
    // =========================================================

    public void TemperaturaMais()
    {
        if (car == null)
            return;

        car.AumentarTemperatura(quantidadePorClique);
    }


    public void TemperaturaMenos()
    {
        if (car == null)
            return;

        car.DiminuirTemperatura(quantidadePorClique);
    }


    // =========================================================
    // PRESSÃO
    // =========================================================

    public void PressaoMais()
    {
        if (car == null)
            return;

        car.AumentarPressao(quantidadePorClique);
    }


    public void PressaoMenos()
    {
        if (car == null)
            return;

        car.DiminuirPressao(quantidadePorClique);
    }


    // =========================================================
    // EMISSÃO
    // =========================================================

    public void EmissaoMais()
    {
        if (car == null)
            return;

        car.AumentarEmissao(quantidadePorClique);
    }


    public void EmissaoMenos()
    {
        if (car == null)
            return;

        car.DiminuirEmissao(quantidadePorClique);
    }


    // =========================================================
    // GASOLINA
    // =========================================================

    public void GasolinaMais()
    {
        if (car == null)
            return;

        car.AumentarGasolina(quantidadePorClique);
    }


    public void GasolinaMenos()
    {
        if (car == null)
            return;

        car.DiminuirGasolina(quantidadePorClique);
    }


    // =========================================================
    // POTÊNCIA
    // =========================================================

    public void PotenciaMais()
    {
        if (car == null)
            return;

        car.AumentarPotencia(quantidadePorClique);
    }


    public void PotenciaMenos()
    {
        if (car == null)
            return;

        car.DiminuirPotencia(quantidadePorClique);
    }
}