//__________________________________________________________________________________________________
//
//  Copyright (C) 2021, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GitHub: https://github.com/mpostol/OPC-UA-OOI/discussions
//__________________________________________________________________________________________________

using Microsoft.VisualStudio.TestTools.UnitTesting;
using OOI.ModelCompiler;
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
      Assert.AreEqual<string>(@".\Bin\nodesets\master\AnsiC\", instance.ansicRootDir);
      Assert.AreEqual<int>(2, instance.designFiles.Count);
      Assert.AreEqual<string>(@".\Opc.Ua.ModelCompiler\Design.v104\StandardTypes.xml", instance.designFiles[0]);
      Assert.AreEqual<string>(@".\Opc.Ua.ModelCompiler\Design.v104\UA Core Services.xml", instance.designFiles[1]);
      Assert.IsNull(instance.excludeCategories);
      Assert.AreEqual<string>("*.xml", instance.filePattern);
      Assert.IsFalse(instance.generateIds);
      Assert.IsTrue(instance.generateMultiFile);
      Assert.AreEqual<string>(@".\Opc.Ua.ModelCompiler\CSVs\StandardTypes.csv", instance.identifierFile);
      Assert.IsFalse(instance.includeDisplayNames);
      Assert.AreEqual<string>(@".", instance.inputDirectory);
      Assert.AreEqual<LicenseType>(LicenseType.MITXML, instance.licenseType);
      Assert.AreEqual<string>(@".\Bin\nodesets\master\Schema\", instance.outputDir);
      Assert.IsFalse(instance.silent);
      Assert.AreEqual<string>("v104", instance.specificationVersion);
      Assert.AreEqual<string>(@".\Bin\nodesets\master\DotNet\", instance.stackRootDir);
      Assert.AreEqual<uint>(1, instance.startId);
      Assert.IsFalse(instance.updateHeaders);
      Assert.IsFalse(instance.useAllowSubtypes);
      Assert.IsFalse(instance.useXmlInitializers);
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
      Assert.IsNull(instance.ansicRootDir);
      Assert.AreEqual<int>(1, instance.designFiles.Count);
      Assert.AreEqual<string>(@".\Opc.Ua.ModelCompiler\Design.v104\DemoModel.xml", instance.designFiles[0]);
      Assert.IsTrue(instance.generateIds);
      Assert.AreEqual<string>(@".\ModelCompiler\CSVs\DemoModel.csv", instance.identifierFile);
      Assert.AreEqual<string>("v104", instance.specificationVersion);
      Assert.AreEqual<string>(null, instance.stackRootDir);
      Assert.IsFalse(instance.updateHeaders);
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
      Assert.AreEqual<string>(null, instance.stackRootDir);
      Assert.IsFalse(instance.generateIds);
      Assert.IsTrue(instance.updateHeaders);
    }
  }
}