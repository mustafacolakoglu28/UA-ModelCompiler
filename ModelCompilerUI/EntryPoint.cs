//__________________________________________________________________________________________________
//
//  Copyright (C) 2021, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GitHub: https://github.com/mpostol/OPC-UA-OOI/discussions
//__________________________________________________________________________________________________

using ModelCompiler;
using OOI.ModelCompilerUI.ToForms;
using System;
using System.Windows.Forms;

namespace OOI.ModelCompilerUI
{
  internal class EntryPoint
  {
    private static void Main(string[] args)
    {
      string commandLine = Environment.CommandLine;
      bool noGui = commandLine.Contains(consoleOutputCommandLineArgument);
      if (!noGui)
      {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
      }
      //IList<string> args = Environment.GetCommandLineArgs();
      int exitCode = 0;
      if (MeasurementUnits.ProcessCommandLine(args))
        exitCode = 0;
      else
        exitCode = Program.Main(noGui, commandLine, new GUIHandling());
      Environment.Exit(exitCode);
    }

    private const string consoleOutputCommandLineArgument = "-console";
  }
}