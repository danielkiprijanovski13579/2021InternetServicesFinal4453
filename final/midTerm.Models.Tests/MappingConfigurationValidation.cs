using midTerm.Models.Tests.Internal;
using System;
using Xunit;
using midTerm.Models.Profiles;

namespace midTerm.Models.Tests
{
    public class MappingConfigurationValidation
    {
        [Fact]
        public void IsValid()
        {
            // Arange
            var configuration1 = AutoMapperModule.CreateMapperConfiguration<OptionProfile>();
            var configuration2 = AutoMapperModule.CreateMapperConfiguration<QuestionProfile>();
            //var configuration = AutoMapperModule.CreateMapperConfiguration<>();
            // Act/Assert
            configuration1.AssertConfigurationIsValid();
            configuration2.AssertConfigurationIsValid();
        }
    }
}
