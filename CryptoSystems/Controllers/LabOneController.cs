using CryptoSystems.Services.Interfaces;
using CryptoSystems.Services.LabOne;
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
        try
        {
            string encryptedText = _labCryptoService.Encrypt(inputText);

            return Content(encryptedText);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public ActionResult Decrypt(string inputText)
    {
        try
        {
            string decryptedText = _labCryptoService.Decrypt(inputText);

            return Content(decryptedText);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public ActionResult SetPolynomial(string input)
    {
        try
        {
            _labCryptoService.SetPolynomial(input);

            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public ActionResult GenerateGammaKey()
    {
        try
        {
            _labCryptoService.GenerateGammaKeyWithLFSR();
            var gammaKeyString = string.Join(" ", _labCryptoService.GammaKey);

            return Content(gammaKeyString);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
