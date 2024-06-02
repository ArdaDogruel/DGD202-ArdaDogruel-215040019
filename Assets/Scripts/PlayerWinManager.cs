using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWinManager : MonoBehaviour
{
    [SerializeField] GameObject winMassagePanel;
    PauseManager pauseManager;
    [SerializeField] DataContainer dataContainer;

    private void Start()
    {
        pauseManager = GetComponent<PauseManager>();
    }

    public void Win()
    {
        winMassagePanel.SetActive(true);
        pauseManager.PauseGame();
        dataContainer.StageComplete(0);
    }
}
