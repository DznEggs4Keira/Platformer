using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    //Base Class for all Enemy actions, the common stuff can be added here

    public enum EnemyTypes{
        Fly,
        Slime,
        Traps
    }

    [Header("Enemy Type")]
    public EnemyTypes _enemy;
}
