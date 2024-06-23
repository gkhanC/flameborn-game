namespace HF.Commands
{
    public abstract class Command<T>
    {
        public abstract bool Execute(T controller);
        public abstract bool Undo(T controller);
    }
}