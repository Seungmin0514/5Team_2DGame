using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultUI : MonoBehaviour
{
    public GameObject panel;
    public TextMeshProUGUI currentGoldTxt;
    public TextMeshProUGUI bestGoldTxt;

    void Start()
    {
        panel.SetActive(false);
    }

    public void ShowResult()
    {
        currentGoldTxt.text = ((int)RunGameManager.Instance.gold).ToString();
        bestGoldTxt.text = RunGameManager.Instance.bestGold.ToString();
        panel.SetActive(true);
        Time.timeScale = 0f;
    }
}
