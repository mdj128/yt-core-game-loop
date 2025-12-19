using System;
using System.Collections;
using HeroCharacter;
using TMPro;
using UnityEngine;

public class GwenInteraction : MonoBehaviour, IHeroInteractable
{
    [SerializeField] string initialText = "Press 'E' to Speak";

    // Reference to the dialog text in the scene
    TextMeshPro dialogText;
    // Child animator on the Gwen character object
    Animator animator;

    Coroutine autoStopCoroutine;

    [Header("Optional Auto-Stop")]
    [Tooltip("If > 0 and using SetBool mode, sets the bool back to false after this many seconds.")]
    [SerializeField] float autoStopAfterSeconds = 0f;

    [Header("Voice Over")]
    [Tooltip("Voice line for the initial help request (NotStarted).")]
    [SerializeField] AudioClip notStartedVoice;
    [Tooltip("Voice line when quest is already in progress.")]
    [SerializeField] AudioClip inProgressVoice;
    [Tooltip("Voice line when quest is complete.")]
    [SerializeField] AudioClip completeVoice;

    AudioSource audioSource;

    void Awake()
    {
        dialogText = GetComponentInChildren<TextMeshPro>();
        dialogText.text = initialText;
        animator = GetComponentInChildren<Animator>();
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = GetComponentInChildren<AudioSource>();
        }
    }

    public void Interact(HeroCharacterController interactor)
    {
        var questState = interactor.GetComponent<HeroQuestState>();
        var status = questState != null ? questState.CurrentStatus : QuestStatus.NotStarted;

        Debug.Log(questState);

        switch (status)
        {
            case QuestStatus.NotStarted:
                dialogText.text = "Please help! A bandit is nearby! Can you kill him and bring me his sash?";
                questState?.StartQuest();
                PlayVoice(notStartedVoice);
                break;
            case QuestStatus.InProgress:
                dialogText.text = "You're still looking for the bandit's sash, right?";
                PlayVoice(inProgressVoice);
                break;
            case QuestStatus.Complete:
                dialogText.text = "Thank you! You saved me from the bandit.";
                PlayVoice(completeVoice);
                break;
            default:
                dialogText.text = initialText;
                break;
        }

        animator.SetBool("IsTalking", true);

        float stopAfter = GetAutoStopDurationForCurrentClip();
        if (stopAfter > 0)
        {
            if (autoStopCoroutine != null)
            {
                StopCoroutine(autoStopCoroutine);
            }
            autoStopCoroutine = StartCoroutine(AutoStopCoroutine(stopAfter));
        }
    }

    void PlayVoice(AudioClip clip)
    {
        if (clip == null || audioSource == null)
        {
            return;
        }
        audioSource.Stop();
        audioSource.clip = clip;
        audioSource.Play();
    }

    float GetAutoStopDurationForCurrentClip()
    {
        if (audioSource != null && audioSource.clip != null)
        {
            return audioSource.clip.length;
        }
        return autoStopAfterSeconds;
    }

    IEnumerator AutoStopCoroutine(float delaySeconds)
    {
        yield return new WaitForSeconds(delaySeconds);
        animator.SetBool("IsTalking", false);
        dialogText.text = initialText;
    }
}
