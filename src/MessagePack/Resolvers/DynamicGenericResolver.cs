﻿using MessagePack.Formatters;
using System.Linq;
using MessagePack.Internal;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace MessagePack.Resolvers
{
    public class DynamicGenericResolver : IFormatterResolver
    {
        public static IFormatterResolver Instance = new DynamicGenericResolver();

        DynamicGenericResolver()
        {

        }

        public IMessagePackFormatter<T> GetFormatter<T>()
        {
            return FormatterCache<T>.formatter;
        }

        static class FormatterCache<T>
        {
            public static readonly IMessagePackFormatter<T> formatter;

            static FormatterCache()
            {
                formatter = (IMessagePackFormatter<T>)DynamicGenericResolverGetFormatterHelper.GetFormatter(typeof(T));
            }
        }
    }
}

namespace MessagePack.Internal
{
    internal static class DynamicGenericResolverGetFormatterHelper
    {
        static readonly Dictionary<Type, Type> formatterMap = new Dictionary<Type, Type>()
        {
              {typeof(List<>), typeof(ListFormatter<>)},
              {typeof(LinkedList<>), typeof(LinkedListFormatter<>)},
              {typeof(Queue<>), typeof(QeueueFormatter<>)},
              {typeof(Stack<>), typeof(StackFormatter<>)},
              {typeof(HashSet<>), typeof(HashSetFormatter<>)},
              {typeof(ReadOnlyCollection<>), typeof(ReadOnlyCollectionFormatter<>)},
              {typeof(ObservableCollection<>), typeof(ObservableCollectionFormatter<>)},
              {typeof(ReadOnlyObservableCollection<>),(typeof(ReadOnlyObservableCollectionFormatter<>))},
              {typeof(IList<>), typeof(InterfaceListFormatter<>)},
              {typeof(ICollection<>), typeof(InterfaceCollectionFormatter<>)},
              {typeof(IEnumerable<>), typeof(InterfaceEnumerableFormatter<>)},
              {typeof(IReadOnlyList<>), typeof(InterfaceReadOnlyListFormatter<>)},
              {typeof(IReadOnlyCollection<>), typeof(InterfaceReadOnlyCollectionFormatter<>)},
              {typeof(ISet<>), typeof(InterfaceSetFormatter<>)},
              {typeof(System.Collections.Concurrent.ConcurrentBag<>), typeof(ConcurrentBagFormatter<>)},
              {typeof(System.Collections.Concurrent.ConcurrentQueue<>), typeof(ConcurrentQueueFormatter<>)},
              {typeof(System.Collections.Concurrent.ConcurrentStack<>), typeof(ConcurrentStackFormatter<>)},
              {typeof(Dictionary<,>), typeof(DictionaryFormatter<,>)},
              {typeof(IDictionary<,>), typeof(InterfaceDictionaryFormatter<,>)},
              {typeof(SortedDictionary<,>), typeof(SortedDictionaryFormatter<,>)},
              {typeof(SortedList<,>), typeof(SortedListFormatter<,>)},
              {typeof(ReadOnlyDictionary<,>), typeof(ReadOnlyDictionaryFormatter<,>)},
              {typeof(IReadOnlyDictionary<,>), typeof(InterfaceReadOnlyDictionaryFormatter<,>)},
              {typeof(System.Collections.Concurrent.ConcurrentDictionary<,>), typeof(ConcurrentDictionaryFormatter<,>)},
              {typeof(ILookup<,>), typeof(InterfaceLookupFormatter<,>)},
              {typeof(IGrouping<,>), typeof(InterfaceGroupingFormatter<,>)},
              {typeof(Lazy<>), typeof(LazyFormatter<>)},
              {typeof(Task<>), typeof(TaskValueFormatter<>)},
        };

        // Reduce IL2CPP code generate size(don't write long code in <T>)
        internal static object GetFormatter(Type t)
        {
            var ti = t.GetTypeInfo();

            if (t.IsArray)
            {
                var rank = t.GetArrayRank();
                if (rank == 1)
                {
                    if (t.GetElementType() == typeof(byte[])) // byte[] is also supported in builtin formatter.
                    {
                        return ByteArrayFormatter.Instance;
                    }

                    return Activator.CreateInstance(typeof(ArrayFormatter<>).MakeGenericType(t.GetElementType()));
                }
                else if (rank == 2)
                {
                    return Activator.CreateInstance(typeof(TwoDimentionalArrayFormatter<>).MakeGenericType(t.GetElementType()));
                }
                else if (rank == 3)
                {
                    return Activator.CreateInstance(typeof(ThreeDimentionalArrayFormatter<>).MakeGenericType(t.GetElementType()));
                }
                else if (rank == 4)
                {
                    return Activator.CreateInstance(typeof(FourDimentionalArrayFormatter<>).MakeGenericType(t.GetElementType()));
                }
                else
                {
                    return null; // not supported built-in
                }
            }
            else if (ti.IsGenericType)
            {
                var genericType = ti.GetGenericTypeDefinition();
                var genericTypeInfo = genericType.GetTypeInfo();
                var isNullable = genericTypeInfo.IsNullable();
                var nullableElementType = isNullable ? ti.GenericTypeArguments[0] : null;

                if (genericType == typeof(KeyValuePair<,>))
                {
                    return CreateInstance(typeof(KeyValuePairFormatter<,>), ti.GenericTypeArguments);
                }
                else if (isNullable && nullableElementType.IsConstructedGenericType && nullableElementType.GetGenericTypeDefinition() == typeof(KeyValuePair<,>))
                {
                    return CreateInstance(typeof(NullableFormatter<>), new[] { nullableElementType });
                }

                // Tuple
                else if (ti.FullName.StartsWith("System.Tuple"))
                {
                    Type tupleFormatterType = null;
                    switch (ti.GenericTypeArguments.Length)
                    {
                        case 1:
                            tupleFormatterType = typeof(TupleFormatter<>);
                            break;
                        case 2:
                            tupleFormatterType = typeof(TupleFormatter<,>);
                            break;
                        case 3:
                            tupleFormatterType = typeof(TupleFormatter<,,>);
                            break;
                        case 4:
                            tupleFormatterType = typeof(TupleFormatter<,,,>);
                            break;
                        case 5:
                            tupleFormatterType = typeof(TupleFormatter<,,,,>);
                            break;
                        case 6:
                            tupleFormatterType = typeof(TupleFormatter<,,,,,>);
                            break;
                        case 7:
                            tupleFormatterType = typeof(TupleFormatter<,,,,,,>);
                            break;
                        case 8:
                            tupleFormatterType = typeof(TupleFormatter<,,,,,,,>);
                            break;
                        default:
                            break;
                    }

                    return CreateInstance(tupleFormatterType, ti.GenericTypeArguments);
                }

                // ArraySegement
                else if (genericType == typeof(ArraySegment<>))
                {
                    if (ti.GenericTypeArguments[0] == typeof(byte))
                    {
                        return new ByteArraySegmentFormatter();
                    }
                    else
                    {
                        return CreateInstance(typeof(ArraySegmentFormatter<>), ti.GenericTypeArguments);
                    }
                }
                else if (isNullable && nullableElementType.IsConstructedGenericType && nullableElementType.GetGenericTypeDefinition() == typeof(ArraySegment<>))
                {
                    if (nullableElementType == typeof(ArraySegment<byte>))
                    {
                        return new StaticNullableFormatter<ArraySegment<byte>>(new ByteArraySegmentFormatter());
                    }
                    else
                    {
                        return CreateInstance(typeof(NullableFormatter<>), new[] { nullableElementType });
                    }
                }

                // Mapped formatter
                else
                {
                    Type formatterType;
                    if (formatterMap.TryGetValue(genericType, out formatterType))
                    {
                        return CreateInstance(formatterType, ti.GenericTypeArguments);
                    }

                    // generic collection
                    else if (ti.GenericTypeArguments.Length == 1
                          && ti.ImplementedInterfaces.Any(x => x.IsConstructedGenericType && x.GetGenericTypeDefinition() == typeof(ICollection<>))
                          && ti.DeclaredConstructors.Any(x => x.GetParameters().Length == 0))
                    {
                        var elemType = ti.GenericTypeArguments[0];
                        return CreateInstance(typeof(GenericCollectionFormatter<,>), new[] { elemType, t });
                    }
                    // generic dictionary
                    else if (ti.GenericTypeArguments.Length == 2
                          && ti.ImplementedInterfaces.Any(x => x.IsConstructedGenericType && x.GetGenericTypeDefinition() == typeof(IDictionary<,>))
                          && ti.DeclaredConstructors.Any(x => x.GetParameters().Length == 0))
                    {
                        var keyType = ti.GenericTypeArguments[0];
                        var valueType = ti.GenericTypeArguments[1];
                        return CreateInstance(typeof(GenericDictionaryFormatter<,,>), new[] { keyType, valueType, t });
                    }
                }
            }

            return null;
        }

        static object CreateInstance(Type genericType, Type[] genericTypeArguments, params object[] arguments)
        {
            return Activator.CreateInstance(genericType.MakeGenericType(genericTypeArguments), arguments);
        }
    }
}