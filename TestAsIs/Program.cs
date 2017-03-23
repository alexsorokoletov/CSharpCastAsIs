using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace TestAsIs
{
    public class Program
    {
        public static void Main()
        {
			var summary = BenchmarkRunner.Run<AsOrIs>();
            Console.WriteLine("Hello World");
        }

        public class AsOrIs
        {
            public class A { }
            public class B : A { }
            public class C : A { }

            private static readonly List<A> objectsForCast = new List<A>()
            {
                new A(),
                new B(),
                new C(),
                new A(),
                new B(),
                new C(),
                new A(),
                new B(),
                new C(),
                new A(),
                new B(),
                new C(),
                new A(),
                new B(),
                new C(),
                new A(),
                new B(),
                new C(),
            };

            [Benchmark]
            public int CastUsingAs()
            {
                var result = 0;
                for (int i = 0; i < objectsForCast.Count; i++)
                {
                    var obj = objectsForCast[i];
                    if (obj is B)
                    {
                        var b = (B)obj;
                        result += 2;
                        continue;
                    }
                    if (obj is C)
                    {
                        var c = (C)obj;
                        result += 3;
                        continue;
                    }
                    result += 1;
                }
                return result;
            }

            [Benchmark]
            public int CastUsingIs()
            {
                var result = 0;
                for (int i = 0; i < objectsForCast.Count; i++)
                {
                    var obj = objectsForCast[i];
                    var b = obj as B;
                    if (b != null)
                    {
                        result += 2;
                        continue;
                    }
					var c = obj as C;
                    if (c!=null)
                    {
                        result += 3;
                        continue;
                    }
                    result += 1;
                }
                return result;
            }
        }
    }
}
