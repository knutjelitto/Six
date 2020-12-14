using Six.Support;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace SixComp.Sema
{
    public static class ReportExtensions
    {
        private const int width = 16;

        public static void Report(this IReportable? reportable, IWriter writer, string label, bool maybeEmpty = false)
        {
            if (reportable != null)
            {
                if (label == Strings.Head.TupleType)
                {
                    Debug.Assert(true);
                }
                var check = reportable.ToString()!;
                if (!check.StartsWith("SixComp."))
                {
                    check.Report(writer, label);
                }
                else
                {
                    writer.WriteLine(label);
                    writer.Indent(() => reportable.Report(writer));
                }
            }
            else if (maybeEmpty)
            {
                string.Empty.Report(writer, label);
            }
        }

        public static void ReportList<T>(this IReadOnlyList<T> items, IWriter writer, string label)
            where T : IReportable
        {
            if (items.Count > 0)
            {
                if (items.Count == 1)
                {
                    items[0].Report(writer, label);
                }
                else
                {
                    using (writer.Indent(label))
                    {
                        foreach (var item in items)
                        {
                            item.Report(writer);
                        }
                    }
                }
            }
        }

        public static void Tree(this object any, IWriter writer)
        {
            if (any is IWritable writable)
            {
                var lines = new LinesWriter(";; ");
                writable.Write(lines);
                foreach (var line in lines)
                {
                    writer.WriteLine(line);
                }
            }
            else
            {
                writer.WriteLine($";; {any}");
            }
        }

        public static void Report(this string reportable, IWriter writer, string label)
        {
            writer.WriteLine($"{label,-width}{reportable}");
        }

        public static void Report(this bool reportable, IWriter writer, string label)
        {
            var value = reportable ? "true" : "false";
            value.Report(writer, label);
        }

        public static void Report(this BaseName? reportable, IWriter writer, string label, bool maybeEmpty = false)
        {
            if (reportable != null)
            {
                var value = reportable.Text;
                value.Report(writer, label);
            }
            else if (maybeEmpty)
            {
                string.Empty.Report(writer, label);
            }

        }

        public static void Report(this Enum reportable, IWriter writer, string label)
        {
            var value = Enum.GetName(reportable.GetType(), reportable)!.ToLower();
            value.Report(writer, label);
        }
    }
}
