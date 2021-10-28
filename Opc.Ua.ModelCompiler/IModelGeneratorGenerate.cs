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
    bool generateMultiFile { get; }
    bool UseXmlInitializers { get; }
    string[] ExcludeCategories { get; }
    bool IncludeDisplayNames { get; }
    //string OutputDir { get; }
  }
}