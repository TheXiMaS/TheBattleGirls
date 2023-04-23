using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementSystem : MonoBehaviour
{
    [SerializeField] private List<Achievement> achievements;

    public void UnlockAchievement(int achievementId)
    {
        foreach (Achievement achievement in achievements)
        {
            if (achievement.id == achievementId && !achievement.isUnlocked)
            {
                achievement.Unlock();
                Debug.Log("Achievement unlocked: " + achievement.title);
            }
        }
    }
}

[System.Serializable]
public class Achievement
{
    public int id;
    public string title;
    public string description;
    public bool isUnlocked;

    public void Unlock()
    {
        isUnlocked = true;
    }
}
