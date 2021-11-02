//__________________________________________________________________________________________________
//
//  Copyright (C) 2021, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GitHub: https://github.com/mpostol/OPC-UA-OOI/discussions
//__________________________________________________________________________________________________

using System.Collections.Generic;

namespace OOI.ModelCompiler
{
  public interface IStackGeneratorGenerate
  {
    List<string> DesignFiles { get; }
    string IdentifierFile { get; }
    string SpecificationVersion { get; }
  }

  public interface IModelGeneratorValidate : IStackGeneratorGenerate
  {
    uint StartId { get; }
    bool UseAllowSubtypes { get; }
  }
}