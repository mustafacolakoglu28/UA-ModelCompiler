//__________________________________________________________________________________________________
//
//  Copyright (C) 2022, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GitHub: https://github.com/mpostol/OPC-UA-OOI/discussions
//__________________________________________________________________________________________________

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using UAOOI.SemanticData.BuildingErrorsHandling;
using static OOI.ModelCompilerUI.Diagnostic.AssemblyTraceSource;

namespace OOI.ModelCompilerUI
{
  internal class EntryPoint
  {
    private const string Copyright = "Copyright(c) 2022 Mariusz Postol";

    [STAThread]
    private static void Main(string[] args)
    {
      AssemblyName myAssembly = Assembly.GetExecutingAssembly().GetName();
      string AssemblyHeader = $"Starting UA-Compiler Version {myAssembly.Version}";
      TraceMessage message = TraceMessage.BuildErrorTraceMessage(BuildError.DiagnosticInformation, AssemblyHeader);
      Log.WriteTraceMessage(message, 716624168);
      Log.WriteTraceMessage(TraceMessage.DiagnosticTraceMessage(Copyright), 716624169);

      string commandLine = Environment.CommandLine;
      //bool noGui = commandLine.Contains(consoleOutputCommandLineArgument);
      //if (!noGui)
      //{
      //  Application.EnableVisualStyles();
      //  Application.SetCompatibleTextRenderingDefault(false);
      //}
      //IList<string> args = Environment.GetCommandLineArgs();
      int exitCode = 0;
      //if (MeasurementUnits.ProcessCommandLine(args))
      //  exitCode = 0;
      //else
      exitCode = Main(commandLine);
      Environment.Exit(exitCode);
    }

    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    private static int Main(string commandLine)
    {
      try
      {
        //ServiceMessageContext context = ServiceMessageContext.GlobalContext;
        ProcessCommandLine(commandLine);
        //if (!ProcessCommandLine(commandLine))
        //{
        //  StreamReader reader = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream("OOI.ModelCompilerUI.HelpFile.txt"));
        //  if (noGui)
        //    Console.Error.WriteLine(reader.ReadToEnd());
        //  else
        //    guiHandling.Show(reader.ReadToEnd(), "ModelCompiler");
        //  reader.Close();
        //  return 2;
        //}
      }
      catch (Exception e)
      {
        Log.WriteTraceMessage(TraceMessage.BuildErrorTraceMessage(BuildError.NonCategorized, e.Message), 823547120);
        Log.WriteTraceMessage(TraceMessage.BuildErrorTraceMessage(BuildError.NonCategorized, $"Stack Trace: {Environment.NewLine}{e.StackTrace}"), 823547121);
        //if (noGui)
        //{
        //  Console.Error.WriteLine(e.Message);
        //  Console.Error.WriteLine(e.StackTrace);
        //  return 3;
        //}
        //else
        //  guiHandling.ShowDialog(e);
      }
      return 0;
    }

    //private const string consoleOutputCommandLineArgument = "-console";

    /// <summary>
    /// Processes the command line arguments.
    /// </summary>
    private static void ProcessCommandLine(string commandLine)
    {
      //if (commandLine.IndexOf("-?") != -1)
      //{
      //  StreamReader reader = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream("OOI.ModelCompilerUI.HelpFile.txt"));
      //  guiHandling.Show(reader.ReadToEnd(), "ModelCompiler");
      //  reader.Close();
      //  return true;
      //}
      List<string> tokens = GetTokens(commandLine);
      // launch gui if no arguments provided.
      //if (tokens.Count == 1)
      //  return false;
      try
      {
        ModelCompilerAPIInternal mc = new ModelCompilerAPIInternal();
        mc.ProcessCommandLine(tokens);
        mc.Build();
      }
      catch (Exception e)
      {
        Log.WriteTraceMessage(TraceMessage.BuildErrorTraceMessage(BuildError.NonCategorized, e.Message), 823547120);
        Log.WriteTraceMessage(TraceMessage.BuildErrorTraceMessage(BuildError.NonCategorized, $"Stack Trace: {Environment.NewLine}{e.StackTrace}"), 823547121);
        //  if (!noGui)
        //  {
        //    Console.Error.WriteLine(e.Message);
        //    Console.Error.WriteLine(e.StackTrace);
        //    Environment.Exit(4);
        //  }
        //  else
        //    guiHandling.ShowDialog(e);
      }
      //return true;
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
      {
        tokens.Add(token.ToString());
      }

      return tokens;
    }
  }
}