using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class MainMenu : MonoBehaviour
{
    public GameObject menuPanel;
    public GameObject settingsPanel;
    public GameObject selectionPanel;
    void Start()
    {
        menuPanel.SetActive(true);
        settingsPanel.SetActive(false);
        selectionPanel.SetActive(false);
    }

    public void OpenSelection()
    {
        menuPanel.SetActive(false);
        selectionPanel.SetActive(true);
        selectionPanel.GetComponent<PlayerInputManager>().EnableJoining();
    }
    
    public void CloseSelection()
    {
        selectionPanel.GetComponent<PlayerJoin>().Clear_player();
        selectionPanel.SetActive(false);
        menuPanel.SetActive(true);
    }
    
    public void OpenSettings()
    {
        menuPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
        menuPanel.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}