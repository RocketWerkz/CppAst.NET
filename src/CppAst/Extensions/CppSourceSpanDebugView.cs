using System.Diagnostics;
using System.Text;
using CppAst;

[assembly: DebuggerDisplay(
    "{CppAst.Debugging.CppSourceSpanDebugView.DebuggerDisplay(this)}",
    Target = typeof(CppSourceSpan)
)]

namespace CppAst.Debugging;

/// Display the source code for the span, extracted from the source file.
internal sealed class CppSourceSpanDebugView
{
    static readonly Dictionary<string, string[]> SourceFiles = new();
    static readonly StringBuilder sb = new();

    static string DebuggerDisplay(CppSourceSpan value)
    {
        var start = value.Start;
        var end = value.End;
        var file = start.File;

        Debug.Assert(start.File == end.File);
        Debug.Assert(start.Line < end.Line || start.Column < end.Column);

        if (string.IsNullOrEmpty(file))
            return "<no source code>";

        if (!File.Exists(file))
            return $"{file} ({start.Line}, {start.Column}) -> ({end.Line}, {end.Column})";

        sb.Clear();

        if (!SourceFiles.TryGetValue(file, out var lines))
        {
            lines = File.ReadAllLines(file);
            SourceFiles.Add(file, lines);
        }

        for (var i = start.Line; i <= end.Line; i++)
        {
            var line = lines[i - 1];
            var startColumn = i == start.Line ? start.Column - 1 : 0;
            var endColumn = i == end.Line ? end.Column : line.Length;
            if (i != start.Line)
                sb.AppendLine();
            sb.Append(line, startColumn, endColumn - startColumn);
        }

        return sb.ToString();
    }
}
