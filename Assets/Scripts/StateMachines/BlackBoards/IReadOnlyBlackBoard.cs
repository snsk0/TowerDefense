namespace StateMachines.BlackBoards
{
    public interface IReadOnlyBlackBoard
    {
        public T GetValue<T>(string name);
    }
}