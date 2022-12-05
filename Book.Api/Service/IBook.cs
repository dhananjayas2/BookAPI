using System.Data;

namespace Book.Api.Service
{
    public interface IBook
    {
        DataTable GetBooks();

        bool BookAvailable(string BookId);
    }
}
