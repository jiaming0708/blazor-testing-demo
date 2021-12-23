using System;
using client.Pages;
using NUnit.Framework;

namespace test;

public class CounterTest
{
  [Test]
  public void CounterRender()
  {
    using var ctx = new Bunit.TestContext();

    var comp = ctx.RenderComponent<Counter>();
    StringAssert.Contains("Current count: 0", comp.Markup);
  }
}
