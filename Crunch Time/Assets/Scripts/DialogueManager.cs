﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Permissions;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class DialogueManager : MonoBehaviour
{
    public Text currentSpeakerText;
    public Text dialogueText;

    private Queue<string> sentences;

    public Animator dialogueBoxAnimator;
    //public Animator npcFaceAnimator;

    //public List<TalkIconController> NPCPrefabs;

    //public bool battleTime;
    //public string battleScene;

    public List<GameObject> talkButtons;

    private bool _isTalking;


    void Start()
    {
        sentences = new Queue<string>();
        _isTalking = false;
    }

    // public but unsettable IsTalking variable lets others know
    // if we're already busy.
    public bool IsTalking
    {
        get { return _isTalking; }
    }

    void Update()
    {
      // Only listen for next-bit-of-dialogue key if we're in conversation
      if (_isTalking)
      {
        if (Input.GetKeyUp(KeyCode.Space)) 
        {
          DisplayNextSentence();
        }
      }
    }

    public void StartDialogue(Dialogue dialogue)
    {
        foreach (GameObject talkButton in talkButtons)
        {
            talkButton.gameObject.SetActive(false);
        }

        dialogueBoxAnimator.SetBool("IsOpen", true);
        //npcFaceAnimator.SetBool("IsOpen", true);

        _isTalking = true;

        currentSpeakerText.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }


        DisplayNextSentence();

    }

    public void DisplayNextSentence()
    {
        if (_isTalking)
        {
            if (sentences.Count == 0)
            {
                EndDialogue();
                return;
            }

            string sentence = sentences.Dequeue();
            StopAllCoroutines();
            StartCoroutine(TypeSentence(sentence));
        }

    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    void EndDialogue()
    {
        foreach (GameObject talkButton in talkButtons)
        {

        }

        

        //if(battleTime = true)
        //    SceneManager.LoadScene(battleScene);

        dialogueBoxAnimator.SetBool("IsOpen", false);
        //npcFaceAnimator.SetBool("IsOpen", false);

        _isTalking = false;
    }
}
