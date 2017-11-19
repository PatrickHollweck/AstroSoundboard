// ****************************** Module Header ****************************** //
//
//
// Last Modified: 18:11:2017 / 16:40
// Creation: 18:11:2017
// Project: AstroSoundBoard.Tests
//
//
// <copyright file="MemoryStoreTests.cs" company="Patrick Hollweck" GitHub="https://github.com/FetzenRndy">//</copyright>
// *************************************************************************** //

namespace AstroSoundBoard.Tests
{
    using AstroSoundBoard.Services.Persistence;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class MemoryStoreTests
    {
        [TestMethod]
        public void InsertTest()
        {
            var store = new MemoryStore<string>();

            store.Insert("Item 1");
            store.Insert("Item 2");
            store.Insert("Item 3");

            Assert.AreEqual(store.GetAll().Count, 3);
        }
    }
}