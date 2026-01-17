using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform[] spawn;
    public List<GameObject> Players = new List<GameObject>();
    
    private void Spawn_player()
    {
        List<Transform> spawn_possible = new List<Transform>(spawn);
        
        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
        {
            int index = Random.Range(0, spawn_possible.Count);
            player.transform.localPosition = spawn_possible[index].position;
            spawn_possible.RemoveAt(index);

            Player Player_script = player.GetComponent<Player>();
            Player_script.GameManager = this;
            Player_script.Revive();
            
            Players.Add(player);
        }
    }

    public void Death(GameObject player)
    {
        for (int i = 0; i < Players.Count; i++)
        {
            if (player == Players[i])
            {
                player.GetComponent<Player>().Die();
                Players.RemoveAt(i);
                break;
            } 
        }
    }

    private void Win(GameObject player)
    {
        Players[0].GetComponent<Player>().WIN += 1;
    }
    
    public void Start()
    {
        Spawn_player();
    }
}
