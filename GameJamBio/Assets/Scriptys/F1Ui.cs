using System.Collections;
using TMPro;
using UnityEngine;

public class F1Ui : MonoBehaviour
{
    [SerializeField] private float Speed;
    [SerializeField] private TMP_Text SpeedTXT;
    [SerializeField] private float MinimeValue = 190f;
    [SerializeField] private float MaxmeValue = 300f;
    [SerializeField] private float VelocidadeVariacao = 2f;
    [SerializeField] private TMP_Text lapsTXT;
    private float laps = 17f;
    private int _voltaAtual = 1;
    private float _speedAtual;
    void Start()
    {
        Speed = Random.Range(MinimeValue, MaxmeValue + 1);
        StartCoroutine(TempoVoltas());
        
        if (lapsTXT != null)
        {
            lapsTXT.text = _voltaAtual.ToString();
        }
    }

    void Update()
    {
        float fator = Mathf.PingPong(Time.time * VelocidadeVariacao * 10f, MaxmeValue - MinimeValue);
        _speedAtual = MinimeValue + fator;

        if (SpeedTXT != null)
        {
            SpeedTXT.text = _speedAtual.ToString("0");
        }
    }
    public IEnumerator TempoVoltas()
    {
        // Loop para continuar somando voltas a cada 15 segundos
        while (true)
        {
            yield return new WaitForSeconds(laps);

            _voltaAtual++; // Incrementa 1 no número da volta

            if (lapsTXT != null)
            {
                lapsTXT.text = _voltaAtual.ToString();
            }
        }
    }
}
