// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

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
