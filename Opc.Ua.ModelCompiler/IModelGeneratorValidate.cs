//__________________________________________________________________________________________________
//
//  Copyright (C) 2021, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GitHub: https://github.com/mpostol/OPC-UA-OOI/discussions
//__________________________________________________________________________________________________

using System.Collections.Generic;

namespace OOI.ModelCompiler
{
  internal interface IModelGeneratorValidate
  {
    List<string> DesignFiles { get; }
    string IdentifierFile { get; }
    uint StartId { get; }
    string specificationVersion { get; }
    bool UseAllowSubtypes { get; }
  }
}