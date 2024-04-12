namespace NogetMedThreadLib.model
{
    public class Hotel : IComparable<Hotel>
    {
        public int HotelNo { get; set; }
        public String Name { get; set; }
        public String Address { get; set; }

        public Hotel(int hotelNo, string name, string address)
        {
            HotelNo = hotelNo;
            Name = name;
            Address = address;
        }

        public Hotel() : this(-1, "dummy", "some address")
        { }

        public override string ToString()
        {
            return $"{{{nameof(HotelNo)}={HotelNo.ToString()}, {nameof(Name)}={Name}, {nameof(Address)}={Address}}}";
        }

        public int CompareTo(Hotel? other)
        {
            return HotelNo - other.HotelNo;
        }
    }
}
