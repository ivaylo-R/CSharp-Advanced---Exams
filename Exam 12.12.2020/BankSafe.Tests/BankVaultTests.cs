using NUnit.Framework;
using System;

namespace BankSafe.Tests
{
    public class BankVaultTests
    {
        private BankVault bank;
        private Item item;

        [SetUp]
        public void Setup()
        {
            bank = new BankVault();
            item = new Item("Gosho", "A3");
        }

        [Test]
        public void PuttingItemOnInvalidCellShouldThrowException()
        {
            Assert.Throws<ArgumentException>(() => bank.AddItem("A15", item));

        }

        [Test]
        public void PuttinhItemOnTakenCellShouldThrowException()
        {
            bank.AddItem("A3", item);
            Assert.Throws<ArgumentException>(() =>  bank.AddItem("A3",new Item("P","A8")));

        }

        [Test]
        public void PuttingTheSameItemOnTakenCellShouldThrowException()
        {
            bank.AddItem("A3", item);
            Assert.Throws<ArgumentException>(() => bank.AddItem("A3",item));

        }


        [Test]
        public void TryRemoveItemFromNonExistingCellShouldThrowException()
        {
            bank.AddItem("A3", item);
            Assert.Throws<ArgumentException>(() => bank.RemoveItem("C19",item));

        }


        [Test]
        public void TryRemoveItemFromWrongExistingCellShouldThrowException()
        {
            bank.AddItem("A3", item);
            Assert.Throws<ArgumentException>(() => bank.RemoveItem("A3", new Item("Misho","a153")));

        }


        [Test]
        public void SuccessfullyRemovedItemShouldReturnExpectedMessage()
        {
            bank.AddItem("A3", item);
            string actualMessage = bank.RemoveItem("A3", item);
            string expected = $"Remove item:{item.ItemId} successfully!";
            Assert.AreEqual(expected,actualMessage);

        }


        [Test]
        public void SuccessfullySavedItemShouldReturnMessage()
        {
            string actualMessage = bank.AddItem("A3", item);
            string expected = $"Item:{item.ItemId} saved successfully!";
            Assert.AreEqual(expected, actualMessage);

        }

        
    }
}
