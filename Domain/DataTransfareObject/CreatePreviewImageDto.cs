using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DataTransfareObject
{
    public class CreatePreviewImageDto
    {
        public MultipartFormDataContent  File { get; set; }
    }

}
