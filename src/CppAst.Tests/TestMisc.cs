using System;
using NUnit.Framework;

namespace CppAst.Tests
{
    public class TestMisc : InlineTestBase
    {
        [Test]
        public void TestMiscFeatures()
        {
            ParseAssert(@"

class Foo
{
public:
  Foo(int x) : x_{x} {}
private:
  int x_{0};
};

class Bar : public Foo
{
public:
  using Foo::Foo;
};

class Baz : public Bar
{
public:
  using Bar::Bar;
  Baz(double y) : y_{y} {}
private:
  double y_{0};
};
",
                compilation =>
                {
                    Assert.False(compilation.HasErrors);
                    Assert.AreEqual(3, compilation.Classes.Count);
                    Assert.AreEqual(1, compilation.Classes[0].Constructors.Count);
                    Assert.AreEqual(2, compilation.Classes[1].Constructors.Count);
                    Assert.AreEqual(CppVisibility.Public, compilation.Classes[1].Constructors[0].Visibility);
                    Assert.AreEqual(CppVisibility.Public, compilation.Classes[1].Constructors[1].Visibility);

                    Assert.AreEqual(3, compilation.Classes[2].Constructors.Count);
                    Assert.AreEqual(CppVisibility.Public, compilation.Classes[2].Constructors[0].Visibility);
                    Assert.AreEqual(CppVisibility.Public, compilation.Classes[2].Constructors[1].Visibility);
                    Assert.AreEqual(CppVisibility.Public, compilation.Classes[2].Constructors[2].Visibility);

                }
            );
        }

        [Test]
        public void TestPartialSpecialization()
        {
            ParseAssert(@"

template <typename T, int N>
class ArrayBase
{
public:
  ArrayBase(T array[N]) { for(int i=0;i<N;i++) array_[i] = array_[i]; }
private:
  T array_[N];
};

template <typename T>
class Array2d : public ArrayBase<T, 2>
{
public:
  using ArrayBase<T,2>::ArrayBase;
};

using Array2i = Array2d<int>;

#include <array>

template <typename T, int N>
class VectorBase : public virtual std::array<T, N>
{
};

template <typename T>
class Vector2d : public VectorBase<T, 2>
{
public:
  Vector2d(const std::array<T, 2> & elements = {0, 0})
      : Vector2d<T>(elements[0], elements[1]) {}

  Vector2d(T x, T y)
  {
    (*this)[0] = x, (*this)[1] = y;
  }
};

using Vect2i = Vector2d<int32_t>;
",
                compilation =>
                {
                    Assert.False(compilation.HasErrors);
                    Assert.AreEqual(6, compilation.Classes.Count);
                    Assert.AreEqual(CppTemplateKind.TemplateClass, compilation.Classes[0].TemplateKind);
                    Assert.AreEqual(2, compilation.Classes[0].TemplateParameters.Count);
                    Assert.AreEqual(CppTemplateKind.TemplateClass, compilation.Classes[1].TemplateKind);
                    Assert.AreEqual(1, compilation.Classes[1].BaseTypes.Count);
                    Assert.True(compilation.Classes[1].BaseTypes[0].Type is CppClass);
                    Assert.AreEqual(2, (compilation.Classes[1].BaseTypes[0].Type as CppClass).TemplateParameters.Count);
                    Assert.AreEqual(1, (compilation.Classes[1].BaseTypes[0].Type as CppClass).TemplateSpecializedArguments.Count);
                    Assert.AreEqual(CppTemplateKind.TemplateSpecializedClass, compilation.Classes[2].TemplateKind);
                    Assert.AreEqual(2, compilation.Classes[2].TemplateSpecializedArguments.Count);

                    Assert.AreEqual(CppTemplateKind.TemplateClass, compilation.Classes[3].TemplateKind);
                    Assert.AreEqual(2, compilation.Classes[3].TemplateParameters.Count);
                    Assert.AreEqual(CppTemplateKind.TemplateClass, compilation.Classes[4].TemplateKind);
                    Assert.AreEqual(1, compilation.Classes[4].BaseTypes.Count);
                    Assert.True(compilation.Classes[4].BaseTypes[0].Type is CppClass);
                    Assert.AreEqual(2, (compilation.Classes[4].BaseTypes[0].Type as CppClass).TemplateParameters.Count);
                    Assert.AreEqual(1, (compilation.Classes[4].BaseTypes[0].Type as CppClass).TemplateSpecializedArguments.Count);
                    Assert.AreEqual(CppTemplateKind.TemplateSpecializedClass, compilation.Classes[5].TemplateKind);
                    Assert.AreEqual(2, compilation.Classes[5].TemplateSpecializedArguments.Count);
                }
            );
        }
    }
}