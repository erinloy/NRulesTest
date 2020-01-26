using NRules;
using NRules.Diagnostics;
using System;
using System.Reactive.Linq;

namespace NRulesTest
{
    public static class SessionFactoryExtentions
    {
        public static ISessionFactory Log(this ISessionFactory factory)
        {
            Observable.FromEventPattern<AgendaEventArgs>(h => factory.Events.ActivationCreatedEvent += h, h => factory.Events.ActivationCreatedEvent -= h)
                .Subscribe(e => Console.WriteLine($"EVENT:ActivationCreatedEvent {e.EventArgs.Rule.Name}"));
            Observable.FromEventPattern<AgendaEventArgs>(h => factory.Events.ActivationDeletedEvent += h, h => factory.Events.ActivationDeletedEvent -= h)
                .Subscribe(e => Console.WriteLine($"EVENT:ActivationDeletedEvent {e.EventArgs.Rule.Name}"));
            Observable.FromEventPattern<AgendaEventArgs>(h => factory.Events.ActivationUpdatedEvent += h, h => factory.Events.ActivationUpdatedEvent -= h)
                .Subscribe(e => Console.WriteLine($"EVENT:ActivationUpdatedEvent {e.EventArgs.Rule.Name}"));
            Observable.FromEventPattern<AgendaExpressionEventArgs>(h => factory.Events.AgendaExpressionEvaluatedEvent += h, h => factory.Events.AgendaExpressionEvaluatedEvent -= h)
                .Subscribe(e => Console.WriteLine($"EVENT:AgendaExpressionEvaluatedEvent {e.EventArgs.Rule.Name}"));
            Observable.FromEventPattern<AgendaExpressionErrorEventArgs>(h => factory.Events.AgendaExpressionFailedEvent += h, h => factory.Events.AgendaExpressionFailedEvent -= h)
                .Subscribe(e => Console.WriteLine($"EVENT:AgendaExpressionFailedEvent {e.EventArgs.Rule.Name}"));
            Observable.FromEventPattern<WorkingMemoryEventArgs>(h => factory.Events.FactInsertedEvent += h, h => factory.Events.FactInsertedEvent -= h)
                .Subscribe(e => Console.WriteLine($"EVENT:FactInsertedEvent {e.EventArgs.Fact.Source} {e.EventArgs.Fact.Type.Name} {e.EventArgs.Fact.Value}"));
            //Observable.FromEventPattern<WorkingMemoryEventArgs>(h => factory.Events.FactInsertingEvent += h, h => factory.Events.FactInsertingEvent -= h)
            //    .Subscribe(e => Console.WriteLine($"EVENT:FactInsertingEvent {e.EventArgs.Fact.Source} {e.EventArgs.Fact.Type.Name} {e.EventArgs.Fact.Value}"));
            Observable.FromEventPattern<WorkingMemoryEventArgs>(h => factory.Events.FactRetractedEvent += h, h => factory.Events.FactRetractedEvent -= h)
                .Subscribe(e => Console.WriteLine($"EVENT:FactRetractedEvent {e.EventArgs.Fact.Source} {e.EventArgs.Fact.Type.Name} {e.EventArgs.Fact.Value}"));
            //Observable.FromEventPattern<WorkingMemoryEventArgs>(h => factory.Events.FactRetractingEvent += h, h => factory.Events.FactRetractingEvent -= h)
            //    .Subscribe(e => Console.WriteLine($"EVENT:FactRetractingEvent {e.EventArgs.Fact.Source} {e.EventArgs.Fact.Type.Name} {e.EventArgs.Fact.Value}"));
            Observable.FromEventPattern<WorkingMemoryEventArgs>(h => factory.Events.FactUpdatedEvent += h, h => factory.Events.FactUpdatedEvent -= h)
                .Subscribe(e => Console.WriteLine($"EVENT:FactUpdatedEvent {e.EventArgs.Fact.Source} {e.EventArgs.Fact.Type.Name} {e.EventArgs.Fact.Value}"));
            //Observable.FromEventPattern<WorkingMemoryEventArgs>(h => factory.Events.FactUpdatingEvent += h, h => factory.Events.FactUpdatingEvent -= h)
            //    .Subscribe(e => Console.WriteLine($"EVENT:FactUpdatingEvent {e.EventArgs.Fact.Source} {e.EventArgs.Fact.Type.Name} {e.EventArgs.Fact.Value}"));
            //Observable.FromEventPattern<LhsExpressionEventArgs>(h => factory.Events.LhsExpressionEvaluatedEvent += h, h => factory.Events.LhsExpressionEvaluatedEvent -= h)
            //    .Subscribe(e => Console.WriteLine($"EVENT:LhsExpressionEvaluatedEvent"));
            Observable.FromEventPattern<LhsExpressionErrorEventArgs>(h => factory.Events.LhsExpressionFailedEvent += h, h => factory.Events.LhsExpressionFailedEvent -= h)
                .Subscribe(e => Console.WriteLine($"EVENT:LhsExpressionFailedEvent"));
            //Observable.FromEventPattern<RhsExpressionEventArgs>(h => factory.Events.RhsExpressionEvaluatedEvent += h, h => factory.Events.RhsExpressionEvaluatedEvent -= h)
            //    .Subscribe(e => Console.WriteLine($"EVENT:RhsExpressionEvaluatedEvent"));
            Observable.FromEventPattern<RhsExpressionErrorEventArgs>(h => factory.Events.RhsExpressionFailedEvent += h, h => factory.Events.RhsExpressionFailedEvent -= h)
                .Subscribe(e => Console.WriteLine($"EVENT:RhsExpressionFailedEvent"));
            Observable.FromEventPattern<AgendaEventArgs>(h => factory.Events.RuleFiredEvent += h, h => factory.Events.RuleFiredEvent -= h)
                .Subscribe(e => Console.WriteLine($"EVENT:RuleFiredEvent"));
            //Observable.FromEventPattern<AgendaEventArgs>(h => factory.Events.RuleFiringEvent += h, h => factory.Events.RuleFiringEvent -= h)
            //    .Subscribe(e => Console.WriteLine($"EVENT:RuleFiringEvent"));
            return factory;
        }
    }


    //Save for later
    //Observable.FromEventPattern<AgendaEventArgs>(h => session.Events.ActivationCreatedEvent += h, h => session.Events.ActivationCreatedEvent -= h);
    //Observable.FromEventPattern<AgendaEventArgs>(h => session.Events.ActivationDeletedEvent += h, h => session.Events.ActivationDeletedEvent -= h);
    //Observable.FromEventPattern<AgendaEventArgs>(h => session.Events.ActivationUpdatedEvent += h, h => session.Events.ActivationUpdatedEvent -= h);
    //Observable.FromEventPattern<AgendaExpressionEventArgs>(h => session.Events.AgendaExpressionEvaluatedEvent += h, h => session.Events.AgendaExpressionEvaluatedEvent -= h);
    //Observable.FromEventPattern<AgendaExpressionErrorEventArgs>(h => session.Events.AgendaExpressionFailedEvent += h, h => session.Events.AgendaExpressionFailedEvent -= h);
    //Observable.FromEventPattern<WorkingMemoryEventArgs>(h => session.Events.FactInsertedEvent += h, h => session.Events.FactInsertedEvent -= h);
    //Observable.FromEventPattern<WorkingMemoryEventArgs>(h => session.Events.FactInsertingEvent += h, h => session.Events.FactInsertingEvent -= h);
    //Observable.FromEventPattern<WorkingMemoryEventArgs>(h => session.Events.FactRetractedEvent += h, h => session.Events.FactRetractedEvent -= h);
    //Observable.FromEventPattern<WorkingMemoryEventArgs>(h => session.Events.FactRetractingEvent += h, h => session.Events.FactRetractingEvent -= h);
    //Observable.FromEventPattern<WorkingMemoryEventArgs>(h => session.Events.FactUpdatedEvent += h, h => session.Events.FactUpdatedEvent -= h);
    //Observable.FromEventPattern<WorkingMemoryEventArgs>(h => session.Events.FactUpdatingEvent += h, h => session.Events.FactUpdatingEvent -= h);
    //Observable.FromEventPattern<LhsExpressionEventArgs>(h => session.Events.LhsExpressionEvaluatedEvent += h, h => session.Events.LhsExpressionEvaluatedEvent -= h);
    //Observable.FromEventPattern<LhsExpressionErrorEventArgs>(h => session.Events.LhsExpressionFailedEvent += h, h => session.Events.LhsExpressionFailedEvent -= h);
    //Observable.FromEventPattern<RhsExpressionEventArgs>(h => session.Events.RhsExpressionEvaluatedEvent += h, h => session.Events.RhsExpressionEvaluatedEvent -= h);
    //Observable.FromEventPattern<RhsExpressionErrorEventArgs>(h => session.Events.RhsExpressionFailedEvent += h, h => session.Events.RhsExpressionFailedEvent -= h);
    //Observable.FromEventPattern<AgendaEventArgs>(h => session.Events.RuleFiredEvent += h, h => session.Events.RuleFiredEvent -= h);
    //Observable.FromEventPattern<AgendaEventArgs>(h => session.Events.RuleFiringEvent += h, h => session.Events.RuleFiringEvent -= h);
}


