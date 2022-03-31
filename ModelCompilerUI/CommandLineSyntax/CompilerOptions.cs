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
    [Option('d', "d2", HelpText = d2Help, MetaValue = "ModelDesign", Required = true)]
    public IList<string> DesignFiles { get; set; }

    [Option('c', "CSVfile", SetName = "csv", HelpText = cHelp, MetaValue = "CSVfile", Required = false)]
    public string IdentifierFile { get; set; }

    [Option('g', "cg", SetName = "csv", HelpText = cgHelp, MetaValue = "CSVfileGenrate", Required = false)]
    public bool CreateIdentifierFile { get; set; }

    [Option('o', "o2", HelpText = oHelp, MetaValue = "output", Required = false)]
    public string OutputPath { get; set; }

    [Option('i', "id", HelpText = idHelp, Required = false, Default = (uint)0)]
    public uint StartId { get; set; }

    [Option('e', "exclude", HelpText = excludeHeelp, Required = false)]
    public IList<string> Exclusions { get; set; }

    [Option('v', "version", HelpText = versionHelp, Required = false, Default = "v104")]
    public string Version { get; set; }

    [Option('s', "useAllowSubtypes", HelpText = useAllowSubtypesHelp, Required = false)]
    public bool UseAllowSubtypes { get; set; }

    [Option('m', "mv", HelpText = mvHelp, Required = false)]
    public string ModelVersion { get; set; }

    [Option('m', "mv", HelpText = pdHelp, Required = false)]
    public string ModelPublicationDate { get; set; }

    public string InputPath;
    public string FilePattern;
    public LicenseType LicenseType;
    public bool Silent;
    public string Annex1Path;
    public string Annex2Path;

    private const string d2Help =
      "The path to the ModelDesign files which contain the UA information model. The new version of the code generator is used (option -stack forces to use -d2 switch)";

    private const string cHelp = "The path to the CSV file which contains the unique identifiers for the types defined in the UA information model.";
    private const string cgHelp = "Creates the identifier file if it does not exist (used instead of the -c option).";
    private const string oHelp = "The output directory for the generated files.";
    private const string idHelp = "The first identifier to use when assigning new ids to nodes.";
    private const string excludeHeelp = "Comma seperated list of ReleaseStatus values to exclude from output.";
    private const string versionHelp = "Selects the source for the input files. v103 | v104 | v105 are supported.";
    private const string useAllowSubtypesHelp = " When subtypes are allowed for a field, C# code with the class name from the model is created instead of ExtensionObject. No effect when subtypes are not allowed.";
    private const string mvHelp = "The version of the model to produce.";
    private const string pdHelp = "The publication date of the model to produce.";
  }

  [Verb("DotNetStack", false, HelpText = "Generates code for the core model (not used for vendor defined models)")]
  public class DotNetStackOptions
  {
    [Option('n', "stack", HelpText = stackHelp, Group = "StackType", Required = true)]
    public string DotNetStackPath { get; set; }

    [Option('a', "ansic", HelpText = ansicHelp, Group = "StackType", Required = true)]
    public string AnsiCStackPath { get; set; }

    private const string ansicHelp = "Generates the ANSI C stack code for the core model (not used for vendor defined models). The path to use when generating ANSI C stack code.";
    private const string stackHelp = "Generates the .NET stack code for the core model (not used for vendor defined models). The path to use when generating .NET stack code.";
  }

  [Verb("units", false, HelpText = "Generates the OPC UA Engineering Units CSV from the official UNECE table of units.")]
  public class UnitsOptions
  {
    [Option("annex1", HelpText = annex1Help, Required = false)]
    public string Annex1Path { get; set; }

    [Option("annex2", HelpText = annex2Help, Required = false)]
    public string Annex2Path { get; set; }

    [Option("output", HelpText = outputHelp, Required = false)]
    public string OutputPath { get; set; }

    private const string annex1Help = "The path to the UNECE Annex 1 CSV file.";
    private const string annex2Help = "The path to the UNECE Annex 2/3 CSV file.";
    private const string outputHelp = "The units output directory.";
  }

  public class UpdateHeadersOptions
  {
  }
}

//Opc.Ua.ModelCompiler.exe - d2 < filepath > -c[g] < filepath > -o2 < directorypath >

//-console The output goes to the standard error output (console) instead of error window

// OptionsNames:
//
//InputPath = "input";
//FilePattern = "pattern";
//LicenseType = "license";
//Silent = "silent";