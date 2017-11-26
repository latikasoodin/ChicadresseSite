using Chicadresse.Business.Services.Base;
using Chicadresse.Data.Base;
using Chicadresse.Data.Repositories.Guests;
using Chicadresse.Data.UnitOfWork;
using Chicadresse.Entities.Domain;
using Chicadresse.Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Chicadresse.Business.Services.Guests
{
    public class GuestService : IGuestService
    {

        #region Fields

        private readonly IGuestRepository _guestRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICacheService _cacheService;
        private readonly IGuestTableRepository _guestTableRepository;

        #endregion

        #region ctor

        public GuestService(IGuestRepository guestRepository,
            IUnitOfWork unitOfWork,
            ICacheService cacheService,
            IGuestTableRepository guestTableRepository)
        {
            this._guestRepository = guestRepository;
            this._unitOfWork = unitOfWork;
            this._cacheService = cacheService;
            this._guestTableRepository = guestTableRepository;
        }

        #endregion

        #region Implementations

        public IEnumerable<Guest_List_Result> GetGuestList(GuestFilterViewModel filter)
        {
            return this._guestRepository.GetGuestList(filter).ToList();
        }

        public Guest_Details GetGuest(int guestId)
        {
            return this._guestRepository.GetByID(guestId);
        }

        public bool Save(Guest_Details guest)
        {
            if (guest.Id <= 0)
            {
                guest.AttendanceId = (int)Entities.Enumerations.Attendance.Waiting;
                this._guestRepository.Insert(guest);
            }
            else
            {
                this._guestRepository.UpdateGuest(guest);
            }
            _unitOfWork.Save();
            return true;
        }

        public bool Delete(int guestId)
        {
            this._guestRepository.RemoveGuest(guestId);
            return true;
        }

        public bool UpdatePresence(int presenceId, int guestId)
        {
            Guest_Details guest = this.GetGuest(guestId);
            guest.AttendanceId = presenceId;
            guest.ModifiedDate = DateTime.Now;

            this._guestRepository.UpdateGuest(guest);
            this._unitOfWork.Save();
            return true;
        }

        public bool UpdateTable(int tableId, int guestId)
        {
            Guest_Details guest = this.GetGuest(guestId);
            var guestTable = guest.Guest_Table.Where(g => g.GuestId == guestId && g.IsDeleted == false).FirstOrDefault();

            if (guestTable != null)
            {
                guestTable.TableId = tableId;
                guestTable.ModifiedDate = DateTime.Now;
                this._guestTableRepository.Update(guestTable);
                this._unitOfWork.Save();
            }
            return true;
        }

        public bool UpdateGroup(int groupId, int guestId)
        {
            Guest_Details guest = this.GetGuest(guestId);
            guest.GroupId = groupId;
            guest.ModifiedDate = DateTime.Now;

            this._guestRepository.Update(guest);
            this._unitOfWork.Save();
            return true;
        }

        /// <summary>
        /// Get List without paging
        /// </summary>
        /// <param name="weddingId"></param>
        /// <returns></returns>
        public IEnumerable<Guest_Details> GuestList(int weddingId)
        {
            return this._guestRepository.GetMany(g => g.WeddingId == weddingId && g.IsDeleted == false);
        }

        public InviteStats GetGuestStats(int weddingId)
        {
            var guests = this._guestRepository.GetMany(g => g.WeddingId == weddingId && g.IsDeleted == false);
            InviteStats stats = new InviteStats
            {
                Total = guests.Count()
            };
            stats.Confirms = guests.Where(g => g.AttendanceId == (int)Entities.Enumerations.Attendance.Confirm).Count();

            stats.Waiting = guests.Where(g => g.AttendanceId == (int)Entities.Enumerations.Attendance.Waiting).Count();

            stats.Tables = guests.Select(x => x.Guest_Table.Count()).Sum();

            return stats;
        }

        #endregion
    }
}
