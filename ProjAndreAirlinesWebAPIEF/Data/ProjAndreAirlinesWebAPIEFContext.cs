using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjAndreAirlinesWebAPI.Model.DTO;

namespace ProjAndreAirlinesWebAPIEF.Data
{
    public class ProjAndreAirlinesWebAPIEFContext : DbContext
    {
        public ProjAndreAirlinesWebAPIEFContext (DbContextOptions<ProjAndreAirlinesWebAPIEFContext> options)
            : base(options)
        {
        }

        public DbSet<ProjAndreAirlinesWebAPI.Model.DTO.Airport> Airport { get; set; }
    }
}
