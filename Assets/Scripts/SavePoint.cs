using UnityEngine;

public class SavePoint : MonoBehaviour
{
    public string SaveScene;
    public int SaveInt;

    public void SaveGame()
    {
        SaveManager.Instance.SaveGame(SaveScene, SaveInt);
    }
    private void Update()
    {
        if (Input.GetAxis("Vertical") > 0.4)
        {
            print("savegame");
            SaveGame();
        }
    }
}
