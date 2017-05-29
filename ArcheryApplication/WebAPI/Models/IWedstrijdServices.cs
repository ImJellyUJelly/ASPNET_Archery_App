using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArcheryApplication.Classes;

namespace WebAPI.Models
{
    public interface IWedstrijdServices
    {
        List<Wedstrijd> GetAllWedstrijden();
        Wedstrijd GetWedstrijdById(int wedstrijdId);
    }
}
