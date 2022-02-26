using System.Collections.Generic;
using System.Linq;

namespace TextEditor.Commands
{
    public class UndoingHistory
    {
        private readonly Stack<Command> history = new Stack<Command>();
        public bool IsEmpty => !history.Any();

        public void Push(Command command) => history.Push(command);
        public Command Pop() => history.Pop();
    }
}
