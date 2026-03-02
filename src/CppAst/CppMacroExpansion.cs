namespace CppAst;

public class CppMacroExpansionExpression(CppExpressionKind kind) : CppExpression(kind)
{
    public required CppMacro Macro;

    public override string ToString()
    {
        return Macro.Name;
    }
}