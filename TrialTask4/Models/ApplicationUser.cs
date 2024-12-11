using Microsoft.AspNetCore.Identity;

namespace TrialTask4.Models;

public class ApplicationUser : IdentityUser
{
    public List<Claim> Claims { get; set; }
}