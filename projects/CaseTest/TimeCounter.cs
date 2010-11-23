using System;
using System.IO;
namespace CaseTest
{
	public class TimeCounter
	{
		private DateTime start = DateTime.Now;
		private DateTime end;
		private TimeSpan span = new TimeSpan(0);
		private bool startCalled = false;
		private int callsNumber = 0;
		private string operation;
		
		public int CallsNumber 
		{
			get { return this.callsNumber; }
		}

		public TimeSpan Total
		{
			get { return this.span; }
		}

		public TimeCounter(string operationName)
		{
			operation = operationName;
		}
		
		public void Start()
		{
			start = DateTime.Now;
			startCalled = true;
		}
		
		public void Stop()
		{
			if (!startCalled)
			{
				throw new InvalidOperationException();
			}
			end = DateTime.Now;
			startCalled = false;
			callsNumber++;
			span += end.Subtract(start);
		}
		
		public void PrintLine(string line)
		{
			File.AppendAllText("benchmark_output.txt", line + "\n");
			Console.Write(line + "\n");
		}

		public  void PrintTime()
		{
			string line = string.Format("Operation: {0}, Time spent: {1:F0} minutes and {2} seconds.\n", operation, 
			                  span.TotalMinutes, span.Seconds);
			File.AppendAllText("benchmark_output.txt", line);
			Console.Write(line);
			if (callsNumber > 0)
			{
				line = string.Format("Average per call: {0} seconds, {1} calls.\n",
				                  span.TotalSeconds / callsNumber, callsNumber);
			}
			File.AppendAllText("benchmark_output.txt", line);
			Console.Write(line);
		}		
		
	}
}

