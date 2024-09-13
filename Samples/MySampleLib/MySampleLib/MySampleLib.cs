// ReSharper disable MemberCanBeMadeStatic.Global

namespace MySampleLib;

public class Calculator : ICalculator
{
    private int _state;

    public int Add(int op1, int op2) => op1 + op2;

    public int Sub(int op1, int op2) => op1 + op2;
}