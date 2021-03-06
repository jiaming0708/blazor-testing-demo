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
    ctx.JSInterop.Setup<int>("incrementCount", 0).SetResult(1);

    var comp = ctx.RenderComponent<Counter>();
    comp.Find("button").Click();

    StringAssert.Contains("Current count: 1", comp.Markup);
  }

  [Test]
  public void CounterShouldIncrementWhenClicked_CompareWithFirst()
  {
    using var ctx = new Bunit.TestContext();
    ctx.JSInterop.Setup<int>("incrementCount", 0).SetResult(1);

    var comp = ctx.RenderComponent<Counter>();
    comp.Find("button").Click();

    var diffs = comp.GetChangesSinceFirstRender();
    var diff = diffs.ShouldHaveSingleChange();
    diff.ShouldBeTextChange("Current count: 1");
  }
  
  [Test]
  public void CounterShouldIncrementWhenClicked_CompareWithSnapshot()
  {
    using var ctx = new Bunit.TestContext();
    var count = 0;
    var jsruntime = ctx.JSInterop.Setup<int>("incrementCount", count);

    var comp = ctx.RenderComponent<Counter>();
    var button = comp.Find("button");
    jsruntime.SetResult(1);
    button.Click();

    comp.SaveSnapshot();

    jsruntime = ctx.JSInterop.Setup<int>("incrementCount", 1);
    jsruntime.SetResult(2);
    button.Click();

    var diffs = comp.GetChangesSinceSnapshot();
    var diff = diffs.ShouldHaveSingleChange();
    diff.ShouldBeTextChange("Current count: 2");
  }
  
  [Test]
  public void CountShouldBe5_OnInit()
  {
    using var ctx = new Bunit.TestContext();

    var count = 5;
    var comp = ctx.RenderComponent<Counter>(parameters => parameters.Add(p => p.CurrentCount, count));
    comp.Find("p").TextContent.MarkupMatches($"Current count: {count}");
  }

  [Test]
  public void CountShouldBe5_AfterInit()
  {
    using var ctx = new Bunit.TestContext();

    var count = 5;
    var comp = ctx.RenderComponent<Counter>();
    var elm = comp.Find("p");
    elm.TextContent.MarkupMatches("Current count: 0");

    comp.SetParametersAndRender(parameters => parameters.Add(p => p.CurrentCount, count));
    elm.TextContent.MarkupMatches($"Current count: {count}");
  }
}
