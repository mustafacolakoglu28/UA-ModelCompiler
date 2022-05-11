//__________________________________________________________________________________________
//
//  Copyright 2022 Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community by pressing the `Watch` button and to get started
//  comment using the discussion panel at
//  https://github.com/mpostol/TP/discussions/182
//  with an introduction of yourself and tell us about what you do with this community.
//__________________________________________________________________________________________

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;

namespace OOI.ModelCompiler.ASPDNETUserInterface.Pages
{
  [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
  [IgnoreAntiforgeryToken]
  public class ErrorModel : PageModel
  {
    public string? RequestId { get; set; }
    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    private readonly ILogger<ErrorModel> _logger;

    public ErrorModel(ILogger<ErrorModel> logger)
    {
      _logger = logger;
    }

    public void OnGet()
    {
      RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
    }
  }
}