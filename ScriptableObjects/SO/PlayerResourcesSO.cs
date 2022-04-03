using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Resources", menuName = "ScriptableObjects/Resources", order = 2)]
public class PlayerResourcesSO : ScriptableObject
{
    public float currentWeekTime;
    public int currentDay;
    public int currentWeek;
    public bool weekStarted;
    public bool weeklyMiniGameComplete;

    public int food;
    public int fuel;
    public int supply;
    public int bullets;

}
