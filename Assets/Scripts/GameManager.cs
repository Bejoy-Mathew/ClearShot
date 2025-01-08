using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public LevelCondition currentLevelCondition;
    public int destroyedTargets = 0;
    public int lastLevelBuildIndex;
    public float timeRemaing;
    public int stars = 0;
    public int levelScore = 0;
    public string gameResult="";
    public int bulletRemaining = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    
    public void RegisterTargetDestroyed(GameObject gameObject)
    {

        if (gameObject.transform.name.Contains("bottle"))
        {
            levelScore += 50;
        }
        else 
        {
            levelScore += 500;
        }
        
        destroyedTargets++;
        Debug.Log("Game manager :"+destroyedTargets);


        if (currentLevelCondition.IsLevelComplete(destroyedTargets))
        {
            stars=CalculateStars();
            gameResult = "Round Clear !!";
            LevelComplete();
        }
    }

    private void LevelComplete()
    {
        SceneManager.LoadScene("TransitionScene");
    }

    private int CalculateStars()
    {
        
        if (destroyedTargets >= currentLevelCondition.totalTargetsToDestroy && timeRemaing>5 && bulletRemaining>1)
            return 3; 
        if (destroyedTargets >= currentLevelCondition.totalTargetsToDestroy && timeRemaing > 1 )
            return 2; 
        else
        return 1; 
    }

    public void SetLastLevel(int buildIndex)
    {
        lastLevelBuildIndex = buildIndex;
    }

    public void SetLevelData(int stars, int score)
    {
        
        Debug.Log($"Level Completed with {stars} stars and score: {score}");
    }
}
