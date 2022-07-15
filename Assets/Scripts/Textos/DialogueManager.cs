using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.InputSystem;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;
    public Animator animator;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI SpeechText;
    public TextMeshProUGUI[] OptionText;
    private GameObject _talker;
    private DialogueNode _currentNode;
    private DialogueNode _NextNode;
    private DialogueNode _LastNode;
    bool shortConversation = false;
    bool MediumConversation = false;
    bool LongConversation = false;
    private IEnumerator coroutine;


    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        Localizator.OnLanguageChangeDelegate += OnLanguageChanged;
        InputManager.NextText += MenuSelect;
    }

    private void OnDisable()
    {
        Localizator.OnLanguageChangeDelegate -= OnLanguageChanged;
        InputManager.NextText -= MenuSelect;
    }

    public void StartConversation(Conversation dialogueData, GameObject talker)
    {
        shortConversation = true;
        MediumConversation = false;
        LongConversation = false;
        _talker = talker;
        _currentNode = dialogueData.StartNode;
        nameText.text = Localizator.GetText(dialogueData.Key);
        SetText(_currentNode);
        ShowDialogue();
    }
    public void StartConversationMedium(ConversationMedium dialogueData, GameObject talker)
    {
        shortConversation = false;
        MediumConversation = true;
        LongConversation = false;
        _talker = talker;
        _currentNode = dialogueData.StartNode;
        _NextNode = dialogueData.SecondNode;
        nameText.text = Localizator.GetText(dialogueData.Key);
        SetText(_currentNode);
        ShowDialogue();
    }
    public void StartConversationLong(LongConversation dialogueData, GameObject talker)
    {
        shortConversation = false;
        MediumConversation = false;
        LongConversation = true;
        _talker = talker;
        _currentNode = dialogueData.StartNode;
        _NextNode = dialogueData.SecondNode;
        _LastNode = dialogueData.LastNode;
        nameText.text = Localizator.GetText(dialogueData.Key);
        SetText(_currentNode);
        ShowDialogue();
    }

    private void SetText(DialogueNode currentNode)
    {
        SpeechText.text = "";
        coroutine = textEffect(Localizator.GetText(currentNode.Key));
        StartCoroutine(coroutine);
       
    }
     IEnumerator  textEffect(String text)
    {
        foreach (var character in text)
        {

            SpeechText.text = SpeechText.text + character;
            yield return new WaitForSeconds(0.02f);
        }
        
    }
    void OnLanguageChanged()
    {
        if (_currentNode != null)
        {
            SetText(_currentNode);
        }
    }
   
    private void DoEndNode(EndNode endNode)
    {
        endNode.OnChosen(_talker);
        HideDialogue();
    }

    public void ShowDialogue()
    {
        animator.SetBool("Show", true);
    }
    public void HideDialogue()
    {
        animator.SetBool("Show", false);
    }

    public void MenuSelect()
    {
        StopCoroutine(coroutine);
        if (shortConversation)
        {
            HideDialogue();
        }
        if (MediumConversation)
        {
            if(_currentNode != _NextNode)
            {
                _currentNode = _NextNode;
                SetText(_currentNode);
            }
            else
            {
                HideDialogue();
            }
        }
        if (LongConversation)
        {
            if (_currentNode != _NextNode && _currentNode != _LastNode)
            {
                _currentNode = _NextNode;
                SetText(_currentNode);
            }
            else
            {
                if (_currentNode == _NextNode)
                {
                    _currentNode = _LastNode;
                    SetText(_currentNode);
                }
                else
                {
                    HideDialogue();
                }  
            }
        }
        
    }
}
