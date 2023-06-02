# ILGeneratorEx

Extensions of ILGenerator and easily create delegates

## Emit Extension

Along with [T4](https://learn.microsoft.com/zh-cn/visualstudio/modeling/code-generation-and-t4-text-templates?view=vs-2022) generated [ILExtension (Emit)](https://github.com/Antelcat/Antelcat.Shared/blob/main/src/Shared/IL/Extensions/ILExtension.g.cs), you can use Fluent Style EmitEx like :

```c#
ilGenerator.EmitEx(OpCodes.Ldarg_0)
                .EmitEx(OpCodes.Ldind_Ref)
                .EmitEx(OpCodes.Unbox_Any, targetType)
                .EmitEx(OpCodes.Stloc_0)
                .EmitEx(OpCodes.Ldloca, 0)
                ...
```

## Delegates

### Supports

You can use extensions to create targets from these sources :

| Source | Target |  
| :---: | :---: |
| Type   | Ctor<> |  
| ConstructorInfo | Ctor<> |
| PropertyInfo | Setter<,> & Getter<,> |
| FieldInfo | Setter<,> & Getter<,> |
| MethodInfo | Invoker<,> |
  
### Usage

Can be easily create delegate when you have a class like:

``` c#
class Foo{
    public Foo(int dependency){}
    public int Method(ref int val) => ++val;
    public static int StaticMethod(int val, out int source) {
        source = val--;
        return val;
    }
    protected string Property { get; set; }
    protected static string StaticProperty { get; set; }
    private string field;
    private static string staticField;
}
```

Then :

``` c#
var type = typeof(Foo);

var foo = type.GetConstructors()[0].CreateCtor().Invoke(1);
object? nothing = null;

type.GetMethod(nameof(Foo.Method)).CreateInvoker().Invoke(foo, 1);
type.GetMethod(nameof(Foo.StaticMethod)).CreateInvoker().Invoke(null, new object?[]{ 1, null });

var prop = type.GetProperty(nameof(Foo.Property));
prop.CreateGetter().Invoke(foo);
prop.CreateSetter().Invoke(ref foo, "value");

var staticProp = type.GetProperty(nameof(Foo.StaticProperty));
staticProp.CreateGetter().Invoke(null);
staticProp.CreateSetter().Invoke(ref nothing, "value");

var field = type.GetField(nameof(Foo.field));
field.CreateGetter().Invoke(foo);
field.CreateSetter().Invoke(ref foo, "value");

var staticField = type.GetField(nameof(Foo.staticField));
staticField.CreateGetter().Invoke(null);
staticField.CreateSetter().Invoke(ref nothing, "value");
```

Tests can be found in [UnitTest.cs](./src/Antelcat.ILGeneratorEx.Test/UnitTest.cs)
