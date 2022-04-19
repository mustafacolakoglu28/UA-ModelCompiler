//__________________________________________________________________________________________________
//
//  Copyright (C) 2022, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GitHub: https://github.com/mpostol/OPC-UA-OOI/discussions
//__________________________________________________________________________________________________

using CommandLine;
using System.Collections.Generic;

namespace OOI.ModelCompilerUI.CommandLineSyntax
{
  internal abstract class OptionsBase
  {
    #region options

    [Option('d', OptionsNames.DesignFiles, HelpText = d2Help, MetaValue = "ModelDesign", Required = true)]
    public IList<string> DesignFiles { get; set; }

    [Option(OptionsNames.IdentifierFile, SetName = "csv", HelpText = cHelp, MetaValue = "CSVfile", Required = false)]
    public string IdentifierFile { get; set; }

    [Option('o', OptionsNames.OutputPath, HelpText = oHelp, MetaValue = "output", Required = false)]
    public string OutputPath { get; set; }

    [Option('e', OptionsNames.Exclusions, HelpText = excludeHeelp, Required = false)]
    public IList<string> Exclusions { get; set; }

    [Option('v', OptionsNames.SpecVersion, HelpText = versionHelp, Required = false, Default = "v104")]
    public string Version { get; set; }

    #endregion options

    #region private

    private const string oHelp = "The output directory for the generated files.";
    private const string d2Help = "The path to the ModelDesign files which contain the UA information model. The new version of the code generator is used (option -stack forces to use -d2 switch)";
    private const string cHelp = "The path to the CSV file which contains the unique identifiers for the types defined in the UA information model.";
    private const string excludeHeelp = "Comma separated list of ReleaseStatus values to exclude from output.";
    private const string versionHelp = "Selects the specification version the source text is compliant with. The values v103 | v104 | v105 are supported.";

    private static class OptionsNames
    {
      public const string DesignFiles = "d2";
      public const string OutputPath = "o2";
      public const char IdentifierFile = 'c';
      public const string SpecVersion = "spec";
      public const string Exclusions = "exclude";
    }

    #endregion private
  }
}