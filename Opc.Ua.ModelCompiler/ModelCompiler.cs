//__________________________________________________________________________________________________
//
//  Copyright (C) 2021, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GitHub: https://github.com/mpostol/OPC-UA-OOI/discussions
//__________________________________________________________________________________________________

namespace OOI.ModelCompiler
{
  public static class ModelDesignCompiler
  {
    public static void BuildModel(ICompilerOptions options)
    {
      ModelGenerator2 Generator = new ModelGenerator2();
      Generator.ValidateAndUpdateIds(
                    options.DesignFiles,
                    options.IdentifierFile,
                    options.StartId,
                    options.Version,
                    options.UseAllowSubtypes,
                    options.Exclusions,
                    options.ModelVersion,
                    options.ModelPublicationDate,
                    options.ReleaseCandidate);
      Generator.GenerateMultipleFiles(options.OutputPath, false, options.Exclusions, false);
    }
  }
}