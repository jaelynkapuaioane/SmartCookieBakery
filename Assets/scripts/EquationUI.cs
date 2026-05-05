using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EquationUI : MonoBehaviour
{
    [Header("Left Operand")]
    public Button leftUpButton;
    public Button leftDownButton;
    public TextMeshProUGUI leftNumberText;

    [Header("Right Operand")]
    public Button rightUpButton;
    public Button rightDownButton;
    public TextMeshProUGUI rightNumberText;

    [Header("Operator")]
    public Button operatorUpButton;
    public Button operatorDownButton;
    public TextMeshProUGUI operatorText;

    [Header("Result")]
    public TextMeshProUGUI resultText;

    private int leftValue = 0;
    private int rightValue = 0;
    private bool isAddition = true;

    private const int MIN_VALUE = 0;
    private const int MAX_VALUE = 9;

    void Start()
    {
        leftUpButton.onClick.AddListener(() => {
        ChangeValue(ref leftValue, 1, leftNumberText);
        });

        leftDownButton.onClick.AddListener(() => ChangeValue(ref leftValue, -1, leftNumberText));
        rightUpButton.onClick.AddListener(() => ChangeValue(ref rightValue, 1, rightNumberText));
        rightDownButton.onClick.AddListener(() => ChangeValue(ref rightValue, -1, rightNumberText));
        operatorUpButton.onClick.AddListener(() => ToggleOperator(true));
        operatorDownButton.onClick.AddListener(() => ToggleOperator(false));

        ResetUI();
    }

    void ChangeValue(ref int value, int change, TextMeshProUGUI display)
    {
        value += change;
        if (value > MAX_VALUE) value = MIN_VALUE;
        if (value < MIN_VALUE) value = MAX_VALUE;
        display.text = value.ToString();
    }

    void ToggleOperator(bool upPressed)
    {
        if (operatorText.text == "")
        {
            operatorText.text = upPressed ? "+" : "-";
        }
        else
        {
            operatorText.text = operatorText.text == "+" ? "-" : "+";
        }

        isAddition = operatorText.text == "+";
    }

    public void SetResult(int result)
    {
        resultText.text = result.ToString();
    }

    public void ResetUI()
    {
        leftValue = 0;
        rightValue = 0;
        isAddition = true;
        leftNumberText.text = "";
        rightNumberText.text = "";
        operatorText.text = "";
        resultText.text = "";
    }

    public bool CheckAnswer(int correctLeft, int correctRight, bool correctIsAddition)
    {
        return leftValue == correctLeft && rightValue == correctRight && isAddition == correctIsAddition;
    }
}