using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using midTerm.Service.Test.Internal;
using midTerm.Services.Services;
using Xunit;

namespace midTerm.Service.Test.Service
{
    public class OptionServiceShould
    : SqlLiteContext
    {
        private readonly IMapper _mapper;
        private readonly OptionService _service;

        public OptionServiceShould()
        : base(true)
        {
            if (_mapper == null)
            {
                var mapper = new MapperConfiguration(cfg =>
                {
                    cfg.AddMaps(typeof(OptionProfile));
                }).CreateMapper();
                _mapper = mapper;
            }
            _service = new OptionService(DbContext, _mapper);
        }

        [Fact]
        public async Task GetOptionById()
        {
            // Arrange
            var expected = 1;

            // Act
            var result = await _service.Get(expected);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<OptionModelExtended>();
            result.Id.Should().Be(expected);
        }

        [Fact]
        public async Task GetOptions()
        {
            // Arrange
            var expected = 3;

            // Act
            var result = await _service.Get();

            // Assert
            result.Should().NotBeEmpty().And.HaveCount(expected);
            result.Should().BeAssignableTo<IEnumerable<OptionModelBase>>();
        }

        [Fact]
        public async Task InsertNewOption()
        {
            // Arrange
            var option = new OptionCreateModel
            {
                OptionDate = DateTime.Today,
                Venue = "VisualStudio 2019"
            };

            // Act
            var result = await _service.Insert(option);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<OptionModelBase>();
            result.Id.Should().NotBe(0);
        }

        [Fact]
        public async Task UpdateOption()
        {
            // Arrange
            var option = new OptionUpdateModel
            {
                Id = 1,
                OptionDate = DateTime.Today,
                Venue = "VisualStudio 2019"
            };

            // Act
            var result = await _service.Update(option);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<OptionModelBase>();
            result.Id.Should().Be(option.Id);
            result.OptionDate.Should().Be(option.OptionDate);
            result.Venue.Should().Be(option.Venue);

        }

        [Fact]
        public async Task ThrowExceptionOnUpdateOption()
        {
            // Arrange
            var option = new OptionUpdateModel
            {
                Id = 10,
                OptionDate = DateTime.Today,
                Venue = "VisualStudio 2019"
            };

            // Act & Assert
            var ex = await Assert.ThrowsAsync<Exception>(() => _service.Update(option));
            Assert.Equal("Option not found", ex.Message);

        }

        [Fact]
        public async Task DeleteOption()
        {
            // Arrange
            var expected = 1;

            // Act
            var result = await _service.Delete(expected);
            var option = await _service.Get(expected);

            // Assert
            result.Should().Be(true);
            option.Should().BeNull();
        }
    }
}
