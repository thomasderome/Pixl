using System;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject menuPanel;
    public GameObject settingsPanel;
    public GameObject selectionPanel;
    public GameObject gadgetPanel;

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

    }
    
    public void CloseSelection()
    {
        selectionPanel.SetActive(false);
        gadgetPanel.SetActive(false);
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