using System;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using WebApPizza.Models;
using TechTalk.SpecFlow;
using WebApPizza.Controllers;
using Assert = NUnit.Framework.Assert;

namespace SpecFlowPizza.StepDefinitions
{
    [Binding]
    public class PizzaStepDefinitions
    {
        private readonly PizzaController pizzaController;
        private IActionResult actionResult;
        private ViewResult viewResult;

        public PizzaStepDefinitions()
        {
            pizzaController = new PizzaController();
        }

        [Given(@"I am on the Pizza Selection page")]
        public void GivenIAmOnThePizzaSelectionPage()
        {
            actionResult = pizzaController.PizzaSelection();
        }

        [When(@"I select ""([^""]*)"" from the menu")]
        public void WhenISelectFromTheMenu(string pizzaType)
        {
            actionResult = pizzaController.OrderCheckout(pizzaType);
        }

        [When(@"I proceed to checkout")]
        public void WhenIProceedToCheckout()
        {
            viewResult = actionResult as ViewResult;
            Assert.IsNotNull(viewResult);
        }

        [Then(@"I should be on the Order Checkout page")]
        public void ThenIShouldBeOnTheOrderCheckoutPage()
        {
            Assert.IsNotNull(viewResult);
        }

        [When(@"I select an invalid pizza type ""([^""]*)""")]
        public void WhenISelectAnInvalidPizzaType(string invalidPizza)
        {
            actionResult = pizzaController.OrderCheckout(invalidPizza);
        }

        [Then(@"I should be redirected to the Pizza Selection page")]
        public void ThenIShouldBeRedirectedToThePizzaSelectionPage()
        {
            var redirectToActionResult = actionResult as RedirectToActionResult;
            Assert.IsNotNull(redirectToActionResult);
            Assert.AreEqual("PizzaSelection", redirectToActionResult.ActionName);
        }
    }
}
