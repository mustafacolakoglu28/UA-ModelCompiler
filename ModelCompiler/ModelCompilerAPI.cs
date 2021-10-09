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
    protected List<string> designFiles = new List<string>();
    protected string identifierFile = null;
    protected string outputDir = null;
    protected bool generateIds = false;
    protected uint startId = 1;
    protected string stackRootDir = null;
    protected string ansicRootDir = null;
    protected bool generateMultiFile = false;
    protected bool useXmlInitializers = false;
    protected string[] excludeCategories = null;
    protected bool includeDisplayNames = false;
    protected bool useAllowSubtypes = false;

    protected bool updateHeaders = false;
    protected string inputDirectory = ".";
    protected string filePattern = "*.xml";
    protected string specificationVersion = "";
    protected LicenseType licenseType = LicenseType.MITXML;
    protected bool silent = false;

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