using System.Diagnostics.CodeAnalysis;

namespace CppAst.Extensions;

public static class CppAstExtensions
{
    public static bool IsVoidPointer(this CppType type)
    {
        return type
            is CppPointerType { ElementType: CppPrimitiveType { Kind: CppPrimitiveKind.Void } };
    }

    public static bool IsFunctionPointer(this CppType type)
    {
        return type is CppPointerType { ElementType: CppFunctionType };
    }

    public static bool IsUnion(this CppType type, [NotNullWhen(true)] out CppClass? union)
    {
        if (type is CppClass { ClassKind: CppClassKind.Union } unionClass)
        {
            union = unionClass;
            return true;
        }

        union = null;
        return false;
    }

    public static bool IsInline(this CppFunction function)
    {
        return (function.Flags & CppFunctionFlags.Inline) != 0;
    }
}
