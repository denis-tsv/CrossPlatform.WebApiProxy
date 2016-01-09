using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace AdventureWorks.UILogic.Services
{
    public interface ILocationService
    {
        Task<ReadOnlyCollection<string>> GetStatesAsync();
    }
}
