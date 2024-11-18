
namespace DeskBook.Core.Domain
{
    public class DeskBookingResult : DeskBookinBase
    {
        public DeskBookingResultCode Code { get; set; }
        public int? DeskBookingId { get; set; }
    }
}