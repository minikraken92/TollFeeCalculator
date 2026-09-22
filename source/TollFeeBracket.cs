namespace TollFeeCalculator
{
    public record TollFeeBracket(TimeOnly Start, TimeOnly? End, int Fee)
    {
        public bool Contains(TimeOnly time)
        {
            return time >= Start && (End == null || time < End);
        }
    }
}
