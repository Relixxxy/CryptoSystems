using CryptoSystems.Services;
using CryptoSystems.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CryptoSystems.Controllers;

public class LabOneController : Controller
{
    private readonly ILaboratoryWorkService _labService;
    private readonly LabOneCryptoService _labCryptoService;

    public LabOneController(
        ILaboratoryWorkService labService,
        LabOneCryptoService labCryptoService)
    {
        _labService = labService;
        _labCryptoService = labCryptoService;
    }

    public async Task<IActionResult> Index(int id)
    {
        var lab = await _labService.GetByIdAsync(id);
        return View(lab);
    }

    [HttpPost]
    public ActionResult Encrypt(string inputText)
    {
        string encryptedText = _labCryptoService.Encrypt(inputText);

        return Content(encryptedText);
    }

    [HttpPost]
    public ActionResult Decrypt(string inputText)
    {
        string decryptedText = _labCryptoService.Decrypt(inputText);

        return Content(decryptedText);
    }
}
