using System.Threading;
using System.Threading.Tasks;
using QueryStress.Core.LoadProfiles;
using Xunit;

namespace QueryStress.Tests
{
    public class SequentialLoadProfileTests
    {
        [Fact]
        public void WhenNextCanBeExecutedAsync_FirstCall_ReturnsCompletedTask()
        {
            var profile = new SequentialLoadProfile();
            var task = profile.WhenNextCanBeExecutedAsync(CancellationToken.None);

            Assert.True(task.IsCompletedSuccessfully);
        }

        [Fact]
        public async Task WhenNextCanBeExecutedAsync_SecondCall_CompletedOnlyAfter_WhenQueryExecutor_Called()
        {
            var profile = new SequentialLoadProfile();
            var _ = profile.WhenNextCanBeExecutedAsync(CancellationToken.None);
            var task = profile.WhenNextCanBeExecutedAsync(CancellationToken.None);

            await Task.Delay(10);
            Assert.False(task.IsCompleted);

            await profile.OnQueryExecutedAsync(null, CancellationToken.None);

            await Task.Delay(10);
            Assert.True(task.IsCompletedSuccessfully);
        }
    }
}