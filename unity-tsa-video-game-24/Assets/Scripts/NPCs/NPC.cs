using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NPC : MonoBehaviour
{
    private PlayerInteractS playerInteract;
    private InventoryPage inventoryUI;
    [SerializeField]
    private GameObject dialoguePanel;

    [SerializeField]
    private TMP_Text dialogueText;
    public string[] dialogue;
    public int index;

    public float wordSpeed;
    // public bool playerIsClose;

    void Start() {
        PlayerInteractS.fairyTalk += PuzzleOneDialogue;
        InventoryPage.gardenComplete += EndPuzzle;
        CloseDialogue();
        index = 0;
    }
    // Update is called once per frame

    IEnumerator Typing() {
        foreach (char letter in dialogue[index].ToCharArray()) {
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }

    private void OnDestroy() {
        // if (puzzleOne) {
        PlayerInteractS.fairyTalk -= PuzzleOneDialogue;
        index = 1;
        // } ugh
    }

    public void NextLine() {
        if (index< dialogue.Length - 1) {
            index++;
            dialogueText.text="";
            StartCoroutine(Typing());
        } else {
            CloseDialogue();
        }
    }

    private void PuzzleOneDialogue() {
        print("dialogue called");
        dialoguePanel.SetActive(true);
        StartCoroutine(Typing());
        OnDestroy();
        
    }

    private void EndPuzzle() {
        CloseDialogue();
        InventoryPage.gardenComplete -= EndPuzzle;

    }

    private void CloseDialogue() {
        dialogueText.text="";
        // index=0;
        dialoguePanel.SetActive(false);
    }

//hi
}
