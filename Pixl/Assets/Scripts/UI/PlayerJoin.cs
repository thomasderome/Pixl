using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;
using UnityEngine.SceneManagement;

public class PlayerJoin : MonoBehaviour
{
    [SerializeField] private GameObject Select_Panel;
    [SerializeField] private GameObject[] Player;

    private bool play = false;
    
    private int number_player = 0;

    private List<GameObject> pannels = new List<GameObject>();
    private List<PlayerInput> controller = new List<PlayerInput>();
    
    public void Join(PlayerInput playerInput)
    {
        if (number_player == 2) return;
        
        GameObject Temp = Instantiate(Select_Panel);
        Temp.transform.SetParent(transform);

        if (number_player == 0) Temp.transform.localPosition = new Vector3(50, -20, 0);

        else if (number_player == 1) Temp.transform.localPosition = new Vector3(350, -20, 0);

        pannels.Add(Temp);
        controller.Add(playerInput);
        number_player += 1;
    }

    public void Clear_player()
    {
        foreach (GameObject pannel in pannels)
        {
            Destroy(pannel);
        }

        foreach (PlayerInput control in controller)
        {
            Destroy(control);
        }
        
        pannels.Clear();
        controller.Clear();
        number_player = 0;
    }
    
    public void Play()
    {
        if (play) return;
        play = true;
        GetComponent<PlayerInputManager>().enabled = false;
        
        for (int i = 0; i < controller.Count; i++)
        {
            GameObject Player_Temp = Instantiate(Player[i]);
            Player_Temp.GetComponent<Player>().Die();
            
            PlayerInput Player_controller = Player_Temp.GetComponent<PlayerInput>();
            
            Player_controller.user.UnpairDevices();
            var devices = controller[i].user.pairedDevices;
            foreach (var device in devices) InputUser.PerformPairingWithDevice(device, Player_controller.user);
            
            DontDestroyOnLoad(Player_Temp);
        }
        SceneManager.LoadScene("Map1");
    }
}