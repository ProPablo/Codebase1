using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.StaticFiles;
using System.IO;

[assembly: OwinStartup(typeof(Forge.Museum.API.Startup1))]

namespace Forge.Museum.API
{
    public class Startup1
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888

            string root = AppDomain.CurrentDomain.BaseDirectory;
            var physicalFileSystem = new PhysicalFileSystem(Path.Combine(root, "wwwroot"));
            var options = new FileServerOptions
            {
                RequestPath = PathString.Empty,
                EnableDefaultFiles = true,
                FileSystem = physicalFileSystem
            };
            options.StaticFileOptions.FileSystem = physicalFileSystem;
            options.StaticFileOptions.ServeUnknownFileTypes = false;
            app.UseFileServer(options);
        }
    }
}
