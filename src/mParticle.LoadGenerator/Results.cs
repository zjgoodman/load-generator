namespace mParticle.LoadGenerator
{
    public class Results
    {
        public int SuccessCount { get; set; }
        public int FailCount { get; set; }
        public Results(int successCount, int failCount)
        {
            this.SuccessCount = successCount;
            this.FailCount = failCount;
        }
        public int TotalCount
        {
            get => SuccessCount + FailCount;
        }
    }
}