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
  public class UnitsOptionsParserUnitTest
  {
    [TestMethod]
    public void UnitsTest()
    {
      List<string> commandLine = new List<string>()
      {
        "units",
        "--annex1", "The path to the UNECE Annex 1 CSV file.",
        "--annex2", "The path to the UNECE Annex 2/3 CSV file.",
        "--output", @".\Bin\nodesets\master\DemoModel\"
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
      Assert.IsNull(dotNetStackOptions);
      Assert.IsNotNull(unitsOptions);
      Assert.IsNull(updateHeadersOptions);
      Assert.IsNull(error);
      Assert.AreEqual<string>("The path to the UNECE Annex 1 CSV file.", unitsOptions.Annex1Path);
      Assert.AreEqual<string>("The path to the UNECE Annex 2/3 CSV file.", unitsOptions.Annex2Path);
      Assert.AreEqual<string>(@".\Bin\nodesets\master\DemoModel\", unitsOptions.OutputPath);
    }
  }
}