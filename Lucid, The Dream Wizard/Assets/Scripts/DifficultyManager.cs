using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    public Difficulty difficulty;

    static string difficultyName = "default";
    static float attackMultiplier = 1;
    static float speedMultiplier = 1;
    static Vector2Int dropRate = new Vector2Int(1,3);
    static int lives = 3;

    private void Start()
    {
        setUpValues();
    }

    private void Update()
    {
        if (difficulty != null && difficulty.difficultyName != difficultyName)
            setUpValues();
    }

    public static float getAttackMultiplier()
    {
        return attackMultiplier;
    }

    public static float getSpeedMultiplier()
    {
        return speedMultiplier;
    }

    public static Vector2Int getDropRateMax()
    {
        return dropRate;
    }
    public static int getLives()
    {
        return lives;
    }

    void setUpValues()
    {
        difficultyName = difficulty.difficultyName;
        attackMultiplier = difficulty.attackMultiplier;
        speedMultiplier = difficulty.speedMultiplier;
        dropRate = difficulty.dropRate;
        lives = difficulty.lives;
    }
}
