namespace CppAst;

public class CppMacroExpansionExpression(CppExpressionKind kind) : CppExpression(kind)
{
    public required string MacroName;

    public override string ToString()
    {
        return MacroName;
    }
}