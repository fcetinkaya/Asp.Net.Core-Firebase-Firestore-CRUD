using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.NodeServices;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BS_Core_WepApp.Services
{
    public class JavaScriptService : IJavaScriptService
    {
        private readonly INodeServices _nodeServices;
        private readonly string _scriptFolder;

        public JavaScriptService([FromServices] INodeServices nodeServices) : this(nodeServices, ".")
        {

        }

        public JavaScriptService([FromServices] INodeServices nodeServices, string scriptFolder)
        {
            _nodeServices = nodeServices;
            _scriptFolder = scriptFolder;
        }
        public async Task<dynamic> FileList()
        {
            string path = Path.Combine(_scriptFolder, "./wwwroot/js/storage/public/firebase");
            var result = await _nodeServices.InvokeAsync<int>(path);
            return result;
        }

        public async Task<int> AddNumbers(int x, int y)
        {
            string path = Path.Combine(_scriptFolder, "./wwwroot/js/scripts/addNumbers");
            var result = await _nodeServices.InvokeAsync<int>(path, x, y);
            return result;
        }

        public async Task<string> Hello(string name)
        {
            string path = Path.Combine(_scriptFolder, "./wwwroot/js/scripts/hello");
            var result = await _nodeServices.InvokeAsync<string>(path, name);
            return result;
        }

        public async Task<string> Goodbye(string name)
        {
            string path = Path.Combine(_scriptFolder, "./wwwroot/js/scripts/goodbye");
            var result = await _nodeServices.InvokeAsync<string>(path, name);
            return result;
        }

    }
}
