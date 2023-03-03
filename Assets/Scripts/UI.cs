using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UI : MonoBehaviour
{
    [Header ("Start Menu")]
    [Space]
    [SerializeField] private GameObject _startMenu;
    [SerializeField] private Button _startButton;

    [Header("Game Menu")]
    [Space]
    [SerializeField] private GameObject _gameMenu;
    [SerializeField] private GameObject _joystickPanel;
    [SerializeField] private Button _pauseButton;

    [Header("AttackButtons")]
    [SerializeField] private Button _hitButton;
    [SerializeField] private Button _ultraButton;
    public Button hitButton => _hitButton;
    public Button ultraButton => _ultraButton;

    [Header("Pause Menu")]
    [Space]
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private Button _resumeButton;
    [SerializeField] private Button _restartPauseButton;

    [Header("Finish Menu")]
    [Space]
    [SerializeField] private GameObject _finishMenu;
    [SerializeField] private Button _restartButton;
    [SerializeField] private TextMeshProUGUI _summaryKillText;
    [SerializeField] private TextMeshProUGUI _blueKillText;
    [SerializeField] private TextMeshProUGUI _redKillText;
    public int blueKilled, redKilled;

    private void Awake()
    {
        _startButton.onClick.AddListener(StartGame);
        _resumeButton.onClick.AddListener(ResumeGame);
        _pauseButton.onClick.AddListener(PauseOpen);
        _restartPauseButton.onClick.AddListener(RestartGame);
        _restartButton.onClick.AddListener(RestartGame);
        Time.timeScale = 0f;
    }


    private void StartGame()
    {
        Time.timeScale = 1f;
        _startMenu.SetActive(false);
        _gameMenu.SetActive(true);
        _joystickPanel.SetActive(true);
    }

    private void PauseOpen()
    {
        Time.timeScale = 0f;
        _pauseMenu.SetActive(true);
    }

    private void ResumeGame()
    {
        Time.timeScale = 1f;
        _pauseMenu.SetActive(false);
    }

    public void FinishGame()
    {
        Time.timeScale = 0f;
        _finishMenu.SetActive(true);
        _gameMenu.SetActive(false);
        _joystickPanel.SetActive(false);
        int summaryKill;
        summaryKill = blueKilled + redKilled;
        _summaryKillText.text = ($"You Kill:  {summaryKill} enemy");
        _blueKillText.text = blueKilled.ToString();
        _redKillText.text = redKilled.ToString();
    }

    private void RestartGame()
    {
        SceneManager.LoadScene("Game");
    }

}
