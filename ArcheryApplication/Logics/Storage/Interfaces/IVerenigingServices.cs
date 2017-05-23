using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArcheryApplication.Classes;

namespace ArcheryApplication.Storage
{
    public interface IVerenigingServices
    {
        Vereniging GetVerenigingById(int verId);
        Vereniging GetVerenigingByName(string name);
        Vereniging GetVerenigingByNr(int verNr);
        List<Baan> GetListBanen(int verNr);
    }
}
