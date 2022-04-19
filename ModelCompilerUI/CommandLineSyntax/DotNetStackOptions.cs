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
  internal class DotNetStackOptions : OptionsBase
  {
    [Option('n', OptionsNames.DotNetStackPath, HelpText = dotnetHelp, Required = false)]
    public string DotNetStackPath { get; set; }

    [Option('a', OptionsNames.AnsiCStackPath, HelpText = ansicHelp, Required = false)]
    public string AnsiCStackPath { get; set; }

    public override string ToString()
    {
      return Parser.Default.FormatCommandLine<DotNetStackOptions>(this);
    }

    #region private

    private const string ansicHelp = "Generates the ANSI C stack code for the core model (not used for vendor defined models). The path to use when generating ANSI C stack code.";
    private const string dotnetHelp = "Generates the .NET stack code for the core model (not used for vendor defined models). The path to use when generating .NET stack code.";

    private static class OptionsNames
    {
      public const string AnsiCStackPath = "ansic";
      public const string DotNetStackPath = "dotnet";
    }

    #endregion private
  }
}