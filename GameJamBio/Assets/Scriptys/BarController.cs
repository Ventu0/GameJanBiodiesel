using UnityEngine;

public class BarController : MonoBehaviour
{
    [SerializeField] private RectTransform barra;

    [SerializeField] private float _passo = 20f;
    [SerializeField] private float _limiteEsquerda = -150f;
    [SerializeField] private float _limiteDireita = 150f;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void Aumentar()
    {
        MoverBarra(_passo);
    }

    public void Diminuir()
    {
        MoverBarra(-_passo);
    }

    private void MoverBarra(float valor)
    {
        Vector2 posicao = barra.anchoredPosition;

        posicao.x += valor;

        posicao.x = Mathf.Clamp(
            posicao.x,
            _limiteEsquerda,
            _limiteDireita
        );

        barra.anchoredPosition = posicao;
    }
}