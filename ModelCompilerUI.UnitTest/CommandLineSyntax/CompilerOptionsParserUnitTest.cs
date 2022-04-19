//__________________________________________________________________________________________________
//
//  Copyright (C) 2022, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GitHub: https://github.com/mpostol/OPC-UA-OOI/discussions
//__________________________________________________________________________________________________

using CommandLine;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace OOI.ModelCompilerUI.CommandLineSyntax
{
  [TestClass]
  public class CompilerOptionsParserUnitTest
  {
    [TestMethod]
    public void CompileVerbTest()
    {
      List<string> commandLine = new List<string>()
      {
        "compile",
        "--d2", @".\Opc.Ua.ModelCompiler\Design.v104\StandardTypes.xml", @".\Opc.Ua.ModelCompiler\Design.v104\UA Core Services.xml",
        "--spec",  "v104",
        "-c", @".\Opc.Ua.ModelCompiler\CSVs\StandardTypes.csv",
        "--o2",  @".\Bin\nodesets\master\Schema\" 
      };
      string args = string.Join(",", commandLine.ToArray());
      Assert.IsNotNull(args);
      ParserResult<object> result = Parser.Default.ParseArguments<CompilerOptions, DotNetStackOptions, UnitsOptions, UpdateHeadersOptions>(commandLine);
      CompilerOptions compilerOptions = null;
      DotNetStackOptions dotNetStackOptions = null;
      UnitsOptions unitsOptions = null;
      UpdateHeadersOptions updateHeadersOptions = null;
      IEnumerable<Error> error = null;
      result.WithParsed<CompilerOptions>(options => compilerOptions = options).
             WithParsed<DotNetStackOptions>(options => dotNetStackOptions = options).
             WithParsed<UnitsOptions>(options => unitsOptions = options).
             WithParsed<UpdateHeadersOptions>(options => updateHeadersOptions = options).
             WithNotParsed(errors => error = errors);
      Assert.IsNotNull(compilerOptions);
      Assert.IsNull(dotNetStackOptions);
      Assert.IsNull(unitsOptions);
      Assert.IsNull(updateHeadersOptions);
      Assert.IsNull(error);
      Assert.IsFalse(compilerOptions.CreateIdentifierFile);
      Assert.AreEqual<int>(2, compilerOptions.DesignFiles.Count);
      Assert.AreEqual<string>(@".\Opc.Ua.ModelCompiler\Design.v104\StandardTypes.xml", compilerOptions.DesignFiles[0]);
      Assert.AreEqual<string>(@".\Opc.Ua.ModelCompiler\Design.v104\UA Core Services.xml", compilerOptions.DesignFiles[1]);
      Assert.AreEqual<string>(@".\Opc.Ua.ModelCompiler\CSVs\StandardTypes.csv", compilerOptions.IdentifierFile);
      Assert.AreEqual<string>(@".\Bin\nodesets\master\Schema\", compilerOptions.OutputPath);
      Assert.IsNotNull(compilerOptions.Exclusions);
      Assert.AreEqual<int>(0, compilerOptions.Exclusions.Count);
      Assert.IsTrue(string.IsNullOrEmpty(compilerOptions.ModelVersion));
      Assert.IsFalse(compilerOptions.ReleaseCandidate);
      Assert.IsTrue(string.IsNullOrEmpty(compilerOptions.ModelPublicationDate));
      Assert.AreEqual<string>("v104", compilerOptions.Version);
    }

    [TestMethod]
    public void DemoModelCompileTest()
    {
      List<string> commandLine = new List<string>()
      {
        "compile",
        "--spec", "v104",
        "--d2", @".\Opc.Ua.ModelCompiler\Design.v104\DemoModel.xml",
        "-c", @".\ModelCompiler\CSVs\DemoModel.csv",
        "--cg",
        "--o2", @".\Bin\nodesets\master\DemoModel\"
      };
      string args = string.Join(",", commandLine.ToArray());
      Assert.IsNotNull(args);
      ParserResult<object> result = Parser.Default.ParseArguments<CompilerOptions, DotNetStackOptions, UnitsOptions, UpdateHeadersOptions>(commandLine);
      CompilerOptions compilerOptions = null;
      DotNetStackOptions dotNetStackOptions = null;
      UnitsOptions unitsOptions = null;
      UpdateHeadersOptions updateHeadersOptions = null;
      IEnumerable<Error> error = null;
      result.WithParsed<CompilerOptions>(options => compilerOptions = options).
             WithParsed<DotNetStackOptions>(options => dotNetStackOptions = options).
             WithParsed<UnitsOptions>(options => unitsOptions = options).
             WithParsed<UpdateHeadersOptions>(options => updateHeadersOptions = options).
             WithNotParsed(errors => error = errors);
      Assert.IsNotNull(compilerOptions);
      Assert.IsNull(dotNetStackOptions);
      Assert.IsNull(unitsOptions);
      Assert.IsNull(updateHeadersOptions);
      Assert.IsNull(error);
      Assert.IsTrue(compilerOptions.CreateIdentifierFile);
      Assert.AreEqual<int>(1, compilerOptions.DesignFiles.Count);
      Assert.AreEqual<string>(@".\Opc.Ua.ModelCompiler\Design.v104\DemoModel.xml", compilerOptions.DesignFiles[0]);
      Assert.AreEqual<string>(@".\ModelCompiler\CSVs\DemoModel.csv", compilerOptions.IdentifierFile);
      Assert.AreEqual<string>(@".\Bin\nodesets\master\DemoModel\", compilerOptions.OutputPath);
      Assert.IsNotNull(compilerOptions.Exclusions);
      Assert.AreEqual<int>(0, compilerOptions.Exclusions.Count);
      Assert.IsTrue(string.IsNullOrEmpty(compilerOptions.ModelVersion));
      Assert.IsFalse(compilerOptions.ReleaseCandidate);
      Assert.IsTrue(string.IsNullOrEmpty(compilerOptions.ModelPublicationDate));
      Assert.AreEqual<string>("v104", compilerOptions.Version);
    }
  }
}