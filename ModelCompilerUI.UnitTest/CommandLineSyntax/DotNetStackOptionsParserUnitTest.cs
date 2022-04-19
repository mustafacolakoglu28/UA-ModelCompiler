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
  public class DotNetStackOptionsParserUnitTest
  {
    [TestMethod]
    public void DotNetStackOptionsTest()
    {
      List<string> commandLine = new List<string>()
      {
        "stack",
        "--d2", @".\Opc.Ua.ModelCompiler\Design.v104\StandardTypes.xml", @".\Opc.Ua.ModelCompiler\Design.v104\UA Core Services.xml",
        "--spec",  "v104",
        "-c", @".\Opc.Ua.ModelCompiler\CSVs\StandardTypes.csv",
        "--o2",  @".\Bin\nodesets\master\Schema\",
        "--dotnet", @".\Bin\nodesets\master\DotNet\",
        "--ansic", @".\Bin\nodesets\master\AnsiC\"
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
      Assert.IsNull(compilerOptions);
      Assert.IsNotNull(dotNetStackOptions);
      Assert.IsNull(unitsOptions);
      Assert.IsNull(updateHeadersOptions);
      Assert.IsNull(error);
      Assert.AreEqual<int>(2, dotNetStackOptions.DesignFiles.Count);
      Assert.AreEqual<string>(@".\Opc.Ua.ModelCompiler\Design.v104\StandardTypes.xml", dotNetStackOptions.DesignFiles[0]);
      Assert.AreEqual<string>(@".\Opc.Ua.ModelCompiler\Design.v104\UA Core Services.xml", dotNetStackOptions.DesignFiles[1]);
      Assert.AreEqual<string>(@".\Opc.Ua.ModelCompiler\CSVs\StandardTypes.csv", dotNetStackOptions.IdentifierFile);
      Assert.AreEqual<string>(@".\Bin\nodesets\master\Schema\", dotNetStackOptions.OutputPath);
      Assert.IsNotNull(dotNetStackOptions.Exclusions);
      Assert.AreEqual<int>(0, dotNetStackOptions.Exclusions.Count);
      Assert.AreEqual<string>("v104", dotNetStackOptions.Version);
      Assert.AreEqual<string>(@".\Bin\nodesets\master\DotNet\", dotNetStackOptions.DotNetStackPath);
      Assert.AreEqual<string>(@".\Bin\nodesets\master\AnsiC\", dotNetStackOptions.AnsiCStackPath);
    }
  }
}