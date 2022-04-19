//__________________________________________________________________________________________________
//
//  Copyright (C) 2021, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GitHub: https://github.com/mpostol/OPC-UA-OOI/discussions
//__________________________________________________________________________________________________

using System;
using UAOOI.Common.Infrastructure.Diagnostic;
using UAOOI.SemanticData.BuildingErrorsHandling;

namespace OOI.ModelCompilerUI.Diagnostic
{
  /// <summary>
  /// Class AssemblyTraceSource - an entry point to the compiler errors handling. Implements the <see cref="IBuildErrorsHandling" />
  /// </summary>
  /// <seealso cref="IBuildErrorsHandling" />
  internal class AssemblyTraceSource : IBuildErrorsHandling
  {
    #region constructors

    /// <summary>
    /// Returns singleton of the <see cref="IBuildErrorsHandling"/>.
    /// </summary>
    internal static IBuildErrorsHandling Log => buildErrorsHandling.Value;

    /// <summary>
    /// Initializes a new instance of the <see cref="AssemblyTraceSource"/> class using a provided implementation of the <see cref="ITraceSource"/>.
    /// </summary>
    /// <param name="traceEvent">The provided implementation of the <see cref="ITraceSource"/>.</param>
    private AssemblyTraceSource(ITraceSource traceSource)
    {
      this.traceSource = traceSource;
    }

    #endregion constructors

    #region IBuildErrorsHandling

    /// <summary>
    /// Writes the trace message <see cref="TraceMessage"/>.
    /// </summary>
    /// <param name="id">A numeric identifier for the event.</param>
    /// <param name="traceMessage">The trace message.</param>
    public void WriteTraceMessage(TraceMessage traceMessage, int id = 1560515041)
    {
      traceSource.TraceData(traceMessage.TraceLevel, id, traceMessage.ToString());
      if (traceMessage.BuildError.Focus != Focus.Diagnostic)
        Errors++;
    }

    public int Errors { get; private set; } = 0;

    #endregion IBuildErrorsHandling

    #region private

    private ITraceSource traceSource = null;
    private static Lazy<IBuildErrorsHandling> buildErrorsHandling = new Lazy<IBuildErrorsHandling>(() => new AssemblyTraceSource(new TraceSourceBase("ModelCompilerUI")));

    #endregion private
  }
}