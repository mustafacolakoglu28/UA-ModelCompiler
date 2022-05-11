//__________________________________________________________________________________________
//
//  Copyright 2022 Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community by pressing the `Watch` button and to get started
//  comment using the discussion panel at
//  https://github.com/mpostol/TP/discussions/182
//  with an introduction of yourself and tell us about what you do with this community.
//__________________________________________________________________________________________

using Microsoft.AspNetCore.Mvc.RazorPages;

namespace OOI.ModelCompiler.ASPDNETUserInterface.Pages
{
  public class PrivacyModel : PageModel
  {
    private readonly ILogger<PrivacyModel> _logger;

    public PrivacyModel(ILogger<PrivacyModel> logger)
    {
      _logger = logger;
    }

    public void OnGet()
    {
    }
  }
}