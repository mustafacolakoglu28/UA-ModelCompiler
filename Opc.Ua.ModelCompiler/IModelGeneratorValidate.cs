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
  /// Interface ICompilerOptions
  /// </summary>
  //TODO CLI Syntax #67 - add documentation
  public interface ICompilerOptions
  {
    string OutputPath { get; }
    IList<string> DesignFiles { get; }
    string IdentifierFile { get; }
    string Version { get; }
    uint StartId { get; }
    bool UseAllowSubtypes { get; }
    IList<string> Exclusions { get; }
    string ModelPublicationDate { get; }
    bool ReleaseCandidate { get; }
    string ModelVersion { get; }
  }
}