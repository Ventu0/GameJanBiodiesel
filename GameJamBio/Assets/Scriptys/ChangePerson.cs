using UnityEngine;
using UnityEngine.InputSystem;

public class ChangePerson : MonoBehaviour
{
    [SerializeField] private GameObject _CamPerson;
    [SerializeField]private GameObject _CamPerson2;

    private void Awake()
    {
     _CamPerson2.SetActive(false);   
    }

    public void OnChange(InputValue Value)
    {
        Change();
    }

    public void Change()
    {
        _CamPerson2.SetActive(true);
        bool camera1Ativa = _CamPerson.activeSelf;

        _CamPerson.SetActive(!camera1Ativa);
        _CamPerson2.SetActive(camera1Ativa);
    }
}
