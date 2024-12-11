using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TrialTask4.Data;
using TrialTask4.Models;

namespace TrialTask4.Controllers;

public class ClaimController : Controller
{
    private readonly ApplicationDbContext _context;
    private Repository<Claim> _claimRepository;
    private readonly UserManager<ApplicationUser> _userManager;

    public ClaimController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _claimRepository = new Repository<Claim>(context);
        _userManager = userManager;
    }
    
    
    // GET
    public async Task<IActionResult> Index()
    {
        return View(await _claimRepository.GetAllAsync());
    }

    public async Task<IActionResult> Details(int id)
    {
        return View(await _claimRepository.GetByIdAsync(id, new QueryOptions<Claim>()));
    }

    
    [Authorize]
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [Authorize]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("ClaimId, Name")] Claim claim)
    {
        if (ModelState.IsValid)
        {
            var claimTemp = new Claim()
            {
                ClaimDate = DateTime.Now,
                Name = claim.Name,
                UserId = "1"
            };
            
            await _claimRepository.AddAsync(claimTemp);
            return RedirectToAction("Index");
        }
        
        return View(claim);
    }
}