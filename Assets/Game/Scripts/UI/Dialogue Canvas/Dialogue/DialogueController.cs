using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DialogueController : MonoBehaviour
{
    public static DialogueController Instance;
    public TextMeshProUGUI textComponent;
    public GameObject dialogueEndAlert;
    private string[] lines;
    public float typingSpeed;
    private int index;
    private void Awake()
    {
        Instance = this;
        textComponent.text = string.Empty;
    }
    private void Start()
    {
        gameObject.SetActive(false);
    }
    void Update()
    {
        GoToNextLine();
    }
    public void StartDialogue()
    {
        gameObject.SetActive(true);
        index = 0;
        Player.Instance.isOnDialogue = true;
        ChooseDialogue();
        StartCoroutine(Type());
    }
    IEnumerator Type()
    {
        foreach (char letter in lines[index].ToCharArray())
        {
            textComponent.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        dialogueEndAlert.SetActive(true);
    }
    private void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(Type());
        }
        else
        {
            textComponent.text = string.Empty;
            gameObject.SetActive(false);
            Player.Instance.isOnDialogue = false;
        }
    }
    private void ChooseDialogue()
    {
        if (DialoguesClass.clothesShopOwnerDialogue.meetingCount == 0)
        {
            lines = DialoguesClass.clothesShopOwnerDialogue.firstMeetingDialogue;
            DialoguesClass.clothesShopOwnerDialogue.meetingCount++;
        }
        else
        {
            lines = DialoguesClass.clothesShopOwnerDialogue.nextMeetingsDialogue;
        }
    }
    private void GoToNextLine()
    {
        if (Input.GetKeyDown(KeyCode.E) && Player.Instance.isOnDialogue && !Player.Instance.isOnFade)
        {
            if (textComponent.text == lines[index])
            {
                dialogueEndAlert.SetActive(false);
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[index];
                dialogueEndAlert.SetActive(true);
            }
        }
    }
}
