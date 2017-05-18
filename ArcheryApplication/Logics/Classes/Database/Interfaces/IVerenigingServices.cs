using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcheryApplication.Classes.Database.Interfaces
{
    public interface IVerenigingServices
    {
        Vereniging GetVerenigingById(int verId);
        Vereniging GetVerenigingByName(string name);
        Vereniging GetVerenigingByNr(int verNr);
    }
}
