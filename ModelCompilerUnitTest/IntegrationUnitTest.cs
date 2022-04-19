//__________________________________________________________________________________________________
//
//  Copyright (C) 2022, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GitHub: https://github.com/mpostol/OPC-UA-OOI/discussions
//__________________________________________________________________________________________________

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
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
      Assert.IsTrue(File.Exists(Path.Combine(SourcePath, "DemoModel.xml")));
      Assert.IsTrue(File.Exists(Path.Combine(SourcePath, "DemoModel.csv")));
    }

    [TestMethod]
    public void BuildingModelDemoTest()
    {
      CompilerOptionsFixture options = new CompilerOptionsFixture();
      ModelDesignCompiler.BuildModel(options);
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
    }

    #region private stuff

    private const string SourcePath = @".\TestingData\ModelDesign\";
    private const string DemoModelDir = "outputDir";

    private class CompilerOptionsFixture : ICompilerOptions
    {
      public CompilerOptionsFixture()
      {
        DesignFiles.Add(Path.Combine(SourcePath, "DemoModel.xml"));
        IdentifierFile = Path.Combine(SourcePath, "DemoModel.csv");
        OutputPath = DemoModelDir;
        Directory.CreateDirectory(OutputPath);
      }

      public string OutputPath { get; }

      public IList<string> DesignFiles { get; } = new List<string>();

      public string IdentifierFile { get; }

      public string Version => "v104";

      public uint StartId => 1;

      public bool UseAllowSubtypes => false;

      public IList<string> Exclusions => null;

      public string ModelPublicationDate => null;

      public bool ReleaseCandidate => false;

      public string ModelVersion => null;
    }

    #endregion private stuff
  }
}