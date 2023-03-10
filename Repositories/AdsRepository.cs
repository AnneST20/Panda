using System;

namespace Panda.Repositories
{
    public class AdsRepository
    {
        public static string GetNewId()
        {
            return Guid.NewGuid().ToString("N");
        }
    }
}