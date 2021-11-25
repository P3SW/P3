using BlazorApp.Data;
using BlazorApp.Pages;
using Bunit;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Xunit;

namespace P3ConversionDashboard.Tests
{
    public class Testtest
    {
        [Inject] 
        public Test mangerList { get; set; }
        
        [Inject]
        public QueueTest managerList { get; set; }
        private readonly TestContext _testContext;

        
        public Testtest()
        {
            _testContext = new TestContext();
        }

        [Fact]
        public void OverView_Has_Rendered()
        {
            
            var sut = _testContext.RenderComponent<Overview>();

            var renderedMarkup = sut.Markup;
            Assert.Equal("<p>Test</p>", renderedMarkup);

        }



    }
}