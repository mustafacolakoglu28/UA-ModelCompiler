//__________________________________________________________________________________________________
//
//  Copyright (C) 2022, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GitHub: https://github.com/mpostol/OPC-UA-OOI/discussions
//__________________________________________________________________________________________________

using CommandLine;
using OOI.ModelCompiler;
using OOI.ModelCompilerUI.CommandLineSyntax;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using UAOOI.SemanticData.BuildingErrorsHandling;
using static OOI.ModelCompilerUI.Diagnostic.AssemblyTraceSource;

namespace OOI.ModelCompilerUI
{
  internal class EntryPoint
  {
    [STAThread]
    private static void Main(string[] args)
    {
      try
      {
        AssemblyName myAssembly = Assembly.GetExecutingAssembly().GetName();
        string AssemblyHeader = $"ModelDesign Compiler (mdc.exe) Version: {myAssembly.Version}";
        Log.WriteTraceMessage(TraceMessage.DiagnosticTraceMessage(AssemblyHeader), 716624168);
        Log.WriteTraceMessage(TraceMessage.DiagnosticTraceMessage(Copyright), 716624169);
        EntryPoint program = new EntryPoint();
        program.MainExecute(args);
      }
      catch (Exception ex)
      {
        string errorMessage = $"Program stopped by the exception: {ex.Message}";
        Console.WriteLine(errorMessage);
        Log.WriteTraceMessage(TraceMessage.BuildErrorTraceMessage(BuildError.NonCategorized, errorMessage), 828896092);
        Environment.Exit(1);
      }
    }

    private const string Copyright = "Copyright(c) 2022 Mariusz Postol";
    private bool Running = true;

    private void MainExecute(string[] args)
    {
      Task heartbeatTask = Heartbeat();
      Run(args).Wait();
      Running = false;
      heartbeatTask.Wait();
    }

    internal async Task Run(string[] args)
    {
      try
      {
        await Task.Run(() => Processing(args));
      }
      catch (Exception ex)
      {
        string errorMessage = $"Processing stopped by the exception {ex.GetType().Name}: {ex.Message}";
        Console.WriteLine(errorMessage);
        Log.WriteTraceMessage(TraceMessage.BuildErrorTraceMessage(BuildError.NonCategorized, errorMessage), 552021345);
      }
    }

    private void Processing(string[] args)
    {
      ParserResult<object> result = Parser.Default.ParseArguments<CompilerOptions, DotNetStackOptions, UnitsOptions, UpdateHeadersOptions>(args);
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
      Compile(compilerOptions);
      GenerateStack(dotNetStackOptions);
      Units(unitsOptions);
      UpdateHeaders(updateHeadersOptions);
      if (error != null)
        throw new InvalidOperationException(error.ToString());
    }

    private void Compile(CompilerOptions options)
    {
      if (options == null)
        return;
      Log.WriteTraceMessage(TraceMessage.DiagnosticTraceMessage($"Started {nameof(Compile)} with parameters {options}"), 499457465);
      options.ValidateOptionsConsistency();
      ModelDesignCompiler.BuildModel(options);
    }

    private static void GenerateStack(DotNetStackOptions options)
    {
      if (options == null)
        return;
      Log.WriteTraceMessage(TraceMessage.DiagnosticTraceMessage($"Started {nameof(GenerateStack)} with parameters {options}"), 311903328);
      ModelGenerator2 generator = new ModelGenerator2();
      generator.ValidateAndUpdateIds(
        options.DesignFiles,
        options.IdentifierFile,
        1,
        options.Version,
        false,
        null,
        String.Empty,
        String.Empty,
        false);
      //.NET stack generator
      if (!string.IsNullOrEmpty(options.DotNetStackPath))
      {
        if (!Directory.Exists(options.DotNetStackPath))
          throw new ArgumentException($"The directory does not exist: {options.DotNetStackPath}");
        StackGenerator.GenerateDotNet(options.DesignFiles,
                                      options.IdentifierFile,
                                      options.DotNetStackPath,
                                      options.Version,
                                      options.Exclusions);
      }
      //Build ANSI C stack
      if (!string.IsNullOrEmpty(options.AnsiCStackPath))
      {
        if (!Directory.Exists(options.AnsiCStackPath))
          throw new ArgumentException($"The directory does not exist: {options.AnsiCStackPath}");
        StackGenerator.GenerateAnsiC(options.DesignFiles,
                                     options.IdentifierFile,
                                     options.AnsiCStackPath,
                                     options.Version,
                                     options.Exclusions);
        generator.GenerateIdentifiersAndNamesForAnsiC(options.AnsiCStackPath, options.Exclusions);
        if (!String.IsNullOrEmpty(options.OutputPath))
          generator.GenerateMultipleFiles(options.OutputPath, false, options.Exclusions, false);
      }
    }

    private void Units(UnitsOptions options)

    {
      if (options == null)
        return;
      Log.WriteTraceMessage(TraceMessage.DiagnosticTraceMessage($"Started {nameof(Units)} with parameters {options}"), 1166773910);
      MeasurementUnits.Process(options.Annex1Path, options.Annex2Path, options.OutputPath);
    }

    private void UpdateHeaders(UpdateHeadersOptions options)
    {
      if (options == null)
        return;
      Log.WriteTraceMessage(TraceMessage.DiagnosticTraceMessage($"Started {nameof(UpdateHeaders)} with parameters {options}"), 1251663675);
      HeaderUpdateTool.ProcessDirectory(options.InputPath, options.FilePattern, options.LicenseType, options.Silent);
    }

    private async Task Heartbeat()
    {
      await Task.Run(async () =>
      {
        int counter = 0;
        while (Running)
        {
          await Task.Delay(1000);
          Console.Write("\r");
          if (counter % 2 == 0)
            Console.Write(@"\");
          else
            Console.Write("/");
          counter++;
        }
        Log.WriteTraceMessage(TraceMessage.DiagnosticTraceMessage($"Execution time = {counter}s"));
        Console.WriteLine();
        Console.WriteLine($"Execution time = {counter}s");
      });
    }

    #region DEBUG

    [System.Diagnostics.Conditional("DEBUG")]
    internal static void GenerateStackDebugCall(DotNetStackOptions options)
    {
      GenerateStack(options);
    }

    #endregion DEBUG
  }
}