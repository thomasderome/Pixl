using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform[] spawn;
    private List<GameObject> Players = new List<GameObject>();

    public GameObject Scoreboard;
    
    private void Spawn_player()
    {
        List<Transform> spawn_possible = new List<Transform>(spawn);
        
        foreach (GameObject player in Players)
        {
            int index = Random.Range(0, spawn_possible.Count);
            player.transform.localPosition = spawn_possible[index].position;
            spawn_possible.RemoveAt(index);

            Player Player_script = player.GetComponent<Player>();
            Player_script.GameManager = this;
            Player_script.Revive();
        }
    }
    
    private void Init_scoreboard()
    {
        Vector2 position = new Vector2(6, -5);
        foreach (GameObject player in Players)
        {
            GameObject temp = Instantiate(Scoreboard, position, Quaternion.identity);
            temp.GetComponent<SpriteRenderer>().sprite = player.GetComponent<SpriteRenderer>().sprite;
            TextMeshPro text = temp.GetComponentInChildren<TextMeshPro>();
            text.text = ":" + player.GetComponent<Player>().WIN.ToString();
            
            position.x += 2;
        }
    }

    public void Death(GameObject player)
    {
        if (Players.Count == 1) Win(Players[0]);
        
        for (int i = 0; i < Players.Count; i++)
        {
            if (player == Players[i])
            {
                player.GetComponent<Player>().Die();
                Players.RemoveAt(i);
                break;
            } 
        }
        
        if (Players.Count == 1) Win(Players[0]);
    }

    private void Win(GameObject player)
    {
        player.GetComponent<Player>().WIN += 1;
    }
    
    public void Start()
    {
        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player")) Players.Add(player);
        
        Init_scoreboard();
        Spawn_player();
    }
}
