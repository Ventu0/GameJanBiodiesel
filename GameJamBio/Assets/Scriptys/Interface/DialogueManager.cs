using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;

    [Header("UI")]
    public TextMeshProUGUI characterName;
    public TextMeshProUGUI dialogueArea;

    [Header("Configuração")]
    public float typingSpeed = 0.02f;

    private Queue<DialogueLine> lines = new Queue<DialogueLine>();

    public bool isDialogueActive = false;

    private void Awake()
    {
        // Cria o Singleton.
        if (instance == null)
        {
            instance = this;
        }
        typingSpeed = 0.02f;
    }

    public void StartDialogue(Dialogue dialogue)
    {
        // Verifica se o diálogo existe.
        if (dialogue == null)
        {
            Debug.LogError("falta o dialogo");
            return;
        }

        // Verifica se os textos estão configurados.
        if (characterName == null)
        {
            Debug.LogError(
                "falta o persnagem",
                this
            );

            return;
        }


        if (dialogueArea == null)
        {
            Debug.LogError(
                "falta algo ai",
                this
            );

            return;
        }

        isDialogueActive = true;

        // Limpa qualquer diálogo anterior.
        lines.Clear();

        // Coloca todas as falas na fila.
        foreach (DialogueLine dialogueLine in dialogue.dialogueLines)
        {
            if (dialogueLine != null)
            {
                lines.Enqueue(dialogueLine);
            }
        }

        // Mostra a primeira fala.
        DisplayNextDialogueLine();
    }

    public void DisplayNextDialogueLine()
    {
        // Se não existem mais falas,
        // termina o diálogo.
        if (lines.Count == 0)
        {
            EndDialogue();
            return;
        }

        // Pega a próxima fala.
        DialogueLine currentLine = lines.Dequeue();

        // Verifica se o personagem existe.
        if (currentLine.character != null)
        {
            characterName.text = currentLine.character.name;
        }
        else
        {
            characterName.text = "";
        }

        // Para qualquer animação de texto anterior.
        StopAllCoroutines();

        // Começa a escrever a fala.
        StartCoroutine(TypeSentence(currentLine));
    }

    private IEnumerator TypeSentence(DialogueLine dialogueLine)
    {
        dialogueArea.text = "";

        // Escreve a fala letra por letra.
        foreach (char letter in dialogueLine.line)
        {
            dialogueArea.text += letter;

            yield return new WaitForSeconds(typingSpeed);
        }
    }

    private void EndDialogue()
    {
        isDialogueActive = false;
    }
}