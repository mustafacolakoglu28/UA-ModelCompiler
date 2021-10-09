//__________________________________________________________________________________________________
//
//  Copyright (C) 2021, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GitHub: https://github.com/mpostol/OPC-UA-OOI/discussions
//__________________________________________________________________________________________________

using ModelCompiler;
using System;
using System.Collections.Generic;
using System.IO;

namespace OOI.ModelCompiler
{
  public abstract class ModelCompilerAPI
  {
    private List<string> designFiles = new List<string>();
    private string identifierFile = null;
    private string outputDir = null;
    private bool generateIds = false;
    private uint startId = 1;
    private string stackRootDir = null;
    private string ansicRootDir = null;
    private bool generateMultiFile = false;
    private bool useXmlInitializers = false;
    private string[] excludeCategories = null;
    private bool includeDisplayNames = false;
    private bool useAllowSubtypes = false;

    private bool updateHeaders = false;
    private string inputDirectory = ".";
    private string filePattern = "*.xml";
    private string specificationVersion = "";
    private HeaderUpdateTool.LicenseType licenseType = HeaderUpdateTool.LicenseType.MITXML;
    private bool silent = false;

    public abstract void Execute();

    public static ModelCompilerAPI Factory(List<string> tokens)
    {
      ModelCompilerAPIInternal instance = new ModelCompilerAPIInternal();
      instance.ProcessCommandLine(tokens);
      return instance;
    }

    private class ModelCompilerAPIInternal : ModelCompilerAPI
    {
      public void ProcessCommandLine(List<string> tokens)
      {
        for (int ii = 1; ii < tokens.Count; ii++)
        {
          if (tokens[ii] == "-input")
          {
            if (ii >= tokens.Count - 1)
            {
              throw new ArgumentException("Incorrect number of parameters specified with the -input option.");
            }

            inputDirectory = tokens[++ii];
            continue;
          }

          if (tokens[ii] == "-pattern")
          {
            if (ii >= tokens.Count - 1)
            {
              throw new ArgumentException("Incorrect number of parameters specified with the -pattern option.");
            }

            filePattern = tokens[++ii];
            continue;
          }

          if (tokens[ii] == "-silent")
          {
            silent = true;
            continue;
          }

          if (tokens[ii] == "-license")
          {
            if (ii >= tokens.Count - 1)
            {
              throw new ArgumentException("Incorrect number of parameters specified with the -license option.");
            }

            updateHeaders = true;
            licenseType = (HeaderUpdateTool.LicenseType)Enum.Parse(typeof(HeaderUpdateTool.LicenseType), tokens[++ii]);
            continue;
          }

          if (tokens[ii] == "-d2")
          {
            if (ii >= tokens.Count - 1)
            {
              throw new ArgumentException("Incorrect number of parameters specified with the -d2 option.");
            }

            designFiles.Add(tokens[++ii]);
            continue;
          }

          if (tokens[ii] == "-d2")
          {
            if (ii >= tokens.Count - 1)
            {
              throw new ArgumentException("Incorrect number of parameters specified with the -d2 option.");
            }

            designFiles.Add(tokens[++ii]);
            continue;
          }

          if (tokens[ii] == "-d2")
          {
            if (ii >= tokens.Count - 1)
            {
              throw new ArgumentException("Incorrect number of parameters specified with the -d2 option.");
            }

            designFiles.Add(tokens[++ii]);
            continue;
          }

          if (tokens[ii] == "-c" || tokens[ii] == "-cg")
          {
            if (ii >= tokens.Count - 1)
            {
              throw new ArgumentException("Incorrect number of parameters specified with the -c or -cg option.");
            }

            generateIds = tokens[ii] == "-cg";
            identifierFile = tokens[++ii];
            continue;
          }

          if (tokens[ii] == "-o")
          {
            if (ii >= tokens.Count - 1)
            {
              throw new ArgumentException("Incorrect number of parameters specified with the -o option.");
            }

            outputDir = tokens[++ii];
            continue;
          }

          if (tokens[ii] == "-o2")
          {
            if (ii >= tokens.Count - 1)
            {
              throw new ArgumentException("Incorrect number of parameters specified with the -o option.");
            }

            outputDir = tokens[++ii];
            generateMultiFile = true;
            continue;
          }

          if (tokens[ii] == "-id")
          {
            startId = Convert.ToUInt32(tokens[++ii]);
            continue;
          }

          if (tokens[ii] == "-version")
          {
            if (ii >= tokens.Count - 1)
            {
              throw new ArgumentException("Incorrect number of parameters specified with the -version option.");
            }

            specificationVersion = tokens[++ii];
            continue;
          }

          if (tokens[ii] == "-ansic")
          {
            if (ii >= tokens.Count - 1)
            {
              throw new ArgumentException("Incorrect number of parameters specified with the -ansic option.");
            }

            ansicRootDir = tokens[++ii];
            continue;
          }

          if (tokens[ii] == "-useXmlInitializers")
          {
            useXmlInitializers = true;
            continue;
          }

          if (tokens[ii] == "-includeDisplayNames")
          {
            includeDisplayNames = true;
            continue;
          }

          if (tokens[ii] == "-stack")
          {
            if (ii >= tokens.Count - 1)
            {
              throw new ArgumentException("Incorrect number of parameters specified with the -stack option.");
            }

            stackRootDir = tokens[++ii];
            continue;
          }

          if (tokens[ii] == "-exclude")
          {
            if (ii >= tokens.Count - 1)
            {
              throw new ArgumentException("Incorrect number of parameters specified with the -exclude option.");
            }

            excludeCategories = tokens[++ii].Split(',', '+');
            continue;
          }

          if (tokens[ii] == "-useAllowSubtypes")
          {
            useAllowSubtypes = true;
            continue;
          }
        }
      }

      public override void Execute()
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
}