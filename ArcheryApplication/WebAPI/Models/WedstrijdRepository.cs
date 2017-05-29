using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ArcheryApplication.Classes;

namespace WebAPI.Models
{
    public class WedstrijdRepository
    {
        private IWedstrijdServices _wedstrijdLogic;
        public WedstrijdRepository(IWedstrijdServices wedstrijdLogic)
        {
            _wedstrijdLogic = wedstrijdLogic;
        }

        public Wedstrijd GetWedstrijdById(int wedstrijdId)
        {
            return _wedstrijdLogic.GetWedstrijdById(wedstrijdId);
        }
        
        public List<Wedstrijd> GetAllWedstrijden()
        {
            return _wedstrijdLogic.GetAllWedstrijden();
        }
    }
}