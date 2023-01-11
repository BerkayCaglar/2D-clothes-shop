using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSpawnPoints : MonoBehaviour
{
    public static SceneSpawnPoints Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    public Dictionary<string, Vector3> SpawnPoints = new Dictionary<string, Vector3>(){
        {"Clothes Shop To Town", new Vector3(2.5f, 12.5f, 0)},
        {"Town To Clothes Shop", new Vector3(49.5f, 1f, 0)}};
}
