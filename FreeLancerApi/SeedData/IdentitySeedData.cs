using FreeLancer.Models.Models.Domain;
using Microsoft.AspNetCore.Identity;

namespace FreeLancer.Api.SeedData
{
    public  class IdentitySeedData
    {
        public async static Task SeedUser( IApplicationBuilder _app, ILoggerFactory _logger)
        {
            UserManager<Auth> _user = _app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<UserManager<Auth>>();
            if (!_user.Users.Any(x=>x.Email == "titobioluwole@gmail.com"))
            {
                var createdUser = new Freelancer
                {
                    Email = "titobioluwole@gmail.com",
                    PhoneNumber = "09160648182",
                    UserName = "titobioluwole@gmail.com",
                    SkillLevel = Models.Models.Enums.Experience.Intermediate,
                    SkillId = 1
                    
                };
               var result = await _user.CreateAsync(createdUser, "Titobi12345.");
                if (result.Succeeded)
                {
                    var log = _logger.CreateLogger<IdentitySeedData>();
                    log.LogInformation("Created User Successfully");
                }
            }
        }
    }
}
