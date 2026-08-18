using TMPro;
using UnityEngine;

public class CarControlScreen : MonoBehaviour
{
    public CarData car;

    [Header("Textos")]
    public TMP_Text temperaturaText;
    public TMP_Text pressaoText;
    public TMP_Text emissaoText;
    public TMP_Text gasolinaText;
    public TMP_Text potenciaText;

    [Header("Configuração")]
    public float quantidadePorClique = 5f;

    void Update()
    {
        if (car == null)
            return;

        temperaturaText.text =
            Mathf.RoundToInt(car.temperatura).ToString();

        pressaoText.text =
            Mathf.RoundToInt(car.pressao).ToString();

        emissaoText.text =
            Mathf.RoundToInt(car.emissao).ToString();

        gasolinaText.text =
            Mathf.RoundToInt(car.gasolina).ToString();

        potenciaText.text =
            Mathf.RoundToInt(car.potencia).ToString();
    }

    // TEMPERATURA

    public void TemperaturaMais()
    {
        car.AumentarTemperatura(quantidadePorClique);
    }

    public void TemperaturaMenos()
    {
        car.DiminuirTemperatura(quantidadePorClique);
    }

    // PRESSÃO

    public void PressaoMais()
    {
        car.AumentarPressao(quantidadePorClique);
    }

    public void PressaoMenos()
    {
        car.DiminuirPressao(quantidadePorClique);
    }

    // EMISSÃO

    public void EmissaoMais()
    {
        car.AumentarEmissao(quantidadePorClique);
    }

    public void EmissaoMenos()
    {
        car.DiminuirEmissao(quantidadePorClique);
    }

    // GASOLINA

    public void GasolinaMais()
    {
        car.AumentarGasolina(quantidadePorClique);
    }

    public void GasolinaMenos()
    {
        car.DiminuirGasolina(quantidadePorClique);
    }

    // POTÊNCIA

    public void PotenciaMais()
    {
        car.AumentarPotencia(quantidadePorClique);
    }

    public void PotenciaMenos()
    {
        car.DiminuirPotencia(quantidadePorClique);
    }
}