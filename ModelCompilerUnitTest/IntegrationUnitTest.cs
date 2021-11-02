﻿//__________________________________________________________________________________________________
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
  [DeploymentItem(@".\TestingData", "TestingData")]
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
      Assert.IsTrue(File.Exists(Path.Combine(DesignPath, "UA Attributes.csv")));
      Assert.IsTrue(File.Exists(Path.Combine(DesignPath, "UA Status Codes.xml")));
      Assert.IsTrue(File.Exists(Path.Combine(DesignPath, "UA Status Codes.csv")));
      Assert.IsTrue(File.Exists(Path.Combine(SourcePath, "DemoModel.xml")));
      Assert.IsTrue(File.Exists(Path.Combine(SourcePath, "DemoModel.csv")));
    }

    [TestMethod]
    public void StackBuild()
    {
      StackBuildModelCompilerAPI stackBuild = new StackBuildModelCompilerAPI();
      stackBuild.Build();
      Assert.IsTrue(File.Exists(@"nodesetsMaster\DotNet\Opc.Ua.Attributes.cs"));
      Assert.IsTrue(File.Exists(@"nodesetsMaster\DotNet\Opc.Ua.Channels.cs"));
      Assert.IsTrue(File.Exists(@"nodesetsMaster\DotNet\Opc.Ua.Client.cs"));
      Assert.IsTrue(File.Exists(@"nodesetsMaster\DotNet\Opc.Ua.Endpoints.cs"));
      Assert.IsTrue(File.Exists(@"nodesetsMaster\DotNet\Opc.Ua.Endpoints.wsdl"));
      Assert.IsTrue(File.Exists(@"nodesetsMaster\DotNet\Opc.Ua.Interfaces.cs"));
      Assert.IsTrue(File.Exists(@"nodesetsMaster\DotNet\Opc.Ua.Messages.cs"));
      Assert.IsTrue(File.Exists(@"nodesetsMaster\DotNet\Opc.Ua.ServerBase.cs"));
      Assert.IsTrue(File.Exists(@"nodesetsMaster\DotNet\Opc.Ua.Services.wsdl"));
      Assert.IsTrue(File.Exists(@"nodesetsMaster\DotNet\Opc.Ua.StatusCodes.cs"));
      Assert.IsTrue(File.Exists(@"nodesetsMaster\DotNet\Opc.Ua.StatusCodes.csv"));
      //Schema
      Assert.IsTrue(File.Exists(@"nodesetsMaster\Schema\Opc.Ua.Classes.cs"));
      Assert.IsTrue(File.Exists(@"nodesetsMaster\Schema\Opc.Ua.Constants.cs"));
      Assert.IsTrue(File.Exists(@"nodesetsMaster\Schema\Opc.Ua.DataTypes.cs"));
      Assert.IsTrue(File.Exists(@"nodesetsMaster\Schema\Opc.Ua.NodeIds.csv"));
      Assert.IsTrue(File.Exists(@"nodesetsMaster\Schema\Opc.Ua.NodeIds.Services.csv"));
      Assert.IsTrue(File.Exists(@"nodesetsMaster\Schema\Opc.Ua.NodeSet.xml"));
      Assert.IsTrue(File.Exists(@"nodesetsMaster\Schema\Opc.Ua.NodeSet2.Part10.xml"));
      Assert.IsTrue(File.Exists(@"nodesetsMaster\Schema\Opc.Ua.NodeSet2.Part11.xml"));
      Assert.IsTrue(File.Exists(@"nodesetsMaster\Schema\Opc.Ua.NodeSet2.Part12.xml"));
      Assert.IsTrue(File.Exists(@"nodesetsMaster\Schema\Opc.Ua.NodeSet2.Part13.xml"));
      Assert.IsTrue(File.Exists(@"nodesetsMaster\Schema\Opc.Ua.NodeSet2.Part14.xml"));
      Assert.IsTrue(File.Exists(@"nodesetsMaster\Schema\Opc.Ua.NodeSet2.Part17.xml"));
      Assert.IsTrue(File.Exists(@"nodesetsMaster\Schema\Opc.Ua.NodeSet2.Part19.xml"));
      Assert.IsTrue(File.Exists(@"nodesetsMaster\Schema\Opc.Ua.NodeSet2.Part22.xml"));
      Assert.IsTrue(File.Exists(@"nodesetsMaster\Schema\Opc.Ua.NodeSet2.Part3.xml"));
      Assert.IsTrue(File.Exists(@"nodesetsMaster\Schema\Opc.Ua.NodeSet2.Part4.xml"));
      Assert.IsTrue(File.Exists(@"nodesetsMaster\Schema\Opc.Ua.NodeSet2.Part5.xml"));
      Assert.IsTrue(File.Exists(@"nodesetsMaster\Schema\Opc.Ua.NodeSet2.Part8.xml"));
      Assert.IsTrue(File.Exists(@"nodesetsMaster\Schema\Opc.Ua.NodeSet2.Part9.xml"));
      Assert.IsTrue(File.Exists(@"nodesetsMaster\Schema\Opc.Ua.NodeSet2.Services.xml"));
      Assert.IsTrue(File.Exists(@"nodesetsMaster\Schema\Opc.Ua.NodeSet2.xml"));
      Assert.IsTrue(File.Exists(@"nodesetsMaster\Schema\Opc.Ua.PredefinedNodes.uanodes"));
      Assert.IsTrue(File.Exists(@"nodesetsMaster\Schema\Opc.Ua.PredefinedNodes.xml"));
      Assert.IsTrue(File.Exists(@"nodesetsMaster\Schema\Opc.Ua.Types.bsd"));
      Assert.IsTrue(File.Exists(@"nodesetsMaster\Schema\Opc.Ua.Types.xsd"));
      //Assert.Fail();  //Uncomment to preserve the generated code
    }

    [TestMethod]
    public void BuildingModelDemoTest()
    {
      BuildingModelDemoAPI stackBuild = new BuildingModelDemoAPI();
      stackBuild.Build();
      Assert.IsTrue(File.Exists(Path.Combine(DemoModelDir, "DemoModel.Classes.cs")));
      Assert.IsTrue(File.Exists(Path.Combine(DemoModelDir, "DemoModel.Constants.cs")));
      Assert.IsTrue(File.Exists(Path.Combine(DemoModelDir, "DemoModel.DataTypes.cs")));
      Assert.IsTrue(File.Exists(Path.Combine(DemoModelDir, "DemoModel.NodeIds.csv")));
      Assert.IsTrue(File.Exists(Path.Combine(DemoModelDir, "DemoModel.NodeSet.xml")));
      Assert.IsTrue(File.Exists(Path.Combine(DemoModelDir, "DemoModel.NodeSet2.xml")));
      Assert.IsTrue(File.Exists(Path.Combine(DemoModelDir, "DemoModel.PredefinedNodes.uanodes")));
      Assert.IsTrue(File.Exists(Path.Combine(DemoModelDir, "DemoModel.PredefinedNodes.xml")));
      Assert.IsTrue(File.Exists(Path.Combine(DemoModelDir, "DemoModel.Types.bsd")));
      Assert.IsTrue(File.Exists(Path.Combine(DemoModelDir, "DemoModel.Types.xsd")));
      //Assert.Fail();  //Uncomment to preserve the generated code
    }

    #region private stuff

    private const string DesignPath = @".\TestingData\Design.v104\";
    private const string SourcePath = @".\TestingData\ModelDesign\";
    private const string DemoModelDir = "outputDir";

    private class StackBuildModelCompilerAPI : ModelCompilerAPI
    {
      internal void Build()
      {
        Execute();
      }

      public StackBuildModelCompilerAPI()
      {
        DirectoryInfo stack = Directory.CreateDirectory("nodesetsMaster");
        ansicRootDir = Path.Combine(stack.FullName, "AnsiC");
        Directory.CreateDirectory(ansicRootDir);
        DesignFiles.Add(Path.Combine(DesignPath, "StandardTypes.xml"));
        DesignFiles.Add(Path.Combine(DesignPath, "UA Core Services.xml"));
        ExcludeCategories = null;
        GenerateMultiFile = true;
        IdentifierFile = Path.Combine(DesignPath, "StandardTypes.csv");
        IncludeDisplayNames = false;
        OutputDir = Path.Combine(stack.FullName, "Schema");
        Directory.CreateDirectory(OutputDir);
        SpecificationVersion = "v104";
        stackRootDir = Path.Combine(stack.FullName, "DotNet");
        Directory.CreateDirectory(stackRootDir);
        StartId = 1;
        UseAllowSubtypes = false;
        UseXmlInitializers = false;
      }
    }

    private class BuildingModelDemoAPI : ModelCompilerAPI
    {
      internal void Build()
      {
        Execute();
      }

      public BuildingModelDemoAPI()
      {
        ansicRootDir = null;
        DesignFiles.Add(Path.Combine(SourcePath, "DemoModel.xml"));
        ExcludeCategories = null;
        GenerateMultiFile = true;
        IdentifierFile = Path.Combine(SourcePath, "DemoModel.csv");
        IncludeDisplayNames = false;
        OutputDir = DemoModelDir;
        Directory.CreateDirectory(OutputDir);
        SpecificationVersion = "v104";
        stackRootDir = null;
        StartId = 1;
        UseAllowSubtypes = false;
        UseXmlInitializers = false;
      }
    }

    #endregion private stuff
  }
}