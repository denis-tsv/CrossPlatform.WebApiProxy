

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using AdventureWorks.UILogic.Services;
namespace AdventureWorks.UILogic.Tests.Mocks
{
    public class MockLocationService : ILocationService
    {
        public Task<ReadOnlyCollection<string>> GetStatesAsync()
        {
            var states = new List<string> {"State1", "State2"};
            ReadOnlyCollection<string> stateCollection = new ReadOnlyCollection<string>(states);
            return Task.FromResult(stateCollection);
        }
    }
}
