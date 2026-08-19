using UnityEngine;

public class MoverTexturaRoda : MonoBehaviour
{
    [SerializeField] private float velocidade = 5f;
    private Renderer _renderer;

    void Start()
    {
        _renderer = GetComponent<Renderer>();
    }

    void Update()
    {
        float offset = Time.time * velocidade;
        // Move o offset do material para dar a sensação de que o pneu está girando
        _renderer.material.mainTextureOffset = new Vector2(offset, 0);
    }
}