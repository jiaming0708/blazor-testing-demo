using System;
using Bunit;
using Bunit.TestDoubles;
using client.Shared;
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
}
