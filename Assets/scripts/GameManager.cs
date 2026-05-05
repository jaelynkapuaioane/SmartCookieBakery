using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class GameManager : MonoBehaviour
{
    [Header("References")]
    public CookieBox cookieBox;
    public EquationGenerator equationGenerator;
    public EquationUI equationUI;
    public Button confirmButton;
    public OrderTicket orderTicket;

    [Header("Feedback")]
    public TextMeshProUGUI feedbackText;

    [Header("Timer")]
    public Timer timer;
    public float gameDuration = 300f;   

    [Header("Results")]
    public GameObject resultsPanel;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI messageText;
    public TextMeshProUGUI timeRemainingText;
    public Button playAgainButton;

    [Header("Audio")]
    public AudioSource sfxSource;
    public AudioSource musicSource;
    public AudioClip correctSound;
    public AudioClip wrongSound;
    public AudioClip buttonClickSound;
  //  public AudioClip backgroundMusic;

    private EquationGenerator.Equation currentEquation;
    private int currentRound = 0;
    private int correctAnswers = 0;
    private const int TOTAL_ROUNDS = 10;
    private bool gameActive = false;

    private double timeAtEnd;

    void Start()
    {   
    confirmButton.onClick.AddListener(OnConfirmClicked);
    playAgainButton.onClick.AddListener(RestartGame);
    StartCoroutine(StartGameAfterDelay());
    }

    IEnumerator StartGameAfterDelay()
    {
        yield return null;
        yield return null;
  //      musicSource.clip = backgroundMusic;
  //      musicSource.Play();
        timer.StartTimer();
        gameActive = true;
        resultsPanel.SetActive(false);
        StartRound();
    }

    void Update()
    {
    }

    void StartRound()
    {
        equationUI.ResetUI();
        feedbackText.text = "";
        if (currentRound >= TOTAL_ROUNDS)
        {
            EndGame();
            return;
        }

        currentEquation = equationGenerator.GenerateEquation();
        cookieBox.SpawnCookies(currentEquation.leftOperand);
        equationUI.SetResult(currentEquation.result);
        orderTicket.PopulateTicket(currentEquation.result);

        currentRound++;
    }

    void OnConfirmClicked()
    {
        if (!gameActive) return;

        bool isCorrect = equationUI.CheckAnswer(
            currentEquation.leftOperand,
            currentEquation.rightOperand,
            currentEquation.op == EquationGenerator.Operator.Addition
        );

        if (isCorrect)
        {
            correctAnswers++;
            timeAtEnd = timer.GetRemainingSeconds();
            feedbackText.text = "Correct!";
            feedbackText.color = new Color(0.10f, 0.63f, 0.53f);
            PlaySound(correctSound);
            StartCoroutine(NextRoundAfterDelay());
        }
        else
        {
            feedbackText.text = "Try Again!";
            feedbackText.color = new Color(0.62f, 0.17f, 0.23f);
            PlaySound(wrongSound);
        }   
    }

    IEnumerator NextRoundAfterDelay()
    {
        yield return new WaitForSeconds(1.5f);
        StartRound();
    }

    public void EndGame()
    {
        gameActive = false;
        timer.StopTimer();

        double remaining = timeAtEnd;
        int minutes = Mathf.FloorToInt((float)remaining / 60);
        int seconds = Mathf.FloorToInt((float)remaining % 60);

        scoreText.text = "You got " + correctAnswers + " out of " + TOTAL_ROUNDS + "!";
        timeRemainingText.text = "Time remaining: " + string.Format("{0:00}:{1:00}", minutes, seconds);

        if (correctAnswers == TOTAL_ROUNDS)
            messageText.text = "Perfect score! Amazing!";
        else if (correctAnswers >= 7)
            messageText.text = "Great job!";
        else if (correctAnswers >= 4)
            messageText.text = "Good effort!";
        else
            messageText.text = "Keep practicing!";

        resultsPanel.SetActive(true);
    }

    void RestartGame()
    {
        Time.timeScale = 1f;
        resultsPanel.SetActive(false);
        currentRound = 0;
        correctAnswers = 0;
        gameActive = true;
        timer.StartTimer();
        StartRound();
    }

    public void PauseGame()
    {
        gameActive = false;
        timer.PauseTimer();
    }

    public void ResumeGame()
    {
        gameActive = true;
        timer.ResumeTimer();
    }

    void PlaySound(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }
}