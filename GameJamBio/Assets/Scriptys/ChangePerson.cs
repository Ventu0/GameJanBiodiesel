using UnityEngine;
using UnityEngine.InputSystem;

public class ChangePerson : MonoBehaviour
{
    [SerializeField] private GameObject[] _cameras; // Array com todas as câmeras
    private int _cameraAtual = 0; // Índice da câmera ativa no momento

    private void Awake()
    {
        AtualizarCameras();
    }

    public void OnChange(InputValue value)
    {
        Change();
    }

    public void Change()
    {
        if (_cameras == null || _cameras.Length == 0) return;

        // Avança para a próxima câmera e volta para 0 quando chega no fim
        _cameraAtual = (_cameraAtual + 1) % _cameras.Length;

        AtualizarCameras();
    }

    private void AtualizarCameras()
    {
        if (_cameras == null) return;

        // Ativa apenas a câmera do índice atual e desativa todas as outras
        for (int i = 0; i < _cameras.Length; i++)
        {
            if (_cameras[i] != null)
            {
                _cameras[i].SetActive(i == _cameraAtual);
            }
        }
    }
}