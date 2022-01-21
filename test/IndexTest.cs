using System;
using Bunit;
using Bunit.TestDoubles;
using client.Shared;
using Moq;
using NUnit.Framework;
using Index = client.Pages.Index;

namespace test;

[TestFixture]
public class IndexTest
{
  [Test]
  public void MockChildComp()
  {
    using var ctx = new Bunit.TestContext();
    ctx.ComponentFactories.AddStub<SurveyPrompt>();

    var comp = ctx.RenderComponent<Index>();
    Assert.False(comp.HasComponent<SurveyPrompt>());
    Assert.True(comp.HasComponent<Stub<SurveyPrompt>>());
  }

  [Test]
  public void MockChildCompContent()
  {
    using var ctx = new Bunit.TestContext();
    var content = "<div>Mock SurveyPrompt</div>";
    ctx.ComponentFactories.AddStub<SurveyPrompt>(content);

    var comp = ctx.RenderComponent<Index>();
    StringAssert.Contains(content, comp.Markup);
  }

  [Test]
  public void MockChildCompContentWithParameter()
  {
    using var ctx = new Bunit.TestContext();
    ctx.ComponentFactories.AddStub<SurveyPrompt>(paras => $"<div>{paras.Get(x => x.Title)}</div>");

    var comp = ctx.RenderComponent<Index>();
    StringAssert.Contains("How is Blazor working for you?", comp.Markup);
  }

  [Test]
  public void MockComponentByMOQ()
  {
    using var ctx = new Bunit.TestContext();
    var mockComp = new Mock<SurveyPrompt>();
    ctx.ComponentFactories.Add(mockComp.Object);
    // 另一種寫法
    // ctx.ComponentFactories.Add(() => Mock.Of<SurveyPrompt>());

    var comp = ctx.RenderComponent<Index>();
    var actualComp = comp.FindComponent<SurveyPrompt>();
    Assert.AreSame(mockComp.Object, actualComp.Instance);
  }
}
