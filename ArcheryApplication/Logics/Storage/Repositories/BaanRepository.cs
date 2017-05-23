using System.Collections.Generic;
using ArcheryApplication.Classes;

namespace ArcheryApplication.Storage
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

        public void RemoveBaan(Baan baan)
        {
            _baanLogic.RemoveBaan(baan);
        }
    }
}
