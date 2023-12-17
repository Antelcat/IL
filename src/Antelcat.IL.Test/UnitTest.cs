using System.Diagnostics;
using System.Reflection;
using Antelcat.IL.Extensions;

namespace Antelcat.IL.Test;

public class RefClass
{
    public static int Count = 0;
    private readonly int Number = ++Count;
    public override string ToString() => $"This is RefClass No.{Number}";
}
public class TestClass
{
    public RefClass RefProp { get; set; }
    public RefClass RefField;

    public int ValueProp { get; set; } = 0;
    public int ValueField = 0;
    
    public static RefClass StaticRefProp { get; set; } = new();
    public static RefClass StaticRefField = new();

    public static int StaticValueProp { get; set; } = 0;
    public static int StaticValueField = 0;

    public static int StaticMethod(int value, out int result)
    {
        result = value++;
        return value;
    }

    public int Method(ref int value) => --value;
}

public static class ConsoleExtension
{
    public static void WriteLine(this object? o) => Console.WriteLine(o);
}

public class Tests
{
    public static Type Type =  typeof(TestClass);
    
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void TestCtor()
    {
        Type.GetConstructors()[0].CreateCtor().Invoke().WriteLine();
    }
    
    [Test]
    public void TestInvoke()
    {
        var method = Type.GetMethod(nameof(TestClass.Method))!.CreateInvoker();
        method.Invoke(new TestClass(), 1).WriteLine();
        method = Type.GetMethod(nameof(TestClass.StaticMethod))!.CreateInvoker();
        var args = new object?[] { 1, null };
        method.Invoke(null, args).WriteLine();
        Debugger.Break();
    }
    
    [Test]
    public void TestValueType()
    {
        var Prefix = "Value";
        var prop = Type.GetProperty($"{Prefix}Prop")!;
        var field = Type.GetField($"{Prefix}Field")!;
        var i = 1;
        TestGetterAndSetter(new TestClass(), prop, field, () => i++);
    }

    [Test]
    public void TestRefType()
    {
        var Prefix = "Ref";
        var prop = Type.GetProperty($"{Prefix}Prop")!;
        var field = Type.GetField($"{Prefix}Field")!;
        TestGetterAndSetter(new TestClass(), prop, field, () => new RefClass());
    }
    
    public void TestGetterAndSetter<TTarget, TValue>(TTarget? instance, PropertyInfo prop, FieldInfo field, Func<TValue> valueGetter)
    {
        var objInst = (object?)instance;
        prop.CreateSetter<TTarget, object>().Invoke(ref instance,  valueGetter());
        prop.CreateGetter<TTarget, object>().Invoke(instance).WriteLine();
        prop.CreateSetter<TTarget, TValue>().Invoke(ref instance, valueGetter());
        prop.CreateGetter<TTarget, TValue>().Invoke(instance).WriteLine();
        prop.CreateSetter<object, TValue>().Invoke(ref objInst,  valueGetter());
        prop.CreateGetter<object, TValue>().Invoke(instance).WriteLine();
        prop.CreateSetter<object, object>().Invoke(ref objInst,  valueGetter());
        prop.CreateGetter<object, object>().Invoke(instance).WriteLine();

        field.CreateSetter<TTarget, TValue>().Invoke(ref instance, valueGetter());
        field.CreateGetter<TTarget, TValue>().Invoke(instance).WriteLine();
        field.CreateSetter<TTarget, object>().Invoke(ref instance,  valueGetter());
        field.CreateGetter<TTarget, object>().Invoke(instance).WriteLine();
        field.CreateSetter<object, TValue>().Invoke(ref objInst,  valueGetter());
        field.CreateGetter<object, TValue>().Invoke(instance).WriteLine();
        field.CreateSetter<object, object>().Invoke(ref objInst, valueGetter());
        field.CreateGetter<object, object>().Invoke(instance).WriteLine();
        Debugger.Break();
    }

    [Test]
    public void TestPerformance()
    {
        var method = Type.GetMethod($"{nameof(TestClass.Method)}")!;
        var args = new object?[] { 1 };
        var target = new TestClass();
        var times = 1000;
        var watch = new Stopwatch();
        watch.Start();
        while (times -- > 0)
        {
            method.Invoke(target, args);
        }
        watch.Stop();
        Console.WriteLine($"Reflect cost {watch.ElapsedTicks} ticks");

        var del = method.CreateInvoker();
        watch = new Stopwatch();
        watch.Start();
        times = 1000;
        while (times -- > 0)
        {
            del.Invoke(target, args);
        }
        watch.Stop();
        Console.WriteLine($"IL cost {watch.ElapsedTicks} ticks");

    }
}