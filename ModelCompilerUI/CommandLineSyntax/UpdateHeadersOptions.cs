//__________________________________________________________________________________________________
//
//  Copyright (C) 2022, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GitHub: https://github.com/mpostol/OPC-UA-OOI/discussions
//__________________________________________________________________________________________________

using CommandLine;

namespace OOI.ModelCompilerUI.CommandLineSyntax
{
  [Verb("update-headers", false, HelpText = "Updates all files in the output directory with the OPC Foundation MIT license header.")]
  public class UpdateHeadersOptions
  {
    [Option(OptionsNames.InputPath, HelpText = inputPathHelp, Required = true)]
    public string InputPath { get; set; }

    [Option(OptionsNames.FilePattern, HelpText = filePatternHelp, Required = true)]
    public string FilePattern { get; set; }

    [Option(OptionsNames.LicenseType, HelpText = licenseTypeHelp, Required = true)]
    public LicenseType LicenseType { get; set; }

    [Option(OptionsNames.Silent, HelpText = silentHelp, Required = false)]
    public bool Silent { get; set; }

    public override string ToString()
    {
      return Parser.Default.FormatCommandLine<UpdateHeadersOptions>(this);
    }

    #region private

    private const string inputPathHelp = "The path folders to search for files to update.";
    private const string filePatternHelp = "The file pattern to use when selecting files.";
    private const string licenseTypeHelp = "The type of license (MIT | MITXML | NONE).";
    private const string silentHelp = "Suppresses any exceptions.";

    private static class OptionsNames
    {
      public const string InputPath = "input";
      public const string FilePattern = "pattern";
      public const string LicenseType = "license";
      public const string Silent = "silent";
    }

    #endregion private

  }
}