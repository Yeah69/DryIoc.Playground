namespace DryIoc.Playground;

public static class Case1
{
    public static void Do()
    {
        var container = new Container();

        container.RegisterInstance("bar");
        container.Register<Dependency>();
        container.Register<InnerDependency>();

        var resolve = container.Resolve<Func<string, Dependency>>();

        var instance = resolve("foo");

        Console.WriteLine($"instance.Test0: {instance.Test0}, instance.Test1.Test0: {instance.Test1.Test0}");
    }

    public class Dependency
    {
        public string Test0 { get; }

        public InnerDependency Test1 { get; }

        public Dependency(
            string test0, 
            Func<string, InnerDependency> factory1)
        {
            Test0 = test0;
            Test1 = factory1("bar");
        }
    }

    public class InnerDependency
    {
        public string Test0 { get; }

        public InnerDependency(string test0) => Test0 = test0;
    }
}