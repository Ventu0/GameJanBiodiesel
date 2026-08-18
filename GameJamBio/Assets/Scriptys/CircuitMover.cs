using UnityEngine;

public class CircuitMover : MonoBehaviour
{
    [Header("Pontos do circuito")]
    public Transform[] pontos;

    [Header("Configuração")]
    public int pontoInicial = 0;
    public float velocidade = 1f;

    private int pontoAtual;

    void Start()
    {
        if (pontos == null || pontos.Length == 0)
        {
            Debug.LogWarning(gameObject.name + ": Nenhum ponto foi configurado!");
            return;
        }

        pontoAtual = pontoInicial;

        // Coloca o cubo exatamente no ponto inicial
        transform.position = pontos[pontoAtual].position;
    }

    void Update()
    {
        if (pontos == null || pontos.Length == 0)
            return;

        Transform destino = pontos[pontoAtual];

        // Move o cubo em direção ao próximo ponto
        transform.position = Vector3.MoveTowards(
            transform.position,
            destino.position,
            velocidade * Time.deltaTime
        );

        // Quando chegar ao ponto, passa para o próximo
        if (Vector3.Distance(transform.position, destino.position) < 0.01f)
        {
            pontoAtual++;

            // Se chegou ao final, volta para o primeiro
            if (pontoAtual >= pontos.Length)
            {
                pontoAtual = 0;
            }
        }
    }
}