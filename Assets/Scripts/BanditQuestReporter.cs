using HeroCharacter;
using TMPro;
using UnityEngine;

/// <summary>
/// When the bandit dies, shows a loot prompt and becomes interactable.
/// On interaction, completes the quest on the hero.
/// Attach to the same GameObject as CharacterCombatAgent on the bandit.
/// </summary>
public class BanditQuestReporter : MonoBehaviour, IHeroInteractable
{
    [Tooltip("Reference to the hero's quest state component.")]
    [SerializeField] HeroQuestState heroQuestState;

    [Tooltip("World-space TextMeshPro used to display the loot prompt.")]
    [SerializeField] TextMeshPro promptText;

    [Tooltip("Colliders to temporarily re-enable on death so the hero can interact.")]
    [SerializeField] Collider[] collidersToReenable;
    [SerializeField] bool useAllChildCollidersIfNoneProvided = true;

    CharacterCombatAgent agent;
    bool lootAvailable;
    (Collider collider, bool wasEnabled)[] colliderStates;

    void Awake()
    {
        agent = GetComponent<CharacterCombatAgent>();
        if (agent == null)
        {
            Debug.LogError("BanditQuestReporter requires CharacterCombatAgent on the same GameObject.", this);
            enabled = false;
            return;
        }

        if (promptText == null)
        {
            promptText = GetComponentInChildren<TextMeshPro>(true);
        }

        CacheColliders();
        agent.Died += HandleDeath;
        HidePrompt();
    }

    void OnDestroy()
    {
        if (agent != null)
        {
            agent.Died -= HandleDeath;
        }
    }

    void HandleDeath()
    {
        lootAvailable = true;
        RestoreCollidersForLoot();
        ShowPrompt("Loot Sash");
    }

    public void Interact(HeroCharacterController interactor)
    {
        if (!lootAvailable)
        {
            return;
        }

        lootAvailable = false;
        heroQuestState = heroQuestState != null ? heroQuestState : interactor.GetComponent<HeroQuestState>();

        if (heroQuestState != null)
        {
            heroQuestState.CompleteQuest();
            ShowPrompt("Sash Looted");
            RevertColliders();
        }
        else
        {
            Debug.LogWarning("BanditQuestReporter has no heroQuestState assigned; quest not completed.", this);
            HidePrompt();
        }
    }

    void ShowPrompt(string text)
    {
        if (promptText != null)
        {
            promptText.text = text;
            promptText.gameObject.SetActive(true);
        }
    }

    void HidePrompt()
    {
        if (promptText != null)
        {
            promptText.gameObject.SetActive(false);
        }
    }

    void CacheColliders()
    {
        if ((collidersToReenable == null || collidersToReenable.Length == 0) && useAllChildCollidersIfNoneProvided)
        {
            collidersToReenable = GetComponentsInChildren<Collider>(true);
        }

        if (collidersToReenable != null)
        {
            colliderStates = new (Collider, bool)[collidersToReenable.Length];
            for (int i = 0; i < collidersToReenable.Length; i++)
            {
                colliderStates[i] = (collidersToReenable[i], collidersToReenable[i].enabled);
            }
        }
    }

    void RestoreCollidersForLoot()
    {
        if (colliderStates == null)
        {
            return;
        }

        foreach (var entry in colliderStates)
        {
            if (entry.collider != null)
            {
                entry.collider.enabled = true;
            }
        }
    }

    void RevertColliders()
    {
        if (colliderStates == null)
        {
            return;
        }

        foreach (var entry in colliderStates)
        {
            if (entry.collider != null)
            {
                entry.collider.enabled = entry.wasEnabled;
            }
        }
    }
}
