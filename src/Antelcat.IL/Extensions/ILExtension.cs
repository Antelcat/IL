using System.Reflection;
using System.Reflection.Emit;

namespace Antelcat.IL.Extensions;

public static partial class ILExtension
{
    public static ILGenerator CallOrVirtualEx(this ILGenerator il, MethodInfo method) =>
        il.EmitEx(method.IsVirtual ? OpCodes.Callvirt : OpCodes.Call, method);

    public static ILGenerator UnboxOrCastEx(this ILGenerator il, Type type) =>
        il.EmitEx(type.IsValueType ? OpCodes.Unbox : OpCodes.Castclass, type);

    public static ILGenerator SetFldEx(this ILGenerator il, FieldInfo field) =>
        il.EmitEx(field.IsStatic ? OpCodes.Stsfld : OpCodes.Stfld, field);

    public static ILGenerator LodFldEx(this ILGenerator il, FieldInfo field) =>
        il.EmitEx(field.IsStatic ? OpCodes.Ldsfld : OpCodes.Ldfld, field);

    public static ILGenerator LdRefIfClass(this ILGenerator il, Type type) =>
        !type.IsValueType ? il.EmitEx(OpCodes.Ldind_Ref) : il;

    public static ILGenerator BoxIfValueType(this ILGenerator il, Type type) =>
        type.IsValueType ? il.EmitEx(OpCodes.Box, type) : il;

    public static ILGenerator LdArgIfClass(this ILGenerator il, int idx, Type type) =>
        il.EmitEx(!type.IsValueType ? OpCodes.Ldarg : OpCodes.Ldarga, idx);

    public static ILGenerator LdLocIfClass(this ILGenerator il, int idx, Type type) =>
        il.EmitEx(!type.IsValueType ? OpCodes.Ldloc : OpCodes.Ldloca, idx);

    public static ILGenerator LdLocIfNotByRef(this ILGenerator il, LocalBuilder local, Type type) =>
        il.EmitEx(!type.IsByRef ? OpCodes.Ldloc : OpCodes.Ldloca, local);

    public static ILGenerator EmitFastInt(this ILGenerator il, int value) =>
        value switch
        {
            -1 => il.EmitEx(OpCodes.Ldc_I4_M1),
            0  => il.EmitEx(OpCodes.Ldc_I4_0),
            1  => il.EmitEx(OpCodes.Ldc_I4_1),
            2  => il.EmitEx(OpCodes.Ldc_I4_2),
            3  => il.EmitEx(OpCodes.Ldc_I4_3),
            4  => il.EmitEx(OpCodes.Ldc_I4_4),
            5  => il.EmitEx(OpCodes.Ldc_I4_5),
            6  => il.EmitEx(OpCodes.Ldc_I4_6),
            7  => il.EmitEx(OpCodes.Ldc_I4_7),
            8  => il.EmitEx(OpCodes.Ldc_I4_8),
            _  => il.EmitEx(value is > -129 and < 128 ? OpCodes.Ldc_I4_S : OpCodes.Ldc_I4, value)
        };

    public static ILGenerator EmitEx(this ILGenerator il, Action<ILGenerator> action)
    {
        action(il);
        return il;
    }
}