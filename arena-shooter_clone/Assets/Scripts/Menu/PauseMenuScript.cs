using System;
using System.Collections;
using System.Collections.Generic;
using MLAPI;
using MLAPI.Messaging;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;

public class PauseMenuScript : NetworkedBehaviour
{

    public GameObject pauseMenu;
    public GameObject gameMenu;

    public GameObject gameManager;

    public static bool isPaused = false;

    // Update is called once per frame
    public void OnClickLeaveGame()
    {
        if (NetworkingManager.Singleton.IsHost)
        {
            gameManager.GetComponent<ConnectionManager>().DisconnectAllClients();
            StartCoroutine(StopHost());
        }
        else
        {
            NetworkingManager.Singleton.StopClient();
        } 
        pauseMenu.SetActive(false);
        gameMenu.SetActive(true);
        
    }

    IEnumerator StopHost()
    {
        yield return new WaitUntil(allConnectionsClosed);
        NetworkingManager.Singleton.StopHost();
    }

    bool allConnectionsClosed()
    {
        print(NetworkingManager.Singleton.ConnectedClientsList.Count <= 1);
        return NetworkingManager.Singleton.ConnectedClientsList.Count <= 1;
    }
    
    

    public void OnClickBackToGame()
    {
        isPaused = false;
        pauseMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void OnClickOptions()
    {
        print("No Options to show");
    }

    


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !gameMenu.activeSelf)
        {
            if (pauseMenu.activeSelf)
            {
                pauseMenu.SetActive(false);
                isPaused = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                isPaused = true;
                pauseMenu.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
            }
            
        }
    }
}
