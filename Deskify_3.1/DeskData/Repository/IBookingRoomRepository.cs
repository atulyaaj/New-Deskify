using DeskEntity.Model;
using DeskEntity.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeskData.Repository
{
    public interface IBookingRoomRepository
    {
        void AddBookingRoom(BookingRoom bookingRoom);
        void UpdateBookingRoom(BookingRoom bookingRoom);
        void DeleteBookingRoom(int bookingRoomId);

        BookingRoom GetBookingRoomById(int bookingRoomId);
        IEnumerable<BookingRoom> GetBookingRoom();
    }
}
