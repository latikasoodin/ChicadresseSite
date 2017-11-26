using Chicadresse.Data.Base;
using Chicadresse.Entities.Domain;
using Chicadresse.Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chicadresse.Data.Repositories.Guests
{
    public interface IGuestRepository : IRepository<Guest_Details>
    {
        IEnumerable<Guest_List_Result> GetGuestList(GuestFilterViewModel filter);

        bool RemoveGuest(int guestId);

        void UpdateGuest(Guest_Details guest);
    }
}
