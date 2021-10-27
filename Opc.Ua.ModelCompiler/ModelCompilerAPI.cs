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

    #region IModelGeneratorGenerate

    public string[] ExcludeCategories { get; protected set; } = null;

    public string OutputDir { get; protected set; } = null;

    public bool UseXmlInitializers { get; protected set; } = false;

    public bool IncludeDisplayNames { get; protected set; } = false;

    #endregion IModelGeneratorGenerate

    #region IModelGeneratorValidate

    public List<string> DesignFiles { get; protected set; } = new List<string>();

    public string IdentifierFile { get; protected set; } = null;

    public uint StartId { get; protected set; } = 1;

    public string specificationVersion { get; protected set; } = string.Empty;

    public bool UseAllowSubtypes { get; protected set; } = false;

    public bool generateMultiFile { get; protected set; } = false;

    #endregion IModelGeneratorValidate

    protected virtual void Execute()
    {
      ModelGenerator2 Generator = new ModelGenerator2();
      Generator.ValidateAndUpdateIds(this);
      //.NET stack generator
      if (!string.IsNullOrEmpty(stackRootDir))
      {
        if (!Directory.Exists(stackRootDir))
          throw new ArgumentException($"The directory does not exist: {stackRootDir}");
        StackGenerator.GenerateDotNet(DesignFiles, IdentifierFile, stackRootDir, specificationVersion);
      }
      //Build ANSI C stack
      if (!string.IsNullOrEmpty(ansicRootDir))
      {
        if (!Directory.Exists(ansicRootDir))
          throw new ArgumentException($"The directory does not exist: {ansicRootDir}");
        StackGenerator.GenerateAnsiC(DesignFiles, IdentifierFile, ansicRootDir, specificationVersion);
        Generator.GenerateIdentifiersAndNamesForAnsiC(ansicRootDir, ExcludeCategories);
      }
      //Build model
      if (!string.IsNullOrEmpty(OutputDir))
        Generator.Generate(this);
    }
  }

  internal interface IModelGeneratorValidate
  {
    List<string> DesignFiles { get; }
    string IdentifierFile { get; }
    uint StartId { get; }
    string specificationVersion { get; }
    bool UseAllowSubtypes { get; }
  }

  internal interface IModelGeneratorGenerate
  {
    bool generateMultiFile { get; }
    bool UseXmlInitializers { get; }
    string[] ExcludeCategories { get; }
    bool IncludeDisplayNames { get; }
    string OutputDir { get; }
  }
}