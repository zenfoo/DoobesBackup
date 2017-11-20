namespace DoobesBackup.Service.Modules
{
    using DoobesBackup.Service.ResourceModels;
    using DoobesBackup.Service.Services;
    using Nancy;
    using Nancy.ModelBinding;

    /// <summary>
    /// Authentication module
    /// </summary>
    public class AuthModule : NancyModule
    { 
        public AuthModule(IAuthService authService) : base("/auth")
        {
            this.Post("/login", async formData =>
            {
                var loginModel = this.Bind<LoginRequestRM>();

                var user = await authService.Login(loginModel.Email, loginModel.Password);
                if (user != null)
                {
                    return this.Response.AsJson(new LoginResponseRM() { Token = authService.GenerateUserToken(user) });
                }

                // Login was not successful
                return this.Response.AsJson(new LoginResponseRM() { Token = null }, HttpStatusCode.Unauthorized);
            });
        }
    }
}
