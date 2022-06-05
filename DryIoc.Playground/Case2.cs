namespace DryIoc.Playground;

public static class Case2
{
    public static void Do()
    {
        var container = new Container();

        container.RegisterInstance("bar");
        container.Register<Dependency>();
        container.Register<InnerDependency>();

        var instance = container.Resolve<Dependency>();

        Console.WriteLine($"instance.Test0: {instance.Test0.Test0}");
    }

    public class Dependency
    {
        public InnerDependency Test0 { get; }

        public Dependency(
            Func<string, InnerDependency> factory1) =>
            Test0 = factory1("foo");
    }

    public class InnerDependency
    {
        private readonly string _test0;

        public string Test0 => 
            _test0;

        public InnerDependency(string test0) => _test0 = test0;
    }
}