using System;
using Bunit;
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
  
  [Test]
  public void CounterShouldIncrementWhenClicked()
  {
    using var ctx = new Bunit.TestContext();

    var comp = ctx.RenderComponent<Counter>();
    comp.Find("button").Click();

    StringAssert.Contains("Current count: 1", comp.Markup);
  }
}
