using System.Collections.Generic;
using ArcheryApplication.Classes.Database.Interfaces;

namespace ArcheryApplication.Classes.Database.Repositories
{
    public class BaanRepository
    {
        private IBaanServices _baanLogic;
        public BaanRepository(IBaanServices baanLogic)
        {
            this._baanLogic = baanLogic;
        }

        public void AddBaan(Baan baan)
        {
            _baanLogic.AddBaan(baan);
        }

        public void AddSchutterTobaan(Schutter schutter, int baanId)
        {
            _baanLogic.AddSchutterTobaan(schutter, baanId);
        }

        public void EditBaan(Baan baan)
        {
            _baanLogic.EditBaan(baan);
        }

        public Baan GetBaanById(int baanId)
        {
            return _baanLogic.GetBaanById(baanId);
        }

        public Baan GetBaanByNummer(int baanNummer)
        {
            return _baanLogic.GetBaanByNummer(baanNummer);
        }

        public List<Baan> ListBanen(int VerNr)
        {
            return _baanLogic.ListBanen(VerNr);
        }

        public List<Schutter> ListSchuttersOpBaan(int baanId)
        {
            return _baanLogic.ListSchuttersOpBaan(baanId);
        }

        public void RemoveBaan(Baan baan)
        {
            _baanLogic.RemoveBaan(baan);
        }

        public void RemoveSchutterFromBaan(Schutter schutter, int baanId)
        {
            _baanLogic.RemoveSchutterFromBaan(schutter, baanId);
        }
    }
}
