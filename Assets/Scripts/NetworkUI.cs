/*
 * Host and Join from UI Buttons
 * 
 */
using Unity.Netcode;
using UnityEngine;

public class NetworkUI : MonoBehaviour
{
    public GameObject menuPanel;

    public void StartHost()
    {
        // Starts Server + Client, Spawns Player 1
        NetworkManager.Singleton.StartHost();
        menuPanel.SetActive(false); // Hides UI after connection
    }

    public void StartClient()
    {
        // Starts Client Only, Spawns Player 2
        NetworkManager.Singleton.StartClient();
        menuPanel.SetActive(false); // Same Hides UI after connection
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
