using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ChessPairingProto.Pages;

public class AddUserModel : PageModel
{
    private readonly ILogger<AddUserModel> _logger;

    public AddUserModel(ILogger<AddUserModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
    }
}

