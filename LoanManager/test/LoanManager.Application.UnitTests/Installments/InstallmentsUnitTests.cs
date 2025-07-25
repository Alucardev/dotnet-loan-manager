using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using FluentAssertions;
using LoanManager.Application.Abstractions.Data;
using LoanManager.Application.Installments.GetInstallments;
using LoanManager.Domain.Abstractions;
using Moq;
using Xunit;

namespace LoanManager.Application.UnitTests.Installments;

public class GetInstallmentsQueryHandlerTests
{
    [Fact]
    public async Task Handle_Should_CallConnectionAndReturnResult()
    {
        // Arrange
        var mockFactory = new Mock<ISqlConnectionFactory>();
        var mockConnection = new Mock<IDbConnection>();
        mockFactory.Setup(f => f.CreateConnection()).Returns(mockConnection.Object);

        // Simular Dapper QueryMultipleAsync
        var mockGridReader = new Mock<SqlMapper.GridReader>(null, null);
        mockConnection.Setup(c => c.QueryMultipleAsync(It.IsAny<string>(), It.IsAny<object>(), null, null, null))
            .ReturnsAsync(mockGridReader.Object);

        // Simular lectura de resultados
        mockGridReader.Setup(g => g.ReadAsync<GetInstallmentsQueryResponse>(null)).ReturnsAsync(new List<GetInstallmentsQueryResponse>());
        mockGridReader.Setup(g => g.ReadFirstAsync<int>(null)).ReturnsAsync(0);

        var handler = new GetInstallmentsQueryHandler(mockFactory.Object);
        var query = new GetInstallmentsQuery(null, null, null, 1, 10, null, true);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
    }
}
