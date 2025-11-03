using UnityEngine;

// Difficulty settings for map generation
// Create Asset: Right-click > Create > Game/Difficulty Data
[CreateAssetMenu(fileName = "NewDifficulty", menuName = "Game/Difficulty Data")]
public class DifficultyData : ScriptableObject
{
    public enum DifficultyLevel 
    { 
        Easy,
        Normal,
        Hard
    }

    [Header("=== Basic Settings ===")]
    public DifficultyLevel level = DifficultyLevel.Easy;
    public float duration = 60f; // Duration in seconds

    [Header("=== Speed Settings ===")]
    [Range(0.5f, 3.0f)]
    public float speedMultiplier = 1.0f; // Map speed multiplier
}
