using System;
using Bunit;
using Bunit.TestDoubles;
using client.Shared;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace test;

[TestFixture]
public class NavMenuTest
{
  [Test]
  public void RenderComponent()
  {
    using var ctx = new Bunit.TestContext();
    var comp = ctx.RenderComponent<NavMenu>();

    StringAssert.Contains(@"class=""nav-link counter""", comp.Markup);
  }

  [Test]
  public void Navigate2CounterPage()
  {
    using var ctx = new Bunit.TestContext();
    var navMan = ctx.Services.GetRequiredService<FakeNavigationManager>();
    var comp = ctx.RenderComponent<NavMenu>();
    var menu = comp.Find(@"a.counter");
    menu.Click();
    StringAssert.Contains("/counter", navMan.Uri);
  }
}
