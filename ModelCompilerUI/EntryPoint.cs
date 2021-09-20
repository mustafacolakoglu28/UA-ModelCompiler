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
      bool noGui = Environment.CommandLine.Contains(consoleOutputCommandLineArgument);
      if (!noGui)
      {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
      }
      Program.Main(noGui, new GUIHandling());
    }

    private const string consoleOutputCommandLineArgument = "-console";
  }
}