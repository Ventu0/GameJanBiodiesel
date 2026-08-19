using UnityEngine;

public class TVHover : MonoBehaviour
{
    [Header("Modelo que aparece")]
    public GameObject modeloParaMostrar;

    void Start()
    {
        // Começa escondido
        if (modeloParaMostrar != null)
        {
            modeloParaMostrar.SetActive(false);
        }
    }

    void OnMouseEnter()
    {
        if (modeloParaMostrar != null)
        {
            modeloParaMostrar.SetActive(true);
        }
    }

    void OnMouseExit()
    {
        if (modeloParaMostrar != null)
        {
            modeloParaMostrar.SetActive(false);
        }
    }
}