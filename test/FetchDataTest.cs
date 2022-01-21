using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Bunit;
using client.Pages;
using NUnit.Framework;
using RichardSzalay.MockHttp;
using test.mocks;

namespace test;

[TestFixture]
public class FetchDataTest
{
  [Test]
  public void RenderWithoutResponse()
  {
    using var ctx = new Bunit.TestContext();
    var mock = ctx.Services.AddMockHttpClient();

    var comp = ctx.RenderComponent<FetchData>();
    StringAssert.Contains("Loading...", comp.Markup);
  }
}
