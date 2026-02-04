using System.Collections;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;
    public Transform player;
    public PlayerUI UI;
    public Movement movement;
    public Health health;
    public GameObject playerPrefab;
    public GameObject UIPrefab;
    public Vector3 checkpointPosition = Vector3.zero;

    public void SetCheckpoint(Vector3 CheckpointPosition)
    {
        checkpointPosition = CheckpointPosition;
    }
    public void ResetToCheckpoint()
    {
        player.position = checkpointPosition;
    }
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

        DontDestroyOnLoad(gameObject);
        UI = Instantiate(UIPrefab).GetComponent<PlayerUI>();
        DontDestroyOnLoad(UI.gameObject);
        StartCoroutine(RespawnPlayer());
    }
    public IEnumerator RespawnPlayer()
    {
        yield return new WaitForSeconds(0.5f);
        if (player != null)
        {
            Destroy(player.gameObject);
        }
        GameObject NewPlayer = Instantiate(playerPrefab);
        player = NewPlayer.transform;
        health = player.gameObject.GetComponent<Health>();
        health.currentGears = PlayerPrefs.GetInt("CurrentGears");
        UI.UpdateGears();
        UI.UpdateHealth();
        movement = player.gameObject.GetComponent<Movement>();
        if (PlayerPrefs.GetString("RespawnScene") != string.Empty)
        {
            AreaManager.Instance.ChangeScene(PlayerPrefs.GetString("RespawnScene"), PlayerPrefs.GetInt("RespawnEntranceInt"));
        }
        else
        {
            AreaManager.Instance.ChangeScene("RoomTest-West" ,0);
        }
            DontDestroyOnLoad(player.gameObject);
    }
}
