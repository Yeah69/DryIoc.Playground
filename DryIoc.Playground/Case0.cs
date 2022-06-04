namespace DryIoc.Playground;

public static class Case0
{
    public static void Do()
    {
        var container = new Container();

        container.RegisterInstance("bar");
        container.Register<Dependency>();
        container.Register<InnerDependency>();

        var resolve = container.Resolve<Func<string, Dependency>>();

        var instance = resolve("foo");

        Console.WriteLine($"instance.Test0: {instance.Test0}, instance.Test1.Test0: {instance.Test1.Test0}, instance.Test1.Test1: {instance.Test1.Test1}");
    }

    public class Dependency
    {
        public string Test0 { get; }

        public InnerDependency Test1 { get; }

        public Dependency(
            string test0, 
            Func<int, InnerDependency> factory1)
        {
            Test0 = test0;
            Test1 = factory1(23);
        }
    }

    public class InnerDependency
    {
        public string Test0 { get; }
        public int Test1 { get; }

        public InnerDependency(string test0, int test1)
        {
            Test0 = test0;
            Test1 = test1;
        }
    }
}