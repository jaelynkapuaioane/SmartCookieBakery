using UnityEngine;
using TMPro;

public class OrderTicket : MonoBehaviour
{
    [Header("Content")]
    public string[] names;
    public string[] lastInitials;
    public string[] reasons;

    private TextMeshProUGUI nameText;
    private TextMeshProUGUI answerText;
    private TextMeshProUGUI reasonText;

    void Start()
    {
        nameText = transform.Find("FirstNameText")?.GetComponent<TextMeshProUGUI>();
        answerText = transform.Find("EquationNumber")?.GetComponent<TextMeshProUGUI>();
        reasonText = transform.Find("RandomReason")?.GetComponent<TextMeshProUGUI>();
    }

    public void PopulateTicket(int answer)
    {
        string randomName = names[Random.Range(0, names.Length)];
        string randomInitial = lastInitials[Random.Range(0, lastInitials.Length)];
        nameText.text = randomName + " " + randomInitial;
        reasonText.text = reasons[Random.Range(0, reasons.Length)];
        answerText.text = answer.ToString();
    }
}