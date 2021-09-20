using ModelCompiler;
using OOI.ModelCompilerUI.ToForms;
using System;

namespace OOI.ModelCompilerUI
{
  internal class EntryPoint
  {
    private static void Main(string[] args)
    {
      bool noGui = Environment.CommandLine.Contains(consoleOutputCommandLineArgument);
      Program.Main(noGui, new GUIHandling());
    }

    private const string consoleOutputCommandLineArgument = "-console";
  }
}