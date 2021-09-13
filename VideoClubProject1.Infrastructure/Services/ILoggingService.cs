using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoClubProject1.Infrastructure.Services
{
    public interface ILoggingService
    {
        ILogger Writer { get; }
    }
}
