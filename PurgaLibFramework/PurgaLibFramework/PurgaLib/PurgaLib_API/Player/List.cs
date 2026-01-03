using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLib_API.Player
{
    public static class List
    {
        public static IEnumerable<LabApi.Features.Wrappers.Player> GetAll()
        {
            return LabApi.Features.Wrappers.Player.List;
        }
        
        public static int Count()
        {
            return LabApi.Features.Wrappers.Player.List.Count;
        }
    }
}