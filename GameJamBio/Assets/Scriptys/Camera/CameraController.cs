using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CameraController : MonoBehaviour
{
    [Header("Referências")]
    public CarData carData;
    public GameObject painelDerrota;
    public TextMeshProUGUI txtMensagemDerrota;
    public TextMeshProUGUI txtTimer;

    [Header("Configurações do Pitstop")]
    public float tempoMaximoPitstop = 20f;
    public float margemToleranciaExata = 0.5f;

    private float _timerPitstop;

    private bool _pitstopAtivo = false;
    private bool _jogoAcabou = false;
    [Header("Cena de Derrota")]
    [SerializeField] private string nomeCenaDerrota = "GameOver";

    void Start()
    {
        if (painelDerrota != null)
        {
            painelDerrota.SetActive(false);
        }

        _timerPitstop = tempoMaximoPitstop;

        AtualizarTextoTimer();
    }

    void Update()
    {
        if (_jogoAcabou)
            return;

        if (!_pitstopAtivo)
            return;

        _timerPitstop -= Time.deltaTime;

        if (_timerPitstop <= 0f)
        {
            _timerPitstop = 0f;

            AtualizarTextoTimer();

            ValidarAjustesPitstop();
        }
        else
        {
            AtualizarTextoTimer();
        }
    }

    // =========================================================
    // ATUALIZAR TIMER
    // =========================================================

    private void AtualizarTextoTimer()
    {
        if (txtTimer != null)
        {
            txtTimer.text =
                $"Pitstop: {Mathf.CeilToInt(_timerPitstop)}s";
        }
    }

    // =========================================================
    // INICIAR PIT STOP
    // =========================================================

    public void IniciarPitstop()
    {
        if (_jogoAcabou)
            return;

        if (_pitstopAtivo)
            return;

        Debug.Log("🔧 PIT STOP INICIADO");

        _pitstopAtivo = true;

        _timerPitstop = tempoMaximoPitstop;

        AtualizarTextoTimer();

        if (carData != null)
        {
            carData.IniciarPitstop();
        }

        ChangePerson trocaCamera =
            FindFirstObjectByType<ChangePerson>();

        if (trocaCamera != null)
        {
            trocaCamera.AtivarModoPitstopApenas();
        }
    }

    // =========================================================
    // VALIDAR PIT STOP
    // =========================================================

    public void ValidarAjustesPitstop()
    {
        if (!_pitstopAtivo)
            return;

        if (_jogoAcabou)
            return;

        _pitstopAtivo = false;

        if (carData == null)
        {
            Debug.LogError(
                "❌ CarData não encontrado!"
            );

            return;
        }

        /*
         * As regras abaixo são as mesmas
         * do seu CameraController original.
         */

        bool tempOk =
            carData.temperatura < 60f;

        bool gasolinaOk =
            carData.gasolina >= 10f;

        bool pressaoOk =
            Mathf.Abs(carData.pressao - 30f)
            <= margemToleranciaExata;

        bool emissaoOk =
            Mathf.Abs(carData.emissao - 30f)
            <= margemToleranciaExata;

        bool potenciaOk =
            Mathf.Abs(carData.potencia - 30f)
            <= margemToleranciaExata;

        Debug.Log("========= RESULTADO PIT STOP =========");

        Debug.Log(
            "Temperatura OK: " + tempOk
        );

        Debug.Log(
            "Gasolina OK: " + gasolinaOk
        );

        Debug.Log(
            "Pressão OK: " + pressaoOk
        );

        Debug.Log(
            "Emissão OK: " + emissaoOk
        );

        Debug.Log(
            "Potência OK: " + potenciaOk
        );

        bool pitstopCorreto =
            tempOk &&
            gasolinaOk &&
            pressaoOk &&
            emissaoOk &&
            potenciaOk;

        if (pitstopCorreto)
        {
            if (txtTimer != null)
            {
                txtTimer.text =
                    "Pitstop concluído!";
            }

            Debug.Log(
                "✅ PIT STOP CONCLUÍDO COM SUCESSO"
            );

            FinalizarPitstopESeguirJogo();
        }
        else
        {
            Derrota(
                "As configurações não atenderam aos requisitos exigidos no tempo certo!"
            );
        }
    }

    // =========================================================
    // FINALIZAR E VOLTAR PARA CORRIDA
    // =========================================================

    private void FinalizarPitstopESeguirJogo()
    {
        /*
         * 1. Libera o CarData.
         *
         * Importante:
         * CarData NÃO chama mais RetomarCorrida.
         */

        if (carData != null)
        {
            carData.EncerrarPitstop();
        }

        /*
         * 2. Volta para a câmera da corrida.
         */

        ChangePerson trocaCamera =
            FindFirstObjectByType<ChangePerson>();

        if (trocaCamera != null)
        {
            trocaCamera.BotaoEntrarNaCorrida();
        }

        /*
         * 3. Inicia novamente o loop.
         *
         * SOMENTE AQUI a corrida é retomada.
         */

        PitsTop gerenciadorPista =
            FindFirstObjectByType<PitsTop>();

        if (gerenciadorPista != null)
        {
            Debug.Log(
                "🏁 Enviando comando para retomar corrida."
            );

            gerenciadorPista.RetomarCorrida();
        }
        else
        {
            Debug.LogError(
                "❌ PitsTop não encontrado!"
            );
        }
    }

    // =========================================================
    // DERROTA
    // =========================================================

    public void Derrota(string motivo)
    {
        if (_jogoAcabou)
            return;

        _jogoAcabou = true;

        _pitstopAtivo = false;

        if (carData != null)
        {
            carData.EncerrarPitstop();
        }

        Debug.Log(
            "❌ DERROTA: " + motivo
        );

        // Garante que o jogo não fique pausado
        Time.timeScale = 1f;

        // Vai para a cena de derrota
        SceneManager.LoadScene(nomeCenaDerrota);
    }
}