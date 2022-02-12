//__________________________________________________________________________________________________
//
//  Copyright (C) 2021, Mariusz Postol LODZ POLAND.
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
            //Assert.IsTrue(File.Exists(Path.Combine(DesignPath, "StandardTypes.xml")));
            //Assert.IsTrue(File.Exists(Path.Combine(DesignPath, "StandardTypes.csv")));
            //Assert.IsTrue(File.Exists(Path.Combine(DesignPath, "UA Core Services.xml")));
            //Hard coded files names that must be in the DesignPath folder to pass
            //Assert.IsTrue(File.Exists(Path.Combine(DesignPath, "UA Attributes.xml")));
            //Assert.IsTrue(File.Exists(Path.Combine(DesignPath, "UA Attributes.csv")));
            //Assert.IsTrue(File.Exists(Path.Combine(DesignPath, "UA Status Codes.xml")));
            //Assert.IsTrue(File.Exists(Path.Combine(DesignPath, "UA Status Codes.csv")));
            Assert.IsTrue(File.Exists(Path.Combine(SourcePath, "DemoModel.xml")));
            Assert.IsTrue(File.Exists(Path.Combine(SourcePath, "DemoModel.csv")));
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

        private const string SourcePath = @".\TestingData\ModelDesign\";
        private const string DemoModelDir = "outputDir";

        private class BuildingModelDemoAPI : IModelGeneratorGenerate, IModelGeneratorValidate
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

            #endregion IModelGeneratorValidate

            internal void Build()
            {
                ModelCompiler.BuildModel(OutputDir, this, this);
            }

            public BuildingModelDemoAPI()
            {
                DesignFiles.Add(Path.Combine(SourcePath, "DemoModel.xml"));
                ExcludeCategories = null;
                GenerateMultiFile = true;
                IdentifierFile = Path.Combine(SourcePath, "DemoModel.csv");
                IncludeDisplayNames = false;
                OutputDir = DemoModelDir;
                Directory.CreateDirectory(OutputDir);
                SpecificationVersion = "v104";
                StartId = 1;
                UseAllowSubtypes = false;
                UseXmlInitializers = false;
            }

            private readonly string OutputDir = null;
        }

        #endregion private stuff
    }
}