using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Enemies), true), CanEditMultipleObjects]
public class EnemiesEditor : Editor
{
    //Custom Editor for the Enemies Script

    Enemies enemies;

    private void OnEnable() {
        enemies = (Enemies)target;   
    }

    public override void OnInspectorGUI() {
        
        // we wanna show all the variables we make by default
        //base.OnInspectorGUI();

        // and then hide based on whatever enum is selected
        enemies._enemy = (Enemies.EnemyTypes)EditorGUILayout.EnumPopup("Enemy Type", enemies._enemy);

        switch (enemies._enemy) {
            case Enemies.EnemyTypes.Fly:
            default:

                EditorGUILayout.Space();
                EditorGUILayout.PrefixLabel("Enemy Movement");

                // direction
                enemies._direction = EditorGUILayout.Vector2Field("Direction", enemies._direction);

                // max distance
                enemies._maxDistance = EditorGUILayout.FloatField("Max Distance", enemies._maxDistance);

                // speed
                enemies._speed = EditorGUILayout.FloatField("Enemy Speed", enemies._speed);

                break;
            case Enemies.EnemyTypes.Slime:

                EditorGUILayout.Space();
                EditorGUILayout.PrefixLabel("Enemy Movement");

                // direction
                enemies._direction = EditorGUILayout.Vector2Field("Direction", enemies._direction);

                // max distance
                enemies._maxDistance = EditorGUILayout.FloatField("Max Distance", enemies._maxDistance);

                // speed
                enemies._speed = EditorGUILayout.FloatField("Enemy Speed", enemies._speed);

                EditorGUILayout.Space();
                EditorGUILayout.PrefixLabel("Slime Sensors");

                // left sensor
                enemies._leftSensor = EditorGUILayout.ObjectField("Left Sensor", enemies._leftSensor, typeof(Transform), true) as Transform;

                // right sensor
                enemies._rightSensor = EditorGUILayout.ObjectField("Right Sensor", enemies._rightSensor, typeof(Transform), true) as Transform;

                break;
            case Enemies.EnemyTypes.Traps:
                // nothing, just enum list

                break;
        }

    }
}
