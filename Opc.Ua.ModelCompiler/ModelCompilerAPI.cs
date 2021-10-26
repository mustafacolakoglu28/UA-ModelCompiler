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
    protected internal bool generateIds = false;
    protected internal string stackRootDir = null;
    protected internal string ansicRootDir = null;
    protected internal bool generateMultiFile = false;
    protected internal string filePattern = "*.xml";

    protected internal bool silent = false;

    #region IModelGeneratorGenerate

    public string[] ExcludeCategories { get; protected set; } = null;

    public string OutputDir { get; protected set; } = null;

    public bool UseXmlInitializers { get; protected set; } = false;

    public bool IncludeDisplayNames { get; protected set; } = false;

    #endregion IModelGeneratorGenerate

    #region IModelGeneratorValidate

    public List<string> designFiles { get; protected set; } = new List<string>();

    public string identifierFile { get; protected set; } = null;

    public uint startId { get; protected set; } = 1;

    public string specificationVersion { get; protected set; } = string.Empty;

    public bool useAllowSubtypes { get; protected set; } = false;

    #endregion IModelGeneratorValidate

    protected virtual void Execute()
    {
      for (int ii = 0; ii < designFiles.Count; ii++)
      {
        if (string.IsNullOrEmpty(designFiles[ii]))
          throw new ArgumentException("No design file specified.");
        if (!File.Exists(designFiles[ii]))
          throw new ArgumentException($"The design file does not exist: {designFiles[ii]}");
      }
      if (string.IsNullOrEmpty(identifierFile))
        throw new ArgumentException("No identifier file specified.");
      if (!File.Exists(identifierFile))
      {
        if (!generateIds)
          throw new ArgumentException($"The identifier file does not exist: {identifierFile}");
        File.Create(identifierFile).Close();
      }
      ModelGenerator2 Generator = new ModelGenerator2();
      Generator.ValidateAndUpdateIds(this);
      //.NET stack generator
      if (!string.IsNullOrEmpty(stackRootDir))
      {
        if (!Directory.Exists(stackRootDir))
          throw new ArgumentException($"The directory does not exist: {stackRootDir}");
        StackGenerator.GenerateDotNet(designFiles, identifierFile, stackRootDir, specificationVersion);
      }
      //Build ANSI C stack
      if (!string.IsNullOrEmpty(ansicRootDir))
      {
        if (!Directory.Exists(ansicRootDir))
          throw new ArgumentException($"The directory does not exist: {ansicRootDir}");
        StackGenerator.GenerateAnsiC(designFiles, identifierFile, ansicRootDir, specificationVersion);
        Generator.GenerateIdentifiersAndNamesForAnsiC(ansicRootDir, ExcludeCategories);
      }
      //Build model
      if (!string.IsNullOrEmpty(OutputDir))
      {
        if (generateMultiFile)
          Generator.GenerateMultipleFiles(this);
        else
          Generator.GenerateInternalSingleFile(this);
      }
    }
  }

  internal interface IModelGeneratorValidate
  {
    List<string> designFiles { get; }
    string identifierFile { get; }
    uint startId { get; }
    string specificationVersion { get; }
    bool useAllowSubtypes { get; }
  }

  internal interface IModelGeneratorGenerate
  {
    bool UseXmlInitializers { get; }
    string[] ExcludeCategories { get; }
    bool IncludeDisplayNames { get; }
    string OutputDir { get; }
  }
}