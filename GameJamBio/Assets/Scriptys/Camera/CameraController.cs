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

    // Intervalo central aceitável (baseado na faixa de 5 a 60 do CarData)
    public float valorMinimoIdeal = 25f;
    public float valorMaximoIdeal = 35f;

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

    // Vinculo esta função ao botão "Concluir Pitstop" na UI
    public void ValidarAjustesPitstop()
    {
        if (!_pitstopAtivo || _jogoAcabou) return;

        _pitstopAtivo = false;

        bool tempOk = EstaNoCentro(carData.temperatura);
        bool pressaoOk = EstaNoCentro(carData.pressao);
        bool emissaoOk = EstaNoCentro(carData.emissao);
        bool potenciaOk = EstaNoCentro(carData.potencia);

        if (tempOk && pressaoOk && emissaoOk && potenciaOk)
        {
            Debug.Log("Pitstop Concluído com Sucesso!");
            // Transição de volta para a corrida
        }
        else
        {
            Derrota("Você não sincronizou a Temperatura, Pressão, Emissão e Potência no centro a tempo!");
        }
    }

    private bool EstaNoCentro(float valor)
    {
        return valor >= valorMinimoIdeal && valor <= valorMaximoIdeal;
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