using NUnit.Framework;

namespace CppAst.Tests
{
    public class TestFunctions : InlineTestBase
    {
        [Test]
        public void TestSimple()
        {
            ParseAssert(@"
void function0();
int function1(int a, float b);
float function2(int);
char function3(char);
",
                compilation =>
                {
                    Assert.False(compilation.HasErrors);

                    Assert.AreEqual(4, compilation.Functions.Count);

                    {
                        var cppFunction = compilation.Functions[0];
                        Assert.AreEqual("function0", cppFunction.Name);
                        Assert.AreEqual(0, cppFunction.Parameters.Count);
                        Assert.AreEqual("void", cppFunction.ReturnType.ToString());

                        var cppFunction1 = compilation.FindByName<CppFunction>("function0");
                        Assert.AreEqual(cppFunction, cppFunction1);
                    }

                    {
                        var cppFunction = compilation.Functions[1];
                        Assert.AreEqual("function1", cppFunction.Name);
                        Assert.AreEqual(2, cppFunction.Parameters.Count);
                        Assert.AreEqual("a", cppFunction.Parameters[0].Name);
                        Assert.AreEqual(CppTypeKind.Primitive, cppFunction.Parameters[0].Type.TypeKind);
                        Assert.AreEqual(CppPrimitiveKind.Int, ((CppPrimitiveType)cppFunction.Parameters[0].Type).Kind);
                        Assert.AreEqual("b", cppFunction.Parameters[1].Name);
                        Assert.AreEqual(CppTypeKind.Primitive, cppFunction.Parameters[1].Type.TypeKind);
                        Assert.AreEqual(CppPrimitiveKind.Float, ((CppPrimitiveType)cppFunction.Parameters[1].Type).Kind);
                        Assert.AreEqual("int", cppFunction.ReturnType.ToString());

                        var cppFunction1 = compilation.FindByName<CppFunction>("function1");
                        Assert.AreEqual(cppFunction, cppFunction1);
                    }
                    {
                        var cppFunction = compilation.Functions[2];
                        Assert.AreEqual("function2", cppFunction.Name);
                        Assert.AreEqual(1, cppFunction.Parameters.Count);
                        Assert.AreEqual(string.Empty, cppFunction.Parameters[0].Name);
                        Assert.AreEqual(CppTypeKind.Primitive, cppFunction.Parameters[0].Type.TypeKind);
                        Assert.AreEqual(CppPrimitiveKind.Int, ((CppPrimitiveType)cppFunction.Parameters[0].Type).Kind);
                        Assert.AreEqual("float", cppFunction.ReturnType.ToString());

                        var cppFunction1 = compilation.FindByName<CppFunction>("function2");
                        Assert.AreEqual(cppFunction, cppFunction1);
                    }
                    {
                        var cppFunction = compilation.Functions[3];
                        Assert.AreEqual("function3", cppFunction.Name);
                        Assert.AreEqual(1, cppFunction.Parameters.Count);
                        Assert.AreEqual(string.Empty, cppFunction.Parameters[0].Name);
                        Assert.AreEqual(CppTypeKind.Primitive, cppFunction.Parameters[0].Type.TypeKind);
                        Assert.AreEqual(CppPrimitiveKind.Char, ((CppPrimitiveType)cppFunction.Parameters[0].Type).Kind);
                        Assert.AreEqual(CppTypeKind.Primitive, cppFunction.ReturnType.TypeKind);
                        Assert.AreEqual(CppPrimitiveKind.Char, ((CppPrimitiveType)cppFunction.ReturnType).Kind);
                    }
                }
            );
        }

        [Test]
        public void TestSimpleArm()
        {
            var options = new CppParserOptions();

            options.TargetCpu = CppTargetCpu.ARM64;
            options.TargetCpuSub = string.Empty;
            options.TargetVendor = "arm";
            options.TargetSystem = "linux";
            options.TargetAbi = "aarch64-linux-gnu";
            options.AdditionalArguments.Add("-m64");
            options.AdditionalArguments.Add("-O0");

            ParseAssert(@"
void function0();
int function1(int a, float b);
float function2(int);
char function3(char);
",
                compilation =>
                {
                    Assert.False(compilation.HasErrors);

                    Assert.AreEqual(4, compilation.Functions.Count);

                    {
                        var cppFunction = compilation.Functions[0];
                        Assert.AreEqual("function0", cppFunction.Name);
                        Assert.AreEqual(0, cppFunction.Parameters.Count);
                        Assert.AreEqual("void", cppFunction.ReturnType.ToString());

                        var cppFunction1 = compilation.FindByName<CppFunction>("function0");
                        Assert.AreEqual(cppFunction, cppFunction1);
                    }

                    {
                        var cppFunction = compilation.Functions[1];
                        Assert.AreEqual("function1", cppFunction.Name);
                        Assert.AreEqual(2, cppFunction.Parameters.Count);
                        Assert.AreEqual("a", cppFunction.Parameters[0].Name);
                        Assert.AreEqual(CppTypeKind.Primitive, cppFunction.Parameters[0].Type.TypeKind);
                        Assert.AreEqual(CppPrimitiveKind.Int, ((CppPrimitiveType)cppFunction.Parameters[0].Type).Kind);
                        Assert.AreEqual("b", cppFunction.Parameters[1].Name);
                        Assert.AreEqual(CppTypeKind.Primitive, cppFunction.Parameters[1].Type.TypeKind);
                        Assert.AreEqual(CppPrimitiveKind.Float, ((CppPrimitiveType)cppFunction.Parameters[1].Type).Kind);
                        Assert.AreEqual("int", cppFunction.ReturnType.ToString());

                        var cppFunction1 = compilation.FindByName<CppFunction>("function1");
                        Assert.AreEqual(cppFunction, cppFunction1);
                    }
                    {
                        var cppFunction = compilation.Functions[2];
                        Assert.AreEqual("function2", cppFunction.Name);
                        Assert.AreEqual(1, cppFunction.Parameters.Count);
                        Assert.AreEqual(string.Empty, cppFunction.Parameters[0].Name);
                        Assert.AreEqual(CppTypeKind.Primitive, cppFunction.Parameters[0].Type.TypeKind);
                        Assert.AreEqual(CppPrimitiveKind.Int, ((CppPrimitiveType)cppFunction.Parameters[0].Type).Kind);
                        Assert.AreEqual("float", cppFunction.ReturnType.ToString());

                        var cppFunction1 = compilation.FindByName<CppFunction>("function2");
                        Assert.AreEqual(cppFunction, cppFunction1);
                    }
                    {
                        var cppFunction = compilation.Functions[3];
                        Assert.AreEqual("function3", cppFunction.Name);
                        Assert.AreEqual(1, cppFunction.Parameters.Count);
                        Assert.AreEqual(string.Empty, cppFunction.Parameters[0].Name);
                        Assert.AreEqual(CppTypeKind.Primitive, cppFunction.Parameters[0].Type.TypeKind);
                        Assert.AreEqual(CppPrimitiveKind.UnsignedChar, ((CppPrimitiveType)cppFunction.Parameters[0].Type).Kind);
                        Assert.AreEqual(CppTypeKind.Primitive, cppFunction.ReturnType.TypeKind);
                        Assert.AreEqual(CppPrimitiveKind.UnsignedChar, ((CppPrimitiveType)cppFunction.ReturnType).Kind);
                    }
                },
                options
            );
        }

        [Test]
        public void TestFunctionPrototype()
        {
            ParseAssert(@"
typedef void (*function0)(int a, float b);
typedef void (*function1)(int, float);
",
                compilation =>
                {
                    Assert.False(compilation.HasErrors);

                    Assert.AreEqual(2, compilation.Typedefs.Count);

                    {
                        var cppType = compilation.Typedefs[0].ElementType;
                        Assert.AreEqual(CppTypeKind.Pointer, cppType.TypeKind);
                        var cppPointerType = (CppPointerType)cppType;
                        Assert.AreEqual(CppTypeKind.Function, cppPointerType.ElementType.TypeKind);
                        var cppFunctionType = (CppFunctionType)cppPointerType.ElementType;
                        Assert.AreEqual(2, cppFunctionType.Parameters.Count);

                        Assert.AreEqual("a", cppFunctionType.Parameters[0].Name);
                        Assert.AreEqual(CppPrimitiveType.Int, cppFunctionType.Parameters[0].Type);

                        Assert.AreEqual("b", cppFunctionType.Parameters[1].Name);
                        Assert.AreEqual(CppPrimitiveType.Float, cppFunctionType.Parameters[1].Type);
                    }

                    {
                        var cppType = compilation.Typedefs[1].ElementType;
                        Assert.AreEqual(CppTypeKind.Pointer, cppType.TypeKind);
                        var cppPointerType = (CppPointerType)cppType;
                        Assert.AreEqual(CppTypeKind.Function, cppPointerType.ElementType.TypeKind);
                        var cppFunctionType = (CppFunctionType)cppPointerType.ElementType;
                        Assert.AreEqual(2, cppFunctionType.Parameters.Count);

                        Assert.AreEqual(string.Empty, cppFunctionType.Parameters[0].Name);
                        Assert.AreEqual(CppPrimitiveType.Int, cppFunctionType.Parameters[0].Type);

                        Assert.AreEqual(string.Empty, cppFunctionType.Parameters[1].Name);
                        Assert.AreEqual(CppPrimitiveType.Float, cppFunctionType.Parameters[1].Type);
                    }

                }
            );
        }

        [Test]
        public void TestFunctionFields()
        {
            ParseAssert(@"
typedef struct struct0 {
    void (*function0)(int a, float b);
    void (*function1)(char, int);
} struct0;
",
                compilation =>
                {
                    Assert.False(compilation.HasErrors);

                    var cls = compilation.Classes[0];
                    Assert.AreEqual(2, cls.Fields.Count);

                    {
                        var cppType = cls.Fields[0].Type;
                        Assert.AreEqual(CppTypeKind.Pointer, cppType.TypeKind);
                        var cppPointerType = (CppPointerType)cppType;
                        Assert.AreEqual(CppTypeKind.Function, cppPointerType.ElementType.TypeKind);
                        var cppFunctionType = (CppFunctionType)cppPointerType.ElementType;
                        Assert.AreEqual(2, cppFunctionType.Parameters.Count);

                        Assert.AreEqual("a", cppFunctionType.Parameters[0].Name);
                        Assert.AreEqual(CppPrimitiveType.Int, cppFunctionType.Parameters[0].Type);

                        Assert.AreEqual("b", cppFunctionType.Parameters[1].Name);
                        Assert.AreEqual(CppPrimitiveType.Float, cppFunctionType.Parameters[1].Type);
                    }

                    {
                        var cppType = cls.Fields[1].Type;
                        Assert.AreEqual(CppTypeKind.Pointer, cppType.TypeKind);
                        var cppPointerType = (CppPointerType)cppType;
                        Assert.AreEqual(CppTypeKind.Function, cppPointerType.ElementType.TypeKind);
                        var cppFunctionType = (CppFunctionType)cppPointerType.ElementType;
                        Assert.AreEqual(2, cppFunctionType.Parameters.Count);

                        Assert.AreEqual(string.Empty, cppFunctionType.Parameters[0].Name);
                        Assert.AreEqual(CppPrimitiveType.Char, cppFunctionType.Parameters[0].Type);

                        Assert.AreEqual(string.Empty, cppFunctionType.Parameters[1].Name);
                        Assert.AreEqual(CppPrimitiveType.Int, cppFunctionType.Parameters[1].Type);
                    }

                }
            );
        }


        [Test]
        public void TestFunctionExport()
        {
            var text = @"
#ifdef WIN32
#define EXPORT_API __declspec(dllexport)
#else
#define EXPORT_API __attribute__((visibility(""default"")))
#endif
EXPORT_API int function0();
int function1();
";

            ParseAssert(text,
                compilation =>
                {
                    Assert.False(compilation.HasErrors);

                    Assert.AreEqual(2, compilation.Functions.Count);

                    {
                        var cppFunction = compilation.Functions[0];
                        Assert.AreEqual(1, cppFunction.Attributes.Count);
                        Assert.True(cppFunction.IsPublicExport());
                    }
                    {
                        var cppFunction = compilation.Functions[1];
                        Assert.AreEqual(0, cppFunction.Attributes.Count);
                        Assert.True(cppFunction.IsPublicExport());
                    }
                },
                new CppParserOptions() { }
            );

            ParseAssert(text,
                compilation =>
                {
                    Assert.False(compilation.HasErrors);

                    Assert.AreEqual(2, compilation.Functions.Count);

                    {
                        var cppFunction = compilation.Functions[0];
                        Assert.AreEqual(1, cppFunction.Attributes.Count);
                        Assert.True(cppFunction.IsPublicExport());
                    }
                    {
                        var cppFunction = compilation.Functions[1];
                        Assert.AreEqual(0, cppFunction.Attributes.Count);
                        Assert.True(cppFunction.IsPublicExport());
                    }
                }, new CppParserOptions() { }.ConfigureForWindowsMsvc()
            );
        }

        [Test]
        public void TestFunctionVariadic()
        {
            ParseAssert(@"
void function0();
void function1(...);
void function2(int, ...);
",
                compilation =>
                {
                    Assert.False(compilation.HasErrors);

                    Assert.AreEqual(3, compilation.Functions.Count);

                    {
                        var cppFunction = compilation.Functions[0];
                        Assert.AreEqual(0, cppFunction.Parameters.Count);
                        Assert.AreEqual("void", cppFunction.ReturnType.ToString());
                        Assert.AreEqual(CppFunctionFlags.None, cppFunction.Flags & CppFunctionFlags.Variadic);
                    }

                    {
                        var cppFunction = compilation.Functions[1];
                        Assert.AreEqual(0, cppFunction.Parameters.Count);
                        Assert.AreEqual("void", cppFunction.ReturnType.ToString());
                        Assert.AreEqual(CppFunctionFlags.Variadic, cppFunction.Flags & CppFunctionFlags.Variadic);
                    }

                    {
                        var cppFunction = compilation.Functions[2];
                        Assert.AreEqual(1, cppFunction.Parameters.Count);
                        Assert.AreEqual(string.Empty, cppFunction.Parameters[0].Name);
                        Assert.AreEqual(CppTypeKind.Primitive, cppFunction.Parameters[0].Type.TypeKind);
                        Assert.AreEqual(CppPrimitiveKind.Int, ((CppPrimitiveType)cppFunction.Parameters[0].Type).Kind);
                        Assert.AreEqual("void", cppFunction.ReturnType.ToString());
                        Assert.AreEqual(CppFunctionFlags.Variadic, cppFunction.Flags & CppFunctionFlags.Variadic);
                    }
                }
            );
        }



        [Test]
        public void TestFunctionTemplate()
        {
            ParseAssert(@"
template<class T>
void function0(T t);
",
                compilation =>
                {
                    Assert.False(compilation.HasErrors);

                    Assert.AreEqual(1, compilation.Functions.Count);

                    {
                        var cppFunction = compilation.Functions[0];
                        Assert.AreEqual(1, cppFunction.Parameters.Count);
                        Assert.AreEqual("void", cppFunction.ReturnType.ToString());
                        Assert.AreEqual(cppFunction.IsFunctionTemplate, true);
                        Assert.AreEqual(cppFunction.TemplateParameters.Count, 1);
                    }

                }
            );
        }

        [Test]
        public void TestTemplatedConstructor()
        {
            ParseAssert(@"
class TemplatedClass
{
public:
  template <typename... Args>
  TemplatedClass(Args... args); // Templated, variadic, default, converting

  template <typename T>
  TemplatedClass(T value, double anotherValue); // Templated, normal
};
", compilation =>
      {
          Assert.False(compilation.HasErrors);
          Assert.AreEqual(1, compilation.Classes.Count);
          var cppClass = compilation.Classes[0];
          Assert.AreEqual(2, cppClass.Constructors.Count);
          Assert.AreEqual(1, cppClass.Constructors[0].Parameters.Count);
          Assert.AreEqual(1, cppClass.Constructors[0].TemplateParameters.Count);
          // Variadic types are still unexposed
          Assert.AreEqual(CppTypeKind.Unexposed, cppClass.Constructors[0].Parameters[0].Type.TypeKind);
          Assert.AreEqual("Args...", cppClass.Constructors[0].Parameters[0].Type.ToString());

          Assert.AreEqual(2, cppClass.Constructors[1].Parameters.Count);
          Assert.AreEqual(1, cppClass.Constructors[1].TemplateParameters.Count);
          Assert.AreEqual("T", cppClass.Constructors[1].TemplateParameters[0].FullName);
          Assert.AreEqual("value", cppClass.Constructors[1].Parameters[0].Name);
          Assert.AreEqual("double", cppClass.Constructors[1].Parameters[1].Type.ToString());
      });
        }


        [Test]
        public void TestConstructor()
        {
            ParseAssert(@"
class MyClass
{
public:
  MyClass(); // Default
  MyClass(const MyClass&); // Copy
  MyClass(MyClass&&); // Move
  MyClass(int a); // Converting
  MyClass(int a, int b); // Normal
};
", compilation =>
      {
          Assert.False(compilation.HasErrors);
          Assert.AreEqual(1, compilation.Classes.Count);
          var cppClass = compilation.Classes[0];
          Assert.AreEqual(5, cppClass.Constructors.Count);

          var defaultConstructor = cppClass.Constructors[0];
          Assert.AreEqual(0, defaultConstructor.Parameters.Count);
          Assert.True(defaultConstructor.IsConstructor);

          var copyConstructor = cppClass.Constructors[1];
          Assert.AreEqual(1, copyConstructor.Parameters.Count);
          Assert.AreEqual("const MyClass&", copyConstructor.Parameters[0].Type.ToString());
          Assert.True(copyConstructor.IsConstructor);

          var moveConstructor = cppClass.Constructors[2];
          Assert.AreEqual(1, moveConstructor.Parameters.Count);
          Assert.AreEqual("MyClass &&", moveConstructor.Parameters[0].Type.ToString());
          Assert.True(moveConstructor.IsConstructor);

          var convertingConstructor = cppClass.Constructors[3];
          Assert.AreEqual(1, convertingConstructor.Parameters.Count);
          Assert.AreEqual("int", convertingConstructor.Parameters[0].Type.ToString());
          Assert.True(convertingConstructor.IsConstructor);

          var normalConstructor = cppClass.Constructors[4];
          Assert.AreEqual(2, normalConstructor.Parameters.Count);
          Assert.AreEqual("int", normalConstructor.Parameters[0].Type.ToString());
          Assert.AreEqual("int", normalConstructor.Parameters[1].Type.ToString());
      });
        }

        [Test]
        public void TestDefaultConstructor()
        {
            ParseAssert(@"
class MyClass
{
public:
  MyClass() = default;
};",
       compilation =>
       {
           Assert.False(compilation.HasErrors);
           Assert.AreEqual(1, compilation.Classes.Count);
           var cppClass = compilation.Classes[0];
           Assert.AreEqual(1, cppClass.Constructors.Count);
           var defaultConstructor = cppClass.Constructors[0];
           Assert.AreEqual(0, defaultConstructor.Parameters.Count);
           Assert.True(defaultConstructor.IsConstructor);
       });

        }

        [Test]
        public void TestImplicitConstructor()
        {
            ParseAssert(@"
class MyClass
{
  public:
    int a;
};",
        compilation =>
        {
            Assert.False(compilation.HasErrors);
            Assert.AreEqual(1, compilation.Classes.Count);
            var cppClass = compilation.Classes[0];
            // Compiler generated constructors are not parsed in CppAST;
            // they have to be explicitly declared.
            Assert.AreEqual(0, cppClass.Constructors.Count);
        });
        }

    }
}