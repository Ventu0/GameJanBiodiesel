using UnityEngine;
using TMPro;

public class CarData : MonoBehaviour
{
    [Header("Valores do carro")]
    public float temperatura = 15f;
    public float pressao = 30f;
    public float emissao = 30f;
    public float gasolina = 50f;
    public float potencia = 30f;

    [Header("Margem de Acerto do Pitstop")]
    public float valorAlvo = 32.5f;
    public float margemTolerancia = 7.5f;

    [Header("Limites")]
    public float valorMinimo = 0f;
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

    [Header("Sistema de Pitstop e Derrota")]
    public GameObject painelDerrota;
    public TextMeshProUGUI txtMensagemDerrota;
    public TextMeshProUGUI txtTimerPitstop; // <--- AQUI ESTÁ O SEU TIMER DE VOLTA!
    public float tempoPitstop = 20f; // Cravado em 20 segundos

    private float contadorTemperatura;
    private float contadorGasolina;
    private float contadorEmissao;
    private float contadorPotencia;

    private bool pitstopAtivo = false;
    private bool jogoAcabou = false;

    void Start()
    {
        temperatura = 15f;
        pressao = Random.Range(20f, 40f);
        emissao = Random.Range(10f, 50f);
        gasolina = Random.Range(40f, 60f);
        potencia = Random.Range(20f, 50f);

        if (painelDerrota != null)
            painelDerrota.SetActive(false);
    }

    void Update()
    {
        if (jogoAcabou) return;

        if (gasolina <= 10f)
        {
            AcionarDerrota("A gasolina chegou a 10 ou menos!");
            return;
        }

        if (temperatura >= 60f)
        {
            AcionarDerrota("A temperatura chegou a 60 ou mais!");
            return;
        }

        if (!pitstopAtivo)
        {
            AtualizarTemperatura();
            AtualizarGasolina();
            AtualizarEmissao();
            AtualizarPotencia();
        }
        else // O PITSTOP ESTÁ ATIVO, VAMOS CONTAR OS 20s
        {
            tempoPitstop -= Time.deltaTime;

            if (txtTimerPitstop != null)
            {
                // Atualiza o seu texto na tela
                txtTimerPitstop.text = $"Pitstop: {Mathf.CeilToInt(Mathf.Max(tempoPitstop, 0f))}s";
            }

            // O tempo acabou! Vamos ver se você alinhou as barras
            if (tempoPitstop <= 0f)
            {
                tempoPitstop = 0f;
                ValidarPitstop();
            }
        }
    }

    public void IniciarPitstop()
    {
        if (pitstopAtivo || jogoAcabou) return;

        pitstopAtivo = true;
        tempoPitstop = 20f; // Garante que você sempre terá 20s

        temperatura = Mathf.Clamp(temperatura + 10f, valorMinimo, valorMaximo);
        gasolina = Mathf.Clamp(gasolina - 15f, valorMinimo, valorMaximo);
        pressao = Random.Range(valorMinimo, valorMaximo);
        emissao = Random.Range(valorMinimo, valorMaximo);
        potencia = Random.Range(valorMinimo, valorMaximo);

        contadorTemperatura = 0f;
        contadorGasolina = 0f;
        contadorEmissao = 0f;
        contadorPotencia = 0f;

        ChangePerson trocaCamera = FindFirstObjectByType<ChangePerson>();
        if (trocaCamera != null)
        {
            trocaCamera.AtivarModoPitstopApenas();
        }
    }

    public void ValidarPitstop()
    {
        pitstopAtivo = false;

        float limiteMin = valorAlvo - margemTolerancia;
        float limiteMax = valorAlvo + margemTolerancia;

        bool tempOk = temperatura >= limiteMin && temperatura <= limiteMax;
        bool pressaoOk = pressao >= limiteMin && pressao <= limiteMax;
        bool emissaoOk = emissao >= limiteMin && emissao <= limiteMax;
        bool gasolinaOk = gasolina >= limiteMin && gasolina <= limiteMax;
        bool potenciaOk = potencia >= limiteMin && potencia <= limiteMax;

        if (tempOk && pressaoOk && emissaoOk && gasolinaOk && potenciaOk)
        {
            if (txtTimerPitstop != null) txtTimerPitstop.text = "Pitstop concluído!";
            
            // 1. VENCEU! VOLTA A CÂMERA DO MONITOR IMEDIATAMENTE
            ChangePerson trocaCamera = FindFirstObjectByType<ChangePerson>();
            if (trocaCamera != null)
            {
                trocaCamera.AtivarModoPitstopApenas(); // Lembre-se: Troque esse nome se a função que liga o monitor for diferente
            }

            contadorTemperatura = 0f;
            contadorGasolina = 0f;
            contadorEmissao = 0f;
            contadorPotencia = 0f;

            // 2. AVISA A PISTA PARA OS CARROS VOLTAREM A CORRER
            PitsTop gerenciadorPista = FindFirstObjectByType<PitsTop>();
            if (gerenciadorPista != null) gerenciadorPista.RetomarCorrida();
        }
        else
        {
            AcionarDerrota("Você não alinhou todos os componentes na barra permitida a tempo!");
        }
    }

    public void AcionarDerrota(string motivo)
    {
        jogoAcabou = true;
        pitstopAtivo = false;

        if (painelDerrota != null) painelDerrota.SetActive(true);
        if (txtMensagemDerrota != null) txtMensagemDerrota.text = motivo;

        Time.timeScale = 0f;
    }

    void AtualizarTemperatura()
    {
        contadorTemperatura += Time.deltaTime;
        if (contadorTemperatura >= tempoParaAumentarTemperatura)
        {
            temperatura = Mathf.Clamp(temperatura + quantoAumentaTemperatura, valorMinimo, valorMaximo);
            contadorTemperatura = 0f;
        }
    }

    void AtualizarGasolina()
    {
        contadorGasolina += Time.deltaTime;
        if (contadorGasolina >= tempoParaGastarGasolina)
        {
            gasolina = Mathf.Clamp(gasolina - quantoGastaGasolina, valorMinimo, valorMaximo);
            contadorGasolina = 0f;
        }
    }

    void AtualizarEmissao()
    {
        contadorEmissao += Time.deltaTime;
        if (contadorEmissao >= tempoParaRandomizarEmissao)
        {
            emissao = Random.Range(valorMinimo, valorMaximo);
            contadorEmissao = 0f;
        }
    }

    void AtualizarPotencia()
    {
        contadorPotencia += Time.deltaTime;
        if (contadorPotencia >= tempoParaRandomizarPotencia)
        {
            potencia = Random.Range(valorMinimo, valorMaximo);
            contadorPotencia = 0f;
        }
    }

    public void AumentarPressao(float q) { pressao = Mathf.Clamp(pressao + q, valorMinimo, valorMaximo); }
    public void DiminuirPressao(float q) { pressao = Mathf.Clamp(pressao - q, valorMinimo, valorMaximo); }
    public void AumentarTemperatura(float q) { temperatura = Mathf.Clamp(temperatura + q, valorMinimo, valorMaximo); }
    public void DiminuirTemperatura(float q) { temperatura = Mathf.Clamp(temperatura - q, valorMinimo, valorMaximo); }
    public void AumentarEmissao(float q) { emissao = Mathf.Clamp(emissao + q, valorMinimo, valorMaximo); }
    public void DiminuirEmissao(float q) { emissao = Mathf.Clamp(emissao - q, valorMinimo, valorMaximo); }
    public void AumentarGasolina(float q) { gasolina = Mathf.Clamp(gasolina + q, valorMinimo, valorMaximo); }
    public void DiminuirGasolina(float q) { gasolina = Mathf.Clamp(gasolina - q, valorMinimo, valorMaximo); }
    public void AumentarPotencia(float q) { potencia = Mathf.Clamp(potencia + q, valorMinimo, valorMaximo); }
    public void DiminuirPotencia(float q) { potencia = Mathf.Clamp(potencia - q, valorMinimo, valorMaximo); }
}