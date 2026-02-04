using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaManager : MonoBehaviour
{
    public static AreaManager Instance;
    public int NewEntrance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void ChangeScene(string sceneName, int EntranceInt)
    {
        SceneManager.LoadScene(sceneName);
        NewEntrance = EntranceInt;

    }
}
