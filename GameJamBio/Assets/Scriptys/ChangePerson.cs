using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class ChangePerson : MonoBehaviour
{
    public enum ModoCamera
    {
        Monitor,
        Corrida,
        Pitstop
    }

    [Header("Estado Atual")]
    public ModoCamera modoAtual =
        ModoCamera.Monitor;

    [Header("Câmeras")]
    [SerializeField]
    private GameObject cameraMonitor;

    [SerializeField]
    private GameObject[] camerasPista;

    [SerializeField]
    private GameObject[] camerasPitstop;

    private int _cameraAtual = 0;

    public static ChangePerson Instance
    {
        get;
        private set;
    }

    private void Awake()
    {
        Instance = this;

        AtualizarCameras();
    }

    // =========================================================
    // INPUT PARA TROCAR CÂMERA
    // =========================================================

    public void OnChange(InputValue value)
    {
        if (modoAtual == ModoCamera.Monitor)
            return;

        TrocarProximaCamera();
    }

    // =========================================================
    // ENTRAR NA CORRIDA
    // =========================================================

    public void BotaoEntrarNaCorrida()
    {
        modoAtual = ModoCamera.Corrida;

        _cameraAtual = 0;

        AtualizarCameras();

        if (EventSystem.current != null)
        {
            EventSystem.current
                .SetSelectedGameObject(null);
        }

        Debug.Log("📷 Câmera: CORRIDA");
    }

    // =========================================================
    // VOLTAR AO MONITOR
    // =========================================================

    public void BotaoVoltarAoMonitor()
    {
        modoAtual = ModoCamera.Monitor;

        _cameraAtual = 0;

        AtualizarCameras();

        Debug.Log("📷 Câmera: MONITOR");
    }

    // =========================================================
    // ATIVAR PIT STOP
    // =========================================================

    public void AtivarModoPitstopApenas()
    {
        modoAtual = ModoCamera.Pitstop;

        _cameraAtual = 0;

        AtualizarCameras();

        Debug.Log("📷 Câmera: PIT STOP");
    }

    // =========================================================
    // TROCAR CÂMERA
    // =========================================================

    public void TrocarProximaCamera()
    {
        if (modoAtual == ModoCamera.Corrida)
        {
            if (
                camerasPista == null ||
                camerasPista.Length == 0
            )
            {
                return;
            }

            _cameraAtual++;

            if (_cameraAtual >= camerasPista.Length)
            {
                _cameraAtual = 0;
            }
        }
        else if (modoAtual == ModoCamera.Pitstop)
        {
            if (
                camerasPitstop == null ||
                camerasPitstop.Length == 0
            )
            {
                return;
            }

            _cameraAtual++;

            if (_cameraAtual >= camerasPitstop.Length)
            {
                _cameraAtual = 0;
            }
        }

        AtualizarCameras();
    }

    // =========================================================
    // ATUALIZAR CÂMERAS
    // =========================================================

    private void AtualizarCameras()
    {
        if (cameraMonitor != null)
        {
            cameraMonitor.SetActive(false);
        }

        DesativarGrupo(camerasPista);

        DesativarGrupo(camerasPitstop);

        switch (modoAtual)
        {
            case ModoCamera.Monitor:

                if (cameraMonitor != null)
                {
                    cameraMonitor.SetActive(true);
                }

                break;


            case ModoCamera.Corrida:

                if (
                    camerasPista != null &&
                    camerasPista.Length > 0
                )
                {
                    camerasPista[_cameraAtual]
                        .SetActive(true);
                }

                break;


            case ModoCamera.Pitstop:

                if (
                    camerasPitstop != null &&
                    camerasPitstop.Length > 0
                )
                {
                    camerasPitstop[_cameraAtual]
                        .SetActive(true);
                }

                break;
        }
    }

    // =========================================================
    // DESATIVAR GRUPO
    // =========================================================

    private void DesativarGrupo(
        GameObject[] lista
    )
    {
        if (lista == null)
            return;

        foreach (GameObject obj in lista)
        {
            if (obj != null)
            {
                obj.SetActive(false);
            }
        }
    }
}