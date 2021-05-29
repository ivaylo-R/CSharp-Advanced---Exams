using NUnit.Framework;
using System;

namespace BankSafe.Tests
{
    public class BankVaultTests
    {
        private Item item;
        private Item item2;
        private BankVault bankVault;

        [SetUp]
        public void Setup()
        {
            item = new Item("Stefcho", "10");
            item2 = new Item("Pesho", "20");
            bankVault = new BankVault();
        }

        //[Test]
        //public void CreateItem()
        //{
        //    Assert.That(item.Owner, Is.EqualTo("Stefcho"));
        //    Assert.That(item.ItemId, Is.EqualTo("10"));
        //}
        [Test]
        public void VaultIsCreated()
        {
            int expected = 12;
            Assert.That(bankVault.VaultCells.Count, Is.EqualTo(expected));
        }
        [TestCase("A1")]
        [TestCase("A2")]
        [TestCase("A3")]
        [TestCase("A4")]
        [TestCase("B1")]
        [TestCase("B2")]
        [TestCase("B3")]
        [TestCase("B4")]
        [TestCase("C1")]
        [TestCase("C2")]
        [TestCase("C3")]
        [TestCase("C4")]
        public void VaultNamesAreProper(string name)
        {
            Assert.True(bankVault.VaultCells.ContainsKey(name));
            Assert.True(bankVault.VaultCells[name] == null);
        }
        [Test]
        public void VaultThrowsWhenCellDontExist()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                bankVault.AddItem("D1", item);
            });
        }
        [Test]
        public void VaultThrowsWhenCellNotEmpty()
        {
            bankVault.AddItem("A1", item);
            Assert.Throws<ArgumentException>(() =>
            {
                bankVault.AddItem("A1", item2);
            });
        }
        [Test]
        public void VaultThrowsWhenItemExists()
        {
            bankVault.AddItem("A1", item);
            Assert.Throws<InvalidOperationException>(() =>
            {
                bankVault.AddItem("B1", item);
            });
        }
        [Test]
        public void VaultAddWorksFine()
        {
            bankVault.AddItem("A1", item);

            Assert.NotNull(bankVault.VaultCells["A1"]);
            Assert.That(bankVault.VaultCells["A1"].ItemId == item.ItemId);
            Assert.That(bankVault.VaultCells["A1"].Owner == item.Owner);
        }
        [Test]
        public void AddVaultReturnProperMessage()
        {
            string expected = "Item:10 saved successfully!";
            string expected2 = "Wrong Input";

            Assert.That(() => bankVault.AddItem("A1", item), Is.EqualTo(expected));
            Assert.That(() => bankVault.AddItem("A2", item2), Is.Not.EqualTo(expected2));
        }
        [Test]
        public void VaultRemoveThowsWhenCellNotExist()
        {
            bankVault.AddItem("A1", item);
            bankVault.AddItem("A2", item2);

            Assert.Throws<ArgumentException>(() =>
            {
                bankVault.RemoveItem("D1", item);
            });
        }
        [Test]
        public void VaultRemoveThowsWhenItemNotExist()
        {
            bankVault.AddItem("A1", item);

            Assert.Throws<ArgumentException>(() =>
            {
                bankVault.RemoveItem("A1", item2);
            });
        }
        [Test]
        public void VaultRemoveWorksFine()
        {
            bankVault.AddItem("A1", item);
            bankVault.AddItem("A2", item2);

            bankVault.RemoveItem("A1", item);

            Assert.True(bankVault.VaultCells["A1"] == null);
        }
        [Test]
        public void RemoveVaultReturnProperMessage()
        {
            string expected = "Remove item:10 successfully!";
            string expected2 = "Wrong Input";

            bankVault.AddItem("A1", item);
            bankVault.AddItem("A2", item2);

            Assert.That(() => bankVault.RemoveItem("A1", item), Is.EqualTo(expected));
            Assert.That(() => bankVault.RemoveItem("A2", item2), Is.Not.EqualTo(expected2));
        }
    }
}