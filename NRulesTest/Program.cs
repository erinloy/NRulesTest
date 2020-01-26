using NRules;
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
using System.Threading.Tasks;

namespace NRulesTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Random rnd = new Random();

            List<Task> tasks = new List<Task>();

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            for (int p = 0; p < Environment.ProcessorCount; p++)
            {
                tasks.Add(Task.Run(() =>
                {
                    //Initialize once, memory will grow
                    var processor = new Processor<int>();

                    //while (true)
                    for (int i = 0; i < 100000; i++)
                    {
                        //Initialize every loop, memory will remain low
                        //var processor = new Processor<int>();

                        var x = new Property<int>(rnd.Next());
                        var y = new Property<int>(rnd.Next());

                        var result = processor.Add(x, y);
                        //Console.WriteLine(result.Value);
                    }
                }));
            }

            Task.WaitAll(tasks.ToArray());

            stopwatch.Stop();
            Console.WriteLine(stopwatch.Elapsed);
        }
    }

    public class Processor<T>
    {
        RuleRepository repository;

        Dictionary<string, Lazy<ISessionFactory>> sessions;

        public Processor()
        {
            repository = new RuleRepository();
            sessions = new Dictionary<string, Lazy<ISessionFactory>>();

            var rc = new RuleCompiler();

            var addFactory = new Lazy<ISessionFactory>(() =>
            {
                repository.Load(x => x.To("add").From(typeof(AddRule<T>)));
                var addRuleset = repository.GetRuleSets().Single(r => r.Name == "add");
                return rc.Compile(addRuleset.Rules)/*.Log()*/;
            });

            sessions.Add("add", addFactory);
        }

        public Property<T> Add(Property<T> value1, Property<T> value2)
        {
            var session = sessions["add"].Value.CreateSession();
            var payload = new AddInstruction<T>(value1, value2);
            try
            {
                session.Insert(payload);
                session.Fire();
                return session.Query<Property<T>>().Single();
            }
            finally
            {
                session.Retract(payload);
            }
        }
    }

    public class AddInstruction<T>
    {
        public Property<T> LeftValue { get; }
        public Property<T> RightValue { get; }

        public AddInstruction(Property<T> value1, Property<T> value2)
        {
            LeftValue = value1;
            RightValue = value2;
        }

        public override string ToString()
        {
            return $"{LeftValue} + {RightValue}";
        }
    }

    [Name("Add")]
    [Repeatability(RuleRepeatability.NonRepeatable)]
    public class AddRule<T> : Rule
    {
        public override void Define()
        {
            AddInstruction<T> instruction = default;

            When()
                .Match(() => instruction);

            Then()
                .Yield(ctx => Add(instruction.LeftValue, instruction.RightValue));
        }

        public virtual Property<T> Add(Property<T> v1, Property<T> v2)
        {
            return new Property<T>((T)(dynamic)v1.Value + (dynamic)v2.Value);
        }
    }

    public class Property<T>
    {
        public T Value { get; }

        public Property(T value)
        {
            Value = value;
        }

        public override string ToString()
        {
            return $"{Value}";
        }
    }
}


