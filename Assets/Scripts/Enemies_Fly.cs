using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies_Fly : Enemies
{
    protected override void Start() {
        base.Start();
        _enemy = EnemyTypes.Fly;
    }
}
