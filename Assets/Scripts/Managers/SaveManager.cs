using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;
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

    public void SaveGame(string RespawnScene,int RespawnEntranceInt)
    {
        PlayerPrefs.SetString("RespawnScene", RespawnScene);
        PlayerPrefs.SetInt("RespawnEntranceInt", RespawnEntranceInt);
        PlayerPrefs.SetInt("CurrentGears", PlayerManager.Instance.health.currentGears);

        PlayerPrefs.Save();
    }
    void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("CurrentGears", PlayerManager.Instance.health.currentGears);

        PlayerPrefs.Save();
    }
}
