using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private RectTransform content;
    [SerializeField] private Text playerScorePrefab;

    private Dictionary<string, Text> textDic = new Dictionary<string, Text>();

    public void Initialize() {
        foreach (var player in GameManager.instance.playerList) {
            var t = Instantiate(playerScorePrefab, content);
            t.color = player.stone.gameObject.GetComponent<Renderer>().material.color;
            t.text = player.PlayerName + ": 0";
            textDic.Add(player.PlayerName, t);
        }
    }

    private void Update() {
        if (!GameManager.instance.gameStarted) return;

        foreach (var player in GameManager.instance.playerList) {
            textDic[player.PlayerName].text = player.PlayerName + ": " + player.score.ToString();
        }
    }
}
