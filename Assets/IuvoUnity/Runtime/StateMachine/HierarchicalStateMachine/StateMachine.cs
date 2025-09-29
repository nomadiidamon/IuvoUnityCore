using System.Collections.Generic;
using IuvoUnity.BaseClasses;

namespace IuvoUnity
{
    namespace StateMachines
    {
        namespace HSM
        {
            public class StateMachine : IDataStructBase
            {
                public readonly State Root;
                public readonly TransitionSequencer Sequencer;
                bool started;

                public StateMachine(State root)
                {
                    Root = root;
                    Sequencer = new TransitionSequencer(this);
                }

                public void Start()
                {
                    if (started) return;

                    started = true;
                    Root.Enter();
                }

                public void Tick(float deltaTime)
                {
                    if (!started) return;
                    InternalTick(deltaTime);
                }

                internal void InternalTick(float deltaTime) => Root.Update(deltaTime);


                // Performs the actual switch 'from' to 'to' by exiting up to the LCA and entering down to 'to'
                public void ChangeState(State from, State to)
                {
                    if (from == to || from == null || to == null) return;

                    State lca = TransitionSequencer.LCA(from, to);

                    // exit current branch upt to (not including) LCA
                    for (State s = from; s != lca; s = s.Parent) s.Exit();

                    // Enter target branch from LCA down to target
                    var stack = new Stack<State>();
                    for (State s = to; s != lca; s = s.Parent) stack.Push(s);
                    while (stack.Count > 0) stack.Pop().Enter();

                }
            }

        }
    }
}
