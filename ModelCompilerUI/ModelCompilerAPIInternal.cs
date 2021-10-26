//__________________________________________________________________________________________________
//
//  Copyright (C) 2021, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GitHub: https://github.com/mpostol/OPC-UA-OOI/discussions
//__________________________________________________________________________________________________

using OOI.ModelCompiler;
using System;
using System.Collections.Generic;

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
          designFiles.Add(tokens[++ii]);
          continue;
        }
        if (tokens[ii] == "-d2")
        {
          if (ii >= tokens.Count - 1)
            throw new ArgumentException("Incorrect number of parameters specified with the -d2 option.");
          designFiles.Add(tokens[++ii]);
          continue;
        }
        if (tokens[ii] == "-d2")
        {
          if (ii >= tokens.Count - 1)
            throw new ArgumentException("Incorrect number of parameters specified with the -d2 option.");
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
            throw new ArgumentException("Incorrect number of parameters specified with the -o option.");
          OutputDir = tokens[++ii];
          continue;
        }
        if (tokens[ii] == "-o2")
        {
          if (ii >= tokens.Count - 1)
            throw new ArgumentException("Incorrect number of parameters specified with the -o option.");
          OutputDir = tokens[++ii];
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
            throw new ArgumentException("Incorrect number of parameters specified with the -version option.");
          specificationVersion = tokens[++ii];
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
          useAllowSubtypes = true;
          continue;
        }
      }
    }

    internal void Build()
    {
      Execute();
    }

    internal LicenseType licenseType = LicenseType.MITXML;
    internal bool updateHeaders = false;
    private string inputDirectory { get; set; } = ".";


    protected override void Execute()
    {
      if (updateHeaders)
      {
        HeaderUpdateTool.ProcessDirectory(inputDirectory, filePattern, licenseType, silent);
        return;
      }
      base.Execute();
    }
  }
}