using System;
using System.Threading.Tasks;
using System.Threading;
using QueryStress.Core.LoadProfiles;
using Xunit;

namespace QueryStress.Tests
{
    public class SequentialLoadProfileWithDelayTests
    {
        [Fact]
        public void WhenNextCanBeExecutedAsync_FirstCall_ReturnsCompletedTask()
        {
            var profile = new SequentialWithDelayLoadProfile(TimeSpan.FromMilliseconds(500));
            var task = profile.WhenNextCanBeExecutedAsync(CancellationToken.None);

            Assert.True(task.IsCompletedSuccessfully);
        }

        [Fact]
        public async Task WhenNextCanBeExecutedAsync_SecondCall_CompletesOnlyAfter_WhenQueryExecutor_Called_WithDelay()
        {
            var profile = new SequentialWithDelayLoadProfile(TimeSpan.FromMilliseconds(50));
            var _ = profile.WhenNextCanBeExecutedAsync(CancellationToken.None);
            var task = profile.WhenNextCanBeExecutedAsync(CancellationToken.None);

            await Task.Delay(60);
            Assert.False(task.IsCompleted);

            await profile.OnQueryExecutedAsync(null, CancellationToken.None);

            await Task.Delay(10);
            Assert.False(task.IsCompleted);

            await Task.Delay(50);
            Assert.True(task.IsCompletedSuccessfully);
        }
    }
}
