using System.Configuration;
using MarkdownSharp;
using NUnit.Framework;

namespace MarkdownSharpTests
{
    [TestFixture]
    public class ConfigTest
    {
        [Test]
        public void TestLoadFromOptions()
        {
            
            var markdown = new Markdown(new MarkdownOptions
            {
                AutoHyperlink=true,
                AutoNewlines=true,
                EmptyElementSuffix=">",
                LinkEmails=false,
                StrictBoldItalic=true
            });
            Assert.AreEqual(true, markdown.AutoHyperlink);
            Assert.AreEqual(true, markdown.AutoNewLines);
            Assert.AreEqual(">", markdown.EmptyElementSuffix);
            Assert.AreEqual(false, markdown.LinkEmails);
            Assert.AreEqual(true, markdown.StrictBoldItalic);
        }

        [Test]
        public void TestNoLoadFromConfigFile()
        {
            foreach (var markdown in new[] {new Markdown(), new Markdown()})
            {
                Assert.AreEqual(false, markdown.AutoHyperlink);
                Assert.AreEqual(false, markdown.AutoNewLines);
                Assert.AreEqual(" />", markdown.EmptyElementSuffix);
                Assert.AreEqual(true, markdown.LinkEmails);
                Assert.AreEqual(false, markdown.StrictBoldItalic);
            }
        }

        [Test]
        public void TestAutoHyperlink()
        {
            var markdown = new Markdown();  
            Assert.IsFalse(markdown.AutoHyperlink);
            Assert.AreEqual("<p>foo http://example.com bar</p>\n", markdown.Transform("foo http://example.com bar"));
            markdown.AutoHyperlink = true;
            Assert.AreEqual("<p>foo <a href=\"http://example.com\">http://example.com</a> bar</p>\n", markdown.Transform("foo http://example.com bar"));
        }

        [Test]
        public void TestAutoNewLines()
        {
            var markdown = new Markdown();
            Assert.IsFalse(markdown.AutoNewLines);
            Assert.AreEqual("<p>Line1\nLine2</p>\n", markdown.Transform("Line1\nLine2"));
            markdown.AutoNewLines = true;
            Assert.AreEqual("<p>Line1<br />\nLine2</p>\n", markdown.Transform("Line1\nLine2"));
        }

        [Test]
        public void TestEmptyElementSuffix()
        {
            var markdown = new Markdown();
            Assert.AreEqual(" />", markdown.EmptyElementSuffix);
            Assert.AreEqual("<hr />\n", markdown.Transform("* * *"));
            markdown.EmptyElementSuffix = ">";
            Assert.AreEqual("<hr>\n", markdown.Transform("* * *"));
        }

        [Test]
        public void TestLinkEmails()
        {
            var markdown = new Markdown();
            Assert.IsTrue(markdown.LinkEmails);
            Assert.AreEqual("<p><a href=\"&#", markdown.Transform("<aa@bb.com>").Substring(0,14));
            markdown.LinkEmails = false;
            Assert.AreEqual("<p><aa@bb.com></p>\n", markdown.Transform("<aa@bb.com>"));
        }

        [Test]
        public void TestStrictBoldItalic()
        {
            var markdown = new Markdown();
            Assert.IsFalse(markdown.StrictBoldItalic);
            Assert.AreEqual("<p>before<strong>bold</strong>after before<em>italic</em>after</p>\n", markdown.Transform("before**bold**after before_italic_after"));
            markdown.StrictBoldItalic = true;
            Assert.AreEqual("<p>before*bold*after before_italic_after</p>\n", markdown.Transform("before*bold*after before_italic_after"));
        }
    }
}
