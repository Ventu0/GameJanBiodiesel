using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class ChangePerson : MonoBehaviour
{
    public enum ModoCamera { Monitor, Corrida, Pitstop }

    [Header("Estado Atual")]
    public ModoCamera modoAtual = ModoCamera.Monitor;

    [Header("Câmeras")]
    [SerializeField] private GameObject cameraMonitor;
    [SerializeField] private GameObject[] camerasPista;
    [SerializeField] private GameObject[] camerasPitstop;

    [Header("Configuração de Pitstop")]
    [SerializeField] private float tempoParaLiberarPitstop = 60f;

    private int _cameraAtual = 0;
    private bool _pitstopLiberado = false;
    private float _cronometro = 0f;

    private void Awake()
    {
        AtualizarCameras();
    }

    private void Update()
    {
        // Se o pitstop ainda não foi liberado, conta o tempo
        if (!_pitstopLiberado)
        {
            _cronometro += Time.deltaTime;

            // Quando o cronômetro atinge o tempo configurado (ex: 10 segundos)
            if (_cronometro >= tempoParaLiberarPitstop)
            {
                _pitstopLiberado = true;

                // Força a troca imediata para a câmera de Pitstop
                AtivarModoPitstopApenas();

                // Inicia o minigame de 30s e a checagem de gasolina no CarData
                CarData carData = FindFirstObjectByType<CarData>();
                if (carData != null)
                {
                    carData.IniciarPitstop();
                }
            }
        }
    }

    public void OnChange(InputValue value)
    {
        if (modoAtual == ModoCamera.Monitor) return;
        TrocarProximaCamera();
    }

    public void BotaoEntrarNaCorrida()
    {
        modoAtual = ModoCamera.Corrida;
        _cameraAtual = 0;
        AtualizarCameras();

        if (EventSystem.current != null)
        {
            EventSystem.current.SetSelectedGameObject(null);
        }
    }

    public void BotaoVoltarAoMonitor()
    {
        modoAtual = ModoCamera.Monitor;
        AtualizarCameras();
    }

    // Função chamada pelo CarData para trocar a câmera sem criar loop infinito
    public void AtivarModoPitstopApenas()
    {
        _pitstopLiberado = true;
        modoAtual = ModoCamera.Pitstop;
        _cameraAtual = 0;
        AtualizarCameras();
    }

    public void TrocarProximaCamera()
    {
        if (modoAtual == ModoCamera.Corrida)
        {
            if (camerasPista == null || camerasPista.Length == 0) return;

            _cameraAtual++;

            if (_cameraAtual >= camerasPista.Length)
            {
                BotaoVoltarAoMonitor();
                return;
            }
        }
        else if (modoAtual == ModoCamera.Pitstop)
        {
            if (camerasPitstop == null || camerasPitstop.Length == 0) return;
            _cameraAtual = (_cameraAtual + 1) % camerasPitstop.Length;
        }

        AtualizarCameras();
    }

    private void AtualizarCameras()
    {
        if (cameraMonitor != null) cameraMonitor.SetActive(false);
        DesativarGrupo(camerasPista);
        DesativarGrupo(camerasPitstop);

        switch (modoAtual)
        {
            case ModoCamera.Monitor:
                if (cameraMonitor != null) cameraMonitor.SetActive(true);
                break;

            case ModoCamera.Corrida:
                if (camerasPista != null && camerasPista.Length > 0)
                {
                    camerasPista[_cameraAtual].SetActive(true);
                }
                break;

            case ModoCamera.Pitstop:
                if (camerasPitstop != null && camerasPitstop.Length > 0)
                {
                    camerasPitstop[_cameraAtual].SetActive(true);
                }
                break;
        }
    }

    private void DesativarGrupo(GameObject[] lista)
    {
        if (lista == null) return;
        foreach (GameObject obj in lista)
        {
            if (obj != null) obj.SetActive(false);
        }
    }
}