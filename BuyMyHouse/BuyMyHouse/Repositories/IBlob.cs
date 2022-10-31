using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuyMyHouse.Repositories
{
    public interface IBlob
    {
        Task<string> CreateFile(string file, string refFile);
        Task<string> GetBlob(string file);
        Task<bool> DeleteBlob(string file);
    }
}
