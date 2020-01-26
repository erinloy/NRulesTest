using NRules;
using NRules.Diagnostics;
using NRules.Fluent;
using NRules.Fluent.Dsl;
using NRules.RuleModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Linq;

namespace NRulesTest
{
    class Program
    {
        static void Main(string[] args)
        {
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


            //Load domain model

            Random rnd = new Random();

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            for (int i = 0; i < 100000; i++)
            {
                var x = new Is<int>(rnd.Next());
                var y = new Is<int>(rnd.Next());

                var result = Actions<int>.Add(x, y);
                //Console.WriteLine(result.Value);
            }
            stopwatch.Stop();
            Console.WriteLine(stopwatch.Elapsed);
        }


    }

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

    //public class IsDiscounted : Rule
    //{
    //    public override void Define()
    //    {
    //        Order order = null;

    //        When()
    //            .Match<Order>(() => order);

    //        Then()
    //            .Do(ctx =>);
    //    }
    //}

    //public class PreferredCustomerDiscountRule : Rule
    //{
    //    public override void Define()
    //    {
    //        Customer customer = null;
    //        IEnumerable<Order> orders = null;

    //        When()
    //            .Match<Customer>(() => customer, c => c.IsPreferred)
    //            .Query(() => orders, x => x
    //                .Match<Order>(
    //                    o => o.Customer == customer,
    //                    o => o.IsOpen,
    //                    o => !o.IsDiscounted)
    //                .Collect()
    //                .Where(c => c.Any()));

    //        Then()
    //            .Do(ctx => ApplyDiscount(orders, 10.0))
    //            .Do(ctx => ctx.UpdateAll(orders));
    //    }

    //    private static void ApplyDiscount(IEnumerable<Order> orders, double discount)
    //    {
    //        foreach (var order in orders)
    //        {
    //            Console.WriteLine("Order {0} applying a discount", order.Id);
    //            order.ApplyDiscount(discount);
    //        }
    //    }
    //}
    //public class DiscountNotificationRule : Rule
    //{
    //    public override void Define()
    //    {
    //        Customer customer = null;

    //        When()
    //            .Match<Customer>(() => customer)
    //            .Exists<Order>(o => o.Customer == customer, o => o.PercentDiscount > 0.0);

    //        Then()
    //            .Do(_ => customer.NotifyAboutDiscount());
    //    }
    //}

    //Standard actions for any given data type
    public static class Actions<T>
    {
        static RuleRepository repository;
        static Dictionary<string, Lazy<ISession>> sessions;

        static Actions()
        {
            repository = new RuleRepository();
            sessions = new Dictionary<string, Lazy<ISession>>();

            var rc = new RuleCompiler();

            var addFactory = new Lazy<ISession>(() =>
            {
                repository.Load(x => x.To("add").From(typeof(AddRule<T>)));
                var addRuleset = repository.GetRuleSets().Single(r => r.Name == "add");
                return rc.Compile(addRuleset.Rules)/*.Log()*/.CreateSession();
            });

            sessions.Add("add", addFactory);
        }

        public static Is<T> Add(Is<T> value1, Is<T> value2)
        {
            var session = sessions["add"].Value;
            var payload = new AddPayload<T>(value1, value2);
            try
            {
                session.Insert(payload);
                session.Fire();
                return session.Query<Is<T>>().Single();
            }
            finally
            {
                session.Retract(payload);
            }
        }
    }

    public class AddPayload<T>
    {
        public Is<T> Value1 { get; }
        public Is<T> Value2 { get; }

        public AddPayload(Is<T> value1, Is<T> value2)
        {
            Value1 = value1;
            Value2 = value2;
        }
    }

    public class AddRule<T> : Rule
    {
        public override void Define()
        {
            AddPayload<T> payload = default;

            When()
                .Match<AddPayload<T>>(() => payload);

            Then()
                .Yield(ctx => new Is<T>(Add(payload.Value1, payload.Value2)));
        }

        public virtual T Add(Is<T> v1, Is<T> v2)
        {
            return (T)(dynamic)v1.Value + (dynamic)v2.Value;
        }
    }

    public interface Unit<T>
    {

    }

    public class Has<T> : Unit<T>
    {
        public string Name { get; }
        public T Value { get; }

        public Has(string name, T value)
        {
            Name = name;
            Value = value;
        }
    }

    public class Is<T> : Unit<T>
    {
        public T Value { get; }

        public Is(T value)
        {
            Value = value;
        }
    }

    public class Was<T> : Unit<T>
    {
        public T Value { get; }

        public Was(T value)
        {
            Value = value;
        }
    }

}


//using NRules;
//using NRules.Diagnostics;
//using NRules.Fluent;
//using NRules.Fluent.Dsl;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Reactive;
//using System.Reactive.Linq;

//namespace NRulesTest
//{
//    class Program
//    {
//        static void Main(string[] args)
//        {
//            //Load rules
//            var repository = new RuleRepository();
//            repository.Load(x => x.From(typeof(PreferredCustomerDiscountRule).Assembly));

//            //Compile rules
//            var factory = repository.Compile();

//            Observable.FromEventPattern<AgendaEventArgs>(h => factory.Events.ActivationCreatedEvent += h, h => factory.Events.ActivationCreatedEvent -= h)
//                .Subscribe(e => Console.WriteLine($"EVENT:ActivationCreatedEvent {e.EventArgs.Rule.Name}"));
//            Observable.FromEventPattern<AgendaEventArgs>(h => factory.Events.ActivationDeletedEvent += h, h => factory.Events.ActivationDeletedEvent -= h)
//                .Subscribe(e => Console.WriteLine($"EVENT:ActivationDeletedEvent {e.EventArgs.Rule.Name}"));
//            Observable.FromEventPattern<AgendaEventArgs>(h => factory.Events.ActivationUpdatedEvent += h, h => factory.Events.ActivationUpdatedEvent -= h)
//                .Subscribe(e => Console.WriteLine($"EVENT:ActivationUpdatedEvent {e.EventArgs.Rule.Name}"));
//            Observable.FromEventPattern<AgendaExpressionEventArgs>(h => factory.Events.AgendaExpressionEvaluatedEvent += h, h => factory.Events.AgendaExpressionEvaluatedEvent -= h)
//                .Subscribe(e => Console.WriteLine($"EVENT:AgendaExpressionEvaluatedEvent {e.EventArgs.Rule.Name}"));
//            Observable.FromEventPattern<AgendaExpressionErrorEventArgs>(h => factory.Events.AgendaExpressionFailedEvent += h, h => factory.Events.AgendaExpressionFailedEvent -= h)
//                .Subscribe(e => Console.WriteLine($"EVENT:AgendaExpressionFailedEvent {e.EventArgs.Rule.Name}"));
//            Observable.FromEventPattern<WorkingMemoryEventArgs>(h => factory.Events.FactInsertedEvent += h, h => factory.Events.FactInsertedEvent -= h)
//                .Subscribe(e => Console.WriteLine($"EVENT:FactInsertedEvent {e.EventArgs.Fact.Source} {e.EventArgs.Fact.Type.Name} {e.EventArgs.Fact.Value}"));
//            //Observable.FromEventPattern<WorkingMemoryEventArgs>(h => factory.Events.FactInsertingEvent += h, h => factory.Events.FactInsertingEvent -= h)
//            //    .Subscribe(e => Console.WriteLine($"EVENT:FactInsertingEvent {e.EventArgs.Fact.Source} {e.EventArgs.Fact.Type.Name} {e.EventArgs.Fact.Value}"));
//            Observable.FromEventPattern<WorkingMemoryEventArgs>(h => factory.Events.FactRetractedEvent += h, h => factory.Events.FactRetractedEvent -= h)
//                .Subscribe(e => Console.WriteLine($"EVENT:FactRetractedEvent {e.EventArgs.Fact.Source} {e.EventArgs.Fact.Type.Name} {e.EventArgs.Fact.Value}"));
//            //Observable.FromEventPattern<WorkingMemoryEventArgs>(h => factory.Events.FactRetractingEvent += h, h => factory.Events.FactRetractingEvent -= h)
//            //    .Subscribe(e => Console.WriteLine($"EVENT:FactRetractingEvent {e.EventArgs.Fact.Source} {e.EventArgs.Fact.Type.Name} {e.EventArgs.Fact.Value}"));
//            Observable.FromEventPattern<WorkingMemoryEventArgs>(h => factory.Events.FactUpdatedEvent += h, h => factory.Events.FactUpdatedEvent -= h)
//                .Subscribe(e => Console.WriteLine($"EVENT:FactUpdatedEvent {e.EventArgs.Fact.Source} {e.EventArgs.Fact.Type.Name} {e.EventArgs.Fact.Value}"));
//            //Observable.FromEventPattern<WorkingMemoryEventArgs>(h => factory.Events.FactUpdatingEvent += h, h => factory.Events.FactUpdatingEvent -= h)
//            //    .Subscribe(e => Console.WriteLine($"EVENT:FactUpdatingEvent {e.EventArgs.Fact.Source} {e.EventArgs.Fact.Type.Name} {e.EventArgs.Fact.Value}"));
//            //Observable.FromEventPattern<LhsExpressionEventArgs>(h => factory.Events.LhsExpressionEvaluatedEvent += h, h => factory.Events.LhsExpressionEvaluatedEvent -= h)
//            //    .Subscribe(e => Console.WriteLine($"EVENT:LhsExpressionEvaluatedEvent"));
//            Observable.FromEventPattern<LhsExpressionErrorEventArgs>(h => factory.Events.LhsExpressionFailedEvent += h, h => factory.Events.LhsExpressionFailedEvent -= h)
//                .Subscribe(e => Console.WriteLine($"EVENT:LhsExpressionFailedEvent"));
//            //Observable.FromEventPattern<RhsExpressionEventArgs>(h => factory.Events.RhsExpressionEvaluatedEvent += h, h => factory.Events.RhsExpressionEvaluatedEvent -= h)
//            //    .Subscribe(e => Console.WriteLine($"EVENT:RhsExpressionEvaluatedEvent"));
//            Observable.FromEventPattern<RhsExpressionErrorEventArgs>(h => factory.Events.RhsExpressionFailedEvent += h, h => factory.Events.RhsExpressionFailedEvent -= h)
//                .Subscribe(e => Console.WriteLine($"EVENT:RhsExpressionFailedEvent"));
//            Observable.FromEventPattern<AgendaEventArgs>(h => factory.Events.RuleFiredEvent += h, h => factory.Events.RuleFiredEvent -= h)
//                .Subscribe(e => Console.WriteLine($"EVENT:RuleFiredEvent"));
//            //Observable.FromEventPattern<AgendaEventArgs>(h => factory.Events.RuleFiringEvent += h, h => factory.Events.RuleFiringEvent -= h)
//            //    .Subscribe(e => Console.WriteLine($"EVENT:RuleFiringEvent"));

//            //Create a working session
//            var session = factory.CreateSession();

//            Observable.FromEventPattern<AgendaEventArgs>(h => session.Events.ActivationCreatedEvent += h, h => session.Events.ActivationCreatedEvent -= h);
//            Observable.FromEventPattern<AgendaEventArgs>(h => session.Events.ActivationDeletedEvent += h, h => session.Events.ActivationDeletedEvent -= h);
//            Observable.FromEventPattern<AgendaEventArgs>(h => session.Events.ActivationUpdatedEvent += h, h => session.Events.ActivationUpdatedEvent -= h);
//            Observable.FromEventPattern<AgendaExpressionEventArgs>(h => session.Events.AgendaExpressionEvaluatedEvent += h, h => session.Events.AgendaExpressionEvaluatedEvent -= h);
//            Observable.FromEventPattern<AgendaExpressionErrorEventArgs>(h => session.Events.AgendaExpressionFailedEvent += h, h => session.Events.AgendaExpressionFailedEvent -= h);
//            Observable.FromEventPattern<WorkingMemoryEventArgs>(h => session.Events.FactInsertedEvent += h, h => session.Events.FactInsertedEvent -= h);
//            Observable.FromEventPattern<WorkingMemoryEventArgs>(h => session.Events.FactInsertingEvent += h, h => session.Events.FactInsertingEvent -= h);
//            Observable.FromEventPattern<WorkingMemoryEventArgs>(h => session.Events.FactRetractedEvent += h, h => session.Events.FactRetractedEvent -= h);
//            Observable.FromEventPattern<WorkingMemoryEventArgs>(h => session.Events.FactRetractingEvent += h, h => session.Events.FactRetractingEvent -= h);
//            Observable.FromEventPattern<WorkingMemoryEventArgs>(h => session.Events.FactUpdatedEvent += h, h => session.Events.FactUpdatedEvent -= h);
//            Observable.FromEventPattern<WorkingMemoryEventArgs>(h => session.Events.FactUpdatingEvent += h, h => session.Events.FactUpdatingEvent -= h);
//            Observable.FromEventPattern<LhsExpressionEventArgs>(h => session.Events.LhsExpressionEvaluatedEvent += h, h => session.Events.LhsExpressionEvaluatedEvent -= h);
//            Observable.FromEventPattern<LhsExpressionErrorEventArgs>(h => session.Events.LhsExpressionFailedEvent += h, h => session.Events.LhsExpressionFailedEvent -= h);
//            Observable.FromEventPattern<RhsExpressionEventArgs>(h => session.Events.RhsExpressionEvaluatedEvent += h, h => session.Events.RhsExpressionEvaluatedEvent -= h);
//            Observable.FromEventPattern<RhsExpressionErrorEventArgs>(h => session.Events.RhsExpressionFailedEvent += h, h => session.Events.RhsExpressionFailedEvent -= h);
//            Observable.FromEventPattern<AgendaEventArgs>(h => session.Events.RuleFiredEvent += h, h => session.Events.RuleFiredEvent -= h);
//            Observable.FromEventPattern<AgendaEventArgs>(h => session.Events.RuleFiringEvent += h, h => session.Events.RuleFiringEvent -= h);


//            //Load domain model
//            var customer = new Customer("John Doe") { IsPreferred = true };
//            var order1 = new Order(123456, customer, 2, 25.0);
//            var order2 = new Order(123457, customer, 1, 100.0);

//            //Insert facts into rules engine's memory
//            session.Insert(customer);
//            session.Insert(order1);
//            session.Insert(order2);

//            //Start match/resolve/act cycle
//            session.Fire();

//            var order3 = new Order(123458, customer, 1, 100.0);
//            session.Insert(order3);

//            session.Fire();

//            //Console.WriteLine("Hello World!");
//        }
//    }

//    public class IsDiscounted : Rule
//    {
//        public override void Define()
//        {
//            Order order = null;

//            When()
//                .Match<Order>(() => order);

//            Then()
//                .Do(ctx =>);
//        }
//    }


//    public class PreferredCustomerDiscountRule : Rule
//    {
//        public override void Define()
//        {
//            Customer customer = null;
//            IEnumerable<Order> orders = null;

//            When()
//                .Match<Customer>(() => customer, c => c.IsPreferred)
//                .Query(() => orders, x => x
//                    .Match<Order>(
//                        o => o.Customer == customer,
//                        o => o.IsOpen,
//                        o => !o.IsDiscounted)
//                    .Collect()
//                    .Where(c => c.Any()));

//            Then()
//                .Do(ctx => ApplyDiscount(orders, 10.0))
//                .Do(ctx => ctx.UpdateAll(orders));
//        }

//        private static void ApplyDiscount(IEnumerable<Order> orders, double discount)
//        {
//            foreach (var order in orders)
//            {
//                Console.WriteLine("Order {0} applying a discount", order.Id);
//                order.ApplyDiscount(discount);
//            }
//        }
//    }
//    public class DiscountNotificationRule : Rule
//    {
//        public override void Define()
//        {
//            Customer customer = null;

//            When()
//                .Match<Customer>(() => customer)
//                .Exists<Order>(o => o.Customer == customer, o => o.PercentDiscount > 0.0);

//            Then()
//                .Do(_ => customer.NotifyAboutDiscount());
//        }
//    }

//    public class Customer
//    {
//        public string Name { get; private set; }
//        public bool IsPreferred { get; set; }

//        public Customer(string name)
//        {
//            Name = name;
//        }

//        public void NotifyAboutDiscount()
//        {
//            Console.WriteLine("Customer {0} was notified about a discount", Name);
//        }
//    }

//    public class Order
//    {
//        public int Id { get; private set; }
//        public Customer Customer { get; private set; }
//        public int Quantity { get; private set; }
//        public double UnitPrice { get; private set; }
//        public double PercentDiscount { get; set; }
//        public bool IsDiscounted { get { return PercentDiscount > 0; } }

//        public bool IsOpen { get; private set; }

//        public double Price
//        {
//            get { return UnitPrice * Quantity * (1.0 - PercentDiscount / 100.0); }
//        }

//        public Order(int id, Customer customer, int quantity, double unitPrice)
//        {
//            Id = id;
//            Customer = customer;
//            Quantity = quantity;
//            UnitPrice = unitPrice;
//            IsOpen = true;
//        }

//        public void ApplyDiscount(double percentDiscount)
//        {
//            PercentDiscount = percentDiscount;
//        }
//    }


//}
