#if !NET && !NETSTANDARD
using System;
#endif
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices;

namespace Antelcat.IL.Extensions;

public static partial class ILExtension
{
	public static ILGenerator EmitEx(this ILGenerator il, OpCode opcode)
	{
		il.Emit(opcode);
		return il;
	}

	public static ILGenerator EmitEx(this ILGenerator il, OpCode opcode, Byte arg)
	{
		il.Emit(opcode, arg);
		return il;
	}

	public static ILGenerator EmitEx(this ILGenerator il, OpCode opcode, SByte arg)
	{
		il.Emit(opcode, arg);
		return il;
	}

	public static ILGenerator EmitEx(this ILGenerator il, OpCode opcode, Int16 arg)
	{
		il.Emit(opcode, arg);
		return il;
	}

	public static ILGenerator EmitEx(this ILGenerator il, OpCode opcode, Int32 arg)
	{
		il.Emit(opcode, arg);
		return il;
	}

	public static ILGenerator EmitEx(this ILGenerator il, OpCode opcode, MethodInfo meth)
	{
		il.Emit(opcode, meth);
		return il;
	}

	public static ILGenerator EmitCalliEx(this ILGenerator il, OpCode opcode, CallingConventions callingConvention, Type returnType, Type[] parameterTypes, Type[] optionalParameterTypes)
	{
		il.EmitCalli(opcode, callingConvention, returnType, parameterTypes, optionalParameterTypes);
		return il;
	}

	public static ILGenerator EmitCalliEx(this ILGenerator il, OpCode opcode, CallingConvention unmanagedCallConv, Type returnType, Type[] parameterTypes)
	{
		il.EmitCalli(opcode, unmanagedCallConv, returnType, parameterTypes);
		return il;
	}

	public static ILGenerator EmitCallEx(this ILGenerator il, OpCode opcode, MethodInfo methodInfo, Type[] optionalParameterTypes)
	{
		il.EmitCall(opcode, methodInfo, optionalParameterTypes);
		return il;
	}

	public static ILGenerator EmitEx(this ILGenerator il, OpCode opcode, SignatureHelper signature)
	{
		il.Emit(opcode, signature);
		return il;
	}

	public static ILGenerator EmitEx(this ILGenerator il, OpCode opcode, ConstructorInfo con)
	{
		il.Emit(opcode, con);
		return il;
	}

	public static ILGenerator EmitEx(this ILGenerator il, OpCode opcode, Type cls)
	{
		il.Emit(opcode, cls);
		return il;
	}

	public static ILGenerator EmitEx(this ILGenerator il, OpCode opcode, Int64 arg)
	{
		il.Emit(opcode, arg);
		return il;
	}

	public static ILGenerator EmitEx(this ILGenerator il, OpCode opcode, Single arg)
	{
		il.Emit(opcode, arg);
		return il;
	}

	public static ILGenerator EmitEx(this ILGenerator il, OpCode opcode, Double arg)
	{
		il.Emit(opcode, arg);
		return il;
	}

	public static ILGenerator EmitEx(this ILGenerator il, OpCode opcode, Label label)
	{
		il.Emit(opcode, label);
		return il;
	}

	public static ILGenerator EmitEx(this ILGenerator il, OpCode opcode, Label[] labels)
	{
		il.Emit(opcode, labels);
		return il;
	}

	public static ILGenerator EmitEx(this ILGenerator il, OpCode opcode, FieldInfo field)
	{
		il.Emit(opcode, field);
		return il;
	}

	public static ILGenerator EmitEx(this ILGenerator il, OpCode opcode, String str)
	{
		il.Emit(opcode, str);
		return il;
	}

	public static ILGenerator EmitEx(this ILGenerator il, OpCode opcode, LocalBuilder local)
	{
		il.Emit(opcode, local);
		return il;
	}

	public static ILGenerator EndExceptionBlockEx(this ILGenerator il)
	{
		il.EndExceptionBlock();
		return il;
	}

	public static ILGenerator BeginExceptFilterBlockEx(this ILGenerator il)
	{
		il.BeginExceptFilterBlock();
		return il;
	}

	public static ILGenerator BeginCatchBlockEx(this ILGenerator il, Type exceptionType)
	{
		il.BeginCatchBlock(exceptionType);
		return il;
	}

	public static ILGenerator BeginFaultBlockEx(this ILGenerator il)
	{
		il.BeginFaultBlock();
		return il;
	}

	public static ILGenerator BeginFinallyBlockEx(this ILGenerator il)
	{
		il.BeginFinallyBlock();
		return il;
	}

	public static ILGenerator MarkLabelEx(this ILGenerator il, Label loc)
	{
		il.MarkLabel(loc);
		return il;
	}

	public static ILGenerator ThrowExceptionEx(this ILGenerator il, Type excType)
	{
		il.ThrowException(excType);
		return il;
	}

	public static ILGenerator UsingNamespaceEx(this ILGenerator il, String usingNamespace)
	{
		il.UsingNamespace(usingNamespace);
		return il;
	}

	public static ILGenerator BeginScopeEx(this ILGenerator il)
	{
		il.BeginScope();
		return il;
	}

	public static ILGenerator EndScopeEx(this ILGenerator il)
	{
		il.EndScope();
		return il;
	}

	public static ILGenerator EmitWriteLineEx(this ILGenerator il, String value)
	{
		il.EmitWriteLine(value);
		return il;
	}

	public static ILGenerator EmitWriteLineEx(this ILGenerator il, LocalBuilder localBuilder)
	{
		il.EmitWriteLine(localBuilder);
		return il;
	}

	public static ILGenerator EmitWriteLineEx(this ILGenerator il, FieldInfo fld)
	{
		il.EmitWriteLine(fld);
		return il;
	}

}