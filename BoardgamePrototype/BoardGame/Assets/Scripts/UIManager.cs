using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private RectTransform content;
    [SerializeField] private Text playerScorePrefab;

    private Dictionary<string, Text> textDic = new Dictionary<string, Text>();

    private void Start() {
        foreach (var player in GameManager.instance.playerList) {
            var t = Instantiate(playerScorePrefab, content);
            t.text = player.PlayerName + ": 0";
            textDic.Add(player.PlayerName, t);
        }
    }

    private void Update() {
        foreach (var player in GameManager.instance.playerList) {
            textDic[player.PlayerName].text = player.PlayerName + ": " + player.score.ToString();
        }
    }
}
