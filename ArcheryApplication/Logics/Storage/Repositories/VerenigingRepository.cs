using System;
using System.Collections.Generic;
using ArcheryApplication.Classes;

namespace ArcheryApplication.Storage
{
    public class VerenigingRepository
    {
        private IVerenigingServices _verenigingLogic;

        public VerenigingRepository(IVerenigingServices _verenigingLogic)
        {
            this._verenigingLogic = _verenigingLogic;
        }

        public Vereniging GetVerenigingById(int verId)
        {
            return _verenigingLogic.GetVerenigingById(verId);
        }

        public Vereniging GetVerenigingByName(string name)
        {
            return _verenigingLogic.GetVerenigingByName(name);
        }

        public Vereniging GetVerenigingByNr(int verNr)
        {
            return _verenigingLogic.GetVerenigingByNr(verNr);
        }

        public List<Baan> GetListBanen(int verNr)
        {
            return _verenigingLogic.GetListBanen(verNr);
        }
    }
}
