//__________________________________________________________________________________________________
//
//  Copyright (C) 2021, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GitHub: https://github.com/mpostol/OPC-UA-OOI/discussions
//__________________________________________________________________________________________________

using OOI.ModelCompiler;
using System;
using System.Collections.Generic;
using System.IO;

namespace OOI.ModelCompilerUI
{
  internal class ModelCompilerAPIInternal : ModelCompilerAPI
  {
    internal void ProcessCommandLine(List<string> tokens)
    {
      for (int ii = 1; ii < tokens.Count; ii++)
      {
        if (tokens[ii] == "-input")
        {
          if (ii >= tokens.Count - 1)
            throw new ArgumentException("Incorrect number of parameters specified with the -input option.");
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
            throw new ArgumentException("Incorrect number of parameters specified with the -license option.");
          updateHeaders = true;
          licenseType = (LicenseType)Enum.Parse(typeof(LicenseType), tokens[++ii]);
          continue;
        }
        if (tokens[ii] == "-d2")
        {
          if (ii >= tokens.Count - 1)
            throw new ArgumentException("Incorrect number of parameters specified with the -d2 option.");
          DesignFiles.Add(tokens[++ii]);
          continue;
        }
        if (tokens[ii] == "-d2")
        {
          if (ii >= tokens.Count - 1)
            throw new ArgumentException("Incorrect number of parameters specified with the -d2 option.");
          DesignFiles.Add(tokens[++ii]);
          continue;
        }
        if (tokens[ii] == "-d2")
        {
          if (ii >= tokens.Count - 1)
            throw new ArgumentException("Incorrect number of parameters specified with the -d2 option.");
          DesignFiles.Add(tokens[++ii]);
          continue;
        }
        if (tokens[ii] == "-c" || tokens[ii] == "-cg")
        {
          if (ii >= tokens.Count - 1)
          {
            throw new ArgumentException("Incorrect number of parameters specified with the -c or -cg option.");
          }
          generateIds = tokens[ii] == "-cg";
          IdentifierFile = tokens[++ii];
          continue;
        }
        if (tokens[ii] == "-o")
        {
          if (ii >= tokens.Count - 1)
            throw new ArgumentException("Incorrect number of parameters specified with the -o option.");
          OutputDir = tokens[++ii];
          continue;
        }
        if (tokens[ii] == "-o2")
        {
          if (ii >= tokens.Count - 1)
            throw new ArgumentException("Incorrect number of parameters specified with the -o option.");
          OutputDir = tokens[++ii];
          GenerateMultiFile = true;
          continue;
        }
        if (tokens[ii] == "-id")
        {
          StartId = Convert.ToUInt32(tokens[++ii]);
          continue;
        }
        if (tokens[ii] == "-version")
        {
          if (ii >= tokens.Count - 1)
            throw new ArgumentException("Incorrect number of parameters specified with the -version option.");
          SpecificationVersion = tokens[++ii];
          continue;
        }
        if (tokens[ii] == "-ansic")
        {
          if (ii >= tokens.Count - 1)
            throw new ArgumentException("Incorrect number of parameters specified with the -ansic option.");
          ansicRootDir = tokens[++ii];
          continue;
        }
        if (tokens[ii] == "-useXmlInitializers")
        {
          UseXmlInitializers = true;
          continue;
        }
        if (tokens[ii] == "-includeDisplayNames")
        {
          IncludeDisplayNames = true;
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
            throw new ArgumentException("Incorrect number of parameters specified with the -exclude option.");
          ExcludeCategories = tokens[++ii].Split(',', '+');
          continue;
        }
        if (tokens[ii] == "-useAllowSubtypes")
        {
          UseAllowSubtypes = true;
          continue;
        }
      }
    }

    internal void Build()
    {
      Execute();
    }

    private LicenseType licenseType = LicenseType.MITXML;
    private bool updateHeaders = false;
    private string inputDirectory { get; set; } = ".";
    private bool generateIds = false;
    private string filePattern = "*.xml";
    private bool silent = false;

    protected override void Execute()
    {
      if (updateHeaders)
      {
        HeaderUpdateTool.ProcessDirectory(inputDirectory, filePattern, licenseType, silent);
        return;
      }
      for (int ii = 0; ii < DesignFiles.Count; ii++)
      {
        if (string.IsNullOrEmpty(DesignFiles[ii]))
          throw new ArgumentException("No design file specified.");
        if (!File.Exists(DesignFiles[ii]))
          throw new ArgumentException($"The design file does not exist: {DesignFiles[ii]}");
      }
      if (string.IsNullOrEmpty(IdentifierFile))
        throw new ArgumentException("No identifier file specified.");
      if (!File.Exists(IdentifierFile))
      {
        if (!generateIds)
          throw new ArgumentException($"The identifier file does not exist: {IdentifierFile}");
        File.Create(IdentifierFile).Close();
      }
      base.Execute();
    }
  }
}