using UnityEngine;

public class CreditsScroll : MonoBehaviour
{
    public float velocidade = 40f;
    public float inicioY = -250f;
    public float fimY = 500f;

    private RectTransform rect;

    void Start()
    {
        rect = GetComponent<RectTransform>();
        Reiniciar();
    }

    void Update()
    {
        rect.anchoredPosition += Vector2.up * velocidade * Time.deltaTime;

        if (rect.anchoredPosition.y >= fimY)
        {
            Reiniciar();
        }
    }

    void Reiniciar()
    {
        Vector2 pos = rect.anchoredPosition;
        pos.y = inicioY;
        rect.anchoredPosition = pos;
    }
}