using System.Threading;
using System.Threading.Tasks;
using QueryStress.Core.LoadProfiles;
using Xunit;

namespace QueryStress.Tests
{
    public class TargetThroughputLoadProfileTests
    {
        [Fact]
        public void WhenNextCanBeExecutedAsync_FirstCall_Return_CompletedTask()
        {
            var profile = new TargetThroughputLoadProfile(10);
            var task = profile.WhenNextCanBeExecutedAsync(CancellationToken.None);

            Assert.True(task.IsCompleted);
        }

        [Fact]
        public async Task WhenNextCanBeExecutedAsync_SecondCall_Return_CompletedTaskOnlyAfterDelay()
        {
            var profile = new TargetThroughputLoadProfile(2);
            var _ = profile.WhenNextCanBeExecutedAsync(CancellationToken.None);
            var task = profile.WhenNextCanBeExecutedAsync(CancellationToken.None);

            await Task.Delay(100);
            Assert.False(task.IsCompleted);

            await Task.Delay(500);
            Assert.True(task.IsCompleted);
        }

        [Fact]
        public async Task WhenNextCanBeExecutedAsync_IfCalledWithoutDelay_CompletionAreDistributed_InTime()
        {
            var profile = new TargetThroughputLoadProfile(2);
            var _ = profile.WhenNextCanBeExecutedAsync(CancellationToken.None);
            _ = profile.WhenNextCanBeExecutedAsync(CancellationToken.None);
            var task = profile.WhenNextCanBeExecutedAsync(CancellationToken.None);

            await Task.Delay(750);
            Assert.False(task.IsCompleted);

            await Task.Delay(300);
            Assert.True(task.IsCompleted);
        }
    }
}
