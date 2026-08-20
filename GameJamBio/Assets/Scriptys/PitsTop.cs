using System.Collections;
using UnityEngine;
using UnityEngine.Splines;

public class PitsTop : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _cars;

    [Header("Conexão com o Minigame")]
    public CameraController cameraController;

    [Header("Configurações de Tempo")]
    [SerializeField]
    private float _tempoAtivo = 10f;

    private Coroutine _rotinaAtual;

    private bool _corridaAtiva = false;
    private bool _pitstopAtivo = false;

    void Start()
    {
        IniciarCorrida();
    }

    // =========================================================
    // INICIAR CORRIDA
    // =========================================================

    public void IniciarCorrida()
    {
        if (_rotinaAtual != null)
        {
            StopCoroutine(_rotinaAtual);
            _rotinaAtual = null;
        }

        _corridaAtiva = true;
        _pitstopAtivo = false;

        Debug.Log("🏎️ ===============================");
        Debug.Log("🏎️ CORRIDA INICIADA");
        Debug.Log("🏎️ ===============================");

        _rotinaAtual =
            StartCoroutine(RotinaDeCorrida());
    }

    // =========================================================
    // ROTINA DA CORRIDA
    // =========================================================

    private IEnumerator RotinaDeCorrida()
    {
        AtivarCarros(true);

        Debug.Log(
            "🏁 Carros correndo por " +
            _tempoAtivo +
            " segundos."
        );

        yield return new WaitForSeconds(_tempoAtivo);

        if (!_corridaAtiva)
        {
            yield break;
        }

        _corridaAtiva = false;
        _pitstopAtivo = true;

        Debug.Log("🔧 ===============================");
        Debug.Log("🔧 CORRIDA FINALIZADA");
        Debug.Log("🔧 INICIANDO PIT STOP");
        Debug.Log("🔧 ===============================");

        AtivarCarros(false);

        ResetarSplineDosCarros();

        if (cameraController != null)
        {
            cameraController.IniciarPitstop();
        }
        else
        {
            Debug.LogError(
                "❌ CameraController não foi conectado ao PitsTop!"
            );
        }

        _rotinaAtual = null;
    }

    // =========================================================
    // RETOMAR CORRIDA
    // =========================================================

    public void RetomarCorrida()
    {
        if (_corridaAtiva)
        {
            Debug.Log(
                "⚠️ A corrida já está ativa. Ignorando chamada duplicada."
            );

            return;
        }

        Debug.Log("🏎️ PIT STOP CONCLUÍDO!");
        Debug.Log("🏁 REINICIANDO LOOP DA CORRIDA...");

        IniciarCorrida();
    }

    // =========================================================
    // PARAR CORRIDA
    // =========================================================

    public void PararCorrida()
    {
        _corridaAtiva = false;
        _pitstopAtivo = false;

        if (_rotinaAtual != null)
        {
            StopCoroutine(_rotinaAtual);
            _rotinaAtual = null;
        }

        AtivarCarros(false);
    }

    // =========================================================
    // ATIVAR / DESATIVAR CARROS
    // =========================================================

    private void AtivarCarros(bool estado)
    {
        if (_cars == null)
            return;

        foreach (GameObject car in _cars)
        {
            if (car != null)
            {
                car.SetActive(estado);
            }
        }
    }

    // =========================================================
    // RESETAR SPLINE
    // =========================================================

    private void ResetarSplineDosCarros()
    {
        if (_cars == null)
            return;

        foreach (GameObject car in _cars)
        {
            if (car == null)
                continue;

            SplineAnimate spline =
                car.GetComponent<SplineAnimate>();

            if (spline != null)
            {
                spline.ElapsedTime = 0f;
            }
        }
    }

    // =========================================================
    // ESTADOS
    // =========================================================

    public bool CorridaEstaAtiva()
    {
        return _corridaAtiva;
    }

    public bool PitstopEstaAtivo()
    {
        return _pitstopAtivo;
    }
}