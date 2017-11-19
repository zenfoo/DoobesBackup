namespace DoobesBackup.Service.Modules
{
    using DoobesBackup.Service.ResourceModels;
    using Nancy;
    using Nancy.ModelBinding;

    /// <summary>
    /// Authentication module
    /// </summary>
    public class AuthModule : NancyModule
    { 
        public AuthModule() : base("/auth")
        {
            this.Post("/login", formData =>
            {
                var loginModel = this.Bind<LoginRequestRM>();

                return this.Response.AsRedirect("/");
            });
        }
    }
}
