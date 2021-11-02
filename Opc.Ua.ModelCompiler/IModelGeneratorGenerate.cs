//__________________________________________________________________________________________________
//
//  Copyright (C) 2021, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GitHub: https://github.com/mpostol/OPC-UA-OOI/discussions
//__________________________________________________________________________________________________

namespace OOI.ModelCompiler
{
  internal interface IModelGeneratorGenerate
  {
    string[] ExcludeCategories { get; }
    bool GenerateMultiFile { get; }
    bool IncludeDisplayNames { get; }
    bool UseXmlInitializers { get; }
    //string OutputDir { get; }
  }
}