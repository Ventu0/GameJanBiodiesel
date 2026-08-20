using System.Collections;
using UnityEngine;
using UnityEngine.Splines;

public class PitsTop : MonoBehaviour
{
    [SerializeField] private GameObject[] _cars;

    [Header("Conexão com o Minigame")]
    public CameraController cameraController;

    [Header("Configurações de Tempo")]
    [SerializeField] private float _tempoAtivo = 10f;

    void Start()
    {
        // Começa a corrida no instante em que o jogo abre
        StartCoroutine(RotinaDeCorrida());
    }

    private void ResetarSplineDosCarros()
    {
        if (_cars == null) return;
        foreach (GameObject car in _cars)
        {
            if (car != null)
            {
                SplineAnimate spline = car.GetComponent<SplineAnimate>();
                if (spline != null) spline.ElapsedTime = 0f;
            }
        }
    }

    public IEnumerator RotinaDeCorrida()
    {
        // 1. Liga os carros e deixa eles correrem pelo tempo definido
        AtivarCarros(true);
        yield return new WaitForSeconds(_tempoAtivo);

        // 2. O tempo acabou! Desativa os carros e reseta a posição para o início da Spline
        AtivarCarros(false);
        ResetarSplineDosCarros();

        // 3. Aciona o minigame no CameraController
        if (cameraController != null)
        {
            cameraController.IniciarPitstop();
        }
        else
        {
            Debug.LogError("Atenção: Você esqueceu de arrastar o CameraController para o script PitsTop lá no Unity!");
        }
    }

    // O CameraController vai chamar esta função 13 segundos depois que você vencer
    public void RetomarCorrida()
    {
        StartCoroutine(RotinaDeCorrida());
    }

    private void AtivarCarros(bool estado)
    {
        if (_cars != null)
        {
            foreach (GameObject car in _cars)
            {
                car?.SetActive(estado);
            }
        }
    }
}