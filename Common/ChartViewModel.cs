using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public class ReportWith2Col
    {
        public string Col1 { get; set; }
        public string Col2 { get; set; }
    }
    public class ChartWith2ListCol
    {
        public List<string> Li1 { get; set; }
        public List<string> Li2 { get; set; }
    }
    public class ChartWith3ListCol
    {
        public List<string> Li1 { get; set; }
        public List<string> Li2 { get; set; }
        public List<string> Li3 { get; set; }
    }
    public class ChartWith3Col
    {
        public string Col1 { get; set; }
        public string Col2 { get; set; }
        public DateTime Col3 { get; set; }
    }
    public class ChartWith3ColString
    {
        public string Col1 { get; set; }
        public string Col2 { get; set; }
        public string Col3 { get; set; }
        public string Col4 { get; set; }
        public string Col5 { get; set; }
        public string Col6 { get; set; }
        public string Col7 { get; set; }
        public string Col8 { get; set; }
        public string Col9 { get; set; }
        public string ChartType { get; set; }
        public List<float> Data { get; set; }
        public List<int> Mal_Data { get; set; }
        public List<int> Female_Data { get; set; }
        public List<DateValue> DateValueData1 { get; set; }
        public List<DateValue> DateValueData2 { get; set; }
        public DateTime Date { get; set; }
        public List<DateTime> ListDate { get; set; }
    }
    public class DateValue
    {
        public string Col1 { get; set; }
        public float Col2 { get; set; }
    }
    public class ChartWith6Col
    {
        public string Col1 { get; set; }
        public string Col2 { get; set; }
        public string Col3 { get; set; }
        public string Col4 { get; set; }
        public string Col5 { get; set; }
        public DateTime Col6 { get; set; }
    }
    public class ChartWithColForExam
    {
        public string Col1 { get; set; }
        public List<string> Col2 { get; set; }
        public List<string> Col3 { get; set; }
        public List<string> Col4 { get; set; }
    }
}