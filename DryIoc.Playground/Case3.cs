namespace DryIoc.Playground;

public static class Case3
{
    public static void Do()
    {
        var container = new Container();

        container.RegisterInstance(0);
        container.Register<DependencyA>();
        container.Register<DependencyB>();
        container.Register<Parent>();

        var parent = container.Resolve<Parent>();
        
        Console.WriteLine(parent.Dependency.Value); // 0
        Console.WriteLine(parent.Dependency.Dependency.Value); // 23
        Console.WriteLine(parent.Dependency.Dependency.Parent.Dependency.Value); // 23
    }

    internal class DependencyA
    {
        public int Value { get; }
        public DependencyB Dependency { get; }

        public DependencyA(
            int value,
            Func<int, DependencyB> dependencyB)
        {
            Value = value;
            Dependency = dependencyB(23);
        }
    }


    internal class DependencyB
    {
        private readonly Lazy<Parent> _parentFactory;
        public int Value { get; }
        public Parent Parent => _parentFactory.Value;

        public DependencyB(
            int value,
            Lazy<Parent> parentFactory)
        {
            _parentFactory = parentFactory;
            Value = value;
        }
    }

    internal class Parent
    {
        public DependencyA Dependency { get; }
    
        public Parent(
            DependencyA dependencyA) =>
            Dependency = dependencyA;
    }
}