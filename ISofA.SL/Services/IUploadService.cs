using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ISofA.SL.Services
{
    public interface IUploadService
    {
        Task<string> UploadImageAsync(HttpPostedFile image);
    }
}
