using DeskData.Repository;
using DeskEntity.Model;
using DeskData.Repository;
using DeskEntity.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeskBusiness.Services
{
    public class BookingRoomService
    {
        IBookingRoomRepository _roomRepository;
        public BookingRoomService(IBookingRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }
        //CRUD service operations for BookingRoom
        public void AddBookingRoom(BookingRoom bookingRoom)
        {
            _roomRepository.AddBookingRoom(bookingRoom);
        }
        public void UpdateBookingRoom(BookingRoom bookingRoom)
        {
            _roomRepository.UpdateBookingRoom(bookingRoom);
        }
        public void DeleteBookingRoom(int bookingRoomId)
        {
            _roomRepository.DeleteBookingRoom(bookingRoomId);
        }
        //get room by id
        public BookingRoom GetBookingRoomById(int bookingRoomId)
        {
            return _roomRepository.GetBookingRoomById(bookingRoomId);
        }
        //get rooms
        public IEnumerable<BookingRoom> GetBookingRoom()
        {
            return _roomRepository.GetBookingRoom();
        }

    }
}
