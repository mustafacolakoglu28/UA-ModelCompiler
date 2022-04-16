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
  [Verb("compile", true, HelpText = "Generates classes that implement a UA information model")]
  internal class CompilerOptions
  {
    [Option('d', OptionsNames.DesignFiles, HelpText = d2Help, MetaValue = "ModelDesign", Required = true)]
    public IList<string> DesignFiles { get; set; }

    [Option(OptionsNames.IdentifierFile, SetName = "csv", HelpText = cHelp, MetaValue = "CSVfile", Required = false)]
    public string IdentifierFile { get; set; }

    [Option('g', OptionsNames.GenerateIdentifierFile, SetName = "csv", HelpText = cgHelp, MetaValue = "CSVfileGenrate", Required = false)]
    public bool CreateIdentifierFile { get; set; }

    [Option('o', OptionsNames.OutputPath, HelpText = oHelp, MetaValue = "output", Required = false)]
    public string OutputPath { get; set; }

    [Option('i', OptionsNames.StartId, HelpText = idHelp, Required = false, Default = (uint)0)]
    public uint StartId { get; set; }

    [Option('e', OptionsNames.Exclusions, HelpText = excludeHeelp, Required = false)]
    public IList<string> Exclusions { get; set; }

    [Option('v', OptionsNames.Version, HelpText = versionHelp, Required = false, Default = "v104")]
    public string Version { get; set; }

    [Option('s', OptionsNames.UseAllowSubtypes, HelpText = useAllowSubtypesHelp, Required = false)]
    public bool UseAllowSubtypes { get; set; }

    [Option('m', OptionsNames.ModelVersion, HelpText = mvHelp, Required = false)]
    public string ModelVersion { get; set; }

    [Option(OptionsNames.ModelPublicationDate, HelpText = pdHelp, Required = false)]
    public string ModelPublicationDate { get; set; }

    [Option(OptionsNames.ReleaseCandidate, HelpText = rcHelp, Required = false)]
    public bool ReleaseCandidate { get; set; }

    private const string d2Help =
      "The path to the ModelDesign files which contain the UA information model. The new version of the code generator is used (option -stack forces to use -d2 switch)";

    private const string cHelp = "The path to the CSV file which contains the unique identifiers for the types defined in the UA information model.";
    private const string cgHelp = "Creates the identifier file if it does not exist (used instead of the -c option).";
    private const string oHelp = "The output directory for the generated files.";
    private const string idHelp = "The first identifier to use when assigning new ids to nodes.";
    private const string excludeHeelp = "Comma separated list of ReleaseStatus values to exclude from output.";
    private const string versionHelp = "Selects the source for the input files. v103 | v104 | v105 are supported.";
    private const string useAllowSubtypesHelp = " When subtypes are allowed for a field, C# code with the class name from the model is created instead of ExtensionObject. No effect when subtypes are not allowed.";
    private const string mvHelp = "The version of the model to produce.";
    private const string pdHelp = "The publication date of the model to produce.";
    private const string rcHelp = "Indicates that a release candidate nodeset is being generated.";

    private static class OptionsNames
    {
      public const string DesignFiles = "d2";
      public const char IdentifierFile = 'c';
      public const string GenerateIdentifierFile = "cg";
      public const string OutputPath = "o2";
      public const string Version = "version";
      public const string UseAllowSubtypes = "useAllowSubtypes";
      public const string StartId = "id";
      public const string Exclusions = "exclude";
      public const string ModelVersion = "mv";
      public const string ModelPublicationDate = "pd";
      public const string ReleaseCandidate = "rc";
    }
  }

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
  }
}