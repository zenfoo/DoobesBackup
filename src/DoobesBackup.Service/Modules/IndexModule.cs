namespace DoobesBackup.Service.Modules
{
    using Nancy;
    using Nancy.Configuration;

    public class IndexModule : NancyModule
    {
        public IndexModule() : base("/")
        {   
            this.
            Get("/", _ => Response.AsRedirect("~/dashboard"));
            Get("/dashboard", _ =>
            {
                var config = this.Context.Environment.GetValue<StaticContentConfiguration>();                
                return Response.AsFile("dashboard/index.html");
            });

        }
    }
}
