//__________________________________________________________________________________________________
//
//  Copyright (C) 2022, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GitHub: https://github.com/mpostol/OPC-UA-OOI/discussions
//__________________________________________________________________________________________________

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace OOI.ModelCompilerUI
{
  [TestClass]
  public class CommandLineSyntaxUnitTest
  {
    [TestMethod]
    public void TestMethod1()
    {
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
      string args = string.Join(",", commandLine.ToArray<string>());
      Assert.IsNotNull(args);
      //TODO Integrate with the UA-ModelCompiler #648
    }
  }
}