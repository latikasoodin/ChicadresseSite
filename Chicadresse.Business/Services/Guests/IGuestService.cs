using Chicadresse.Entities.Domain;
using Chicadresse.Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chicadresse.Business.Services.Guests
{
    public interface IGuestService
    {
        IEnumerable<Guest_List_Result> GetGuestList(GuestFilterViewModel filter);

        bool Save(Guest_Details guest);

        Guest_Details GetGuest(int guestId);

        bool Delete(int guestId);

        bool UpdatePresence(int presenceId, int guestId);

        bool UpdateGroup(int groupId, int guestId);

        bool UpdateTable(int tableId, int guestId);

        IEnumerable<Guest_Details> GuestList(int weddingId);

        InviteStats GetGuestStats(int weddingId);
    }
}
