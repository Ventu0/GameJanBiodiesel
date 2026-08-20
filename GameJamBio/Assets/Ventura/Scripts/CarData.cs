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

    [Header("Sistema de Derrota")]
    public GameObject painelDerrota;
    public TextMeshProUGUI txtMensagemDerrota;

    [Header("Compatibilidade")]
    public TextMeshProUGUI txtTimerPitstop;
    public float tempoPitstop = 20f;

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
        {
            painelDerrota.SetActive(false);
        }
    }

    void Update()
    {
        if (jogoAcabou)
            return;

        /*
         * Durante o Pit Stop, o CarData não controla timer.
         *
         * O timer é controlado somente pelo CameraController.
         *
         * Isso evita:
         *
         * CarData -> Validar -> RetomarCorrida
         *
         * e
         *
         * CameraController -> Validar -> RetomarCorrida
         *
         * ao mesmo tempo.
         */

        if (pitstopAtivo)
            return;

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

        AtualizarTemperatura();
        AtualizarGasolina();
        AtualizarEmissao();
        AtualizarPotencia();
    }

    // =========================================================
    // INICIAR PIT STOP
    // =========================================================

    public void IniciarPitstop()
    {
        if (pitstopAtivo || jogoAcabou)
            return;

        pitstopAtivo = true;

        /*
         * Mantém o comportamento original:
         * ao entrar no Pit Stop, os valores recebem
         * uma nova situação para o jogador ajustar.
         */

        temperatura = Mathf.Clamp(
            temperatura + 10f,
            valorMinimo,
            valorMaximo
        );

        gasolina = Mathf.Clamp(
            gasolina - 15f,
            valorMinimo,
            valorMaximo
        );

        pressao = Random.Range(
            valorMinimo,
            valorMaximo
        );

        emissao = Random.Range(
            valorMinimo,
            valorMaximo
        );

        potencia = Random.Range(
            valorMinimo,
            valorMaximo
        );

        contadorTemperatura = 0f;
        contadorGasolina = 0f;
        contadorEmissao = 0f;
        contadorPotencia = 0f;

        Debug.Log("🔧 CarData entrou no Pit Stop");
    }

    // =========================================================
    // ENCERRAR PIT STOP
    // =========================================================

    public void EncerrarPitstop()
    {
        pitstopAtivo = false;

        contadorTemperatura = 0f;
        contadorGasolina = 0f;
        contadorEmissao = 0f;
        contadorPotencia = 0f;

        Debug.Log("🏎️ CarData saiu do Pit Stop");
    }

    // =========================================================
    // COMPATIBILIDADE COM CÓDIGO ANTIGO
    // =========================================================

    public void ValidarPitstop()
    {
        /*
         * Agora este método NÃO inicia a corrida.
         *
         * O CameraController é o único que chama:
         *
         * PitsTop.RetomarCorrida()
         */

        EncerrarPitstop();
    }

    public bool EstaNoPitstop()
    {
        return pitstopAtivo;
    }

    // =========================================================
    // DERROTA
    // =========================================================

    public void AcionarDerrota(string motivo)
    {
        if (jogoAcabou)
            return;

        jogoAcabou = true;
        pitstopAtivo = false;

        if (painelDerrota != null)
        {
            painelDerrota.SetActive(true);
        }

        if (txtMensagemDerrota != null)
        {
            txtMensagemDerrota.text = motivo;
        }

        Time.timeScale = 0f;
    }

    // =========================================================
    // ATUALIZAÇÕES DA CORRIDA
    // =========================================================

    void AtualizarTemperatura()
    {
        contadorTemperatura += Time.deltaTime;

        if (contadorTemperatura >= tempoParaAumentarTemperatura)
        {
            temperatura = Mathf.Clamp(
                temperatura + quantoAumentaTemperatura,
                valorMinimo,
                valorMaximo
            );

            contadorTemperatura = 0f;
        }
    }

    void AtualizarGasolina()
    {
        contadorGasolina += Time.deltaTime;

        if (contadorGasolina >= tempoParaGastarGasolina)
        {
            gasolina = Mathf.Clamp(
                gasolina - quantoGastaGasolina,
                valorMinimo,
                valorMaximo
            );

            contadorGasolina = 0f;
        }
    }

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
    // CONTROLES DE PRESSÃO
    // =========================================================

    public void AumentarPressao(float q)
    {
        pressao = Mathf.Clamp(
            pressao + q,
            valorMinimo,
            valorMaximo
        );
    }

    public void DiminuirPressao(float q)
    {
        pressao = Mathf.Clamp(
            pressao - q,
            valorMinimo,
            valorMaximo
        );
    }

    // =========================================================
    // CONTROLES DE TEMPERATURA
    // =========================================================

    public void AumentarTemperatura(float q)
    {
        temperatura = Mathf.Clamp(
            temperatura + q,
            valorMinimo,
            valorMaximo
        );
    }

    public void DiminuirTemperatura(float q)
    {
        temperatura = Mathf.Clamp(
            temperatura - q,
            valorMinimo,
            valorMaximo
        );
    }

    // =========================================================
    // CONTROLES DE EMISSÃO
    // =========================================================

    public void AumentarEmissao(float q)
    {
        emissao = Mathf.Clamp(
            emissao + q,
            valorMinimo,
            valorMaximo
        );
    }

    public void DiminuirEmissao(float q)
    {
        emissao = Mathf.Clamp(
            emissao - q,
            valorMinimo,
            valorMaximo
        );
    }

    // =========================================================
    // CONTROLES DE GASOLINA
    // =========================================================

    public void AumentarGasolina(float q)
    {
        gasolina = Mathf.Clamp(
            gasolina + q,
            valorMinimo,
            valorMaximo
        );
    }

    public void DiminuirGasolina(float q)
    {
        gasolina = Mathf.Clamp(
            gasolina - q,
            valorMinimo,
            valorMaximo
        );
    }

    // =========================================================
    // CONTROLES DE POTÊNCIA
    // =========================================================

    public void AumentarPotencia(float q)
    {
        potencia = Mathf.Clamp(
            potencia + q,
            valorMinimo,
            valorMaximo
        );
    }

    public void DiminuirPotencia(float q)
    {
        potencia = Mathf.Clamp(
            potencia - q,
            valorMinimo,
            valorMaximo
        );
    }
}