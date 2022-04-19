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
  public class UpdateHeadersOptionsParserUnitTest
  {
    [TestMethod]
    public void UpdateHeadersOptionsTest()
    {
      List<string> commandLine = new List<string>()
      {
        "update-headers",
        "--input", @".\Bin\nodesets\master",
        "--pattern", "*.xml",
        "--license", "MITXML",
        "--silent"
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
      Assert.IsNull(unitsOptions);
      Assert.IsNotNull(updateHeadersOptions);
      Assert.IsNull(error);
      Assert.AreEqual<string>("*.xml", updateHeadersOptions.FilePattern);
      Assert.AreEqual<string>(@".\Bin\nodesets\master", updateHeadersOptions.InputPath);
      Assert.AreEqual<LicenseType>(LicenseType.MITXML, updateHeadersOptions.LicenseType);
      Assert.IsTrue(updateHeadersOptions.Silent);
    }
  }
}