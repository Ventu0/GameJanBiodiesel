using TMPro;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Referências")]
    public CarData carData;
    public GameObject painelDerrota;
    public TextMeshProUGUI txtMensagemDerrota;
    public TextMeshProUGUI txtTimer;

    [Header("Configurações do Pitstop")]
    public float tempoMaximoPitstop = 30f;

    // Margem exata para Pressão, Emissão e Potência em volta de 30
    public float margemToleranciaExata = 0.5f;

    private float _timerPitstop;
    private bool _pitstopAtivo = false;
    private bool _jogoAcabou = false;

    void Start()
    {
        if (painelDerrota != null) painelDerrota.SetActive(false);
        _timerPitstop = tempoMaximoPitstop;
    }

    void Update()
    {
        if (_jogoAcabou || !_pitstopAtivo) return;

        _timerPitstop -= Time.deltaTime;

        if (txtTimer != null)
            txtTimer.text = $"Pitstop: {Mathf.CeilToInt(_timerPitstop)}s";

        // Quando o tempo de 30s esgotar, checa a validação
        if (_timerPitstop <= 0)
        {
            _timerPitstop = 0;
            ValidarAjustesPitstop();
        }
    }

    public void IniciarPitstop()
    {
        if (_jogoAcabou) return;
        _pitstopAtivo = true;
        _timerPitstop = tempoMaximoPitstop;
    }

    // Vinculo esta função ao botão "Concluir Pitstop" na UI ou ela dispara ao zerar o tempo
    public void ValidarAjustesPitstop()
    {
        if (!_pitstopAtivo || _jogoAcabou) return;

        _pitstopAtivo = false;

        if (carData == null)
        {
            Debug.LogError("CarData não está atribuído no CameraController!");
            return;
        }

        // Regra 1: Temperatura < 60
        bool tempOk = carData.temperatura < 60f;

        // Regra 2: Gasolina >= 10
        bool gasolinaOk = carData.gasolina >= 10f;

        // Regra 3: Pressão, Emissão e Potência em 30
        bool pressaoOk = Mathf.Abs(carData.pressao - 30f) <= margemToleranciaExata;
        bool emissaoOk = Mathf.Abs(carData.emissao - 30f) <= margemToleranciaExata;
        bool potenciaOk = Mathf.Abs(carData.potencia - 30f) <= margemToleranciaExata;

        if (tempOk && gasolinaOk && pressaoOk && emissaoOk && potenciaOk)
        {
            Debug.Log("Pitstop Concluído com Sucesso!");
            FinalizarPitstopESeguirJogo();
        }
        else
        {
            Derrota("As configurações do Pitstop não atenderam aos requisitos exigidos!");
        }
    }

    private void FinalizarPitstopESeguirJogo()
    {
        // Chama o método existente no ChangePerson para alternar/desativar a câmera do pitstop
        ChangePerson trocaCamera = FindFirstObjectByType<ChangePerson>();
        if (trocaCamera != null)
        {
            trocaCamera.AtivarModoPitstopApenas();
        }

        // Reseta os valores e libera o loop do jogo no CarData
        if (carData != null)
        {
            carData.ValidarPitstop();
        }
    }

    public void Derrota(string motivo)
    {
        if (_jogoAcabou) return;

        _jogoAcabou = true;
        _pitstopAtivo = false;

        if (painelDerrota != null)
            painelDerrota.SetActive(true);

        if (txtMensagemDerrota != null)
            txtMensagemDerrota.text = motivo;

        Time.timeScale = 0f; // Congela o jogo
    }
}