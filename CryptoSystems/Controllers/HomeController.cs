using System.Diagnostics;
using CryptoSystems.Services.Interfaces;
using CryptoSystems.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CryptoSystems.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ILaboratoryWorkService _labService;

    public HomeController(ILogger<HomeController> logger, ILaboratoryWorkService labService)
    {
        _logger = logger;
        _labService = labService;
    }

    public IActionResult Index()
    {
        return View();
    }

    public async Task<IActionResult> LaboratoryWorks()
    {
        var labs = await _labService.GetListAsync();

        return View(labs);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}