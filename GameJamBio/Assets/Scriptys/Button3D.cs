using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class Button3D : MonoBehaviour
{
    [Header("Evento de Clique")]
    public UnityEvent onClick;

    // Chamado automaticamente pela Unity quando o mouse clica em um objeto 3D com Collider
    private void OnMouseDown()
    {
        if (onClick != null)
        {
            onClick.Invoke();
        }
    }
}