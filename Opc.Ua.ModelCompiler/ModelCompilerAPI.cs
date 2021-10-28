//__________________________________________________________________________________________________
//
//  Copyright (C) 2021, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GitHub: https://github.com/mpostol/OPC-UA-OOI/discussions
//__________________________________________________________________________________________________

using System;
using System.Collections.Generic;
using System.IO;

namespace OOI.ModelCompiler
{
  public abstract class ModelCompilerAPI : IModelGeneratorGenerate, IModelGeneratorValidate
  {
    protected internal string stackRootDir = null;
    protected internal string ansicRootDir = null;
    protected internal string OutputDir = null;

    #region IModelGeneratorGenerate

    public string[] ExcludeCategories { get; protected set; } = null;

    public bool UseXmlInitializers { get; protected set; } = false;

    public bool IncludeDisplayNames { get; protected set; } = false;

    #endregion IModelGeneratorGenerate

    #region IStackGeneratorGenerate

    public List<string> DesignFiles { get; protected set; } = new List<string>();
    public string IdentifierFile { get; protected set; } = null;
    public string SpecificationVersion { get; protected set; } = string.Empty;

    #endregion IStackGeneratorGenerate

    #region IModelGeneratorValidate

    public uint StartId { get; protected set; } = 1;
    public bool UseAllowSubtypes { get; protected set; } = false;
    public bool generateMultiFile { get; protected set; } = false;

    #endregion IModelGeneratorValidate

    protected virtual void Execute()
    {
      ModelGenerator2 Generator = new ModelGenerator2();
      //Build model
      Generator.ValidateAndUpdateIds(this);
      if (!string.IsNullOrEmpty(OutputDir))
        Generator.Generate(this, OutputDir);
      //else
      {
        //.NET stack generator
        if (!string.IsNullOrEmpty(stackRootDir))
        {
          if (!Directory.Exists(stackRootDir))
            throw new ArgumentException($"The directory does not exist: {stackRootDir}");
          StackGenerator.GenerateDotNet(this, stackRootDir);
        }
        //Build ANSI C stack
        if (!string.IsNullOrEmpty(ansicRootDir))
        {
          if (!Directory.Exists(ansicRootDir))
            throw new ArgumentException($"The directory does not exist: {ansicRootDir}");
          StackGenerator.GenerateAnsiC(this, ansicRootDir);
          Generator.GenerateIdentifiersAndNamesForAnsiC(ansicRootDir, ExcludeCategories);
        }
      }
    }
  }
}