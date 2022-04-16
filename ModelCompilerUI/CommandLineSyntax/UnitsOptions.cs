//__________________________________________________________________________________________________
//
//  Copyright (C) 2022, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GitHub: https://github.com/mpostol/OPC-UA-OOI/discussions
//__________________________________________________________________________________________________

using CommandLine;

namespace OOI.ModelCompilerUI.CommandLineSyntax
{
  [Verb("units", false, HelpText = "Generates the OPC UA Engineering Units CSV from the official UNECE table of units.")]
  public class UnitsOptions
  {
    [Option(OptionsNames.Annex1Path, HelpText = annex1Help, Required = true)]
    public string Annex1Path { get; set; }

    [Option(OptionsNames.Annex2Path, HelpText = annex2Help, Required = true)]
    public string Annex2Path { get; set; }

    [Option(OptionsNames.UnitsOutputPath, HelpText = outputHelp, Required = true)]
    public string OutputPath { get; set; }

    private const string annex1Help = "The path to the UNECE Annex 1 CSV file.";
    private const string annex2Help = "The path to the UNECE Annex 2/3 CSV file.";
    private const string outputHelp = "The units output directory.";

    private static class OptionsNames
    {
      public const string Annex1Path = "annex1";
      public const string Annex2Path = "annex2";
      public const string UnitsOutputPath = "output";
    }
  }
}