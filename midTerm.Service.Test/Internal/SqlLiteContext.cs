using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using midTerm.Data;
using midTerm.Data.Entities;
using midTerm.Services.Services;

namespace midTerm.Service.Test.Internal
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
                        Text = "Option1"
                    },
                    new Option
                    {
                         Id = 2,
                        Text = "Option2"
                    },
                    new Option
                    {
                        Id = 3,
                        Text = "Option3"
                    }
                };
            var questions = new List<Question>
                {
                    new Question
                    {
                        Id = 1,
                        Description = "Question1"
                    },
                    new Question
                    {
                       Id = 2,
                        Description = "Question2"
                    },
                    new Question
                    {
                        Id = 3,
                        Description = "Question3"
                    },
                    new Question
                    {
                       Id = 4,
                        Description = "Question4"
                    },
            };
            /*  var playedMatches = new List<PlayerMatch>
                  {
                      new PlayerMatch
                      {
                          Id = 1,
                          MatchId = 1,
                          PlayerId = 1,
                          Score = 2
                      },
                      new PlayerMatch
                      {
                          Id = 2,
                          MatchId = 1,
                          PlayerId = 2,
                          Score = 1
                      },
                      new PlayerMatch
                      {
                          Id = 3,
                          MatchId = 2,
                          PlayerId = 3,
                          Score = 1
                      },
                      new PlayerMatch
                      {
                          Id = 4,
                          MatchId = 2,
                          PlayerId = 4,
                          Score = 0
                      },
                      new PlayerMatch
                      {
                      Id = 5,
                      MatchId = 3,
                      PlayerId = 1,
                      Score = 3
              },
                      new PlayerMatch
              {
                  Id = 6,
                  MatchId = 3,
                  PlayerId = 3,
                  Score = 1
              }
                  };
            */

            context.AddRange(options);
           // context.AddRange(playedMatches);
            context.AddRange(questions);
            context.SaveChanges();
        }






        public void Dispose()
        {
            _connection.Close();
        }

    }
}
