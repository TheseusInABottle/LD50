using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Player Object", menuName = "ScriptableObjects/PlayerObject", order = 1)]
public class PlayerCharacterSO : ScriptableObject
{
    public bool alive;
    public string userName;
    public int health;
    public bool infected;
    public float infection;
}
