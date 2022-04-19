//__________________________________________________________________________________________________
//
//  Copyright (C) 2022, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GitHub: https://github.com/mpostol/OPC-UA-OOI/discussions
//__________________________________________________________________________________________________

using System.Collections.Generic;

namespace OOI.ModelCompiler
{
  /// <summary>
  /// Interface ICompilerOptions - a contract to takes an OPC UA ModelDesign file and generate a NodeSet and code for the .NETStandard stack.
  /// </summary>
  public interface ICompilerOptions
  {
    /// <summary>
    /// The output directory for the generated files.
    /// </summary>
    /// <value>The output path.</value>
    string OutputPath { get; }
    /// <summary>
    /// The path to the ModelDesign files which contain the UA information model.
    /// </summary>
    /// <value>The design files.</value>
    IList<string> DesignFiles { get; }
    /// <summary>
    /// The path to the CSV file which contains the unique identifiers for the types defined in the UA information model.
    /// </summary>
    /// <value>The identifier file.</value>
    string IdentifierFile { get; }
    /// <summary>
    /// Selects the specification version the source text is compliant with. The values v103 | v104 | v105 are supported.
    /// </summary>
    /// <value>The version.</value>
    string Version { get; }
    /// <summary>
    /// Gets the start identifier.
    /// </summary>
    /// <value>The start identifier.</value>
    uint StartId { get; }
    /// <summary>
    /// When subtypes are allowed for a field, C# code with the class name from the model is created instead of ExtensionObject. No effect when subtypes are not allowed.
    /// </summary>
    /// <value><c>true</c> if [use allow subtypes]; otherwise, <c>false</c>.</value>
    bool UseAllowSubtypes { get; }
    /// <summary>
    /// Comma separated list of ReleaseStatus values to exclude from output.
    /// </summary>
    /// <value>The exclusions.</value>
    IList<string> Exclusions { get; }
    /// <summary>
    /// The publication date of the model to produce.
    /// </summary>
    /// <value>The model publication date.</value>
    string ModelPublicationDate { get; }
    /// <summary>
    /// Indicates that a release candidate nodeset is being generated.
    /// </summary>
    /// <value><c>true</c> if [release candidate]; otherwise, <c>false</c>.</value>
    bool ReleaseCandidate { get; }
    /// <summary>
    /// The version of the model to produce.
    /// </summary>
    /// <value>The model version.</value>
    string ModelVersion { get; }
  }
}