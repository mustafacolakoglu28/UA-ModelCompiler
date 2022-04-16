//__________________________________________________________________________________________________
//
//  Copyright (C) 2022, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GitHub: https://github.com/mpostol/OPC-UA-OOI/discussions
//__________________________________________________________________________________________________

using CommandLine;

namespace OOI.ModelCompilerUI.CommandLineSyntax
{
  [Verb("stack", false, HelpText = "Generates code for the core model (not used for vendor defined models)")]
  public class DotNetStackOptions
  {
    [Option('n', OptionsNames.DotNetStackPath, HelpText = stackHelp, Group = "StackType", Required = true)]
    public string DotNetStackPath { get; set; }

    [Option('a', OptionsNames.AnsiCStackPath, HelpText = ansicHelp, Group = "StackType", Required = true)]
    public string AnsiCStackPath { get; set; }

    private const string ansicHelp = "Generates the ANSI C stack code for the core model (not used for vendor defined models). The path to use when generating ANSI C stack code.";
    private const string stackHelp = "Generates the .NET stack code for the core model (not used for vendor defined models). The path to use when generating .NET stack code.";

    private static class OptionsNames
    {
      public const string AnsiCStackPath = "ansic";
      public const string DotNetStackPath = "stack";
    }
  }
}