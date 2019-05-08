using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Drop", menuName = "Drop", order = 1)]
public class Drop : ScriptableObject
{
    public string dropName;
    public GameObject worldItem;

    [Range(0,10)]
    public int probablity;
}
