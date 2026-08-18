using TMPro;
using UnityEngine;

public class CarStatusScreen : MonoBehaviour
{
    public CarData car;

    public TMP_Text temperaturaText;
    public TMP_Text pressaoText;
    public TMP_Text emissaoText;
    public TMP_Text gasolinaText;
    public TMP_Text potenciaText;

    void Update()
    {
        if (car == null)
            return;

        temperaturaText.text =
            "Temperatura: " + Mathf.RoundToInt(car.temperatura);

        pressaoText.text =
            "Pressão: " + Mathf.RoundToInt(car.pressao);

        emissaoText.text =
            "Emissão: " + Mathf.RoundToInt(car.emissao);

        gasolinaText.text =
            "Gasolina: " + Mathf.RoundToInt(car.gasolina);

        potenciaText.text =
            "Potência: " + Mathf.RoundToInt(car.potencia);
    }
}