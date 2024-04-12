using NogetMedThreadLib.model;

namespace NogetMedThreadLib.services
{
    public interface ISillyHotelRepository
    {
        Hotel GetById(int id);
    }
}