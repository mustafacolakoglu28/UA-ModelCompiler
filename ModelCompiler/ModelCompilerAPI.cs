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
    protected internal bool updateHeaders = false;
    protected internal string inputDirectory = ".";
    protected internal string filePattern = "*.xml";
    protected internal string specificationVersion = "";
    protected internal LicenseType licenseType = LicenseType.MITXML;
    protected internal bool silent = false;

    public void Execute()
    {
      if (updateHeaders)
      {
        HeaderUpdateTool.ProcessDirectory(inputDirectory, filePattern, licenseType, silent);
        return;
      }

      ModelGenerator2 generator = new ModelGenerator2();

      for (int ii = 0; ii < designFiles.Count; ii++)
      {
        if (string.IsNullOrEmpty(designFiles[ii]))
        {
          throw new ArgumentException("No design file specified.");
        }

        if (!new FileInfo(designFiles[ii]).Exists)
        {
          throw new ArgumentException("The design file does not exist: " + designFiles[ii]);
        }
      }

      if (string.IsNullOrEmpty(identifierFile))
      {
        throw new ArgumentException("No identifier file specified.");
      }

      if (!new FileInfo(identifierFile).Exists)
      {
        if (!generateIds)
        {
          throw new ArgumentException("The identifier file does not exist: " + identifierFile);
        }

        File.Create(identifierFile).Close();
      }

      generator.ValidateAndUpdateIds(
          designFiles,
          identifierFile,
          startId,
          specificationVersion,
          useAllowSubtypes);

      if (!string.IsNullOrEmpty(stackRootDir))
      {
        if (!new DirectoryInfo(stackRootDir).Exists)
        {
          throw new ArgumentException("The directory does not exist: " + stackRootDir);
        }

        StackGenerator.GenerateDotNet(stackRootDir, specificationVersion);
      }

      if (!string.IsNullOrEmpty(ansicRootDir))
      {
        if (!new DirectoryInfo(ansicRootDir).Exists)
        {
          throw new ArgumentException("The directory does not exist: " + ansicRootDir);
        }

        StackGenerator.GenerateAnsiC(ansicRootDir, specificationVersion);
        generator.GenerateIdentifiersAndNamesForAnsiC(ansicRootDir, excludeCategories);
      }

      if (!string.IsNullOrEmpty(outputDir))
      {
        if (generateMultiFile)
          generator.GenerateMultipleFiles(outputDir, useXmlInitializers, excludeCategories, includeDisplayNames);
        else
          generator.GenerateInternalSingleFile(outputDir, useXmlInitializers, excludeCategories, includeDisplayNames);
      }
    }
  }
}