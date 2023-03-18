using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ChessPairingProto.Pages;

public class PodiumModel : PageModel
{
    private readonly ILogger<PodiumModel> _logger;

    public PodiumModel(ILogger<PodiumModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
    }
}

