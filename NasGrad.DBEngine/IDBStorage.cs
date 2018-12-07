using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NasGrad.DBEngine
{
    public interface IDBStorage
    {
        Task<NasGradConfiguration> GetConfiguration();

    }
}
