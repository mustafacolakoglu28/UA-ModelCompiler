//__________________________________________________________________________________________________
//
//  Copyright (C) 2021, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GitHub: https://github.com/mpostol/OPC-UA-OOI/discussions
//__________________________________________________________________________________________________

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace OOI.ModelCompilerUI
{
  [TestClass]
  public class ModelCompilerAPIInternalUnitTest
  {
    [TestMethod]
    [ExpectedException(typeof(NullReferenceException))]
    public void ConstructorTest()
    {
      ModelCompilerAPIInternal instance = new ModelCompilerAPIInternal();
      instance.ProcessCommandLine(null);
    }

    [TestMethod]
    public void StandardTypesTest()
    {
      ModelCompilerAPIInternal instance = new ModelCompilerAPIInternal();
      List<string> commandLine = new List<string>()
      {
        "exeName",
        "-d2", @".\Opc.Ua.ModelCompiler\Design.v104\StandardTypes.xml",
        "-version",  "v104",
        "-d2",  @".\Opc.Ua.ModelCompiler\Design.v104\UA Core Services.xml",
        "-c", @".\Opc.Ua.ModelCompiler\CSVs\StandardTypes.csv",
        "-o2",  @".\Bin\nodesets\master\Schema\",
        "-stack",  @".\Bin\nodesets\master\DotNet\",
        "-ansic", @".\Bin\nodesets\master\AnsiC\"
      };
      instance.ProcessCommandLine(commandLine);
      //IModelGeneratorValidate
      Assert.AreEqual<int>(2, instance.DesignFiles.Count);
      Assert.AreEqual<string>(@".\Opc.Ua.ModelCompiler\Design.v104\StandardTypes.xml", instance.DesignFiles[0]);
      Assert.AreEqual<string>(@".\Opc.Ua.ModelCompiler\Design.v104\UA Core Services.xml", instance.DesignFiles[1]);
      Assert.AreEqual<string>(@".\Opc.Ua.ModelCompiler\CSVs\StandardTypes.csv", instance.IdentifierFile);
      Assert.AreEqual<string>("v104", instance.SpecificationVersion);
      Assert.AreEqual<uint>(1, instance.StartId);
      Assert.IsFalse(instance.UseAllowSubtypes);
      //IModelGeneratorGenerate
      Assert.IsNull(instance.ExcludeCategories);
      Assert.IsTrue(instance.GenerateMultiFile);
      Assert.IsFalse(instance.IncludeDisplayNames);
      Assert.IsFalse(instance.UseXmlInitializers);
      //Dir
      Assert.AreEqual<string>(@".\Bin\nodesets\master\AnsiC\", instance.ansicRootDir);
      Assert.AreEqual<string>(@".\Bin\nodesets\master\Schema\", instance.OutputDir);
      Assert.AreEqual<string>(@".\Bin\nodesets\master\DotNet\", instance.stackRootDir);
    }

    [TestMethod]
    public void BuildingModelDemoTest()
    {
      ModelCompilerAPIInternal instance = new ModelCompilerAPIInternal();
      List<string> commandLine = new List<string>()
      {
        "exeName",
        "-version", "v104",
        "-d2", @".\Opc.Ua.ModelCompiler\Design.v104\DemoModel.xml",
        "-cg", @".\ModelCompiler\CSVs\DemoModel.csv",
        "-o2", @".\Bin\nodesets\master\DemoModel\"
      };
      instance.ProcessCommandLine(commandLine);
      //IModelGeneratorValidate
      Assert.AreEqual<int>(1, instance.DesignFiles.Count);
      Assert.AreEqual<string>(@".\Opc.Ua.ModelCompiler\Design.v104\DemoModel.xml", instance.DesignFiles[0]);
      Assert.AreEqual<string>(@".\ModelCompiler\CSVs\DemoModel.csv", instance.IdentifierFile);
      Assert.AreEqual<string>("v104", instance.SpecificationVersion);
      Assert.AreEqual<uint>(1, instance.StartId);
      Assert.IsFalse(instance.UseAllowSubtypes);
      //IModelGeneratorGenerate
      Assert.IsNull(instance.ExcludeCategories);
      Assert.IsTrue(instance.GenerateMultiFile);
      Assert.IsFalse(instance.IncludeDisplayNames);
      Assert.IsFalse(instance.UseXmlInitializers);
      //Dir
      Assert.IsNull(instance.ansicRootDir);
      Assert.AreEqual<string>(@".\Bin\nodesets\master\DemoModel\", instance.OutputDir);
      Assert.IsNull(instance.stackRootDir);
    }

    [TestMethod]
    public void UpdatingLicenseTest()
    {
      ModelCompilerAPIInternal instance = new ModelCompilerAPIInternal();
      List<string> commandLine = new List<string>()
      {
        "-input", @".\Bin\nodesets\master",
        "-pattern", "*.xml",
        "-license", "MITXML",
        "-silent"
      };
      instance.ProcessCommandLine(commandLine);
      Assert.IsNull(instance.ansicRootDir);
      Assert.IsNull(instance.OutputDir);
      Assert.IsNull(instance.stackRootDir);
    }
  }
}