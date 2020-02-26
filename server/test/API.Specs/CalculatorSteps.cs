using NUnit.Framework;
using System;
using TechTalk.SpecFlow;

namespace API.Specs
{
    [Binding]
    public class CalculatorSteps
    {
        private int result;
        private Calculator calculator = new Calculator();

        [Given(@"I have entered (.*) into the calculator")]
        public void GivenIHaveEnteredIntoTheCalculator(int number)
        {
            calculator.FirstNumber = number;
        }
        
        [Given(@"I have also entered (.*) into the calculator")]
        public void GivenIHaveAlsoEnteredIntoTheCalculator(int number)
        {
            calculator.SecondNumber = number;
        }
        
        [When(@"I press add")]
        public void WhenIPressAdd()
        {
            result = calculator.Add();
        }
        
        [Then(@"the result should be (.*) on the screen")]
        public void ThenTheResultShouldBeOnTheScreen(int expectedResult)
        {
            Assert.That(result, Is.EqualTo(expectedResult));
        }
    }

    public class Calculator
    {
        public int FirstNumber { get; internal set; }
        public int SecondNumber { get; internal set; }

        internal int Add()
        {
            return FirstNumber + SecondNumber;
        }
    }
}
