
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using Sympli.SearchRank.Core.Helpers;
using Sympli.SearchRank.Core.Helpers.Abstracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sympli.SearchRank.Core.Test.Helpers
{
    [TestFixture]
    public class ExpressionMatchHelperTests
    {

        private readonly ExpressionMatchHelper _sut;
        private readonly Mock<IConfiguration> _configurationMock = new Mock<IConfiguration>();
        public ExpressionMatchHelperTests()
        {
            _sut = new ExpressionMatchHelper(_configurationMock.Object);
        }

        [TestCase]
        public void GetExpression_Request_For_Google()
        {
            _configurationMock.Setup(x => x["SearchEngines:Google:MatchingExpression"]).Returns("(<div class=\"BNeawe UPmit AP7Wnd\">)(\\w+[a-zA-Z0-9.-?=/]*)");

            var expected = "(<div class=\"BNeawe UPmit AP7Wnd\">)(\\w+[a-zA-Z0-9.-?=/]*)";
            string actual = _sut.GetExpression("google");


            Assert.AreEqual(expected, actual);
        }
    }
}
