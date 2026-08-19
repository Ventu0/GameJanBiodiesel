using System.Collections;
using UnityEngine;
using UnityEngine.Splines; // Importante: inclui a biblioteca de Splines da Unity

public class PitsTop : MonoBehaviour
{
    [SerializeField] private GameObject[] _cars;

    [Header("Configurações de Tempo")]
    [SerializeField] private float _tempoAtivo = 10f;
    [SerializeField] private float _tempoDesativado = 5f;

    void Start()
    {
        StartCoroutine(TempoEspera());
    }

    private void ResetarSplineDosCarros()
    {
        if (_cars == null) return;

        foreach (GameObject car in _cars)
        {
            if (car != null)
            {
                // Pega o componente SplineAnimate do carro
                SplineAnimate spline = car.GetComponent<SplineAnimate>();

                if (spline != null)
                {
                    // Método nativo da Unity para resetar o Spline para o início
                    spline.ElapsedTime = 0f;
                }
            }
        }
    }

    public IEnumerator TempoEspera()
    {
        while (true)
        {
            // 1. Aguarda os carros rodarem na Spline
            yield return new WaitForSeconds(_tempoAtivo);

            // 2. Desativa os carros
            if (_cars != null)
            {
                foreach (GameObject car in _cars)
                {
                    car?.SetActive(false);
                }
            }

            // 3. Reseta a posição deles na Spline para o tempo 0
            ResetarSplineDosCarros();

            // 4. Aguarda o tempo desativado
            yield return new WaitForSeconds(_tempoDesativado);

            // 5. Reativa os carros (eles vão começar a rodar do início da Spline)
            if (_cars != null)
            {
                foreach (GameObject car in _cars)
                {
                    car?.SetActive(true);
                }
            }
        }
    }
}