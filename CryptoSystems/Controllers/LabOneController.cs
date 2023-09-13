using CryptoSystems.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CryptoSystems.Controllers;

public class LabOneController : Controller
{
    private readonly ILaboratoryWorkService _labService;

    public LabOneController(ILaboratoryWorkService labService)
    {
        _labService = labService;
    }

    public async Task<IActionResult> Index(int id)
    {
        var lab = await _labService.GetByIdAsync(id);
        return View(lab);
    }
}
