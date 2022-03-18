using UnityEditor;

[CustomEditor(typeof(Enemies))]
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
        enemies._enemy = (Enemies.EnemyTypes)EditorGUILayout.EnumPopup(enemies._enemy);

        switch (enemies._enemy) {
            case Enemies.EnemyTypes.Fly:
                break;
            case Enemies.EnemyTypes.Slime:
                break;
            case Enemies.EnemyTypes.Traps:
                break;
            default:
                break;
        }

    }
}
