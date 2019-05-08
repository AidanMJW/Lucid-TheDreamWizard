using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Difficulty", menuName = "Difficulty", order = 1)]
public class Difficulty : ScriptableObject
{
    public string difficultyName;
    public float attackMultiplier;
    public float speedMultiplier;
    public Vector2Int dropRate;
    [Range(1,5)]
    public int lives;
}
