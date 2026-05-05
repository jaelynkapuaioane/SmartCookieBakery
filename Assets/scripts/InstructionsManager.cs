using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InstructionsManager : MonoBehaviour
{

    [Header("References")]
    public GameManager gameManager;

    [Header("Panel")]
    public GameObject instructionsPanel;

    [Header("Page Panels")]
    public GameObject[] pages;

    [Header("Instructions Content")]
   // public Image instructionImage;
   // public Sprite[] pageSprites;
   // public TextMeshProUGUI instructionText;
    public TextMeshProUGUI pageIndicator;
    public Button previousButton;
    public Button nextButton;

    [Header("Buttons")]
    public Button helpButton;
    public Button closeButton;

    [Header("Instruction Pages")]
   // public string[] instructionTexts = {
   //     "The cookies appear here!",
   //     "The equation panel is here!",
   //     "Count the cookies and input it here!",
   //     "Complete the equation!",
   //     "Watch the timer!"
   //  };

    private int currentPage = 0;

    void Start()
    {
        helpButton.onClick.AddListener(ShowInstructions);
        closeButton.onClick.AddListener(HideInstructions);
        previousButton.onClick.AddListener(PreviousPage);
        nextButton.onClick.AddListener(NextPage);

        instructionsPanel.SetActive(false);
    }

    void ShowInstructions()
{
    instructionsPanel.SetActive(true);
    gameManager.PauseGame();
    currentPage = 0;
    UpdatePage();
}

void HideInstructions()
{
    instructionsPanel.SetActive(false);
    gameManager.ResumeGame();
}

void UpdatePage()
{
    // Show only the current page
    for (int i = 0; i < pages.Length; i++)
    {
        pages[i].SetActive(i == currentPage);
    }

    // Update page indicator
    pageIndicator.text = (currentPage + 1) + " / " + pages.Length;

    // Enable/disable buttons
    previousButton.gameObject.SetActive(currentPage > 0);
    nextButton.gameObject.SetActive(currentPage < pages.Length - 1);
}


void PreviousPage()
    {
        currentPage--;
        UpdatePage();
    }

    void NextPage()
    {
        currentPage++;
        UpdatePage();
    }
}