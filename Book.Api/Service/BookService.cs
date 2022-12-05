using Book.DataAccess;
using System.Data;
using System.Data.SqlClient;

namespace Book.Api.Service
{
    public class BookService : IBook
    {
        private ISqlHelper _objSqlHelper = null;
        public BookService(ISqlHelper objSqlHelper)
        {
            _objSqlHelper = objSqlHelper;
        }
        public DataTable GetBooks()
        {
           
            DataSet ds = _objSqlHelper.ExecuteDataSet("GETBOOKS");
            return ds.Tables[0];
        }

        public bool BookAvailable(string BookId)
        {
            bool returnValue = false;
            SqlParameter[] sqlParameter =
            {
                new SqlParameter("@BookId",BookId),
            };
            DataSet ds = _objSqlHelper.ExecuteDataSet("BOOKAVAILABLE", sqlParameter);
            if(ds != null && ds.Tables[0]!=null && ds.Tables[0].Rows.Count > 0)
            {
                if (Convert.ToInt16( ds.Tables[0].Rows[0]["EXIST"]) == 1)
                {
                    returnValue = true;
                }
            }
            return returnValue;
        }
        
    }
}
