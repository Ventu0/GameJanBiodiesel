using UnityEngine;

public class CarData : MonoBehaviour
{
    [Header("Valores do carro")]
    public float temperatura = 15f;
    public float pressao = 30f;
    public float emissao = 30f;
    public float gasolina = 50f;
    public float potencia = 30f;

    [Header("Limites")]
    public float valorMinimo = 5f;
    public float valorMaximo = 60f;

    [Header("Temperatura")]
    public float tempoParaAumentarTemperatura = 3f;
    public float quantoAumentaTemperatura = 1f;

    [Header("Gasolina")]
    public float tempoParaGastarGasolina = 2f;
    public float quantoGastaGasolina = 1f;

    [Header("Emissão")]
    public float tempoParaRandomizarEmissao = 1f;

    [Header("Potência")]
    public float tempoParaRandomizarPotencia = 1f;

    private float contadorTemperatura;
    private float contadorGasolina;
    private float contadorEmissao;
    private float contadorPotencia;

    void Start()
    {
        // Temperatura começa SEMPRE em 15
        temperatura = 15f;

        // Os outros valores começam aleatórios
        pressao = Random.Range(20f, 40f);
        emissao = Random.Range(10f, 50f);
        gasolina = Random.Range(40f, 60f);
        potencia = Random.Range(20f, 50f);
    }

    void Update()
    {
        AtualizarTemperatura();
        AtualizarGasolina();
        AtualizarEmissao();
        AtualizarPotencia();
    }

    // =========================================================
    // TEMPERATURA
    // =========================================================

    void AtualizarTemperatura()
    {
        contadorTemperatura += Time.deltaTime;

        if (contadorTemperatura >= tempoParaAumentarTemperatura)
        {
            temperatura += quantoAumentaTemperatura;

            temperatura = Mathf.Clamp(
                temperatura,
                valorMinimo,
                valorMaximo
            );

            contadorTemperatura = 0f;
        }
    }

    // =========================================================
    // GASOLINA
    // =========================================================

    void AtualizarGasolina()
    {
        contadorGasolina += Time.deltaTime;

        if (contadorGasolina >= tempoParaGastarGasolina)
        {
            gasolina -= quantoGastaGasolina;

            gasolina = Mathf.Clamp(
                gasolina,
                valorMinimo,
                valorMaximo
            );

            contadorGasolina = 0f;
        }
    }

    // =========================================================
    // EMISSÃO
    // =========================================================

    void AtualizarEmissao()
    {
        contadorEmissao += Time.deltaTime;

        if (contadorEmissao >= tempoParaRandomizarEmissao)
        {
            emissao = Random.Range(
                valorMinimo,
                valorMaximo
            );

            contadorEmissao = 0f;
        }
    }

    // =========================================================
    // POTÊNCIA
    // =========================================================

    void AtualizarPotencia()
    {
        contadorPotencia += Time.deltaTime;

        if (contadorPotencia >= tempoParaRandomizarPotencia)
        {
            potencia = Random.Range(
                valorMinimo,
                valorMaximo
            );

            contadorPotencia = 0f;
        }
    }

    // =========================================================
    // PRESSÃO
    // =========================================================

    public void AumentarPressao(float quantidade)
    {
        pressao += quantidade;

        pressao = Mathf.Clamp(
            pressao,
            valorMinimo,
            valorMaximo
        );
    }

    public void DiminuirPressao(float quantidade)
    {
        pressao -= quantidade;

        pressao = Mathf.Clamp(
            pressao,
            valorMinimo,
            valorMaximo
        );
    }

    // =========================================================
    // TEMPERATURA - CONTROLE MANUAL
    // =========================================================

    public void AumentarTemperatura(float quantidade)
    {
        temperatura += quantidade;

        temperatura = Mathf.Clamp(
            temperatura,
            valorMinimo,
            valorMaximo
        );
    }

    public void DiminuirTemperatura(float quantidade)
    {
        temperatura -= quantidade;

        temperatura = Mathf.Clamp(
            temperatura,
            valorMinimo,
            valorMaximo
        );
    }

    // =========================================================
    // EMISSÃO - CONTROLE MANUAL
    // =========================================================

    public void AumentarEmissao(float quantidade)
    {
        emissao += quantidade;

        emissao = Mathf.Clamp(
            emissao,
            valorMinimo,
            valorMaximo
        );
    }

    public void DiminuirEmissao(float quantidade)
    {
        emissao -= quantidade;

        emissao = Mathf.Clamp(
            emissao,
            valorMinimo,
            valorMaximo
        );
    }

    // =========================================================
    // GASOLINA - CONTROLE MANUAL
    // =========================================================

    public void AumentarGasolina(float quantidade)
    {
        gasolina += quantidade;

        gasolina = Mathf.Clamp(
            gasolina,
            valorMinimo,
            valorMaximo
        );
    }

    public void DiminuirGasolina(float quantidade)
    {
        gasolina -= quantidade;

        gasolina = Mathf.Clamp(
            gasolina,
            valorMinimo,
            valorMaximo
        );
    }

    // =========================================================
    // POTÊNCIA - CONTROLE MANUAL
    // =========================================================

    public void AumentarPotencia(float quantidade)
    {
        potencia += quantidade;

        potencia = Mathf.Clamp(
            potencia,
            valorMinimo,
            valorMaximo
        );
    }

    public void DiminuirPotencia(float quantidade)
    {
        potencia -= quantidade;

        potencia = Mathf.Clamp(
            potencia,
            valorMinimo,
            valorMaximo
        );
    }
}