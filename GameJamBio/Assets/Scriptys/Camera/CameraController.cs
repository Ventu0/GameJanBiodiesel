using TMPro;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Referências")]
    public CarData carData;
    public GameObject painelDerrota;
    public TextMeshProUGUI txtMensagemDerrota;
    public TextMeshProUGUI txtTimer; // <-- SÓ ESSE VOCÊ PRECISA ARRASTAR LÁ NO UNITY!

    [Header("Configurações do Pitstop")]
    public float tempoMaximoPitstop = 20f;
    public float margemToleranciaExata = 0.5f;

    private float _timerPitstop;
    private bool _pitstopAtivo = false;
    private bool _jogoAcabou = false;

    void Start()
    {
        if (painelDerrota != null) painelDerrota.SetActive(false);
        _timerPitstop = tempoMaximoPitstop;
        AtualizarTextoTimer();
    }

    void Update()
    {
        if (_jogoAcabou || !_pitstopAtivo) return;

        _timerPitstop -= Time.deltaTime;

        if (_timerPitstop <= 0)
        {
            _timerPitstop = 0;
            AtualizarTextoTimer();
            ValidarAjustesPitstop();
        }
        else
        {
            AtualizarTextoTimer();
        }
    }

    private void AtualizarTextoTimer()
    {
        if (txtTimer != null)
        {
            txtTimer.text = $"Pitstop: {_timerPitstop.ToString("F0")}s";
        }
    }

    public void IniciarPitstop()
    {
        if (_jogoAcabou) return;

        _pitstopAtivo = true;
        _timerPitstop = tempoMaximoPitstop;
        AtualizarTextoTimer();

        if (carData != null) carData.IniciarPitstop();

        // Procura a câmera na marra e liga o Pitstop
        ChangePerson trocaCamera = FindFirstObjectByType<ChangePerson>();
        if (trocaCamera != null) trocaCamera.AtivarModoPitstopApenas();
    }

    public void ValidarAjustesPitstop()
    {
        if (!_pitstopAtivo || _jogoAcabou) return;
        _pitstopAtivo = false;

        if (carData == null) return;

        bool tempOk = carData.temperatura < 60f;
        bool gasolinaOk = carData.gasolina >= 10f;
        bool pressaoOk = Mathf.Abs(carData.pressao - 30f) <= margemToleranciaExata;
        bool emissaoOk = Mathf.Abs(carData.emissao - 30f) <= margemToleranciaExata;
        bool potenciaOk = Mathf.Abs(carData.potencia - 30f) <= margemToleranciaExata;

        if (tempOk && gasolinaOk && pressaoOk && emissaoOk && potenciaOk)
        {
            if (txtTimer != null) txtTimer.text = "Pitstop concluído!";
            FinalizarPitstopESeguirJogo();
        }
        else
        {
            Derrota("As configurações não atenderam aos requisitos exigidos no tempo certo!");
        }
    }

    private void FinalizarPitstopESeguirJogo()
    {
        // 1. CAÇA A CÂMERA E VOLTA PRO MONITOR NA MARRA
        ChangePerson trocaCamera = FindFirstObjectByType<ChangePerson>();
        if (trocaCamera != null)
        {
            trocaCamera.BotaoVoltarAoMonitor();
        }

        // 2. Libera o CarData
        if (carData != null) carData.ValidarPitstop();

        // 3. CAÇA A PISTA E VOLTA A CORRER NA MARRA
        PitsTop gerenciadorPista = FindFirstObjectByType<PitsTop>();
        if (gerenciadorPista != null)
        {
            gerenciadorPista.RetomarCorrida();
        }
    }

    public void Derrota(string motivo)
    {
        if (_jogoAcabou) return;
        _jogoAcabou = true;
        _pitstopAtivo = false;

        if (painelDerrota != null) painelDerrota.SetActive(true);
        if (txtMensagemDerrota != null) txtMensagemDerrota.text = motivo;
        Time.timeScale = 0f;
    }
}