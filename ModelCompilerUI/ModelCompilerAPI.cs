//__________________________________________________________________________________________________
//
//  Copyright (C) 2021, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GitHub: https://github.com/mpostol/OPC-UA-OOI/discussions
//__________________________________________________________________________________________________

using OOI.ModelCompiler;
using System;
using System.IO;

namespace OOI.ModelCompilerUI
{
  public abstract class ModelCompilerAPI
  {
    protected internal string stackRootDir = null;
    protected internal string ansicRootDir = null;
    protected internal string OutputDir = null;

    protected virtual void Execute(IModelGeneratorGenerate generateParameters, IModelGeneratorValidate validateParameters)
    {
      ModelGenerator2 Generator = new ModelGenerator2();
      //Build model
      Generator.ValidateAndUpdateIds(validateParameters);
      if (!string.IsNullOrEmpty(OutputDir))
        Generator.Generate(generateParameters, OutputDir);
      //.NET stack generator
      if (!string.IsNullOrEmpty(stackRootDir))
      {
        if (!Directory.Exists(stackRootDir))
          throw new ArgumentException($"The directory does not exist: {stackRootDir}");
        StackGenerator.GenerateDotNet(validateParameters, stackRootDir);
      }
      //Build ANSI C stack
      if (!string.IsNullOrEmpty(ansicRootDir))
      {
        if (!Directory.Exists(ansicRootDir))
          throw new ArgumentException($"The directory does not exist: {ansicRootDir}");
        StackGenerator.GenerateAnsiC(validateParameters, ansicRootDir);
        Generator.GenerateIdentifiersAndNamesForAnsiC(ansicRootDir, generateParameters.ExcludeCategories);
      }
    }
  }
}