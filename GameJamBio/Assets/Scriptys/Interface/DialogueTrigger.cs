using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueCharacter
{
    public string name;
}

[System.Serializable]
public class DialogueLine
{
    public DialogueCharacter character;

    [TextArea(3, 10)]
    public string line;

}

[System.Serializable]
public class Dialogue
{
    public List<DialogueLine> dialogueLines = new List<DialogueLine>();
}

public class DialogueTrigger : MonoBehaviour
{
    [Header("Diálogo")]
    public Dialogue dialogue;

    [Header("Referência")]
    public DialogueManager dialogueManager;

    private void Start()
    {
        // Se o Dialogue Manager não foi colocado no Inspector,
        // tenta encontrar um automaticamente na cena.
        if (dialogueManager == null)
        {
            dialogueManager = FindFirstObjectByType<DialogueManager>();
        }

        // Verifica se encontrou o Dialogue Manager.
        if (dialogueManager == null)
        {
            Debug.LogError(
                "DialogueTrigger: Nenhum DialogueManager foi encontrado na cena!",
                this
            );

            return;
        }

        // Verifica se existe um diálogo configurado.
        if (dialogue == null)
        {
            Debug.LogError(
                "DialogueTrigger: O diálogo está vazio!",
                this
            );

            return;
        }

        // Começa o diálogo.
        TriggerDialogue();
    }

    public void TriggerDialogue()
    {
        // Segurança para evitar NullReferenceException.
        if (dialogueManager == null)
        {
            Debug.LogError(
                "DialogueTrigger: dialogueManager está vazio!",
                this
            );

            return;
        }

        if (dialogue == null)
        {
            Debug.LogError(
                "DialogueTrigger: dialogue está vazio!",
                this
            );

            return;
        }

        dialogueManager.StartDialogue(dialogue);
    }
}