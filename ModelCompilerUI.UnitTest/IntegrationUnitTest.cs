//__________________________________________________________________________________________________
//
//  Copyright (C) 2022, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GitHub: https://github.com/mpostol/OPC-UA-OOI/discussions
//__________________________________________________________________________________________________

using Microsoft.VisualStudio.TestTools.UnitTesting;
using OOI.ModelCompiler;
using System.Collections.Generic;
using System.IO;

namespace OOI.ModelCompilerUI
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
      //Assert.IsTrue(File.Exists(@"nodesetsMaster\Schema\Opc.Ua.NodeSet2.Part10.xml"));
      //Assert.IsTrue(File.Exists(@"nodesetsMaster\Schema\Opc.Ua.NodeSet2.Part11.xml"));
      //Assert.IsTrue(File.Exists(@"nodesetsMaster\Schema\Opc.Ua.NodeSet2.Part12.xml"));
      //Assert.IsTrue(File.Exists(@"nodesetsMaster\Schema\Opc.Ua.NodeSet2.Part13.xml"));
      //Assert.IsTrue(File.Exists(@"nodesetsMaster\Schema\Opc.Ua.NodeSet2.Part14.xml"));
      //Assert.IsTrue(File.Exists(@"nodesetsMaster\Schema\Opc.Ua.NodeSet2.Part17.xml"));
      //Assert.IsTrue(File.Exists(@"nodesetsMaster\Schema\Opc.Ua.NodeSet2.Part19.xml"));
      //Assert.IsTrue(File.Exists(@"nodesetsMaster\Schema\Opc.Ua.NodeSet2.Part22.xml"));
      //Assert.IsTrue(File.Exists(@"nodesetsMaster\Schema\Opc.Ua.NodeSet2.Part3.xml"));
      //Assert.IsTrue(File.Exists(@"nodesetsMaster\Schema\Opc.Ua.NodeSet2.Part4.xml"));
      //Assert.IsTrue(File.Exists(@"nodesetsMaster\Schema\Opc.Ua.NodeSet2.Part5.xml"));
      //Assert.IsTrue(File.Exists(@"nodesetsMaster\Schema\Opc.Ua.NodeSet2.Part8.xml"));
      //Assert.IsTrue(File.Exists(@"nodesetsMaster\Schema\Opc.Ua.NodeSet2.Part9.xml"));
      Assert.IsTrue(File.Exists(@"nodesetsMaster\Schema\Opc.Ua.NodeSet2.Services.xml"));
      Assert.IsTrue(File.Exists(@"nodesetsMaster\Schema\Opc.Ua.NodeSet2.xml"));
      Assert.IsTrue(File.Exists(@"nodesetsMaster\Schema\Opc.Ua.PredefinedNodes.uanodes"));
      Assert.IsTrue(File.Exists(@"nodesetsMaster\Schema\Opc.Ua.PredefinedNodes.xml"));
      Assert.IsTrue(File.Exists(@"nodesetsMaster\Schema\Opc.Ua.Types.bsd"));
      Assert.IsTrue(File.Exists(@"nodesetsMaster\Schema\Opc.Ua.Types.xsd"));
      //Assert.Fail();  //Uncomment to preserve the generated code
    }

    private const string DesignPath = @".\TestingData\Design.v104\";

    private class StackBuildModelCompilerAPI : ModelCompilerAPI, IModelGeneratorGenerate, IModelGeneratorValidate
    {
      #region IModelGeneratorGenerate

      public bool GenerateMultiFile { get; protected set; } = false;
      public bool UseXmlInitializers { get; protected set; } = false;
      public IList<string> ExcludeCategories { get; protected set; } = null;
      public bool IncludeDisplayNames { get; protected set; } = false;

      #endregion IModelGeneratorGenerate

      #region IStackGeneratorGenerate

      public List<string> DesignFiles { get; protected set; } = new List<string>();
      public string IdentifierFile { get; protected set; } = null;
      public string SpecificationVersion { get; protected set; } = string.Empty;

      #endregion IStackGeneratorGenerate

      #region IModelGeneratorValidate

      public uint StartId { get; protected set; } = 1;
      public bool UseAllowSubtypes { get; protected set; } = false;

      public string PublicationDate { get; protected set; } = string.Empty;

      public bool ReleaseCandidate => false;

      #endregion IModelGeneratorValidate

      internal void Build()
      {
        Execute(this, this);
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
  }
}