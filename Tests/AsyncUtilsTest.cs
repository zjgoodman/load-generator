using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using mParticle.LoadGenerator;
using System.Collections.Generic;

namespace Tests
{
    public class AsyncUtilsTest
    {
        [Fact]
        public async void TestFlattenListOfTasks()
        {
            List<Task<int>> taskList = new List<Task<int>> {
                Task.FromResult(1),
                Task.FromResult(2),
                Task.FromResult(3),
                Task.FromResult(4)
            };
            var actualResult = await AsyncUtils.FlattenListOfTasks(taskList);

            List<int> expectedResult = new List<int> {1,2,3,4};
            Assert.Equal(expectedResult, actualResult);
        }
    }
}