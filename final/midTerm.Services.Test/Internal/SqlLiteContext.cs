
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using midTerm.Data;
using midTerm.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetServices.Service.Test.Internal
{
    public abstract class SqlLiteContext
    : IDisposable
    {
        private const string InMemoryConnectionString = "DataSource=:memory:";
        private readonly SqliteConnection _connection;
        protected readonly MidTermDbContext DbContext;
        //private bool disposedValue;
        //private bool disposedValue1;



        private DbContextOptions<MidTermDbContext> CreateOptions()
        {
            return new DbContextOptionsBuilder<MidTermDbContext>()
               .EnableDetailedErrors()
               .EnableSensitiveDataLogging()
               .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
               .UseSqlite(_connection)
               .Options;
        }
        protected SqlLiteContext(bool withData = false)
        {
            _connection = new SqliteConnection(InMemoryConnectionString);
            DbContext = new MidTermDbContext(CreateOptions());
            _connection.Open();
            DbContext.Database.EnsureCreated();
            if (withData)
                SeedData(DbContext);
        }

        private void SeedData(MidTermDbContext context)
        {
            var options = new List<Option>
                {
                    new Option
                    {
                        Id = 1,
                        Text = "Option1",
                        Order = 1,
                        QuestionId = 1
                    },
                    new Option
                    {
                          Id = 2,
                        Text = "Option3",
                        Order = 2,
                        QuestionId = 2
                    },
                    new Option
                    {
                        Id = 3,
                        Text = "Option2",
                        Order = 3,
                        QuestionId = 2
                    }
                };
            var questions = new List<Question>
                {
                    new Question
                    {
                        Id = 1,
                       Text = "Some Text",
                        Description = "Question1"
                    },
                    new Question
                    {
                       Id = 2,
                       Text = "Some Text",
                        Description = "Question2"
                    },
                   
            };

            context.AddRange(options);
            context.AddRange(questions);
            context.SaveChanges();
        }








        public void Dispose()
        {
            _connection.Close();
        }

    }
}
