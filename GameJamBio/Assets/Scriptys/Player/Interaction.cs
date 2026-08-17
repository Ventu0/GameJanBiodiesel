using UnityEngine;
using UnityEngine.InputSystem;

public class Interaction : MonoBehaviour
{
    [SerializeField] private float _interactionRange = 3f;
    private Camera MainCamera;
    private Iinteractable Target;

    void Start()
    {
        MainCamera = Camera.main;
    }

    void Update()
    {
        if (Physics.Raycast(MainCamera.transform.position, MainCamera.transform.forward, out RaycastHit hit, _interactionRange))
        {
            if (hit.collider.TryGetComponent(out Iinteractable interactable))
            {
                if (Target == interactable) return;

                Target?.HideOutline();
                Target = interactable;
                Target.ShowOutline(); 
            }
            else
            {
                LimparAlvo();
            }
        }
        else
        {
            LimparAlvo();
        }
    }

    public void OnInteract(InputValue value)
    {
        Target?.Interact();
    }

    private void LimparAlvo()
    {
        if (Target != null)
        {
            Target.HideOutline();
            Target = null;
        }
    }
}