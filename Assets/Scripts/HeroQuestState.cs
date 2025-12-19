using UnityEngine;

namespace HeroCharacter
{
    /// <summary>
    /// Lightweight quest flag to track Gwen's quest on the hero.
    /// Attach to the hero character object so other behaviours (e.g. GwenInteraction)
    /// can read or update the current status.
    /// </summary>
    public class HeroQuestState : MonoBehaviour
    {
        [SerializeField] QuestStatus initialStatus = QuestStatus.NotStarted;

        public QuestStatus CurrentStatus { get; private set; }

        void Awake()
        {
            CurrentStatus = initialStatus;
        }

        public void StartQuest()
        {
            if (CurrentStatus == QuestStatus.NotStarted)
            {
                CurrentStatus = QuestStatus.InProgress;
            }
        }

        public void CompleteQuest()
        {
            if (CurrentStatus == QuestStatus.InProgress)
            {
                CurrentStatus = QuestStatus.Complete;
            }
        }

        public void ResetQuest()
        {
            CurrentStatus = QuestStatus.NotStarted;
        }
    }

    public enum QuestStatus
    {
        NotStarted,
        InProgress,
        Complete
    }
}
