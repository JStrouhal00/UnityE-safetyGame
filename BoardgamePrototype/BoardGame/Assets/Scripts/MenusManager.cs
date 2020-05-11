using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenusManager : MonoBehaviour
{
    public GameObject mainPanel;
    public GameObject leaderboardPanel;
    public GameObject startPanel;

    public TextMeshProUGUI leaderboard;

    public TMP_InputField[] playerNames;
    public TMP_Dropdown dropdown;

    // Start is called before the first frame update
    void Start()
    {
        mainPanel.SetActive(true);
        leaderboardPanel.SetActive(false);
        startPanel.SetActive(false);

        Leaderboard.LoadScores();
        leaderboard.text = Leaderboard.ScoresToString();

        Time.timeScale = 0f;
    }

    public void StartButton()
    {
        GameManager.instance.ResetPlayers();
        int playernumber = dropdown.value + 2;
        for (int i = 0; i < playernumber; i++)
        {
            GameManager.instance.AddPlayer(playerNames[i].text);
        }
        GameManager.instance.gameStarted = true;

        mainPanel.SetActive(false);
        leaderboardPanel.SetActive(false);
        startPanel.SetActive(false);
    }

    public void BackButton()
    {
        mainPanel.SetActive(true);
        leaderboardPanel.SetActive(false);
        startPanel.SetActive(false);
    }

    public void GoToStart()
    {
        //mainPanel.SetActive(false);
        //leaderboardPanel.SetActive(false);
        //startPanel.SetActive(true);

        gameObject.SetActive(false);
        Time.timeScale = 1f;
    }

    public void GoToLeaderboard()
    {
        mainPanel.SetActive(false);
        leaderboardPanel.SetActive(true);
        startPanel.SetActive(false);
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void DropDownChange(System.Int32 value)
    {
        for (int i = 0; i < playerNames.Length; i++)
        {
            playerNames[i].gameObject.SetActive(i < value + 2);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
