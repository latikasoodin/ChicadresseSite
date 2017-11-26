using Chicadresse.Data.Base;
using Chicadresse.Data.UnitOfWork;
using Chicadresse.Entities.Domain;
using Chicadresse.Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chicadresse.Data.Repositories.Guests
{
    public class GuestRepository : GenericRepository<Guest_Details>, IGuestRepository
    {
        #region fields

        private readonly IDbFactory _dbFactory;
        private readonly IUnitOfWork _unitOfWork;

        #endregion

        #region ctor

        public GuestRepository(IDbFactory dbFactory, IUnitOfWork unitOfWork) : base(dbFactory)
        {
            _dbFactory = dbFactory;
            _unitOfWork = unitOfWork;
        }

        #endregion


        #region Implementation

        public IEnumerable<Guest_List_Result> GetGuestList(GuestFilterViewModel filter)
        {
            return this.Context.Guest_List(filter.WeddingId, filter.Name, filter.PageNumber, filter.PageSize, filter.OrderBy);
        }

        public bool RemoveGuest(int guestId)
        {
            var data = this.GetByID(guestId);
            if (data.Guest_Companinons != null)
            {
                foreach (var companion in data.Guest_Companinons.ToList())
                {
                    _unitOfWork.Context.Guest_Companinons.Remove(companion);
                }
            }

            var guestTable = data.Guest_Table.FirstOrDefault();
            if (guestTable != null)
            {
                _unitOfWork.Context.Guest_Table.Remove(guestTable);
            }
            this.Delete(data);
            _unitOfWork.Save();
            return true;
        }

        public void UpdateGuest(Guest_Details guest)
        {
            var data = this.GetByID(guest.Id);

            if (data.Guest_Companinons != null)
            {
                var tobeDeletedCompanions = data.Guest_Companinons.Where(g => !guest.Guest_Companinons.Any(c => c.Id == g.Id)).ToList();

                foreach (var companion in tobeDeletedCompanions)
                {
                    _unitOfWork.Context.Guest_Companinons.Remove(companion);
                }
            }

            if (guest.Guest_Companinons != null)
            {
                foreach (var companion in guest.Guest_Companinons.Where(c => c.Id == 0).ToList())
                {
                    _unitOfWork.Context.Guest_Companinons.Add(companion);
                }
            }
            data.FirstName = guest.FirstName;
            data.LastName = guest.LastName;
            data.GroupId = guest.GroupId;
            foreach (var table in data.Guest_Table.ToList())
            {
                table.TableId = guest.Guest_Table.FirstOrDefault().TableId;

            }

            this.Update(data);
            _unitOfWork.Save();
        }

        #endregion

    }
}
