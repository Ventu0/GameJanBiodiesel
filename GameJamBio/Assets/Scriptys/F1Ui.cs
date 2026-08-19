using TMPro;
using UnityEngine;

public class F1Ui : MonoBehaviour
{
    [SerializeField] private float Speed;
    [SerializeField] private TMP_Text SpeedTXT;
    [SerializeField] private float MinimeValue = 190f;
    [SerializeField] private float MaxmeValue = 300f;
    [SerializeField] private float VelocidadeVariacao = 2f;
    private float _speedAtual;
    void Start()
    {
        Speed = Random.Range(MinimeValue, MaxmeValue + 1);
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
}
