using DeskBook.Core.Domain;

namespace DeskBook.Core.DataInterface
{
    public interface IDeskRepository
    {
        IEnumerable<Desk> GetAvailableDesks(DateTime? date);
    }
}
