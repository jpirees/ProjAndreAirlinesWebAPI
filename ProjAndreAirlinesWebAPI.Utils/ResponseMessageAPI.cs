using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjAndreAirlinesWebAPI.Utils
{
    public class ResponseMessageAPI : ResponseAPI
    {
        public object Response { get; }

        public ResponseMessageAPI(object response) : base(200) =>
            Response = response;
    }
}
