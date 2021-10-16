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
  [DeploymentItem(@".\Design.v104", "Design.v104")]
  public class IntegrationUnitTest
  {
    [ClassInitialize()]
    public static void DeploymentTest(TestContext testContext)
    {
      Assert.IsTrue(File.Exists(Path.Combine(DesignPath, "StandardTypes.xml")));
      Assert.IsTrue(File.Exists(Path.Combine(DesignPath, "StandardTypes.csv")));
      Assert.IsTrue(File.Exists(Path.Combine(DesignPath, "UA Core Services.xml")));
      //Hard coded files names that must be in the DesignPath folder to pass
      Assert.IsTrue(File.Exists(Path.Combine(DesignPath, "UA Attributes.xml")));
      Assert.IsTrue(File.Exists(Path.Combine(DesignPath, "UA Status Codes.xml")));
    }

    private const string DesignPath = @".\Design.v104\";

    [TestMethod]
    public void StackBuild()
    {
      StackBuildModelCompilerAPI stackBuild = new StackBuildModelCompilerAPI();
      stackBuild.Execute();
      Assert.Fail();  //Uncomment to preserve the generated code
    }

    private class StackBuildModelCompilerAPI : ModelCompilerAPI
    {
      public StackBuildModelCompilerAPI()
      {
        DirectoryInfo stack = Directory.CreateDirectory("Stack");
        ansicRootDir = Path.Combine(stack.FullName, "AnsiC");
        Directory.CreateDirectory(ansicRootDir);
        designFiles.Add(@".\Design.v104\StandardTypes.xml");
        designFiles.Add(@".\Design.v104\UA Core Services.xml");
        excludeCategories = null;
        filePattern = "*.xml";
        generateIds = false;
        generateMultiFile = true;
        identifierFile = @".\Design.v104\StandardTypes.csv";
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