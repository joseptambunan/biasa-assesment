using System.Security.Claims;
using AspNetMvcLogin.Models;
using AspNetMvcLogin.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AspNetMvcLogin.Controllers;

public sealed class AccountController : Controller
{
    private readonly DemoCredentialValidator _credentialValidator;

    public AccountController(DemoCredentialValidator credentialValidator)
    {
        _credentialValidator = credentialValidator;
    }

    [AllowAnonymous]
    [HttpGet]
    public IActionResult Login(string? returnUrl = null)
    {
        if (User.Identity?.IsAuthenticated == true)
        {
            return RedirectToAction("Index", "Home");
        }
        return View(new LoginViewModel { ReturnUrl = returnUrl });
    }

    [AllowAnonymous]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        if (_credentialValidator.IsValid(model.Username, model.Password) == false)
        {
            ModelState.AddModelError(string.Empty, "Username atau password tidak sesuai.");
            return View(model);
        }

        await SignInAsync(model.Username, model.RememberMe);

        if (string.IsNullOrWhiteSpace(model.ReturnUrl) == false && Url.IsLocalUrl(model.ReturnUrl))
        {
            return Redirect(model.ReturnUrl);
        }
        return RedirectToAction("Index", "Home");
    }

    [Authorize]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction(nameof(Login));
    }

    [AllowAnonymous]
    [HttpGet]
    public IActionResult AccessDenied() => View();

    private async Task SignInAsync(string username, bool rememberMe)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, "demo-user-1"),
            new Claim(ClaimTypes.Name, username.Trim()),
            new Claim("display_name", _credentialValidator.GetDisplayName(username.Trim()))
        };
        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(identity),
            new AuthenticationProperties { IsPersistent = rememberMe, AllowRefresh = true });
    }
}
