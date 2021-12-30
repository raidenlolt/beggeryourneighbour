using System;
using System.Collections.Generic;
using System.Linq;

namespace Framework.StateSystem
{
    public class StateMachine
    {
        private IState currentState;

        private readonly Dictionary<Type, List<Transition>> transitions = new Dictionary<Type, List<Transition>>();
        private List<Transition> currentTransitions = new List<Transition>();
        private readonly List<Transition> anyTransitions = new List<Transition>();

        private static readonly List<Transition> EmptyTransitions = new List<Transition>(0);

        public void Tick()
        {
            var transition = GetTransition();
            if (transition != null)
                SetState(transition.To);

            currentState?.Tick();
        }

        public void SetState(IState state)
        {
            if (state == currentState)
                return;

            currentState?.OnExit();
            currentState = state;

            transitions.TryGetValue(currentState.GetType(), out currentTransitions);
            if (currentTransitions == null)
                currentTransitions = EmptyTransitions;

            currentState.OnEnter();
        }

        public void AddTransition(IState from, IState to, Func<bool> predicate)
        {
            if (this.transitions.TryGetValue(from.GetType(), out var transitionList) == false)
            {
                transitionList = new List<Transition>();
                this.transitions[from.GetType()] = transitionList;
            }

            transitionList.Add(new Transition(to, predicate));
        }

        public void AddAnyTransition(IState state, Func<bool> predicate)
        {
            anyTransitions.Add(new Transition(state, predicate));
        }

        private Transition GetTransition()
        {
            foreach (var transition in anyTransitions.Where(transition => transition.Condition()))
                return transition;

            return currentTransitions.FirstOrDefault(transition => transition.Condition());
        }

        private class Transition
        {
            public Func<bool> Condition { get; }
            public IState To { get; }

            public Transition(IState to, Func<bool> condition)
            {
                To = to;
                Condition = condition;
            }
        }
    }
}