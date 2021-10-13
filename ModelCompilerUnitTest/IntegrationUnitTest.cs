//__________________________________________________________________________________________________
//
//  Copyright (C) 2021, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GitHub: https://github.com/mpostol/OPC-UA-OOI/discussions
//__________________________________________________________________________________________________

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace OOI.ModelCompiler
{
  [TestClass]
  public class IntegrationUnitTest
  {
    [TestMethod]
    public void StackBuild()
    {
      StackBuildModelCompilerAPI stackBuild = new StackBuildModelCompilerAPI();
      stackBuild.Execute();
    }

    private class StackBuildModelCompilerAPI : ModelCompilerAPI
    {
      public StackBuildModelCompilerAPI()
      {
        DirectoryInfo stack = Directory.CreateDirectory("Ttack");
        ansicRootDir = Path.Combine(stack.FullName, "AnsiC");
        Directory.CreateDirectory(ansicRootDir);
        designFiles.Add(@".\Opc.Ua.ModelCompiler\Design.v104\StandardTypes.xml");
        designFiles.Add(@".\Opc.Ua.ModelCompiler\Design.v104\UA Core Services.xml");
        excludeCategories = null;
        filePattern = "*.xml";
        generateIds = false;
        generateMultiFile = true;
        identifierFile = @".\Opc.Ua.ModelCompiler\CSVs\StandardTypes.csv";
        includeDisplayNames = false;
        inputDirectory = @".";
        licenseType = LicenseType.MITXML;
        outputDir = Path.Combine(stack.FullName, "Schema");
        Directory.CreateDirectory(outputDir);
        silent = false;
        specificationVersion = "v104";
        stackRootDir = Path.Combine(stack.FullName, "DotNet");
        Directory.CreateDirectory(stackRootDir);
        startId = 1;
        updateHeaders = false;
        useAllowSubtypes = false;
        useXmlInitializers = false;
      }
    }
  }
}