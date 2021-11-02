//__________________________________________________________________________________________________
//
//  Copyright (C) 2021, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GitHub: https://github.com/mpostol/OPC-UA-OOI/discussions
//__________________________________________________________________________________________________

using System;

namespace OOI.ModelCompiler
{
  public static class ModelCompiler
  {
    public static void BuildModel(string outputDir, IModelGeneratorGenerate generateParameters, IModelGeneratorValidate validateParameters)
    {
      if (string.IsNullOrEmpty(outputDir))
        throw new ArgumentOutOfRangeException("OutputDir", "Parameters cannot be null or empty");
      ModelGenerator2 Generator = new ModelGenerator2();
      Generator.ValidateAndUpdateIds(validateParameters);
      Generator.Generate(generateParameters, outputDir);
    }
  }
}