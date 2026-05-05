using UnityEngine;

public class EquationGenerator : MonoBehaviour
{
    public enum Operator { Addition, Subtraction }

    public class Equation
    {
        public int leftOperand;
        public int rightOperand;
        public Operator op;
        public int result;
    }

    private const int MIN_VALUE = 1;
    private const int MAX_VALUE = 9;

    public Equation GenerateEquation()
    {
        Equation equation;
        do
        {
            int a = Random.Range(MIN_VALUE, MAX_VALUE + 1);
            int b = Random.Range(MIN_VALUE, MAX_VALUE + 1);
            Operator op = (Operator)Random.Range(0, 2);

            if (op == Operator.Subtraction && a < b)
            {
                int temp = a;
                a = b;
                b = temp;
            }

            equation = new Equation
            {
                leftOperand = a,
                rightOperand = b,
                op = op,
                result = op == Operator.Addition ? a + b : a - b
            };

        } while (equation.result == 0); //This while loop makes sure the answer is never  0.
        //Who even wants 0 cookies?

        return equation;
    }
}