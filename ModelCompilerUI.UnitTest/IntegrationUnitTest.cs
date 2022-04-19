//__________________________________________________________________________________________________
//
//  Copyright (C) 2022, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GitHub: https://github.com/mpostol/OPC-UA-OOI/discussions
//__________________________________________________________________________________________________

using Microsoft.VisualStudio.TestTools.UnitTesting;
using OOI.ModelCompilerUI.CommandLineSyntax;
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
      DotNetStackOptions options = StacksOptions();
      EntryPoint.GenerateStackDebugCall(options);
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
      Assert.IsTrue(File.Exists(@"nodesetsMaster\Schema\Opc.Ua.NodeSet2.Services.xml"));
      Assert.IsTrue(File.Exists(@"nodesetsMaster\Schema\Opc.Ua.NodeSet2.xml"));
      Assert.IsTrue(File.Exists(@"nodesetsMaster\Schema\Opc.Ua.PredefinedNodes.uanodes"));
      Assert.IsTrue(File.Exists(@"nodesetsMaster\Schema\Opc.Ua.PredefinedNodes.xml"));
      Assert.IsTrue(File.Exists(@"nodesetsMaster\Schema\Opc.Ua.Types.bsd"));
      Assert.IsTrue(File.Exists(@"nodesetsMaster\Schema\Opc.Ua.Types.xsd"));
      //Assert.Fail();  //Uncomment to preserve the generated code
    }

    private const string DesignPath = @".\TestingData\Design.v104\";

    private static DotNetStackOptions StacksOptions()
    {
      DirectoryInfo stack = Directory.CreateDirectory("nodesetsMaster");
      string ansicRootDir = Path.Combine(stack.FullName, "AnsiC");
      Directory.CreateDirectory(ansicRootDir);
      List<string> inputFiles = new List<string>();
      inputFiles.Add(Path.Combine(DesignPath, "StandardTypes.xml"));
      inputFiles.Add(Path.Combine(DesignPath, "UA Core Services.xml"));
      string OutputDir = Path.Combine(stack.FullName, "Schema");
      Directory.CreateDirectory(OutputDir);
      string stackRootDir = Path.Combine(stack.FullName, "DotNet");
      Directory.CreateDirectory(stackRootDir);
      return new DotNetStackOptions()
      {
        AnsiCStackPath = ansicRootDir,
        Exclusions = null,
        DesignFiles = inputFiles,
        IdentifierFile = Path.Combine(DesignPath, "StandardTypes.csv"),
        DotNetStackPath = stackRootDir,
        Version = "v104",
        OutputPath = OutputDir
      };
    }
  }
}