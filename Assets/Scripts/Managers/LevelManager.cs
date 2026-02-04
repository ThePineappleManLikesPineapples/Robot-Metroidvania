using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public List<Vector3> Entrances;
    private void Awake()
    {
        if (AreaManager.Instance == null)
        {
            print("Sending to GameLoader");
            SceneManager.LoadScene("GameLoader");
            return;
        }
        Vector3 SpawnPosition = Entrances[AreaManager.Instance.NewEntrance];
        if (PlayerManager.Instance.player != null)
        {
            PlayerManager.Instance.player.position = SpawnPosition;
            if (GameObject.FindGameObjectWithTag("CinemachineCamera") != null)
            {
                GameObject.FindGameObjectWithTag("CinemachineCamera").GetComponent<CinemachineCamera>().Target.TrackingTarget = PlayerManager.Instance.player;
            }
            else
            {
                Debug.LogWarning("Could not find Object with Cinemachine Camera Tag");
            }

        }
        else
        {
            Debug.LogWarning("The Player object was not found upon Level Load");
        }
    }
}
