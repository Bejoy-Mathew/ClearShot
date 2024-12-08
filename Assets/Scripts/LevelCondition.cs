using UnityEngine;

[CreateAssetMenu(fileName = "NewLevelCondition", menuName = "Level/Condition")]
public class LevelCondition : ScriptableObject
{
    public int totalTargetsToDestroy; 

    public bool IsLevelComplete(int destroyedTargets)
    {
        if (destroyedTargets >= totalTargetsToDestroy)
        {
            return true;   
        }      
        return false;
        
    }
}
