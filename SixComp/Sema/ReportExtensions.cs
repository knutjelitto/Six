using SixComp.Support;
using System;
using System.Collections.Generic;

namespace SixComp.Sema
{
    public static class ReportExtensions
    {
        public static void Report(this IReportable? reportable, IWriter writer, string label)
        {
            if (reportable != null)
            {
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
            writer.WriteLine($";; {any}");
        }

        public static void Report(this string reportable, IWriter writer, string label)
        {
            writer.WriteLine($"{label,-12}{reportable}");
        }

        public static void Report(this bool reportable, IWriter writer, string label)
        {
            var value = reportable ? "true" : "false";
            value.Report(writer, label);
        }

        public static void Report(this BaseName reportable, IWriter writer, string label)
        {
            var value = reportable.Text;
            value.Report(writer, label);
        }

        public static void Report(this Enum reportable, IWriter writer, string label)
        {
            var value = Enum.GetName(reportable.GetType(), reportable)!.ToLower();
            value.Report(writer, label);
        }

        public static IDisposable Indent(this IWriter writer, string label)
        {
            writer.WriteLine(label);
            return writer.Indent();
        }
    }
}
