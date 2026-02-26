namespace CppAst;

public class CppMacroExpansion(CppExpressionKind kind) : CppExpression(kind)
{
    public required CppMacro Macro;
    public required CppExpression? Expression;

    public override string ToString()
    {
        return Macro.Name;
    }
}