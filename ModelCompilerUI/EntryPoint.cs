//__________________________________________________________________________________________________
//
//  Copyright (C) 2022, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GitHub: https://github.com/mpostol/OPC-UA-OOI/discussions
//__________________________________________________________________________________________________

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using UAOOI.SemanticData.BuildingErrorsHandling;
using static OOI.ModelCompilerUI.Diagnostic.AssemblyTraceSource;

namespace OOI.ModelCompilerUI
{
  internal class EntryPoint
  {
    [STAThread]
    private static void Main(string[] args)
    {
      AssemblyName myAssembly = Assembly.GetExecutingAssembly().GetName();
      string AssemblyHeader = $"Starting ModelDesign Compiler (mdc.exe) Version {myAssembly.Version}";
      TraceMessage message = TraceMessage.BuildErrorTraceMessage(BuildError.DiagnosticInformation, AssemblyHeader);
      Log.WriteTraceMessage(message, 716624168);
      Log.WriteTraceMessage(TraceMessage.DiagnosticTraceMessage(Copyright), 716624169);
      string commandLine = Environment.CommandLine;
      int exitCode = Main(commandLine);
      Environment.Exit(exitCode);
    }

    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    private static int Main(string commandLine)
    {
      try
      {
        List<string> tokens = GetTokens(commandLine);
        ModelCompilerAPIInternal mc = new ModelCompilerAPIInternal();
        mc.ProcessCommandLine(tokens);
        mc.Build();
      }
      catch (Exception e)
      {
        Log.WriteTraceMessage(TraceMessage.BuildErrorTraceMessage(BuildError.NonCategorized, e.Message), 823547120);
        Log.WriteTraceMessage(TraceMessage.BuildErrorTraceMessage(BuildError.NonCategorized, $"Stack Trace: {Environment.NewLine}{e.StackTrace}"), 823547121);
      }
      return 0;
    }

    /// <summary>
    /// Extracts the tokens from the command line.
    /// </summary>
    private static List<string> GetTokens(string commandLine)
    {
      List<string> tokens = new List<string>();
      bool quotedToken = false;
      StringBuilder token = new StringBuilder();
      for (int ii = 0; ii < commandLine.Length; ii++)
      {
        char ch = commandLine[ii];
        if (quotedToken)
        {
          if (ch == '"')
          {
            if (token.Length > 0)
            {
              tokens.Add(token.ToString());
              token = new StringBuilder();
            }
            quotedToken = false;
            continue;
          }
          token.Append(ch);
        }
        else
        {
          if (token.Length == 0)
          {
            if (ch == '"')
            {
              quotedToken = true;
              continue;
            }
          }
          if (char.IsWhiteSpace(ch))
          {
            if (token.Length > 0)
            {
              tokens.Add(token.ToString());
              token = new StringBuilder();
            }
            continue;
          }
          token.Append(ch);
        }
      }
      if (token.Length > 0)
        tokens.Add(token.ToString());
      return tokens;
    }

    private const string Copyright = "Copyright(c) 2022 Mariusz Postol";
  }
}