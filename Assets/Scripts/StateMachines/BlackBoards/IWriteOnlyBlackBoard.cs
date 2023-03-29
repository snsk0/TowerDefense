namespace StateMachines.BlackBoards
{
    public interface IWriteOnlyBlackBoard
    {
        public bool SetValue<T>(string name, T value);
    }
}
