using BenchmarkDotNet.Attributes;
using System;

namespace ThrowExceptionsBenchmark
{
    public class ResultAndCode<T>
    {
        public T Result { get; set; }

        public int Code { get; set; }
    }

    public interface IThrowExceptionsClient
    {
        void ParseIntWithException();

        void ParseIntWithNonException();

        void WebApiThrowException();

        ResultAndCode<string> WebApiReturnResult();
    }

    [MarkdownExporter, AsciiDocExporter, HtmlExporter, CsvExporter, RPlotExporter]
    public class ThrowExceptionsClient : IThrowExceptionsClient
    {
        private const string Name = "ThrowExceptionsBenchmark";

        [Benchmark]
        public void ParseIntWithException()
        {
            try
            {
                int result = int.Parse(Name);
            }
            catch (FormatException)
            {
            }
        }

        [Benchmark]
        public void ParseIntWithNonException()
        {
            if (int.TryParse(Name, out var result))
            {
                // Success flow
            }
            {
                // Fail flow
            }
        }

        [Benchmark]
        public ResultAndCode<string> WebApiReturnResult()
        {
            return new ResultAndCode<string>()
            {
                Result = "Not found",
                Code = 404
            };
        }

        [Benchmark]
        public void WebApiThrowException()
        {
            try
            {
                throw new Exception("Not found");
            }
            catch (Exception)
            {
            }
        }
    }
}
