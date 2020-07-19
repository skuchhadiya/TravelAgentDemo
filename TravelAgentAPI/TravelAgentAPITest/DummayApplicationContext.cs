using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using TravelAgentAPI.DataModels;

namespace TravelAgentAPITest
{
    public static class DummayApplicationContext
    {
        private static  ApplicationDbContext _context;

        public static ApplicationDbContext GetContextInstance()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
              .UseInMemoryDatabase(databaseName: "TravelAgentDemo")
              .Options;
            _context = new ApplicationDbContext(options);
            return _context;
        }
        public static void DisposeContextInstance()
        {
            _context.Dispose();
        }
    }
}
