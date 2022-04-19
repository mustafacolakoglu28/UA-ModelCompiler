//__________________________________________________________________________________________________
//
//  Copyright (C) 2022, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GitHub: https://github.com/mpostol/OPC-UA-OOI/discussions
//__________________________________________________________________________________________________

using UAOOI.SemanticData.BuildingErrorsHandling;

namespace OOI.ModelCompilerUI.Diagnostic
{
  /// <summary>
  /// Interface IBuildErrorsHandling - a contract of the main errors handling 
  /// </summary>
  internal interface IBuildErrorsHandling
  {
    /// <summary>
    /// Traces the event using <see cref="TraceMessage"/>.
    /// </summary>
    /// <param name="traceMessage">The message to be send to trace.</param>
    void WriteTraceMessage(TraceMessage traceMessage, int id = 1560515041);

    /// <summary>
    /// Gets the number of traced errors.
    /// </summary>
    /// <value>The errors.</value>
    int Errors { get; }
  }
}