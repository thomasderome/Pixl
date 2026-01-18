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
        if (number_player == 4) return;
        
        GameObject Temp = Instantiate(Select_Panel);
        Temp.transform.SetParent(transform);

        Temp.transform.localPosition = new Vector3(-135 + (155 * number_player), -20, 0);

        pannels.Add(Temp);
        controller.Add(playerInput);
        
        number_player += 1;
        if (number_player == 4) GetComponent<PlayerInputManager>().enabled = false;
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
        if (play || controller.Count < 1) return;
        
        play = true;
        GetComponent<PlayerInputManager>().enabled = false;
        
        for (int i = 0; i < controller.Count; i++)
        {
            GameObject Player_Temp = Instantiate(Player[i]);
            
            PlayerInput Player_controller = Player_Temp.GetComponent<PlayerInput>();
            
            Player_controller.user.UnpairDevices();
            var devices = controller[i].user.pairedDevices;
            foreach (var device in devices) InputUser.PerformPairingWithDevice(device, Player_controller.user);
            
            List<GameObject> Gadget_sel = pannels[i].GetComponent<SelectScript>().ConfirmSelection();
            for (int j = 0; j < Gadget_sel.Count; j++) Player_Temp.GetComponent<Gadget_Manager>().Equip(Gadget_sel[j], j+1);
            
            Player_Temp.GetComponent<Player>().Die();
            DontDestroyOnLoad(Player_Temp);
        }

        SceneManager.LoadScene(Random.Range(1, SceneManager.sceneCount));
    }
}