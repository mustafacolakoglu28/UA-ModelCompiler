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
        EntryPoint program = new EntryPoint();
        AssemblyName myAssembly = Assembly.GetExecutingAssembly().GetName();
        string AssemblyHeader = $"ModelDesign Compiler (mdc.exe) Version: {myAssembly.Version}";
        Log.WriteTraceMessage(TraceMessage.DiagnosticTraceMessage(AssemblyHeader), 716624168);
        Log.WriteTraceMessage(TraceMessage.DiagnosticTraceMessage(Copyright), 716624169);
        program.MainExecute(args);
      }
      catch (Exception ex)
      {
        string errorMessage = $"Program stopped by the exception: {ex.Message}";
        Console.WriteLine(errorMessage);
        Log.WriteTraceMessage(TraceMessage.BuildErrorTraceMessage(BuildError.NonCategorized, errorMessage), 828896092); ;
        Environment.Exit(1);
      }
    }

    private const string Copyright = "Copyright(c) 2022 Mariusz Postol";
    private bool Running = true;

    internal async Task Run(string[] args)
    {
      try
      {
        await Task.Run(() => ProcessModelDesign(args));
      }
      catch (Exception ex)
      {
        Console.WriteLine(string.Format("Program stopped by the exception: {0}", ex.Message));
        throw;
      }
    }

    private void ProcessModelDesign(string[] args)
    {
      //args.Parse<Options>(Do, HandleErrors);
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
      options.ValidateOptionsConsistency();
      ModelDesignCompiler.BuildModel(options);
    }

    private static void GenerateStack(DotNetStackOptions options)
    {
      if (options == null)
        return;
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

    [System.Diagnostics.Conditional("DEBUG")]
    internal static void GenerateStackDebugCall(DotNetStackOptions options)
    {
      GenerateStack(options);
    }

    private void Units(UnitsOptions unitsOptions)

    {
      if (unitsOptions == null)
        return;
      //TODO CLI Syntax #67 
      throw new NotImplementedException("Units verb must be implemented");
    }

    private void UpdateHeaders(UpdateHeadersOptions updateHeadersOptions)
    {
      if (updateHeadersOptions == null)
        return;
      //TODO CLI Syntax #67 
      throw new NotImplementedException("update-headers verb must be implemented");
    }

    private void MainExecute(string[] args)
    {
      Task heartbeatTask = Heartbeat();
      Run(args).Wait();
      Running = false;
      heartbeatTask.Wait();
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
  }
}