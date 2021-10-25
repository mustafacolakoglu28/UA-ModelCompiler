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
  public abstract class ModelCompilerAPI
  {
    protected internal List<string> designFiles = new List<string>();
    protected internal string identifierFile = null;
    protected internal string outputDir = null;
    protected internal bool generateIds = false;
    protected internal uint startId = 1;
    protected internal string stackRootDir = null;
    protected internal string ansicRootDir = null;
    protected internal bool generateMultiFile = false;
    protected internal bool useXmlInitializers = false;
    protected internal string[] excludeCategories = null;
    protected internal bool includeDisplayNames = false;
    protected internal bool useAllowSubtypes = false;
    protected internal string inputDirectory = ".";
    protected internal string filePattern = "*.xml";
    protected internal string specificationVersion = "";
    protected internal bool silent = false;

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
      Generator.ValidateAndUpdateIds(designFiles, identifierFile, startId, specificationVersion, useAllowSubtypes);
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
        Generator.GenerateIdentifiersAndNamesForAnsiC(ansicRootDir, excludeCategories);
      }
      //Build model
      if (!string.IsNullOrEmpty(outputDir))
      {
        if (generateMultiFile)
          Generator.GenerateMultipleFiles(outputDir, useXmlInitializers, excludeCategories, includeDisplayNames);
        else
          Generator.GenerateInternalSingleFile(outputDir, useXmlInitializers, excludeCategories, includeDisplayNames);
      }
    }
  }
}