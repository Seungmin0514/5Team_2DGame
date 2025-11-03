using System;
using UnityEngine;

// Manages difficulty progression: Easy -> Normal -> Hard
// Attach this to a GameObject in your scene
public class DifficultyManager : MonoBehaviour
{
    [Header("=== Difficulty Data ===")]
    public DifficultyData easyDifficulty;
    public DifficultyData normalDifficulty;
    public DifficultyData hardDifficulty;

    [Header("=== Debug ===")]
    public bool showDebugLogs = true;

    // Current difficulty data
    public DifficultyData CurrentDifficultyData { get; private set; }
    public DifficultyData.DifficultyLevel CurrentLevel { get; private set; }
    public float CurrentSpeedMultiplier => CurrentDifficultyData?.speedMultiplier ?? 1.0f;

    // Event: fired when difficulty changes
    public event Action<DifficultyData> OnDifficultyChanged;

    private float gameStartTime;
    private bool isEasyCompleted = false;
    private bool isNormalCompleted = false;

    void Start()
    {
        gameStartTime = Time.time;
        SetDifficulty(DifficultyData.DifficultyLevel.Easy);

        if (showDebugLogs)
        {
            Debug.Log("[Difficulty] Game started - Easy difficulty");
            Debug.Log($"[Difficulty] Easy: {easyDifficulty.duration}s, Normal: {normalDifficulty.duration}s");
        }
    }

    void Update()
    {
        CheckDifficultyTransition();
    }

    private void CheckDifficultyTransition()
    {
        float elapsedTime = Time.time - gameStartTime;

        // Easy -> Normal
        if (!isEasyCompleted && elapsedTime >= easyDifficulty.duration)
        {
            isEasyCompleted = true;
            SetDifficulty(DifficultyData.DifficultyLevel.Normal);
        }
        // Normal -> Hard
        else if (isEasyCompleted && !isNormalCompleted && 
                 elapsedTime >= (easyDifficulty.duration + normalDifficulty.duration))
        {
            isNormalCompleted = true;
            SetDifficulty(DifficultyData.DifficultyLevel.Hard);
        }
    }

    private void SetDifficulty(DifficultyData.DifficultyLevel level)
    {
        CurrentLevel = level;

        switch (level)
        {
            case DifficultyData.DifficultyLevel.Easy:
                CurrentDifficultyData = easyDifficulty;
                break;
            case DifficultyData.DifficultyLevel.Normal:
                CurrentDifficultyData = normalDifficulty;
                break;
            case DifficultyData.DifficultyLevel.Hard:
                CurrentDifficultyData = hardDifficulty;
                break;
        }

        if (showDebugLogs)
        {
            Debug.Log($"[Difficulty] Changed to {level} (speed: {CurrentSpeedMultiplier}x)");
        }

        // Fire event for MapGenerator
        OnDifficultyChanged?.Invoke(CurrentDifficultyData);
    }

    // Debug: Force difficulty change
    [ContextMenu("Force Easy")]
    public void ForceEasy() => SetDifficulty(DifficultyData.DifficultyLevel.Easy);

    [ContextMenu("Force Normal")]
    public void ForceNormal() => SetDifficulty(DifficultyData.DifficultyLevel.Normal);

    [ContextMenu("Force Hard")]
    public void ForceHard() => SetDifficulty(DifficultyData.DifficultyLevel.Hard);
}
